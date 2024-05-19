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

        /// <summary>
        /// Initializes a new instance of the <see cref="Orchestrator"/> class.
        /// </summary>
        /// <param name="log">Cake log instance.</param>
        /// <param name="pullRequestSystem">Object for accessing pull request system.
        /// <c>null</c> if only issues should be read.</param>
        public Orchestrator(
            ICakeLog log,
            IPullRequestSystem pullRequestSystem)
        {
            log.NotNull();
            pullRequestSystem.NotNull();

            this.log = log;
            this.pullRequestSystem = pullRequestSystem;
        }

        /// <summary>
        /// Runs the orchestrator.
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <param name="issueProviders">List of issue providers to use.</param>
        /// <param name="settings">Settings.</param>
        /// <returns>Information about the reported and written issues.</returns>
        public PullRequestIssueResult Run(
            IEnumerable<IIssueProvider> issueProviders,
            IReportIssuesToPullRequestFromIssueProviderSettings settings)
        {
            issueProviders.NotNullOrEmptyOrEmptyElement();
            settings.NotNull();

            var issuesReader =
                new IssuesReader(this.log, issueProviders, settings);

            return this.Run(issuesReader.ReadIssues(), settings);
        }

        /// <summary>
        /// Runs the orchestrator.
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <param name="issues">Issues which should be reported.</param>
        /// <param name="settings">Settings.</param>
        /// <returns>Information about the reported and written issues.</returns>
        public PullRequestIssueResult Run(
            IEnumerable<IIssue> issues,
            IReportIssuesToPullRequestSettings settings)
        {
            issues.NotNullOrEmptyElement();
            settings.NotNull();

            // Don't process issues if pull request system could not be initialized.
            if (!this.InitializePullRequestSystem(settings))
            {
                return new PullRequestIssueResult(issues, new List<IIssue>());
            }

            var issuesList = issues.ToList();
            this.log.Information("Processing {0} new issues", issuesList.Count);
            var postedIssues = this.PostAndResolveComments(settings, issuesList);

            return new PullRequestIssueResult(issuesList, postedIssues);
        }

        /// <summary>
        /// Checks if file path from an <see cref="IIssue"/> and <see cref="IPullRequestDiscussionThread"/>
        /// are matching.
        /// </summary>
        /// <param name="issue">Issue to check.</param>
        /// <param name="thread">Comment thread to check.</param>
        /// <returns><c>true</c> if both paths are matching or if both paths are set to <c>null</c>.</returns>
        private static bool FilePathsAreMatching(IIssue issue, IPullRequestDiscussionThread thread) =>
            (issue.AffectedFileRelativePath == null && thread.AffectedFileRelativePath == null) ||
            (
                issue.AffectedFileRelativePath != null &&
                thread.AffectedFileRelativePath != null &&
                thread.AffectedFileRelativePath.ToString() == issue.AffectedFileRelativePath.ToString());

        /// <summary>
        /// Initializes the pull request system.
        /// </summary>
        /// <param name="settings">Settings for posting issues.</param>
        /// <returns><c>True</c> if pull request system could be initialized.</returns>
        private bool InitializePullRequestSystem(IReportIssuesToPullRequestSettings settings)
        {
            // Initialize pull request system.
            this.log.Verbose("Initialize pull request system...");
            var result = this.pullRequestSystem.Initialize(settings);
            if (!result)
            {
                this.log.Warning("Error initializing the pull request system.");
            }

            return result;
        }

        /// <summary>
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <param name="reportIssuesToPullRequestSettings">Settings for posting the issues.</param>
        /// <param name="issues">Issues to post.</param>
        /// <returns>Issues reported to the pull request.</returns>
        private List<IIssue> PostAndResolveComments(
            IReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings,
            IList<IIssue> issues)
        {
            reportIssuesToPullRequestSettings.NotNull();
            issues.NotNull();

            IDictionary<IIssue, IssueCommentInfo> issueComments = null;
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads = null;
            var discussionThreadsCapability = this.pullRequestSystem.GetCapability<ISupportDiscussionThreads>();
            if (discussionThreadsCapability != null)
            {
                this.log.Information("Fetching existing threads and comments...");

                existingThreads =
                    discussionThreadsCapability
                        .FetchDiscussionThreads(reportIssuesToPullRequestSettings.CommentSource)
                        .Where(x => x != null)
                        .ToList();

                issueComments =
                    this.GetCommentsForIssue(
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
                return [];
            }

            // Filter issues which should not be posted.
            var issueFilterer =
                new IssueFilterer(this.log, this.pullRequestSystem, reportIssuesToPullRequestSettings);
            var remainingIssues =
                issueFilterer
                    .FilterIssues(
                        issues,
                        issueComments,
                        existingThreads)
                    .ToList();

            if (remainingIssues.Count > 0)
            {
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
                        return [];
                    }
                }

                var formattedMessages =
                    from issue in remainingIssues
                    select
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "  Rule: {0} Line: {1} File: {2}",
                            issue.RuleId,
                            issue.Line,
                            issue.AffectedFileRelativePath);

                this.log.Verbose(
                    "Posting {0} issue(s):\n{1}",
                    remainingIssues.Count,
                    string.Join(Environment.NewLine, formattedMessages));

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
        /// <param name="issues">Issues for which matching comments should be found.</param>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <returns>Dictionary with issues  associated matching comments on the pull request.</returns>
        private Dictionary<IIssue, IssueCommentInfo> GetCommentsForIssue(
            IList<IIssue> issues,
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads)
        {
            issues.NotNull();
            existingThreads.NotNull();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var threadsWithIssues = new Dictionary<IIssue, IssueCommentInfo>();
            foreach (var issue in issues)
            {
                var (activeComments, wontFixComments, resolvedComments) =
                    this.GetMatchingComments(
                        issue,
                        existingThreads);

                var activeCommentsList = activeComments.ToList();
                var wontFixCommentsList = wontFixComments.ToList();
                var resolvedCommentsList = resolvedComments.ToList();

                if (activeCommentsList.Count == 0 &&
                    wontFixCommentsList.Count == 0 &&
                    resolvedCommentsList.Count == 0)
                {
                    continue;
                }

                var issueCommentInfo =
                    new IssueCommentInfo(
                        activeCommentsList,
                        wontFixCommentsList,
                        resolvedCommentsList);
                threadsWithIssues.Add(issue, issueCommentInfo);
            }

            this.log.Verbose("Built a issue to comment dictionary in {0} ms", stopwatch.ElapsedMilliseconds);

            return threadsWithIssues;
        }

        /// <summary>
        /// Returns all matching comments from discussion threads for an issue.
        /// Comments are considered matching if they fulfill the following conditions:
        /// * The thread is active.
        /// * The thread is for the same file.
        /// * The thread was created by the same logic, i.e. the same <see cref="IPullRequestDiscussionThread.CommentSource"/>.
        /// * The thread was created for the same issue, i.e. the same <see cref="IPullRequestDiscussionThread.CommentIdentifier"/>.
        /// </summary>
        /// <remarks>
        /// The line cannot be used since comments can move around.
        /// </remarks>
        /// <param name="issue">Issue for which the comments should be returned.</param>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <returns>Comments for the issue.</returns>
        private (IEnumerable<IPullRequestDiscussionComment> ActiveComments,
                IEnumerable<IPullRequestDiscussionComment> WontFixComments,
                IEnumerable<IPullRequestDiscussionComment> ResolvedComments) GetMatchingComments(
            IIssue issue,
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads)
        {
            issue.NotNull();
            existingThreads.NotNull();

            // Select threads that point to the same file and issue identifier.
            var matchingThreads =
                existingThreads
                    .Where(x => FilePathsAreMatching(issue, x) && x.CommentIdentifier == issue.Identifier)
                    .ToList();

            if (matchingThreads.Count > 0)
            {
                this.log.Verbose(
                    "Found {0} matching thread(s) for the issue in file {1} on line {2}",
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
                         comment is { IsDeleted: false }
                     select
                         comment).ToList();

                if (matchingComments.Count == 0)
                {
                    continue;
                }

                this.log.Verbose(
                    "Found {0} matching comment(s) for the issue in file {1} on line {2}",
                    matchingComments.Count,
                    issue.AffectedFileRelativePath,
                    issue.Line);

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
                else
                {
                    this.log.Warning(
                        "Thread has unknown status und matching comment(s) are ignored.");
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
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull();
            issueComments.NotNull();
            reportIssuesToPullRequestSettings.NotNull();

            if (existingThreads.Count == 0)
            {
                this.log.Verbose("No existing threads to resolve.");
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
        private List<IPullRequestDiscussionThread> GetThreadsToResolve(
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull();
            issueComments.NotNull();
            reportIssuesToPullRequestSettings.NotNull();

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
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull();
            issueComments.NotNull();
            reportIssuesToPullRequestSettings.NotNull();

            if (existingThreads.Count == 0)
            {
                this.log.Verbose("No existing threads to reopen.");
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
        private List<IPullRequestDiscussionThread> GetThreadsToReopen(
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReportIssuesToPullRequestSettings reportIssuesToPullRequestSettings)
        {
            existingThreads.NotNull();
            issueComments.NotNull();
            reportIssuesToPullRequestSettings.NotNull();

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
