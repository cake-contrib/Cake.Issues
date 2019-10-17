namespace Cake.Issues.PullRequests.AzureDevOps
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Cake.AzureDevOps.PullRequest;
    using Cake.AzureDevOps.PullRequest.CommentThread;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Cake.Issues.PullRequests.AzureDevOps.Capabilities;

    /// <summary>
    /// Class for writing issues to Azure DevOps pull requests.
    /// </summary>
    internal sealed class AzureDevOpsPullRequestSystem : BasePullRequestSystem, IAzureDevOpsPullRequestSystem
    {
        private readonly AzureDevOpsPullRequestSystemSettings settings;
        private readonly AzureDevOpsPullRequest azureDevOpsPullRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestSystem"/> class.
        /// Connects to the Azure DevOps server using NTLM authentication.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing Azure DevOps Server.</param>
        public AzureDevOpsPullRequestSystem(ICakeLog log, AzureDevOpsPullRequestSystemSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;

            if (settings.CheckCommitId)
            {
                this.AddCapability(new AzureDevOpsCheckingCommitIdCapability(log, this));
                this.Log.Information("Commit ID check capability is enabled.");
            }
            else
            {
                this.Log.Information("Commit ID check capability is disabled.");
            }

            if (settings.ManageDiscussionThreadStatus)
            {
                this.AddCapability(new AzureDevOpsDiscussionThreadsCapability(log, this));
                this.Log.Information("Discussion thread status management capability is enabled.");
            }
            else
            {
                this.Log.Information("Discussion thread status management capability is disabled.");
            }

            // Filtering by modified files is always required as we otherwise no longer can compare issues
            // in a subsequent run as we lose information about file and line.
            // See https://github.com/cake-contrib/Cake.Issues.PullRequests.AzureDevOps/issues/46#issuecomment-419149355
            this.AddCapability(new AzureDevOpsFilteringByModifiedFilesCapability(log, this));

            this.azureDevOpsPullRequest = new AzureDevOpsPullRequest(log, settings);
        }

        /// <inheritdoc/>
        AzureDevOpsPullRequest IAzureDevOpsPullRequestSystem.AzureDevOpsPullRequest => this.azureDevOpsPullRequest;

        /// <inheritdoc/>
        public override bool Initialize(ReportIssuesToPullRequestSettings settings)
        {
            // Fail initialization if no pull request could be found.
            return base.Initialize(settings) && this.azureDevOpsPullRequest.HasPullRequestLoaded;
        }

        /// <inheritdoc/>
        bool IAzureDevOpsPullRequestSystem.ValidatePullRequest()
        {
            return this.ValidatePullRequest();
        }

        /// <inheritdoc/>
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            if (!this.ValidatePullRequest())
            {
                return;
            }

            // ReSharper disable once PossibleMultipleEnumeration
            var threads = this.CreateDiscussionThreads(issues, commentSource).ToList();

            if (!threads.Any())
            {
                this.Log.Verbose("No threads to post");
                return;
            }

            foreach (var thread in threads)
            {
                this.azureDevOpsPullRequest.CreateCommentThread(thread);
            }

            this.Log.Information("Posted {0} discussion threads", threads.Count);
        }

        private static void AddCodeFlowProperties(
           IIssue issue,
           int iterationId,
           int changeTrackingId,
           IDictionary<string, object> properties)
        {
            issue.NotNull(nameof(issue));
            properties.NotNull(nameof(properties));

            properties.Add("Microsoft.VisualStudio.Services.CodeReview.ItemPath", "/" + issue.AffectedFileRelativePath);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.Right.StartLine", issue.Line);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.Right.EndLine", issue.Line);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.Right.StartOffset", 0);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.Right.EndOffset", 1);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.FirstComparingIteration", iterationId);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.SecondComparingIteration", iterationId);
            properties.Add("Microsoft.VisualStudio.Services.CodeReview.ChangeTrackingId", changeTrackingId);
        }

        /// <summary>
        /// Validates if a pull request could be found.
        /// Depending on <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// the pull request instance can not be successfully loaded.
        /// </summary>
        /// <returns>True if a valid pull request instance exists.</returns>
        private bool ValidatePullRequest()
        {
            if (this.azureDevOpsPullRequest.HasPullRequestLoaded)
            {
                return true;
            }

            this.Log.Verbose("Skipping, since no pull request instance could be found.");
            return false;
        }

        private IEnumerable<AzureDevOpsPullRequestCommentThread> CreateDiscussionThreads(
            IEnumerable<IIssue> issues,
            string commentSource)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            this.Log.Verbose("Creating new discussion threads");
            var result = new List<AzureDevOpsPullRequestCommentThread>();

            // Code flow properties
            var iterationId = 0;
            IEnumerable<AzureDevOpsPullRequestIterationChange> changes = null;

            if (this.azureDevOpsPullRequest.CodeReviewId > 0)
            {
                iterationId = this.GetCodeFlowLatestIterationId();
                changes = this.GetCodeFlowChanges(iterationId);
            }

            // Filter issues not related to a file.
            if (!this.settings.ReportIssuesNotRelatedToAFile)
            {
                issues = issues.Where(x => x.AffectedFileRelativePath != null);
            }

            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var issue in issues)
            {
                this.Log.Information(
                    "Creating a discussion comment for the issue at line {0} from {1}",
                    issue.Line,
                    issue.AffectedFileRelativePath);

                var newThread = new AzureDevOpsPullRequestCommentThread()
                {
                    Status = AzureDevOpsCommentThreadStatus.Active,
                };

                var discussionComment = new AzureDevOpsComment
                {
                    CommentType = AzureDevOpsCommentType.System,
                    IsDeleted = false,
                    Content = ContentProvider.GetContent(issue),
                };

                if (!this.AddThreadProperties(newThread, changes, issue, iterationId, commentSource))
                {
                    continue;
                }

                newThread.Comments = new List<AzureDevOpsComment> { discussionComment };
                result.Add(newThread);
            }

            return result;
        }

        private bool AddThreadProperties(
            AzureDevOpsPullRequestCommentThread thread,
            IEnumerable<AzureDevOpsPullRequestIterationChange> changes,
            IIssue issue,
            int iterationId,
            string commentSource)
        {
            thread.NotNull(nameof(thread));
            changes.NotNull(nameof(changes));
            issue.NotNull(nameof(issue));

            var properties = new Dictionary<string, object>();

            if (issue.AffectedFileRelativePath != null)
            {
                if (this.azureDevOpsPullRequest.CodeReviewId > 0)
                {
                    var changeTrackingId =
                        this.TryGetCodeFlowChangeTrackingId(changes, issue.AffectedFileRelativePath);
                    if (changeTrackingId < 0)
                    {
                        // Don't post comment if we couldn't determine the change.
                        return false;
                    }

                    AddCodeFlowProperties(issue, iterationId, changeTrackingId, properties);
                }
                else
                {
                    throw new NotSupportedException("Legacy code reviews are not supported.");
                }
            }

            // An Azure DevOps UI extension will recognize this and format the comments differently.
            properties.Add("CodeAnalysisThreadType", "CodeAnalysisIssue");

            thread.Properties = properties;

            // Add a custom property to be able to distinguish all comments created this way.
            thread.SetCommentSource(commentSource);

            // Add a custom property to be able to return issue message from existing threads,
            // without any formatting done by this addin, back to Cake.Issues.PullRequests.
            thread.SetIssueMessage(issue.MessageText);

            return true;
        }

        private int GetCodeFlowLatestIterationId()
        {
            var iterationId = this.azureDevOpsPullRequest.GetLatestIterationId();
            this.Log.Verbose("Determined iteration ID: {0}", iterationId);
            return iterationId;
        }

        private IEnumerable<AzureDevOpsPullRequestIterationChange> GetCodeFlowChanges(int iterationId)
        {
            var changes = this.azureDevOpsPullRequest.GetIterationChanges(iterationId);

            if (changes != null)
            {
                this.Log.Verbose("Change count: {0}", changes.Count());
            }

            return changes;
        }

        private int TryGetCodeFlowChangeTrackingId(IEnumerable<AzureDevOpsPullRequestIterationChange> changes, FilePath path)
        {
            changes.NotNull(nameof(changes));
            path.NotNull(nameof(path));

            var change = changes.Where(x => x.ItemPath != null && x.ItemPath.FullPath == "/" + path.ToString()).ToList();

            if (change.Count == 0)
            {
                this.Log.Error(
                    "Cannot post a comment for the file {0} because no changes on the pull request server could be found.",
                    path);
                return -1;
            }

            if (change.Count > 1)
            {
                this.Log.Error(
                    "Cannot post a comment for the file {0} because more than one change has been found on the pull request server:" + Environment.NewLine + "{1}",
                    path,
                    string.Join(
                        Environment.NewLine,
                        change.Select(
                            x => string.Format(
                                CultureInfo.InvariantCulture,
                                "  ID: {0}, Path: {1}",
                                x.ChangeId,
                                x.ItemPath))));
                return -1;
            }

            var changeTrackingId = change.Single().ChangeTrackingId;
            this.Log.Verbose(
                "Determined ChangeTrackingId of {0} for {1}",
                changeTrackingId,
                path);
            return changeTrackingId;
        }
    }
}
