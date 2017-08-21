namespace Cake.Issues.PullRequests
{
    using Core.IO;

    /// <summary>
    /// Settings affecting how issues are reported to pull requests.
    /// </summary>
    public class ReportIssuesToPullRequestSettings : RepositorySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportIssuesToPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        public ReportIssuesToPullRequestSettings(DirectoryPath repositoryRoot)
            : base(repositoryRoot)
        {
        }

        /// <summary>
        /// Gets or sets the number of issues which should be posted at maximum.
        /// </summary>
        public int MaxIssuesToPost { get; set; } = 100;

        /// <summary>
        /// Gets or sets a value used to decorate comments created by this addin.
        /// Only comments with the same source will be resolved.
        /// </summary>
        public string CommentSource { get; set; } = "CakeIssues";
    }
}
