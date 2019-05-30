namespace Cake.Issues.Markdownlint.Tests.LogFileFormat
{
    using System;
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Issues.Markdownlint.LogFileFormat;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class MarkdownlintCliLogFileFormatTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new MarkdownlintCliLogFileFormat(null));

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
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliLogFileFormat>("empty.log");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Read_Issue_Correct_0_8_1()
            {
                // Given
                var fixture =
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliLogFileFormat>("markdownlint-cli-0.8.1.log");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(8);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "Headers should be surrounded by blank lines [Context: \"# foo\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 1)
                        .OfRule("MD022", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[1],
                    IssueBuilder.NewIssue(
                        "Trailing spaces [Expected: 2; Actual: 1]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 2)
                        .OfRule("MD009", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[2],
                    IssueBuilder.NewIssue(
                        "Line length [Expected: 100; Actual: 811]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 2)
                        .OfRule("MD013", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md013"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[3],
                    IssueBuilder.NewIssue(
                        "Headers should be surrounded by blank lines [Context: \"# bar\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 4)
                        .OfRule("MD022", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[4],
                    IssueBuilder.NewIssue(
                        "Multiple top level headers in the same document [Context: \"# bar\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 4)
                        .OfRule("MD025", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md025"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[5],
                    IssueBuilder.NewIssue(
                        "Fenced code blocks should be surrounded by blank lines [Context: \"```\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 5)
                        .OfRule("MD031", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md031"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[6],
                    IssueBuilder.NewIssue(
                        "Fenced code blocks should have a language specified [Context: \"```\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 5)
                        .OfRule("MD040", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md040"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[7],
                    IssueBuilder.NewIssue(
                        "Trailing spaces [Expected: 2; Actual: 1]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 6)
                        .OfRule("MD009", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009"))
                        .WithPriority(IssuePriority.Warning));
            }

            [Fact]
            public void Should_Read_Issue_Correct_0_10_0()
            {
                // Given
                var fixture =
                    new MarkdownlintIssuesProviderFixture<MarkdownlintCliLogFileFormat>("markdownlint-cli-0.10.0.log");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(8);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "Headings should be surrounded by blank lines [Context: \"# foo\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 1)
                        .OfRule("MD022", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[1],
                    IssueBuilder.NewIssue(
                        "Trailing spaces [Expected: 0 or 2; Actual: 1]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 2)
                        .OfRule("MD009", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[2],
                    IssueBuilder.NewIssue(
                        "Line length [Expected: 100; Actual: 811]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 2)
                        .OfRule("MD013", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md013"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[3],
                    IssueBuilder.NewIssue(
                        "Headings should be surrounded by blank lines [Context: \"# bar\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 4)
                        .OfRule("MD022", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md022"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[4],
                    IssueBuilder.NewIssue(
                        "Multiple top level headings in the same document [Context: \"# bar\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 4)
                        .OfRule("MD025", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md025"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[5],
                    IssueBuilder.NewIssue(
                        "Fenced code blocks should be surrounded by blank lines [Context: \"```\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 5)
                        .OfRule("MD031", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md031"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[6],
                    IssueBuilder.NewIssue(
                        "Fenced code blocks should have a language specified [Context: \"```\"]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 5)
                        .OfRule("MD040", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md040"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[7],
                    IssueBuilder.NewIssue(
                        "Trailing spaces [Expected: 0 or 2; Actual: 1]",
                        "Cake.Issues.Markdownlint.MarkdownlintIssuesProvider",
                        "markdownlint")
                        .InFile("docs/index.md", 6)
                        .OfRule("MD009", new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md009"))
                        .WithPriority(IssuePriority.Warning));
            }
        }
    }
}
