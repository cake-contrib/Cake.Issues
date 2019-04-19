namespace Cake.Issues.Markdownlint.Tests.LogFileFormat
{
    using System;
    using System.Linq;
    using Cake.Issues.Markdownlint.LogFileFormat;
    using Cake.Issues.Testing;
    using Shouldly;
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
                var fixture = new MarkdownlintIssuesProviderFixture<MarkdownlintLogFileFormat>("markdownlint.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(3);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "Hard tabs",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("bad.md", 3)
                        .OfRule("MD010", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md010"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[1],
                    IssueBuilder.NewIssue(
                        "No space after hash on atx style header",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("bad.md", 1)
                        .OfRule("MD018", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md018"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[2],
                    IssueBuilder.NewIssue(
                        "No space after hash on atx style header",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("bad.md", 3)
                        .OfRule("MD018", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md018"))
                        .WithPriority(IssuePriority.Warning));
            }
        }
    }
}
