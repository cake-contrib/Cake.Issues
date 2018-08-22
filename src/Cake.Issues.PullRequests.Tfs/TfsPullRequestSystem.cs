namespace Cake.Issues.PullRequests.Tfs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Core.Diagnostics;
    using Core.IO;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;
    using TfsUrlParser;

    /// <summary>
    /// Class for writing issues to Team Foundation Server or Visual Studio Team Services pull requests.
    /// </summary>
    internal sealed class TfsPullRequestSystem : BasePullRequestSystem, ITfsPullRequestSystem
    {
        private readonly TfsPullRequestSettings settings;
        private readonly RepositoryDescription repositoryDescription;
        private readonly GitPullRequest pullRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSystem"/> class.
        /// Connects to the TFS server using NTLM authentication.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing TFS.</param>
        public TfsPullRequestSystem(ICakeLog log, TfsPullRequestSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;

            this.AddCapability(new TfsCheckingCommitIdCapability(log, this));
            this.AddCapability(new TfsDiscussionThreadsCapability(log, this));
            this.AddCapability(new TfsFilteringByModifiedFilesCapability(log, this));

            this.repositoryDescription = new RepositoryDescription(settings.RepositoryUrl);

            this.Log.Verbose(
                "Repository information:\n  CollectionName: {0}\n  CollectionUrl: {1}\n  ProjectName: {2}\n  RepositoryName: {3}",
                this.repositoryDescription.CollectionName,
                this.repositoryDescription.CollectionUrl,
                this.repositoryDescription.ProjectName,
                this.repositoryDescription.RepositoryName);

            using (var gitClient = this.CreateGitClient(out var authorizedIdenity))
            {
                this.Log.Verbose(
                     "Authorized Identity:\n  Id: {0}\n  DisplayName: {1}",
                     authorizedIdenity.Id,
                     authorizedIdenity.DisplayName);

                if (settings.PullRequestId.HasValue)
                {
                    this.Log.Verbose("Read pull request with ID {0}", settings.PullRequestId.Value);
                    this.pullRequest =
                        gitClient.GetPullRequestAsync(
                            this.repositoryDescription.ProjectName,
                            this.repositoryDescription.RepositoryName,
                            settings.PullRequestId.Value).Result;
                }
                else if (!string.IsNullOrWhiteSpace(settings.SourceBranch))
                {
                    this.Log.Verbose("Read pull request for branch {0}", settings.SourceBranch);

                    var pullRequestSearchCriteria =
                        new GitPullRequestSearchCriteria()
                        {
                            Status = PullRequestStatus.Active,
                            SourceRefName = settings.SourceBranch
                        };

                    this.pullRequest =
                        gitClient.GetPullRequestsAsync(
                            this.repositoryDescription.ProjectName,
                            this.repositoryDescription.RepositoryName,
                            pullRequestSearchCriteria,
                            top: 1).Result.SingleOrDefault();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(settings),
                        "Either PullRequestId or SourceBranch needs to be set");
                }
            }

            if (this.pullRequest == null)
            {
                if (this.settings.ThrowExceptionIfPullRequestDoesNotExist)
                {
                    throw new PullRequestIssuesException("Could not find pull request");
                }

                this.Log.Warning("Could not find pull request");
                return;
            }

            this.Log.Verbose(
                "Pull request information:\n  PullRequestId: {0}\n  RepositoryId: {1}\n  RepositoryName: {2}\n  SourceRefName: {3}",
                this.pullRequest.PullRequestId,
                this.pullRequest.Repository.Id,
                this.pullRequest.Repository.Name,
                this.pullRequest.SourceRefName);
        }

        /// <inheritdoc/>
        RepositoryDescription ITfsPullRequestSystem.RepositoryDescription => this.repositoryDescription;

        /// <inheritdoc/>
        GitPullRequest ITfsPullRequestSystem.PullRequest => this.pullRequest;

        /// <inheritdoc/>
        public override bool Initialize(ReportIssuesToPullRequestSettings settings)
        {
            // Fail initialization if no pull request could be found.
            return base.Initialize(settings) && this.pullRequest != null;
        }

        /// <inheritdoc/>
        public override IssueCommentFormat GetPreferredCommentFormat()
        {
            return IssueCommentFormat.Markdown;
        }

        /// <inheritdoc/>
        bool ITfsPullRequestSystem.ValidatePullRequest()
        {
            return this.ValidatePullRequest();
        }

        /// <inheritdoc/>
        GitHttpClient ITfsPullRequestSystem.CreateGitClient()
        {
            return this.CreateGitClient();
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

            using (var gitClient = this.CreateGitClient())
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var threads = this.CreateDiscussionThreads(gitClient, issues, commentSource).ToList();

                if (!threads.Any())
                {
                    this.Log.Verbose("No threads to post");
                    return;
                }

                foreach (var thread in threads)
                {
                    gitClient.CreateThreadAsync(
                        thread,
                        this.pullRequest.Repository.Id,
                        this.pullRequest.PullRequestId,
                        null,
                        CancellationToken.None).Wait();
                }

                this.Log.Information("Posted {0} discussion threads", threads.Count);
            }
        }

        private static void AddCodeFlowProperties(
           IIssue issue,
           int iterationId,
           int changeTrackingId,
           PropertiesCollection properties)
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
        /// Depending on <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestDoesNotExist"/>
        /// the pull request instance can be null for subsequent calls.
        /// </summary>
        /// <returns>True if a valid pull request instance exists.</returns>
        private bool ValidatePullRequest()
        {
            if (this.pullRequest != null)
            {
                return true;
            }

            this.Log.Verbose("Skipping, since no pull request instance could be found.");
            return false;
        }

        private GitHttpClient CreateGitClient(out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(
                    this.repositoryDescription.CollectionUrl,
                    this.settings.Credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var gitClient = connection.GetClient<GitHttpClient>();
            if (gitClient == null)
            {
                throw new PullRequestIssuesException("Could not retrieve the GitHttpClient object");
            }

            return gitClient;
        }

        private GitHttpClient CreateGitClient()
        {
            return this.CreateGitClient(out var identity);
        }

        private IEnumerable<GitPullRequestCommentThread> CreateDiscussionThreads(
            GitHttpClient gitClient,
            IEnumerable<IIssue> issues,
            string commentSource)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            this.Log.Verbose("Creating new discussion threads");
            var result = new List<GitPullRequestCommentThread>();

            // Code flow properties
            var iterationId = 0;
            GitPullRequestIterationChanges changes = null;

            if (this.pullRequest.CodeReviewId > 0)
            {
                iterationId = this.GetCodeFlowLatestIterationId(gitClient);
                changes = this.GetCodeFlowChanges(gitClient, iterationId);
            }

            // ReSharper disable once PossibleMultipleEnumeration
            // We currenty don't support issues not related to a file.
            // See https://github.com/cake-contrib/Cake.Issues.PullRequests.Tfs/issues/2
            foreach (var issue in issues.Where(x => x.AffectedFileRelativePath != null))
            {
                this.Log.Information(
                    "Creating a discussion comment for the issue at line {0} from {1}",
                    issue.Line,
                    issue.AffectedFileRelativePath);

                var newThread = new GitPullRequestCommentThread()
                {
                    Status = CommentThreadStatus.Active
                };

                var discussionComment = new Comment
                {
                    CommentType = CommentType.System,
                    IsDeleted = false,
                    Content = ContentProvider.GetContent(issue)
                };

                if (!this.AddThreadProperties(newThread, changes, issue, iterationId, commentSource))
                {
                    continue;
                }

                newThread.Comments = new List<Comment> { discussionComment };
                result.Add(newThread);
            }

            return result;
        }

        private bool AddThreadProperties(
            GitPullRequestCommentThread thread,
            GitPullRequestIterationChanges changes,
            IIssue issue,
            int iterationId,
            string commentSource)
        {
            thread.NotNull(nameof(thread));
            changes.NotNull(nameof(changes));
            issue.NotNull(nameof(issue));

            var properties = new PropertiesCollection();

            if (this.pullRequest.CodeReviewId > 0)
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

            // A VSTS UI extension will recognize this and format the comments differently.
            properties.Add("CodeAnalysisThreadType", "CodeAnalysisIssue");

            thread.Properties = properties;

            // Add a custom property to be able to distinguish all comments created this way.
            thread.SetCommentSource(commentSource);

            // Add a custom property to be able to return issue message from existing threads,
            // without any formatting done by this addin, back to Cake.Issues.PullRequests.
            thread.SetIssueMessage(issue.Message);

            return true;
        }

        private int GetCodeFlowLatestIterationId(GitHttpClient gitClient)
        {
            var request =
                gitClient.GetPullRequestIterationsAsync(
                    this.pullRequest.Repository.Id,
                    this.pullRequest.PullRequestId,
                    null,
                    null,
                    CancellationToken.None);

            var iterations = request.Result;

            if (iterations == null)
            {
                throw new PullRequestIssuesException("Could not retrieve the iterations");
            }

            var iterationId = iterations.Max(x => x.Id ?? -1);
            this.Log.Verbose("Determined iteration ID: {0}", iterationId);
            return iterationId;
        }

        private GitPullRequestIterationChanges GetCodeFlowChanges(GitHttpClient gitClient, int iterationId)
        {
            var request =
                gitClient.GetPullRequestIterationChangesAsync(
                    this.pullRequest.Repository.Id,
                    this.pullRequest.PullRequestId,
                    iterationId,
                    null,
                    null,
                    null,
                    null,
                    CancellationToken.None);

            var changes = request.Result;

            if (changes != null)
            {
                this.Log.Verbose("Change count: {0}", changes.ChangeEntries.Count());
            }

            return changes;
        }

        private int TryGetCodeFlowChangeTrackingId(GitPullRequestIterationChanges changes, FilePath path)
        {
            changes.NotNull(nameof(changes));
            path.NotNull(nameof(path));

            var change = changes.ChangeEntries.Where(x => x.Item.Path == "/" + path.ToString()).ToList();

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
                                x.Item.Path))));
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
