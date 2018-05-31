namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;

    internal class GenericIssueReportFixture
    {
        public GenericIssueReportFixture(GenericIssueReportTemplate template)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.GenericIssueReportFormatSettings =
                GenericIssueReportFormatSettings.FromEmbeddedTemplate(template);
        }

        public GenericIssueReportFixture(string templateContent)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.GenericIssueReportFormatSettings =
                GenericIssueReportFormatSettings.FromContent(templateContent);
        }

        public FakeLog Log { get; set; }

        public GenericIssueReportFormatSettings GenericIssueReportFormatSettings { get; set; }

        public string CreateReport(IEnumerable<IIssue> issues)
        {
            var generator =
                new GenericIssueReportGenerator(this.Log, this.GenericIssueReportFormatSettings);

            var reportFile = Path.GetTempFileName();
            try
            {
                var createIssueReportSettings =
                    new CreateIssueReportSettings(@"c:\Source\Cake.Issues.Reporting.Generic", reportFile);
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
