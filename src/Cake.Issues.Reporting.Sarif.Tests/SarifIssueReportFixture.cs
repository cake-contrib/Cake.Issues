namespace Cake.Issues.Reporting.Sarif.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Cake.Testing;
    using Shouldly;

    internal class SarifIssueReportFixture
    {
        public SarifIssueReportFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.SarifIssueReportFormatSettings = new SarifIssueReportFormatSettings();
        }

        public FakeLog Log { get; set; }

        public SarifIssueReportFormatSettings SarifIssueReportFormatSettings { get; set; }

        public string CreateReport(IEnumerable<IIssue> issues)
        {
            var generator =
                new SarifIssueReportGenerator(this.Log, this.SarifIssueReportFormatSettings);

            var reportFile = Path.GetTempFileName();
            try
            {
                var createIssueReportSettings =
                    new CreateIssueReportSettings(@"c:\Source\Cake.Issues.Reporting.Sarif", reportFile);
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

        public void TestReportCreation(Action<SarifIssueReportFormatSettings> settings)
        {
            // Given
            settings(this.SarifIssueReportFormatSettings);

            // When
            var result =
                this.CreateReport(
                    new List<IIssue>
                    {
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                    });

            // Then
            // Currently only checks if genertions failed or not without checking actual output.
            result.ShouldNotBeNull();
        }
    }
}
