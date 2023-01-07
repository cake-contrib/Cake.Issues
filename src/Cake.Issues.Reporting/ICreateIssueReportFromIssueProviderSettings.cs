namespace Cake.Issues.Reporting
{
    /// <summary>
    /// Setting affecting how reports are created which are built passing issue providers.
    /// </summary>
    public interface ICreateIssueReportFromIssueProviderSettings : IReadIssuesSettings, ICreateIssueReportSettings
    {
    }
}
