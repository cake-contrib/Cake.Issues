namespace Cake.Issues.Markdownlint.Tests
{
    using System.Linq;
    using Cake.Testing;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class MarkdownlintLogFileFormatTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new MarkdownlintLogFileFormat(null));

                // Then
                result.IsArgumentNullException("log");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Issues_Correct()
            {
                // Given
                var format = new MarkdownlintLogFileFormat(new FakeLog());
                var fixture = new MarkdownlintIssuesProviderFixture("markdownlint.json", format);

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(3);
                CheckIssue(
                    issues[0],
                    null,
                    null,
                    @"bad.md",
                    3,
                    "MD010",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md010",
                    300,
                    "Warning",
                    "Hard tabs");
                CheckIssue(
                    issues[1],
                    null,
                    null,
                    @"bad.md",
                    1,
                    "MD018",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md018",
                    300,
                    "Warning",
                    "No space after hash on atx style header");
                CheckIssue(
                    issues[2],
                    null,
                    null,
                    @"bad.md",
                    3,
                    "MD018",
                    "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md018",
                    300,
                    "Warning",
                    "No space after hash on atx style header");
            }

            private static void CheckIssue(
                IIssue issue,
                string projectFileRelativePath,
                string projectName,
                string affectedFileRelativePath,
                int? line,
                string rule,
                string ruleUrl,
                int priority,
                string priorityName,
                string message)
            {
                issue.ProviderType.ShouldBe("Cake.Issues.Markdownlint.MarkdownlintIssuesProvider");
                issue.ProviderName.ShouldBe("markdownlint");

                if (issue.ProjectFileRelativePath == null)
                {
                    projectFileRelativePath.ShouldBeNull();
                }
                else
                {
                    issue.ProjectFileRelativePath.ToString().ShouldBe(new FilePath(projectFileRelativePath).ToString());
                    issue.ProjectFileRelativePath.IsRelative.ShouldBe(true, "Issue path is not relative");
                }

                issue.ProjectName.ShouldBe(projectName);

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
