namespace Cake.Issues.Reporting.Console.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Testing;
    using Shouldly;

    internal class ConsoleIssueReportFixture
    {
        public const string RepositoryRootPath = @"c:\Source\Cake.Issues.Reporting.Console";

        public ConsoleIssueReportFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.ConsoleIssueReportFormatSettings = new ConsoleIssueReportFormatSettings();
        }

        public FakeLog Log { get; set; }

        public ConsoleIssueReportFormatSettings ConsoleIssueReportFormatSettings { get; set; }

        public string CreateReport(IEnumerable<IIssue> issues)
        {
            var generator =
                new ConsoleIssueReportGenerator(this.Log, this.ConsoleIssueReportFormatSettings);

            var createIssueReportSettings =
                new CreateIssueReportSettings(RepositoryRootPath, string.Empty);
            generator.Initialize(createIssueReportSettings);
            generator.CreateReport(issues);

            // TODO Return console output
            return string.Empty;
        }

        public void TestReportCreation(Action<ConsoleIssueReportFormatSettings> settings)
        {
            // Given
            settings(this.ConsoleIssueReportFormatSettings);

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
            // Currently only checks if generation failed or not without checking actual output.
            result.ShouldNotBeNull();
        }
    }
}
