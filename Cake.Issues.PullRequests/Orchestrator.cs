namespace Cake.Issues.PullRequests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Class for writing issues to pull requests.
    /// </summary>
    internal class Orchestrator
    {
        private readonly ICakeLog log;
        private readonly IPullRequestSystem pullRequestSystem;
        private readonly ReportIssuesToPullRequestSettings settings;
        private readonly bool pullRequestSystemInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="Orchestrator"/> class.
        /// </summary>
        /// <param name="log">Cake log instance.</param>
        /// <param name="pullRequestSystem">Object for accessing pull request system.
        /// <c>null</c> if only issues should be read.</param>
        /// <param name="settings">Settings.</param>
        public Orchestrator(
            ICakeLog log,
            IPullRequestSystem pullRequestSystem,
            ReportIssuesToPullRequestSettings settings)
        {
#pragma warning disable SA1123 // Do not place regions within elements
            #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

            log.NotNull(nameof(log));
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            settings.NotNull(nameof(settings));

            this.log = log;
            this.pullRequestSystem = pullRequestSystem;
            this.settings = settings;

            #endregion

            // Initialize pull request system.
            this.log.Verbose("Initialize pull request system...");
            this.pullRequestSystemInitialized = this.pullRequestSystem.Initialize(this.settings);
            if (!this.pullRequestSystemInitialized)
            {
                this.log.Warning("Error initializing the pull request system.");
            }
        }

        /// <summary>
        /// Runs the orchestrator.
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <param name="issueProviders">List of issue providers to use.</param>
        /// <returns>Information about the reported and written issues.</returns>
        public PullRequestIssueResult Run(IEnumerable<IIssueProvider> issueProviders)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            var format = IssueCommentFormat.Undefined;

            if (this.pullRequestSystemInitialized)
            {
                format = this.pullRequestSystem.GetPreferredCommentFormat();
                this.log.Verbose("Pull request system prefers comments in {0} format.", format);
            }

            // ReSharper disable once PossibleMultipleEnumeration
            var issuesReader =
                new IssuesReader(this.log, issueProviders, this.settings);

            return this.Run(issuesReader.ReadIssues(format));
        }

        /// <summary>
        /// Runs the orchestrator.
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <param name="issues">Issues which should be reported.</param>
        /// <returns>Information about the reported and written issues.</returns>
        public PullRequestIssueResult Run(IEnumerable<IIssue> issues)
        {
            issues.NotNullOrEmptyElement(nameof(issues));

            // Don't process issues if pull request system could not be initialized.
            if (!this.pullRequestSystemInitialized)
            {
                return new PullRequestIssueResult(issues, new List<IIssue>());
            }

            this.log.Information("Processing {0} new issues", issues.Count());
            var postedIssues = this.PostAndResolveComments(this.settings, issues.ToList());

            return new PullRequestIssueResult(issues, postedIssues);
        }

        /// <summary>
        /// Checks if file path from an <see cref="IIssue"/> and <see cref="IPullRequestDiscussionThread"/>
        /// are matching.
        /// </summary>
        /// <param name="issue">Issue to check.</param>
        /// <param name="thread">Comment thread to check.</param>
        /// <returns><c>true</c> if both paths are matching or if both paths are set to <c>null</c>.</returns>
        private static bool FilePathsAreMatching(IIssue issue, IPullRequestDiscussionThread thread)
        {
            return
                (issue.AffectedFileRelativePath == null && thread.AffectedFileRelativePath == null) ||
                (
                    issue.AffectedFileRelativePath != null &&
                    thread.AffectedFileRelativePath != null &&
                    thread.AffectedFileRelativePath.ToString() == issue.AffectedFileRelativePath.ToString());
        }

        /// <summary>
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <param name="reportIssuesToPullRequestSettings">Settings for posting the issues.</param>
        /// <param name="issues">Issues to post.</param>
        /// <returns>Issues reported to the pull request.</returns>
        private IEnumerable<IIssue> PostAndResolveComments(
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings,
            IList<IIssue> issues)
        {
            issues.NotNull(nameof(issues));

            IDictionary<IIssue, IssueCommentInfo> issueComments = null;
            var discussionThreadsCapability = this.pullRequestSystem.GetCapability<ISupportDiscussionThreads>();
            if (discussionThreadsCapability != null)
            {
                this.log.Information("Fetching existing threads and comments...");

                var existingThreads =
                    discussionThreadsCapability.FetchDiscussionThreads(
                        reportIssuesToPullRequestSettings.CommentSource).ToList();

                issueComments =
                    this.GetCommentsForIssue(
                        reportIssuesToPullRequestSettings,
                        issues,
                        existingThreads);

                // Comments that were created by this logic but do not have corresponding issues can be marked as 'Resolved'.
                this.ResolveExistingComments(
                    discussionThreadsCapability,
                    existingThreads,
                    issueComments,
                    reportIssuesToPullRequestSettings);

                // Comments that were created by this logic and are resolved, but still have a corresponding issue need to be reopened.
                this.ReopenExistingComments(
                    discussionThreadsCapability,
                    existingThreads,
                    issueComments,
                    reportIssuesToPullRequestSettings);
            }

            if (!issues.Any())
            {
                this.log.Information("No new issues were posted");
                return new List<IIssue>();
            }

            // Filter issues which should not be posted.
            var issueFilterer =
                new IssueFilterer(this.log, this.pullRequestSystem, reportIssuesToPullRequestSettings);
            var remainingIssues = issueFilterer.FilterIssues(issues, issueComments).ToList();

            if (remainingIssues.Any())
            {
                var formattedMessages =
                    from issue in remainingIssues
                    select
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "  Rule: {0} Line: {1} File: {2}",
                            issue.Rule,
                            issue.Line,
                            issue.AffectedFileRelativePath);

                this.log.Verbose(
                    "Posting {0} issue(s):\n{1}",
                    remainingIssues.Count,
                    string.Join(Environment.NewLine, formattedMessages));

                if (!string.IsNullOrWhiteSpace(reportIssuesToPullRequestSettings.CommitId))
                {
                    var checkCommitIdCapability = this.pullRequestSystem.GetCapability<ISupportCheckingCommitId>();

                    if (checkCommitIdCapability != null &&
                        !checkCommitIdCapability.IsCurrentCommitId(reportIssuesToPullRequestSettings.CommitId))
                    {
                        this.log.Information(
                            "Skipping posting of issues since commit {0} is outdated. Current commit is {1}",
                            reportIssuesToPullRequestSettings.CommitId,
                            checkCommitIdCapability.GetLastSourceCommitId());
                        return new List<IIssue>();
                    }
                }

                this.pullRequestSystem.PostDiscussionThreads(
                    remainingIssues,
                    reportIssuesToPullRequestSettings.CommentSource);
            }
            else
            {
                this.log.Information("All issues were filtered. Nothing new to post.");
            }

            return remainingIssues;
        }

        /// <summary>
        /// Returns existing matching comments from the pull request for a list of issues.
        /// </summary>
        /// <param name="reportIssuesToPullRequestSettings">Settings to use.</param>
        /// <param name="issues">Issues for which matching comments should be found.</param>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <returns>Dictionary containing issues and its associated matching comments on the pull request.</returns>
        private IDictionary<IIssue, IssueCommentInfo> GetCommentsForIssue(
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings,
            IList<IIssue> issues,
            IList<IPullRequestDiscussionThread> existingThreads)
        {
            issues.NotNull(nameof(issues));
            existingThreads.NotNull(nameof(existingThreads));

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = new Dictionary<IIssue, IssueCommentInfo>();
            foreach (var issue in issues)
            {
                var (activeComments, wontFixComments, resolvedComments) =
                    this.GetMatchingComments(
                        reportIssuesToPullRequestSettings,
                        issue,
                        existingThreads);

                if (activeComments.Any() ||
                    wontFixComments.Any() ||
                    resolvedComments.Any())
                {
                    var issueCommentInfo =
                        new IssueCommentInfo(
                            activeComments,
                            wontFixComments,
                            resolvedComments);
                    result.Add(issue, issueCommentInfo);
                }
            }

            this.log.Verbose("Built a issue to comment dictionary in {0} ms", stopwatch.ElapsedMilliseconds);

            return result;
        }

        /// <summary>
        /// Returns all matching comments from discussion threads for an issue.
        /// Comments are considered matching if they fulfill all of the following conditions:
        /// * The thread is active.
        /// * The thread is for the same file.
        /// * The thread was created by the same logic, i.e. the same <see cref="IPullRequestDiscussionThread.CommentSource"/>.
        /// * The comment contains the same content.
        /// </summary>
        /// <remarks>
        /// The line cannot be used since comments can move around.
        /// </remarks>
        /// <param name="reportIssuesToPullRequestSettings">Settings to use.</param>
        /// <param name="issue">Issue for which the comments should be returned.</param>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <returns>Comments for the issue.</returns>
        private (IEnumerable<IPullRequestDiscussionComment> activeComments,
                IEnumerable<IPullRequestDiscussionComment> wontFixComments,
                IEnumerable<IPullRequestDiscussionComment> resolvedComments) GetMatchingComments(
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings,
            IIssue issue,
            IList<IPullRequestDiscussionThread> existingThreads)
        {
            issue.NotNull(nameof(issue));
            existingThreads.NotNull(nameof(existingThreads));

            // Select threads that point to the same file and have been marked with the given comment source.
            var matchingThreads =
                (from thread in existingThreads
                where
                    thread != null &&
                    FilePathsAreMatching(issue, thread) &&
                    thread.CommentSource == reportIssuesToPullRequestSettings.CommentSource
                select thread).ToList();

            if (matchingThreads.Any())
            {
                this.log.Verbose(
                    "Found {0} matching thread(s) for the issue at {1} line {2}",
                    matchingThreads.Count,
                    issue.AffectedFileRelativePath,
                    issue.Line);
            }

            var activeComments = new List<IPullRequestDiscussionComment>();
            var wontFixComments = new List<IPullRequestDiscussionComment>();
            var resolvedComments = new List<IPullRequestDiscussionComment>();
            foreach (var thread in matchingThreads)
            {
                // Select comments from this thread that are not deleted and that match the given message.
                var matchingComments =
                    (from comment in thread.Comments
                    where
                        comment != null &&
                        !comment.IsDeleted &&
                        comment.Content == issue.Message
                    select
                        comment).ToList();

                if (matchingComments.Any())
                {
                    this.log.Verbose(
                        "Found {0} matching comment(s) for the issue at {1} line {2}",
                        matchingComments.Count,
                        issue.AffectedFileRelativePath,
                        issue.Line);
                }

                if (thread.Status == PullRequestDiscussionStatus.Active)
                {
                    activeComments.AddRange(matchingComments);
                }
                else if (thread.Status == PullRequestDiscussionStatus.Resolved)
                {
                    if (thread.Resolution == PullRequestDiscussionResolution.WontFix)
                    {
                        wontFixComments.AddRange(matchingComments);
                    }
                    else if (thread.Resolution == PullRequestDiscussionResolution.Resolved)
                    {
                        resolvedComments.AddRange(matchingComments);
                    }
                }
            }

            return (activeComments, wontFixComments, resolvedComments);
        }

        /// <summary>
        /// Marks comment threads created by this logic but without active issues as resolved.
        /// </summary>
        /// <param name="discussionThreadsCapability">Pull request system capability for working with discussion threads.</param>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <param name="issueComments">Issues and their related existing comments on the pull request.</param>
        /// <param name="reportIssuesToPullRequestSettings">Settings for posting the issues.</param>
        private void ResolveExistingComments(
            ISupportDiscussionThreads discussionThreadsCapability,
            IList<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull(nameof(existingThreads));
            issueComments.NotNull(nameof(issueComments));
            reportIssuesToPullRequestSettings.NotNull(nameof(reportIssuesToPullRequestSettings));

            if (!existingThreads.Any())
            {
                this.log.Verbose("No existings threads to resolve.");
                return;
            }

            var threadsToResolve =
                this.GetThreadsToResolve(existingThreads, issueComments, reportIssuesToPullRequestSettings).ToList();

            this.log.Verbose("Mark {0} threads as fixed...", threadsToResolve.Count);
            discussionThreadsCapability.ResolveDiscussionThreads(threadsToResolve);
        }

        /// <summary>
        /// Returns threads that can be resolved.
        /// </summary>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <param name="issueComments">Issues and their related existing comments on the pull request.</param>
        /// <param name="reportIssuesToPullRequestSettings">Settings for posting the issues.</param>
        /// <returns>List of threads which can be resolved.</returns>
        private IEnumerable<IPullRequestDiscussionThread> GetThreadsToResolve(
            IList<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull(nameof(existingThreads));
            issueComments.NotNull(nameof(issueComments));
            reportIssuesToPullRequestSettings.NotNull(nameof(reportIssuesToPullRequestSettings));

            var activeComments =
                new HashSet<IPullRequestDiscussionComment>(
                    issueComments.Values.SelectMany(x => x.ActiveComments));

            var result =
                existingThreads.Where(
                    thread =>
                        thread.Status == PullRequestDiscussionStatus.Active &&
                        thread.CommentSource == reportIssuesToPullRequestSettings.CommentSource &&
                        !thread.Comments.Any(x => activeComments.Contains(x))).ToList();

            this.log.Verbose(
                "Found {0} existing thread(s) that do not match any new issue and can be resolved.",
                result.Count);

            return result;
        }

        /// <summary>
        /// Marks resolved comment threads created by this logic with active issues as active.
        /// </summary>
        /// <param name="discussionThreadsCapability">Pull request system capability for working with discussion threads.</param>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <param name="issueComments">Issues and their related existing comments on the pull request.</param>
        /// <param name="reportIssuesToPullRequestSettings">Settings for posting the issues.</param>
        private void ReopenExistingComments(
            ISupportDiscussionThreads discussionThreadsCapability,
            IList<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull(nameof(existingThreads));
            issueComments.NotNull(nameof(issueComments));
            reportIssuesToPullRequestSettings.NotNull(nameof(reportIssuesToPullRequestSettings));

            if (!existingThreads.Any())
            {
                this.log.Verbose("No existings threads to reopen.");
                return;
            }

            var threadsToReopen =
                this.GetThreadsToReopen(existingThreads, issueComments, reportIssuesToPullRequestSettings).ToList();

            this.log.Verbose("Reopen {0} threads...", threadsToReopen.Count);
            discussionThreadsCapability.ReopenDiscussionThreads(threadsToReopen);
        }

        /// <summary>
        /// Returns threads that should be reopened.
        /// </summary>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <param name="issueComments">Issues and their related existing comments on the pull request.</param>
        /// <param name="reportIssuesToPullRequestSettings">Settings for posting the issues.</param>
        /// <returns>List of threads which should be reopened.</returns>
        private IEnumerable<IPullRequestDiscussionThread> GetThreadsToReopen(
            IList<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            ReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull(nameof(existingThreads));
            issueComments.NotNull(nameof(issueComments));
            reportIssuesToPullRequestSettings.NotNull(nameof(reportIssuesToPullRequestSettings));

            var resolvedComments =
                new HashSet<IPullRequestDiscussionComment>(
                    issueComments.Values.SelectMany(x => x.ResolvedComments));

            var result =
                existingThreads.Where(
                    thread =>
                        thread.Status == PullRequestDiscussionStatus.Resolved &&
                        thread.CommentSource == reportIssuesToPullRequestSettings.CommentSource &&
                        thread.Comments.Any(x => resolvedComments.Contains(x))).ToList();

            this.log.Verbose(
                "Found {0} existing thread(s) that are resolved but still have an open issue.",
                result.Count);

            return result;
        }
    }
}
