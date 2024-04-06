namespace Cake.Issues.Reporting.Console.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Cake.Issues.Serialization;
    using Cake.Testing;
    using Shouldly;

    internal class ConsoleIssueReportFixture
    {
        public ConsoleIssueReportFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.ConsoleIssueReportFormatSettings = new ConsoleIssueReportFormatSettings();
        }

        public FakeLog Log { get; set; }

        public ConsoleIssueReportFormatSettings ConsoleIssueReportFormatSettings { get; set; }

        public string CreateReport(string fileResourceName, DirectoryPath repositoryRootPath)
        {
            fileResourceName.NotNullOrWhiteSpace(nameof(fileResourceName));

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

                var issues = IssueDeserializationExtensions.DeserializeToIssues(reader.ReadToEnd());
                return this.CreateReport(issues, repositoryRootPath);
            }
        }

        public string CreateReport(IEnumerable<IIssue> issues, DirectoryPath repositoryRootPath)
        {
            var generator =
                new ConsoleIssueReportGenerator(this.Log, this.ConsoleIssueReportFormatSettings);

            var createIssueReportSettings =
                new CreateIssueReportSettings(repositoryRootPath, string.Empty);
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
                    },
                    @"c:\Source\Cake.Issues.Reporting.Console");

            // Then
            // Currently only checks if generation failed or not without checking actual output.
            result.ShouldNotBeNull();
        }
    }
}
