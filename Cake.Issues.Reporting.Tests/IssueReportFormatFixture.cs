namespace Cake.Issues.Reporting.Tests
{
    using System.Collections.Generic;
    using Cake.Testing;
    using Core.Diagnostics;
    using Core.IO;
    using Issues;
    using Issues.IssueProvider;

    public class IssueReportFormatFixture
    {
        public IssueReportFormatFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.IssueReportFormat = new FakeIssueReportFormat(this.Log);
            this.Settings =
                new RepositorySettings(
                    new DirectoryPath(@"c:\Source\Cake.Issues"));
        }

        public FakeLog Log { get; set; }

        public FakeIssueReportFormat IssueReportFormat { get; set; }

        public RepositorySettings Settings { get; set; }

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
