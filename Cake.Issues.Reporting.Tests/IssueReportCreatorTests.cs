namespace Cake.Issues.Reporting.Tests
{
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueReportCreatorTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new IssueReportFormatFixture
                {
                    Log = null,
                };

                // When
                var result = Record.Exception(() => fixture.CreateReport(new List<IIssue>()));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var fixture = new IssueReportFormatFixture
                {
                    Settings = null,
                };

                // When
                var result = Record.Exception(() => fixture.CreateReport(new List<IIssue>()));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheCreateReportForIssueProvidersMethod
        {
            [Fact]
            public void Should_Initialize_Report_Format()
            {
                // Given
                var fixture = new IssueReportFormatFixture();
                var issueProviders = new List<FakeIssueProvider> { new FakeIssueProvider(fixture.Log) };

                // When
                fixture.CreateReport(issueProviders);

                // Then
                fixture.IssueReportFormat.Settings.ShouldBe(fixture.Settings);
            }

            [Fact]
            public void Should_Return_Null_If_Initialization_Fails()
            {
                // Given
                var fixture = new IssueReportFormatFixture();
                fixture.IssueReportFormat.ShouldFailOnInitialization = true;
                var issueProviders = new List<FakeIssueProvider> { new FakeIssueProvider(fixture.Log) };

                // When
                var result = fixture.CreateReport(issueProviders);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Return_File_Path()
            {
                // Given
                var fixture = new IssueReportFormatFixture();
                var issueProviders = new List<FakeIssueProvider> { new FakeIssueProvider(fixture.Log) };

                // When
                var result = fixture.CreateReport(issueProviders);

                // Then
                result.ShouldNotBeNull();
            }
        }

        public sealed class TheCreateReportForIssuesMethod
        {
            [Fact]
            public void Should_Initialize_Report_Format()
            {
                // Given
                var fixture = new IssueReportFormatFixture();
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                            .NewIssue("Message", "ProviderType", "ProviderName")
                            .InFile(@"src\Cake.Issues.Reporting\Foo.cs", 10)
                            .OfRule("Rule")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    };

                // When
                fixture.CreateReport(issues);

                // Then
                fixture.IssueReportFormat.Settings.ShouldBe(fixture.Settings);
            }

            [Fact]
            public void Should_Return_Null_If_Initialization_Fails()
            {
                // Given
                var fixture = new IssueReportFormatFixture();
                fixture.IssueReportFormat.ShouldFailOnInitialization = true;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                            .NewIssue("Message", "ProviderType", "ProviderName")
                            .InFile(@"src\Cake.Issues.Reporting\Foo.cs", 10)
                            .OfRule("Rule")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    };

                // When
                var result = fixture.CreateReport(issues);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Return_File_Path()
            {
                // Given
                var fixture = new IssueReportFormatFixture();
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                            .NewIssue("Message", "ProviderType", "ProviderName")
                            .InFile(@"src\Cake.Issues.Reporting\Foo.cs", 10)
                            .OfRule("Rule")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    };

                // When
                var result = fixture.CreateReport(issues);

                // Then
                result.ShouldNotBeNull();
            }
        }
    }
}