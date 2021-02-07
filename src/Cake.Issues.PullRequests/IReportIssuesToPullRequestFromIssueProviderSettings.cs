namespace Cake.Issues.PullRequests
{
    /// <summary>
    /// Interface for settings affecting how issues read from issue providers are reported to pull requests.
    /// </summary>
    public interface IReportIssuesToPullRequestFromIssueProviderSettings : IReadIssuesSettings, IReportIssuesToPullRequestSettings
    {
    }
}
