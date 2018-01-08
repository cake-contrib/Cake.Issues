namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Core.IO;

    /// <summary>
    /// Settings affecting how issues are reported to pull requests.
    /// </summary>
    public class ReportIssuesToPullRequestSettings : RepositorySettings
    {
        private readonly Dictionary<IIssueProvider, int> maxIssuesToPost = new Dictionary<IIssueProvider, int>();

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
        /// Set to <see langword="null"/> to not set a global limit.
        /// Default is to not set a global limit.
        /// Use <see cref="MaxIssuesToPostForEachIssueProvider"/> to set the limit for each issue provider.
        /// </summary>
        public int? MaxIssuesToPost { get; set; } = null;

        /// <summary>
        /// Gets or sets the global number of issues which should be posted at for each
        /// <see cref="IIssueProvider"/>.
        /// Set to <see langword="null"/> to not limit issues per issue provider.
        /// Default is to filter to 100 issues for each issue provider.
        /// Use <see cref="MaxIssuesToPost"/> to set the global limit over all issue providers.
        /// </summary>
        public int? MaxIssuesToPostForEachIssueProvider { get; set; } = 100;

        /// <summary>
        /// Gets or sets a value used to decorate comments created by this addin.
        /// Only comments with the same source will be resolved.
        /// </summary>
        public string CommentSource { get; set; } = "CakeIssues";
    }
}
