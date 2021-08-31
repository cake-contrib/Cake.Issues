namespace Cake.Issues.Markdownlint.Tests.LogFileFormat
{
    using System;
    using System.Linq;
    using Cake.Issues.Markdownlint.LogFileFormat;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class MarkdownlintCliJsonLogFileFormatTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new MarkdownlintCliJsonLogFileFormat(null));

                // Then
                result.IsArgumentNullException("log");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Empty_File_Correct()
            {
                // Given
                var fixture =
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliJsonLogFileFormat>("empty.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Read_Issue_MD013_Correct()
            {
                // Given
                var fixture =
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliJsonLogFileFormat>("MD013.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "Line length: Expected: 80; Actual: 124",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/extending/fundamentals.md", 7, 7, 81, 125)
                        .OfRule("MD013", new Uri("https://github.com/DavidAnson/markdownlint/blob/v0.23.1/doc/Rules.md#md013"))
                        .WithPriority(IssuePriority.Warning));
            }

            [Fact]
            public void Should_Read_Issue_MD025_Correct()
            {
                // Given
                var fixture =
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliJsonLogFileFormat>("MD025.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "Multiple top-level headings in the same document",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/extending/issue-provider/helper.md", 13)
                        .OfRule("MD025", new Uri("https://github.com/DavidAnson/markdownlint/blob/v0.23.1/doc/Rules.md#md025"))
                        .WithPriority(IssuePriority.Warning));
            }

            [Fact]
            public void Should_Read_Issue_MD034_Correct()
            {
                // Given
                var fixture =
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliJsonLogFileFormat>("MD034.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "Bare URL used",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/report-formats/sarif/release-notes.md", 139, 139, 6, 74)
                        .OfRule("MD034", new Uri("https://github.com/DavidAnson/markdownlint/blob/v0.23.1/doc/Rules.md#md034"))
                        .WithPriority(IssuePriority.Warning));
            }
        }
    }
}
