namespace Cake.Issues.PullRequests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Class for filtering issues.
    /// </summary>
    internal class IssueFilterer
    {
        private readonly ICakeLog log;
        private readonly IPullRequestSystem pullRequestSystem;
        private readonly IReportIssuesToPullRequestSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueFilterer"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        /// <param name="pullRequestSystem">Pull request system to use.</param>
        /// <param name="settings">Settings to use.</param>
        public IssueFilterer(
            ICakeLog log,
            IPullRequestSystem pullRequestSystem,
            IReportIssuesToPullRequestSettings settings)
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
        }

        /// <summary>
        /// Filters all issues which should not be logged.
        /// </summary>
        /// <param name="issues">Found issues.</param>
        /// <param name="issueComments">List of existing comments on the pull request or null if the
        /// pull request system doesn't support discussions.</param>
        /// <param name="existingThreads">List of threads which were reported by Cake.Issues.</param>
        /// <returns>List of filtered issues.</returns>
        public IEnumerable<IIssue> FilterIssues(
            IEnumerable<IIssue> issues,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            this.log.Verbose("Filtering issues before posting...");

            // ReSharper disable once PossibleMultipleEnumeration
            var result = this.FilterIssuesByPath(issues as IList<IIssue> ?? issues.ToList());

            if (issueComments != null)
            {
                result = this.FilterPreExistingComments(result, issueComments);
            }

            result = this.FilterIssuesByNumber(result, existingThreads);

            // Apply custom filters.
            foreach (var filterer in this.settings.IssueFilters)
            {
                var countBefore = result.Count;

                result = filterer(result).ToList();

                var commentsFiltered = countBefore - result.Count;

                this.log.Information(
                    "{0} issue(s) were filtered by custom filter.",
                    commentsFiltered);
            }

            return result;
        }

        /// <summary>
        /// Checks if there's already a comment for an issue.
        /// </summary>
        /// <param name="issue">Issue to check.</param>
        /// <param name="issueComments">List of existing comments.</param>
        /// <returns>True if there are already comments for an issue.</returns>
        private static bool IssueHasMatchingComments(
            IIssue issue,
            IDictionary<IIssue, IssueCommentInfo> issueComments)
        {
            return
                issueComments.ContainsKey(issue) &&
                (
                    issueComments[issue].ActiveComments.Any() ||
                    issueComments[issue].WontFixComments.Any() ||
                    issueComments[issue].ResolvedComments.Any());
        }

        /// <summary>
        /// Validate the list of modified files in the pull request.
        /// </summary>
        /// <param name="modifiedFilePaths">List of modified files in the pull request.</param>
        private static void ValidateModifiedFiles(IEnumerable<FilePath> modifiedFilePaths)
        {
            foreach (var filePath in modifiedFilePaths.Where(x => !x.IsRelative))
            {
                throw new PullRequestIssuesException(
                    $"Absolute file paths are not suported for modified files. Path: {filePath}");
            }
        }

        /// <summary>
        /// Filters all issues affecting files which do not belong to files changed in this pull request.
        /// </summary>
        /// <param name="issues">List of issues which should be filtered.</param>
        /// <returns>List of issues filtered to only the ones affecting files changed in this pull request.</returns>
        private IList<IIssue> FilterIssuesByPath(IList<IIssue> issues)
        {
            if (!issues.Any())
            {
                return issues;
            }

            var filterByModifiedFilesCapability = this.pullRequestSystem.GetCapability<ISupportFilteringByModifiedFiles>();
            if (filterByModifiedFilesCapability == null)
            {
                return issues;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var modifiedFilesList = filterByModifiedFilesCapability.GetModifiedFilesInPullRequest().ToList();
            ValidateModifiedFiles(modifiedFilesList);

            // Create paths absolute to repository root.
            var modifiedFilesHashSet =
                new HashSet<string>(modifiedFilesList.Select(x => x.MakeAbsolute(this.settings.RepositoryRoot).ToString()));
            this.log.Verbose(
                "Files changed in this pull request:\n{0}",
                string.Join(
                    Environment.NewLine,
                    modifiedFilesHashSet.Select(x => "  " + x)));

            var countBefore = issues.Count;
            var result =
                issues
                    .Where(issue =>
                        issue.AffectedFileRelativePath == null ||
                        modifiedFilesHashSet.Contains(
                            issue.AffectedFileRelativePath.MakeAbsolute(this.settings.RepositoryRoot).ToString()))
                    .ToList();
            var issuesFilteredCount = countBefore - result.Count;

            this.log.Information(
                "{0} issue(s) were filtered because they do not belong to files that were changed in this pull request",
                issuesFilteredCount);
            this.log.Verbose(
                "Filtering out {0} issues for files that were not changed in this pull request took {1} ms",
                issuesFilteredCount,
                stopwatch.ElapsedMilliseconds);

            return result;
        }

        /// <summary>
        /// Filters issues for which already a comment exists.
        /// </summary>
        /// <param name="issues">List of issues which should be filtered.</param>
        /// <param name="issueComments">List of issues and their existing matching comments on the pull request.</param>
        /// <returns>List issues filtered to only the ones not having already a comment.</returns>
        private IList<IIssue> FilterPreExistingComments(
            IList<IIssue> issues,
            IDictionary<IIssue, IssueCommentInfo> issueComments)
        {
            if (!issues.Any())
            {
                return issues;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var countBefore = issues.Count;
            var result = issues.Where(x => !IssueHasMatchingComments(x, issueComments)).ToList();
            var issuesFilteredCount = countBefore - result.Count;

            this.log.Information(
                "{0} issue(s) were filtered because they were already present",
                issuesFilteredCount);
            this.log.Verbose(
                "Filtering out {0} existing issues took {1} ms",
                issuesFilteredCount,
                stopwatch.ElapsedMilliseconds);

            return result;
        }

        /// <summary>
        /// Limits the number of issues so as to not overload the pull request with too many comments.
        /// </summary>
        /// <param name="issues">List of issues which should be filtered.</param>
        /// <param name="existingThreads">List of threads which were reported by Cake.Issues.</param>
        /// <returns>List of issues limited to the maximum number of issues to post.</returns>
        private IList<IIssue> FilterIssuesByNumber(
            IList<IIssue> issues,
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads)
        {
            if (!issues.Any())
            {
                return issues;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int totalIssuesFilteredCount = 0;

            // Apply issue limits per issue provider
            var result = new List<IIssue>();
            if (this.settings.MaxIssuesToPostForEachIssueProvider.HasValue)
            {
                foreach (var group in issues.GroupBy(x => x.ProviderType))
                {
                    var countBefore = group.Count();
                    var issuesFiltered =
                        group
                            .OrderByDescending(x => x.Priority)
                            .ThenBy(x => x.AffectedFileRelativePath is null)
                            .ThenBy(x => x.AffectedFileRelativePath?.FullPath)
                            .Take(this.settings.MaxIssuesToPostForEachIssueProvider.Value);
                    var issuesFilteredCount = countBefore - issuesFiltered.Count();
                    totalIssuesFilteredCount += issuesFilteredCount;

                    this.log.Information(
                        "{0} issue(s) of type {1} were filtered to match the maximum of {2} issues which should be reported for each issue provider",
                        issuesFilteredCount,
                        group.Key,
                        this.settings.MaxIssuesToPostForEachIssueProvider);

                    result.AddRange(issuesFiltered);
                }
            }

            // Apply global issue limit
            if (this.settings.MaxIssuesToPost.HasValue)
            {
                var countBefore = issues.Count;
                result =
                    result
                        .OrderByDescending(x => x.Priority)
                        .ThenBy(x => x.AffectedFileRelativePath is null)
                        .ThenBy(x => x.AffectedFileRelativePath?.FullPath)
                        .Take(this.settings.MaxIssuesToPost.Value)
                        .ToList();
                var issuesFilteredCount = countBefore - result.Count;
                totalIssuesFilteredCount += issuesFilteredCount;

                this.log.Information(
                    "{0} issue(s) were filtered to match the global issue limit of {1}",
                    issuesFilteredCount,
                    this.settings.MaxIssuesToPost);
            }

            // Apply global issue limit over multiple runs
            if (this.settings.MaxIssuesToPostAcrossRuns.HasValue && existingThreads != null)
            {
                var maxIssuesToPostInThisRun =
                    this.settings.MaxIssuesToPostAcrossRuns.Value - existingThreads.Count;
                var countBefore = issues.Count;
                result =
                    result
                        .OrderByDescending(x => x.Priority)
                        .ThenBy(x => x.AffectedFileRelativePath is null)
                        .ThenBy(x => x.AffectedFileRelativePath?.FullPath)
                        .Take(maxIssuesToPostInThisRun)
                        .ToList();
                var issuesFilteredCount = countBefore - result.Count;
                totalIssuesFilteredCount += issuesFilteredCount;

                this.log.Information(
                    "{0} issue(s) were filtered to match the global issue limit of {1} across all runs ({2} issues already posted in previous runs)",
                    issuesFilteredCount,
                    this.settings.MaxIssuesToPostAcrossRuns,
                    existingThreads.Count);
            }

            this.log.Verbose(
                "Filtering out {0} issues to match limits took {1} ms",
                totalIssuesFilteredCount,
                stopwatch.ElapsedMilliseconds);

            return result;
        }
    }
}
