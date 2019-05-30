namespace Cake.Issues.Reporting.Tests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Cake.Testing;

    internal class IssueReportFormatFixture
    {
        public IssueReportFormatFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.IssueReportFormat = new FakeIssueReportFormat(this.Log);
            this.Settings =
                new CreateIssueReportSettings(
                    @"c:\Source\Cake.Issues",
                    @"c:\build\report.txt");
        }

        public FakeLog Log { get; set; }

        public FakeIssueReportFormat IssueReportFormat { get; set; }

        public CreateIssueReportSettings Settings { get; set; }

        public FilePath CreateReport(IEnumerable<IIssueProvider> issueProviders)
        {
            var issueReportCreator = new IssueReportCreator(this.Log, this.Settings);
            return issueReportCreator.CreateReport(issueProviders, this.IssueReportFormat);
        }

        public FilePath CreateReport(IEnumerable<IIssue> issues)
        {
            var issueReportCreator = new IssueReportCreator(this.Log, this.Settings);
            return issueReportCreator.CreateReport(issues, this.IssueReportFormat);
        }
    }
}
