namespace Cake.Issues.Reporting.Sarif.Tests
{
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class SarifIssueReportGeneratorTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SarifIssueReportGenerator(
                        null,
                        new SarifIssueReportFormatSettings()));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SarifIssueReportGenerator(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheInternalCreateReportMethod
        {
            [Fact]
            public void Should_Generate_Report()
            {
                // Given
                var fixture = new SarifIssueReportFixture();
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SerifIssueReportGeneratorTests.cs", 10)
                            .InProjectFile(@"src\Cake.Issues.Reporting.Sarif.Tests\Cake.Issues.Reporting.Sarif.Tests.csproj")
                            .OfRule("Rule Foo")
                            .WithPriority(IssuePriority.Error)
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Bar.", "ProviderType Bar", "ProviderName Bar")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SerifIssueReportGeneratorTests.cs", 12)
                            .OfRule("Rule Bar")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    };

                // When
                var result = fixture.CreateReport(issues);

                // Then
                // TODO
            }
        }
    }
}
