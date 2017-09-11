namespace Cake.Issues.Reporting.Html.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;

    internal class HtmlIssueReportFixture
    {
        public HtmlIssueReportFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.HtmlIssueReportFormatSettings =
                HtmlIssueReportFormatSettings.FromEmbeddedTemplate(HtmlIssueReportTemplate.Diagnostic);
        }

        public HtmlIssueReportFixture(string templateContent)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.HtmlIssueReportFormatSettings =
                HtmlIssueReportFormatSettings.FromContent(templateContent);
        }

        public FakeLog Log { get; set; }

        public HtmlIssueReportFormatSettings HtmlIssueReportFormatSettings { get; set; }

        public string CreateReport(IEnumerable<IIssue> issues)
        {
            var generator =
                new HtmlIssueReportGenerator(this.Log, this.HtmlIssueReportFormatSettings);

            var reportFile = System.IO.Path.GetTempFileName();
            try
            {
                var createIssueReportSettings =
                    new CreateIssueReportSettings(@"c:\Source\Cake.Issues.Reporting.Html", reportFile);
                generator.Initialize(createIssueReportSettings);
                generator.CreateReport(issues);

                using (var stream = new FileStream(reportFile, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (File.Exists(reportFile))
                {
                    File.Delete(reportFile);
                }
            }
        }
    }
}
