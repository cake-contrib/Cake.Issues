namespace Cake.Issues.Reporting.Tests;

using Cake.Core.Diagnostics;
using Cake.Core.IO;

internal class IssueReportFormatFixture
{
    public IssueReportFormatFixture()
    {
        this.Log = new FakeLog { Verbosity = Verbosity.Normal };
        this.IssueReportFormat = new FakeIssueReportFormat(this.Log);
        this.CreateIssueReportSettings =
            new CreateIssueReportSettings(
                @"c:\Source\Cake.Issues",
                @"c:\build\report.txt");
        this.CreateIssueReportFromIssueProviderSettings =
            new CreateIssueReportFromIssueProviderSettings(
                @"c:\Source\Cake.Issues",
                @"c:\build\report.txt");
    }

    public FakeLog Log { get; set; }

    public FakeIssueReportFormat IssueReportFormat { get; set; }

    public CreateIssueReportSettings CreateIssueReportSettings { get; set; }

    public CreateIssueReportFromIssueProviderSettings CreateIssueReportFromIssueProviderSettings { get; set; }

    public FilePath CreateReport(IEnumerable<IIssueProvider> issueProviders)
    {
        var issueReportCreator = new IssueReportCreator(this.Log);
        return
            issueReportCreator.CreateReport(
                issueProviders,
                this.IssueReportFormat,
                this.CreateIssueReportFromIssueProviderSettings);
    }

    public FilePath CreateReport(IEnumerable<IIssue> issues)
    {
        var issueReportCreator = new IssueReportCreator(this.Log);
        return
            issueReportCreator.CreateReport(
                issues,
                this.IssueReportFormat,
                this.CreateIssueReportSettings);
    }
}
