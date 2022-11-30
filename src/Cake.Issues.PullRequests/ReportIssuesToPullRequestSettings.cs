namespace Cake.Issues.PullRequests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.IO;

    /// <summary>
    /// Settings affecting how issues are reported to pull requests.
    /// </summary>
    public class ReportIssuesToPullRequestSettings : RepositorySettings, IReportIssuesToPullRequestSettings
    {
#pragma warning disable SA1124 // Do not use regions
        #region DupFinder Exclusion
#pragma warning restore SA1124 // Do not use regions
        private readonly List<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> issueFilters = new ();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportIssuesToPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        public ReportIssuesToPullRequestSettings(DirectoryPath repositoryRoot)
            : base(repositoryRoot)
        {
        }

        /// <inheritdoc />
        public string CommitId { get; set; }

        /// <inheritdoc />
        public int? MaxIssuesToPost { get; set; }

        /// <inheritdoc />
        public int? MaxIssuesToPostAcrossRuns { get; set; }

        /// <inheritdoc />
        public int? MaxIssuesToPostForEachIssueProvider { get; set; } = 100;

        /// <inheritdoc />
        public Dictionary<string, IProviderIssueLimits> ProviderIssueLimits { get; } =
            new Dictionary<string, IProviderIssueLimits>();

        /// <inheritdoc />
        public string CommentSource { get; set; } = "CakeIssues";

        /// <inheritdoc />
        public IList<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> IssueFilters => this.issueFilters;
    }
}
