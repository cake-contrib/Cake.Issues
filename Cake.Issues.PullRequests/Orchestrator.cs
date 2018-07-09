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
        private readonly List<IIssueProvider> issueProviders = new List<IIssueProvider>();
        private readonly IPullRequestSystem pullRequestSystem;
        private readonly ReportIssuesToPullRequestSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Orchestrator"/> class.
        /// </summary>
        /// <param name="log">Cake log instance.</param>
        /// <param name="issueProviders">List of issue providers to use.</param>
        /// <param name="pullRequestSystem">Object for accessing pull request system.
        /// <c>null</c> if only issues should be read.</param>
        /// <param name="settings">Settings.</param>
        public Orchestrator(
            ICakeLog log,
            IEnumerable<IIssueProvider> issueProviders,
            IPullRequestSystem pullRequestSystem,
            ReportIssuesToPullRequestSettings settings)
        {
            log.NotNull(nameof(log));
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            settings.NotNull(nameof(settings));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            this.log = log;
            this.pullRequestSystem = pullRequestSystem;
            this.settings = settings;

            // ReSharper disable once PossibleMultipleEnumeration
            this.issueProviders.AddRange(issueProviders);
        }

        /// <summary>
        /// Runs the orchestrator.
        /// Posts new issues, ignoring duplicate comments and resolves comments that were open in an old iteration
        /// of the pull request.
        /// </summary>
        /// <returns>Information about the reported and written issues.</returns>
        public PullRequestIssueResult Run()
        {
            var format = IssueCommentFormat.Undefined;

            // Initialize pull request system.
            this.log.Verbose("Initialize pull request system...");
            var pullRequestSystemInitialized = this.pullRequestSystem.Initialize(this.settings);
            if (pullRequestSystemInitialized)
            {
                format = this.pullRequestSystem.GetPreferredCommentFormat();
                this.log.Verbose("Pull request system prefers comments in {0} format.", format);
            }
            else
            {
                this.log.Warning("Error initializing the pull request system.");
            }

            var issuesReader =
                new IssuesReader(this.log, this.issueProviders, this.settings);
            var issues = issuesReader.ReadIssues(format).ToList();

            // Don't process issues if pull request system could not be initialized.
            if (!pullRequestSystemInitialized)
            {
                return new PullRequestIssueResult(issues, new List<IIssue>());
            }

            this.log.Information("Processing {0} new issues", issues.Count);
            var postedIssues = this.PostAndResolveComments(this.settings, issues);

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

            this.log.Information("Fetching existing threads and comments...");

            var existingThreads =
                this.pullRequestSystem.FetchDiscussionThreads(
                    reportIssuesToPullRequestSettings.CommentSource).ToList();

            var issueComments =
                this.GetCommentsForIssue(
                    reportIssuesToPullRequestSettings,
                    issues,
                    existingThreads);

            // Comments that were created by this logic but do not have corresponding issues can be marked as 'Resolved'.
            this.ResolveExistingComments(existingThreads, issueComments);

            // TODO Comments that were created by this logic, are resolved, but still have a corresponding issue needs to be reopened

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

                if (!string.IsNullOrWhiteSpace(reportIssuesToPullRequestSettings.CommitId) &&
                    !this.pullRequestSystem.IsCurrentCommitId(reportIssuesToPullRequestSettings.CommitId))
                {
                    this.log.Information(
                        "Skipping posting of issues since commit {0} is outdated. Current commit is {1}",
                        reportIssuesToPullRequestSettings.CommitId,
                        this.pullRequestSystem.GetLastSourceCommitId());
                    return new List<IIssue>();
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
        private(IEnumerable<IPullRequestDiscussionComment> activeComments,
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
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <param name="issueComments">Issues and their related comments.</param>
        private void ResolveExistingComments(
            IList<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments)
        {
            existingThreads.NotNull(nameof(existingThreads));
            issueComments.NotNull(nameof(issueComments));

            if (!existingThreads.Any())
            {
                this.log.Verbose("No existings threads to resolve.");
                return;
            }

            var threadsToResolve =
                this.GetThreadsToResolve(existingThreads, issueComments).ToList();

            this.log.Verbose("Mark {0} threads as fixed...", threadsToResolve.Count);
            this.pullRequestSystem.MarkThreadsAsFixed(threadsToResolve);
        }

        /// <summary>
        /// Returns threads that can be resolved.
        /// </summary>
        /// <param name="existingThreads">Existing discussion threads on the pull request.</param>
        /// <param name="issueComments">Issues and their related comments.</param>
        /// <returns>List of threads which can be resolved.</returns>
        private IEnumerable<IPullRequestDiscussionThread> GetThreadsToResolve(
            IList<IPullRequestDiscussionThread> existingThreads,
            IDictionary<IIssue, IssueCommentInfo> issueComments)
        {
            existingThreads.NotNull(nameof(existingThreads));
            issueComments.NotNull(nameof(issueComments));

            var currentComments =
                new HashSet<IPullRequestDiscussionComment>(
                    issueComments.Values.SelectMany(x => x.ActiveComments));

            var result =
                existingThreads.Where(thread => !thread.Comments.Any(x => currentComments.Contains(x))).ToList();

            this.log.Verbose(
                "Found {0} existing thread(s) that do not match any new issue and can be resolved.",
                result.Count);

            return result;
        }
    }
}
