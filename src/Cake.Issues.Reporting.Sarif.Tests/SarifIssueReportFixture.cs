﻿namespace Cake.Issues.Reporting.Sarif.Tests;

using System.IO;
using Cake.Core.Diagnostics;

internal class SarifIssueReportFixture
{
    public const string RepositoryRootPath = @"c:\Source\Cake.Issues.Reporting.Sarif\";

    public FakeLog Log { get; set; } = new() { Verbosity = Verbosity.Normal };

    public SarifIssueReportFormatSettings SarifIssueReportFormatSettings { get; set; } = new();

    public string CreateReport(IEnumerable<IIssue> issues)
    {
        var generator =
            new SarifIssueReportGenerator(this.Log, this.SarifIssueReportFormatSettings);

        var reportFile = Path.GetTempFileName();
        try
        {
            var createIssueReportSettings =
                new CreateIssueReportSettings(RepositoryRootPath, reportFile);
            _ = generator.Initialize(createIssueReportSettings);
            _ = generator.CreateReport(issues);

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
                [
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create(),
                ]);

        // Then
        // Currently only checks if generation failed or not without checking actual output.
        _ = result.ShouldNotBeNull();
    }
}
