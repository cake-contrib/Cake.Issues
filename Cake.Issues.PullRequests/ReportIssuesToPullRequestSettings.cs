namespace Cake.Issues.PullRequests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.IO;

    /// <summary>
    /// Settings affecting how issues are reported to pull requests.
    /// </summary>
    public class ReportIssuesToPullRequestSettings : RepositorySettings
    {
        private readonly List<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> issueFilters = new List<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportIssuesToPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        public ReportIssuesToPullRequestSettings(DirectoryPath repositoryRoot)
            : base(repositoryRoot)
        {
        }

        /// <summary>
        /// Gets or sets the hash of the commit for which the issues were reported.
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// Gets or sets the global number of issues which should be posted at maximum over all
        /// <see cref="IIssueProvider"/>.
        /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
        /// are prioritized.
        /// Set to <c>null</c> to not set a global limit.
        /// Default is to not set a global limit.
        /// Use <see cref="MaxIssuesToPostForEachIssueProvider"/> to set the limit for each issue provider
        /// and <see cref="MaxIssuesToPostAcrossRuns"/> to set a limit across multiple runs.
        /// </summary>
        public int? MaxIssuesToPost { get; set; }

        /// <summary>
        /// Gets or sets the global number of issues which should be posted at maximum over all
        /// <see cref="IIssueProvider"/> and across multiple runs.
        /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
        /// are prioritized.
        /// Set to <c>null</c> to not set a limit across multiple runs.
        /// Default is to not set a limit across multiple runs.
        /// Use <see cref="MaxIssuesToPost"/> to set a limit for a single run.
        /// </summary>
        public int? MaxIssuesToPostAcrossRuns { get; set; }

        /// <summary>
        /// Gets or sets the number of issues which should be posted at maximum for each
        /// <see cref="IIssueProvider"/>.
        /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
        /// are prioritized.
        /// Set to <c>null</c> to not limit issues per issue provider.
        /// Default is to filter to 100 issues for each issue provider.
        /// Use <see cref="MaxIssuesToPost"/> to set the global limit over all issue providers.
        /// </summary>
        public int? MaxIssuesToPostForEachIssueProvider { get; set; } = 100;

        /// <summary>
        /// Gets or sets a value used to decorate comments created by this addin.
        /// Only comments with the same source will be resolved.
        /// </summary>
        public string CommentSource { get; set; } = "CakeIssues";

        /// <summary>
        /// Gets list of filter functions which should be applied before posting issues to pull requests.
        /// </summary>
        public IList<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> IssueFilters => this.issueFilters;
    }
}
