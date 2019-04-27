namespace Cake.Issues.InspectCode.Tests
{
    using System;
    using System.Linq;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class InspectCodeIssuesProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new InspectCodeIssuesProvider(
                        null,
                        new InspectCodeIssuesSettings("Foo".ToByteArray())));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_IssueProviderSettings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new InspectCodeIssuesProvider(new FakeLog(), null));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Issue_Correct()
            {
                // Given
                var fixture = new InspectCodeIssuesProviderFixture("inspectcode.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                IssueChecker.Check(
                    issue,
                    IssueBuilder.NewIssue(
                        "Constant 'PullRequestSystemCakeAliasCategory' is never used",
                        "Cake.Issues.InspectCode.InspectCodeIssuesProvider",
                        "InspectCode"
                        )
                        .InProjectOfName("Cake.Issues")
                        .InFile(@"src\Cake.Issues\CakeAliasConstants.cs", 16)
                        .OfRule("UnusedMember.Global")
                        .WithPriority(IssuePriority.Suggestion)
                        .Create());
            }

            [Fact]
            public void Should_Read_Rule_Url()
            {
                // Given
                var fixture = new InspectCodeIssuesProviderFixture("WithWikiUrl.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                IssueChecker.Check(
                    issue,
                    IssueBuilder.NewIssue(
                        @"Using directive is not required by the code and can be safely removed",
                        "Cake.Issues.InspectCode.InspectCodeIssuesProvider",
                        "InspectCode"
                        )
                        .InProjectOfName("Cake.CodeAnalysisReporting")
                        .InFile(@"src\Cake.CodeAnalysisReporting\CodeAnalysisReportingAliases.cs", 3)
                        .OfRule("RedundantUsingDirective", new Uri("http://www.jetbrains.com/resharperplatform/help?Keyword=RedundantUsingDirective"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
            }
        }
    }
}
