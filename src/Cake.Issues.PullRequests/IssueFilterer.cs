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
            log.NotNull();
            pullRequestSystem.NotNull();
            settings.NotNull();

            this.log = log;
            this.pullRequestSystem = pullRequestSystem;
            this.settings = settings;
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
            issues.NotNull();

            this.log.Verbose("Filtering issues before posting...");

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
            IDictionary<IIssue, IssueCommentInfo> issueComments) =>
            issueComments.ContainsKey(issue) &&
            (
                issueComments[issue].ActiveComments.Any() ||
                issueComments[issue].WontFixComments.Any() ||
                issueComments[issue].ResolvedComments.Any());

        /// <summary>
        /// Validate the list of modified files in the pull request.
        /// </summary>
        /// <param name="modifiedFilePaths">List of modified files in the pull request.</param>
        private static void ValidateModifiedFiles(IEnumerable<FilePath> modifiedFilePaths)
        {
            var absoluteFilePaths = modifiedFilePaths.Where(x => !x.IsRelative).ToList();
            if (absoluteFilePaths.Count > 0)
            {
                throw new PullRequestIssuesException(
                    $"Absolute file paths are not supported for modified files:{Environment.NewLine}{string.Join(Environment.NewLine, absoluteFilePaths.Select(x => "  " + x))}");
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
        /// Limits the number of issues to not overload the pull request with too many comments.
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

            var totalIssuesFilteredCount = 0;

            // Apply issue limits per issue provider
            var result = new List<IIssue>();
            if (this.settings.MaxIssuesToPostForEachIssueProvider.HasValue)
            {
                foreach (var group in issues.GroupBy(x => x.ProviderType))
                {
                    var countBefore = group.Count();
                    var issuesFiltered =
                        group
                            .SortWithDefaultPrioritization()
                            .Take(this.settings.MaxIssuesToPostForEachIssueProvider.Value)
                            .ToList();
                    var issuesFilteredCount = countBefore - issuesFiltered.Count;
                    totalIssuesFilteredCount += issuesFilteredCount;

                    this.log.Information(
                        "{0} issue(s) of type {1} were filtered to match the maximum of {2} issues which should be reported for each issue provider",
                        issuesFilteredCount,
                        group.Key,
                        this.settings.MaxIssuesToPostForEachIssueProvider);

                    result.AddRange(issuesFiltered);
                }
            }

            // Apply issue limits per provider for this run
            foreach (var currentProviderLimitPair in this.settings.ProviderIssueLimits)
            {
                var currentProviderType = currentProviderLimitPair.Key;

                this.log.Verbose(
                    "Applying filter for issue provider '{0}' for this run...",
                    currentProviderType);

                var currentProviderTypeMaxLimit = currentProviderLimitPair.Value?.MaxIssuesToPost;
                if (!currentProviderTypeMaxLimit.HasValue)
                {
                    this.log.Verbose(
                        "No issues filtered for issue provider '{0}' for this run since `MaxIssuesToPost` is not set",
                        currentProviderType);
                    continue;
                }

                if (currentProviderTypeMaxLimit < 0)
                {
                    this.log.Verbose(
                        "No issues filtered for issue provider '{0}' for this run since `MaxIssuesToPost` is set to '{1}'",
                        currentProviderType,
                        currentProviderTypeMaxLimit);
                    continue;
                }

                var newIssuesForProviderType =
                    result.Where(p => p.ProviderType == currentProviderType)
                        .SortWithDefaultPrioritization()
                        .ToArray();
                if (newIssuesForProviderType.Length <= currentProviderTypeMaxLimit)
                {
                    this.log.Verbose(
                        "No issues filtered for issue provider '{0}' for this run since number of issues of this type is '{1}' which is less or equal to the value '{2}' configured in `MaxIssuesToPost`",
                        currentProviderType,
                        newIssuesForProviderType.Length,
                        currentProviderTypeMaxLimit);
                    continue;
                }

                var countBefore = result.Count;
                result = result.Where(p => p.ProviderType != currentProviderType)
                    .Concat(newIssuesForProviderType.Take(currentProviderTypeMaxLimit.Value))
                    .ToList();

                var issuesFilteredCount = countBefore - result.Count;
                this.log.Information(
                    "{0} issue(s) were filtered to match the global limit of {1} issues which should be reported for issue provider '{2}'",
                    issuesFilteredCount,
                    currentProviderTypeMaxLimit,
                    currentProviderType);
            }

            // Apply global issue limit
            if (this.settings.MaxIssuesToPost.HasValue)
            {
                var countBefore = result.Count;
                result =
                    result
                        .SortWithDefaultPrioritization()
                        .Take(this.settings.MaxIssuesToPost.Value)
                        .ToList();
                var issuesFilteredCount = countBefore - result.Count;
                totalIssuesFilteredCount += issuesFilteredCount;

                this.log.Information(
                    "{0} issue(s) were filtered to match the global issue limit of {1}",
                    issuesFilteredCount,
                    this.settings.MaxIssuesToPost);
            }

            // Apply issue limits per provider across multiple runs
            foreach (var (currentProviderType, value) in this.settings.ProviderIssueLimits)
            {
                this.log.Verbose(
                    "Applying filter for issue provider '{0}' across multiple runs...",
                    currentProviderType);

                var currentProviderTypeMaxLimit = value?.MaxIssuesToPostAcrossRuns;
                if (!currentProviderTypeMaxLimit.HasValue)
                {
                    this.log.Verbose(
                        "No issues filtered for issue provider '{0}' across multiple runs since `MaxIssuesToPostAcrossRuns` is not set",
                        currentProviderType);
                    continue;
                }

                if (currentProviderTypeMaxLimit < 0)
                {
                    this.log.Verbose(
                        "No issues filtered for issue provider '{0}' across multiple runs since `MaxIssuesToPostAcrossRuns` is not set",
                        currentProviderType);
                    continue;
                }

                var existingThreadCountForProvider =
                    existingThreads.Count(p => p.ProviderType == currentProviderType);

                var maxIssuesLeftToTakeForProviderType =
                    currentProviderTypeMaxLimit.Value - existingThreadCountForProvider;
                if (maxIssuesLeftToTakeForProviderType < 0)
                {
                    maxIssuesLeftToTakeForProviderType = 0;
                }

                var newIssuesForProviderType =
                    result.Where(p => p.ProviderType == currentProviderType)
                        .SortWithDefaultPrioritization()
                        .ToArray();
                if (newIssuesForProviderType.Length <= maxIssuesLeftToTakeForProviderType)
                {
                    this.log.Verbose(
                        "No issues filtered for issue provider '{0}' across multiple runs since number of issues of this type is '{1}' which is less or equal to the value '{2}' configured in `MaxIssuesToPostAcrossRuns`",
                        currentProviderType,
                        newIssuesForProviderType.Length,
                        maxIssuesLeftToTakeForProviderType);
                    continue;
                }

                result = result.Where(p => p.ProviderType != currentProviderType)
                    .Concat(newIssuesForProviderType.Take(maxIssuesLeftToTakeForProviderType))
                    .ToList();

                var issuesFilteredCount = newIssuesForProviderType.Length - maxIssuesLeftToTakeForProviderType;
                this.log.Information(
                    "{0} issue(s) were filtered to match the global issue limit of {1} across all runs for provider '{2}' ({3} issues already posted in previous runs)",
                    issuesFilteredCount,
                    currentProviderTypeMaxLimit,
                    currentProviderType,
                    existingThreads.Count);
            }

            // Apply global issue limit over multiple runs
            if (this.settings.MaxIssuesToPostAcrossRuns.HasValue && existingThreads != null)
            {
                var maxIssuesToPostInThisRun =
                    this.settings.MaxIssuesToPostAcrossRuns.Value - existingThreads.Count;
                var countBefore = result.Count;
                result =
                    result
                        .SortWithDefaultPrioritization()
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
