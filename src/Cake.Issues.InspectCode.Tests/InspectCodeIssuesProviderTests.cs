namespace Cake.Issues.InspectCode.Tests
{
    using System.Linq;
    using Cake.Testing;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public class InspectCodeIssuesProviderTests
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
                        InspectCodeIssuesSettings.FromContent(@"foo")));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new InspectCodeIssuesProvider(new FakeLog(), null));

                // Then
                result.IsArgumentNullException("settings");
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
                CheckIssue(
                    issue,
                    @"src\Cake.Issues\CakeAliasConstants.cs",
                    16,
                    "UnusedMember.Global",
                    null,
                    0,
                    @"Constant 'PullRequestSystemCakeAliasCategory' is never used");
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
                CheckIssue(
                    issue,
                    @"src\Cake.CodeAnalysisReporting\CodeAnalysisReportingAliases.cs",
                    3,
                    "RedundantUsingDirective",
                    "http://www.jetbrains.com/resharperplatform/help?Keyword=RedundantUsingDirective",
                    0,
                    @"Using directive is not required by the code and can be safely removed");
            }

            private static void CheckIssue(
                IIssue issue,
                string affectedFileRelativePath,
                int? line,
                string rule,
                string ruleUrl,
                int priority,
                string message)
            {
                if (issue.AffectedFileRelativePath == null)
                {
                    affectedFileRelativePath.ShouldBeNull();
                }
                else
                {
                    issue.AffectedFileRelativePath.ToString().ShouldBe(new FilePath(affectedFileRelativePath).ToString());
                    issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "Issue path is not relative");
                }

                issue.Line.ShouldBe(line);
                issue.Rule.ShouldBe(rule);

                if (issue.RuleUrl == null)
                {
                    ruleUrl.ShouldBeNull();
                }
                else
                {
                    issue.RuleUrl.ToString().ShouldBe(ruleUrl);
                }

                issue.Priority.ShouldBe(priority);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
