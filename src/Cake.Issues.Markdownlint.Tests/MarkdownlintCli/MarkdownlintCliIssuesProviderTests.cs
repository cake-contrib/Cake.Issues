namespace Cake.Issues.Markdownlint.Tests.MarkdownlintCli
{
    using System.Linq;
    using Cake.Issues.Markdownlint.MarkdownlintCli;
    using Cake.Testing;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class MarkdownlintCliIssuesProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new MarkdownlintCliIssuesProvider(
                        null,
                        MarkdownlintCliIssuesSettings.FromContent("Foo")));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new MarkdownlintCliIssuesProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Issue_Correct_0_8_1()
            {
                // Given
                var fixture = new MarkdownlintCliIssuesProviderFixture("markdownlint-cli-0-8-1.log");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(8);
                CheckIssue(
                    issues[0],
                    @"docs/index.md",
                    1,
                    "MD022",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022",
                    0,
                    "Warning",
                    "Headers should be surrounded by blank lines [Context: \"# foo\"]");
                CheckIssue(
                    issues[1],
                    @"docs/index.md",
                    2,
                    "MD009",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009",
                    0,
                    "Warning",
                    "Trailing spaces [Expected: 2; Actual: 1]");
                CheckIssue(
                    issues[2],
                    @"docs/index.md",
                    2,
                    "MD013",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md013",
                    0,
                    "Warning",
                    "Line length [Expected: 100; Actual: 811]");
                CheckIssue(
                    issues[3],
                    @"docs/index.md",
                    4,
                    "MD022",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022",
                    0,
                    "Warning",
                    "Headers should be surrounded by blank lines [Context: \"# bar\"]");
                CheckIssue(
                    issues[4],
                    @"docs/index.md",
                    4,
                    "MD025",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md025",
                    0,
                    "Warning",
                    "Multiple top level headers in the same document [Context: \"# bar\"]");
                CheckIssue(
                    issues[5],
                    @"docs/index.md",
                    5,
                    "MD031",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md031",
                    0,
                    "Warning",
                    "Fenced code blocks should be surrounded by blank lines [Context: \"```\"]");
                CheckIssue(
                    issues[6],
                    @"docs/index.md",
                    5,
                    "MD040",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md040",
                    0,
                    "Warning",
                    "Fenced code blocks should have a language specified [Context: \"```\"]");
                CheckIssue(
                    issues[7],
                    @"docs/index.md",
                    6,
                    "MD009",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009",
                    0,
                    "Warning",
                    "Trailing spaces [Expected: 2; Actual: 1]");
            }

            [Fact]
            public void Should_Read_Issue_Correct_0_10_0()
            {
                // Given
                var fixture = new MarkdownlintCliIssuesProviderFixture("markdownlint-cli-0.10.0.log");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(8);
                CheckIssue(
                    issues[0],
                    @"docs/index.md",
                    1,
                    "MD022",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022",
                    0,
                    "Warning",
                    "Headings should be surrounded by blank lines [Context: \"# foo\"]");
                CheckIssue(
                    issues[1],
                    @"docs/index.md",
                    2,
                    "MD009",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009",
                    0,
                    "Warning",
                    "Trailing spaces [Expected: 0 or 2; Actual: 1]");
                CheckIssue(
                    issues[2],
                    @"docs/index.md",
                    2,
                    "MD013",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md013",
                    0,
                    "Warning",
                    "Line length [Expected: 100; Actual: 811]");
                CheckIssue(
                    issues[3],
                    @"docs/index.md",
                    4,
                    "MD022",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022",
                    0,
                    "Warning",
                    "Headings should be surrounded by blank lines [Context: \"# bar\"]");
                CheckIssue(
                    issues[4],
                    @"docs/index.md",
                    4,
                    "MD025",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md025",
                    0,
                    "Warning",
                    "Multiple top level headings in the same document [Context: \"# bar\"]");
                CheckIssue(
                    issues[5],
                    @"docs/index.md",
                    5,
                    "MD031",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md031",
                    0,
                    "Warning",
                    "Fenced code blocks should be surrounded by blank lines [Context: \"```\"]");
                CheckIssue(
                    issues[6],
                    @"docs/index.md",
                    5,
                    "MD040",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md040",
                    0,
                    "Warning",
                    "Fenced code blocks should have a language specified [Context: \"```\"]");
                CheckIssue(
                    issues[7],
                    @"docs/index.md",
                    6,
                    "MD009",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009",
                    0,
                    "Warning",
                    "Trailing spaces [Expected: 0 or 2; Actual: 1]");
            }

            private static void CheckIssue(
                IIssue issue,
                string affectedFileRelativePath,
                int? line,
                string rule,
                string ruleUrl,
                int priority,
                string priorityName,
                string message)
            {
                issue.ProviderType.ShouldBe("Cake.Issues.Markdownlint.MarkdownlintCli.MarkdownlintCliIssuesProvider");
                issue.ProviderName.ShouldBe("markdownlint");

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
                issue.PriorityName.ShouldBe(priorityName);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
