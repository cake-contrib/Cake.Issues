namespace Cake.Issues.Reporting.Console.Tests;

using System.IO;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Issues.Serialization;
using Spectre.Console.Testing;

internal class ConsoleIssueReportFixture
{
    public TestConsole Console { get; set; } = new();

    public FakeLog Log { get; set; } = new() { Verbosity = Verbosity.Normal };

    public ConsoleIssueReportFormatSettings ConsoleIssueReportFormatSettings { get; set; } = new();

    public void CreateReportForTestfile(string fileResourceName, DirectoryPath repositoryRootPath)
    {
        fileResourceName.NotNullOrWhiteSpace();

        var resourceName = "Cake.Issues.Reporting.Console.Tests." + fileResourceName;

        using (var stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream))
        {
            if (stream == null)
            {
                throw new ArgumentException(
                    $"Resource {resourceName} not found",
                    nameof(fileResourceName));
            }

            var issues = reader.ReadToEnd().DeserializeToIssues();
            this.CreateReport(issues, repositoryRootPath);
        }
    }

    public void CreateReport(IEnumerable<IIssue> issues, DirectoryPath repositoryRootPath)
    {
        var generator =
            new ConsoleIssueReportGenerator(
                this.Console,
                this.Log,
                this.ConsoleIssueReportFormatSettings);

        var createIssueReportSettings =
            new CreateIssueReportSettings(repositoryRootPath, string.Empty);
        _ = generator.Initialize(createIssueReportSettings);
        _ = generator.CreateReport(issues);

    }

    public void TestReportCreation(Action<ConsoleIssueReportFormatSettings> settings)
    {
        // Given
        settings(this.ConsoleIssueReportFormatSettings);

        // When
        this.CreateReport(
            [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .InFile(@"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs", 10)
                    .OfRule("Rule Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create(),
            ],
            @"c:\Source\Cake.Issues.Reporting.Console");

        // Then
    }
}
