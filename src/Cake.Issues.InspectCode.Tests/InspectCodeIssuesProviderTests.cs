namespace Cake.Issues.InspectCode.Tests
{
    using System.Runtime.InteropServices;

    // ReSharper disable once ClassNeverInstantiated.Global
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
            [SkippableFact]
            public void Should_Read_Issue_Correct()
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

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
                        "InspectCode")
                        .InProjectOfName("Cake.Issues")
                        .InFile(@"src\Cake.Issues\CakeAliasConstants.cs", 16)
                        .OfRule(
                            "UnusedMember.Global",
                            "Type or member is never used: Non-private accessibility")
                        .WithPriority(IssuePriority.Suggestion)
                        .Create());
            }

            [Fact]
            public void Should_Read_Issue_Without_Line_Correct()
            {
                // Given
                var fixture = new InspectCodeIssuesProviderFixture("WithoutLineButOffset.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                IssueChecker.Check(
                    issue,
                    IssueBuilder.NewIssue(
                        "Using directive is not required by the code and can be safely removed",
                        "Cake.Issues.InspectCode.InspectCodeIssuesProvider",
                        "InspectCode")
                        .InProjectOfName("Project1")
                        .InFile(@"Src\myfile.cs", 1)
                        .OfRule(
                            "RedundantUsingDirective",
                            "Redundant using directive",
                            new Uri("https://www.jetbrains.com/resharperplatform/help?Keyword=RedundantUsingDirective"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
            }

            [SkippableFact]
            public void Should_Read_Rule_Url()
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

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
                        "Using directive is not required by the code and can be safely removed",
                        "Cake.Issues.InspectCode.InspectCodeIssuesProvider",
                        "InspectCode")
                        .InProjectOfName("Cake.CodeAnalysisReporting")
                        .InFile(@"src\Cake.CodeAnalysisReporting\CodeAnalysisReportingAliases.cs", 3)
                        .OfRule(
                            "RedundantUsingDirective",
                            "Redundant using directive",
                            new Uri("http://www.jetbrains.com/resharperplatform/help?Keyword=RedundantUsingDirective"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
            }
        }
    }
}
