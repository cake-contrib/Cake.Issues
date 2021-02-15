namespace Cake.Issues.PullRequests
{
    /// <inheritdoc />
    public class ProviderIssueIssueLimits : IProviderIssueLimits
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderIssueIssueLimits"/> class.
        /// </summary>
        /// <param name="maxIssuesToPost">Maximum amount of issues to be posted in a single run.</param>
        /// <param name="maxIssuesToPostAcrossRuns">Maximum amount of issues to be posted across all runs.</param>
        public ProviderIssueIssueLimits(
            int? maxIssuesToPost = null,
            int? maxIssuesToPostAcrossRuns = null)
        {
            this.MaxIssuesToPost = maxIssuesToPost;
            this.MaxIssuesToPostAcrossRuns = maxIssuesToPostAcrossRuns;
        }

        /// <inheritdoc />
        public int? MaxIssuesToPost { get; set; }

        /// <inheritdoc />
        public int? MaxIssuesToPostAcrossRuns { get; set; }
    }
}
