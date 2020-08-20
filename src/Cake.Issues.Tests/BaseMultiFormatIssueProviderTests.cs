namespace Cake.Issues.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class BaseMultiFormatIssueProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var settings =
                    new FakeMultiFormatIssueProviderSettings(
                        "Foo".ToByteArray(),
                        new FakeLogFileFormat(new FakeLog()));

                // When
                var result = Record.Exception(() => new FakeMultiFormatIssueProvider(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_IssueProviderSettings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                FakeMultiFormatIssueProviderSettings settings = null;

                // When
                var result = Record.Exception(() => new FakeMultiFormatIssueProvider(log, settings));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();
                var settings =
                    new FakeMultiFormatIssueProviderSettings(
                        "Foo".ToByteArray(),
                        new FakeLogFileFormat(log));

                // When
                var result = new FakeMultiFormatIssueProvider(log, settings);

                // Then
                result.Log.ShouldBe(log);
            }

            [Fact]
            public void Should_Set_IssueProviderSettings()
            {
                // Given
                var log = new FakeLog();
                var settings =
                    new FakeMultiFormatIssueProviderSettings(
                        "Foo".ToByteArray(),
                        new FakeLogFileFormat(log));

                // When
                var result = new FakeMultiFormatIssueProvider(log, settings);

                // Then
                result.IssueProviderSettings.ShouldBe(settings);
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Issues_From_Format()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                        .OfRule("Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var log = new FakeLog();
                var format =
                    new FakeLogFileFormat(
                        log,
                        new List<IIssue> { issue1, issue2 });
                var settings =
                    new FakeMultiFormatIssueProviderSettings(
                        "Foo".ToByteArray(),
                        format);
                var provider = new FakeMultiFormatIssueProvider(log, settings);
                provider.Initialize(new ReadIssuesSettings(@"c:\repo"));

                // When
                var result = provider.ReadIssues();

                // Then
                result.Count().ShouldBe(2);
                result.ShouldContain(issue1);
                result.ShouldContain(issue2);
            }
        }
    }
}
