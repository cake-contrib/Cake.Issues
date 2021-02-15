namespace Cake.Issues.Reporting
{
    using Cake.Core.IO;

    /// <summary>
    /// Setting affecting how reports are created which are built passing issue providers.
    /// </summary>
    public interface ICreateIssueReportFromIssueProviderSettings : IReadIssuesSettings, ICreateIssueReportSettings
    {
    }
}
