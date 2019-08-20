namespace Cake.Issues.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IIssueExtensionsTests
    {
        public sealed class TheProjectPathExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ProjectPath());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var projectPath = @"src\Cake.Issues\Cake.Issues.csproj";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InProjectFile(projectPath)
                        .Create();

                // When
                var result = issue.ProjectPath();

                // Then
                result.ShouldBe(@"src/Cake.Issues/Cake.Issues.csproj");
            }

            [Fact]
            public void Should_Return_Null_If_Project_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                var result = issue.ProjectPath();

                // Then
                result.ShouldBeNull();
            }
        }

        public sealed class TheProjectDirectoryExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ProjectDirectory());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InProjectFile(filePath)
                        .Create();

                // When
                var result = issue.ProjectDirectory();

                // Then
                result.ShouldBe(@"src/Cake.Issues");
            }

            [Fact]
            public void Should_Return_Null_If_File_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                var result = issue.ProjectDirectory();

                // Then
                result.ShouldBeNull();
            }
        }

        public sealed class TheFilePathExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FilePath());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                var result = issue.FilePath();

                // Then
                result.ShouldBe(@"src/Cake.Issues/Foo.cs");
            }

            [Fact]
            public void Should_Return_Null_If_File_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                var result = issue.FilePath();

                // Then
                result.ShouldBeNull();
            }
        }

        public sealed class TheFileDirectoryExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FileDirectory());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                var result = issue.FileDirectory();

                // Then
                result.ShouldBe(@"src/Cake.Issues");
            }

            [Fact]
            public void Should_Return_Null_If_File_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                var result = issue.FileDirectory();

                // Then
                result.ShouldBeNull();
            }
        }

        public sealed class TheFileNameExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FileName());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                var result = issue.FileName();

                // Then
                result.ShouldBe("Foo.cs");
            }

            [Fact]
            public void Should_Return_Null_If_File_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                var result = issue.FileName();

                // Then
                result.ShouldBeNull();
            }
        }

        public sealed class TheReplaceIssuePatternExtension
        {
            [Fact]
            public void Should_Throw_If_Pattern_Is_Null()
            {
                // Given
                string pattern = null;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                var result = Record.Exception(() => pattern.ReplaceIssuePattern(issue));

                // Then
                result.IsArgumentNullException("pattern");
            }

            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                var pattern = "foo";
                IIssue issue = null;

                // When
                var result = Record.Exception(() => pattern.ReplaceIssuePattern(issue));

                // Then
                result.IsArgumentNullException("issue");
            }

            [Theory]
            [InlineData("", "")]
            [InlineData(" ", " ")]
            [InlineData("foo", "foo")]
            [InlineData("{foo}", "{foo}")]
            [InlineData("foo {ProviderType} bar", "foo ProviderType Foo bar")]
            [InlineData("foo {ProviderName} bar", "foo ProviderName Foo bar")]
            [InlineData("foo {Priority} bar", "foo 400 bar")]
            [InlineData("foo {PriorityName} bar", "foo Error bar")]
            [InlineData("foo {ProjectPath} bar", "foo src/Cake.Issues/Cake.Issues.csproj bar")]
            [InlineData("foo {ProjectDirectory} bar", "foo src/Cake.Issues bar")]
            [InlineData("foo {ProjectName} bar", "foo Cake.Issues bar")]
            [InlineData("foo {FilePath} bar", "foo src/Cake.Issues/foo.cs bar")]
            [InlineData("foo {FileDirectory} bar", "foo src/Cake.Issues bar")]
            [InlineData("foo {FileName} bar", "foo foo.cs bar")]
            [InlineData("foo {Line} bar", "foo 42 bar")]
            [InlineData("foo {Rule} bar", "foo Rule Foo bar")]
            [InlineData("foo {RuleUrl} bar", "foo https://google.com/ bar")]
            [InlineData("foo {MessageText} bar", "foo MessageText Foo bar")]
            [InlineData("foo {MessageHtml} bar", "foo MessageHtml Foo bar")]
            [InlineData("foo {MessageMarkdown} bar", "foo MessageMarkdown Foo bar")]
            public void Should_Replace_Tokens(string pattern, string expectedResult)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("MessageText Foo", "ProviderType Foo", "ProviderName Foo")
                        .WithMessageInHtmlFormat("MessageHtml Foo")
                        .WithMessageInMarkdownFormat("MessageMarkdown Foo")
                        .InFile(@"src/Cake.Issues/foo.cs", 42)
                        .InProject(@"src/Cake.Issues/Cake.Issues.csproj", "Cake.Issues")
                        .OfRule("Rule Foo", new Uri("https://google.com"))
                        .WithPriority(IssuePriority.Error)
                        .Create();

                // When
                var result = pattern.ReplaceIssuePattern(issue);

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
