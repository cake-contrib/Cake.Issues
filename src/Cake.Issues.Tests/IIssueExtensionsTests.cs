namespace Cake.Issues.Tests
{
    public sealed class IIssueExtensionsTests
    {
        public sealed class TheLineRangeExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.LineRange());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Theory]
            [InlineData(null, null, null, null, "")]
            [InlineData(10, null, null, null, "10")]
            [InlineData(23, 42, null, null, "23-42")]
            [InlineData(23, 42, 5, null, "23:5-42")]
            [InlineData(23, 42, 5, 10, "23:5-42:10")]
            public void Should_Return_Correct_LineRange(
                int? startLine,
                int? endLine,
                int? startColumn,
                int? endColumn,
                string expectedLineRange)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile("foo.ch", startLine, endLine, startColumn, endColumn)
                        .Create();

                // When
                var result = issue.LineRange();

                // Then
                result.ShouldBe(expectedLineRange);
            }
        }

        public sealed class TheLineRangeExtensionWithAddColumnParameter
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.LineRange(false));

                // Then
                result.IsArgumentNullException("issue");
            }

            [Theory]
            [InlineData(null, null, null, null, true, "")]
            [InlineData(10, null, null, null, true, "10")]
            [InlineData(23, 42, null, null, true, "23-42")]
            [InlineData(23, 42, 5, null, true, "23:5-42")]
            [InlineData(23, 42, 5, 10, true, "23:5-42:10")]
            [InlineData(null, null, null, null, false, "")]
            [InlineData(10, null, null, null, false, "10")]
            [InlineData(23, 42, null, null, false, "23-42")]
            [InlineData(23, 42, 5, null, false, "23-42")]
            [InlineData(23, 42, 5, 10, false, "23-42")]
            public void Should_Return_Correct_LineRange(
                int? startLine,
                int? endLine,
                int? startColumn,
                int? endColumn,
                bool addColumnInformation,
                string expectedLineRange)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile("foo.ch", startLine, endLine, startColumn, endColumn)
                        .Create();

                // When
                var result = issue.LineRange(addColumnInformation);

                // Then
                result.ShouldBe(expectedLineRange);
            }
        }

        public sealed class TheProjectPathExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ProjectPath());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                const string projectPath = @"src\Cake.Issues\Cake.Issues.csproj";
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
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ProjectDirectory());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                const string filePath = @"src\Cake.Issues\Foo.cs";
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
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FilePath());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                const string filePath = @"src\Cake.Issues\Foo.cs";
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
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FileDirectory());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                const string filePath = @"src\Cake.Issues\Foo.cs";
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
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FileName());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                const string filePath = @"src\Cake.Issues\Foo.cs";
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

        public sealed class TheRuleExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                const IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.Rule());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_RuleName_If_Set()
            {
                // Given
                const string ruleId = "RuleId";
                const string ruleName = "RuleName";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule(ruleId, ruleName)
                        .Create();

                // When
                var result = issue.Rule();

                // Then
                result.ShouldBe(ruleName);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_Return_RuleId_If_RuleName_Not_Set(string ruleName)
            {
                // Given
                const string ruleId = "RuleId";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule(ruleId, ruleName)
                        .Create();

                // When
                var result = issue.Rule();

                // Then
                result.ShouldBe(ruleId);
            }
        }

        public sealed class TheReplaceIssuePatternExtension
        {
            [Fact]
            public void Should_Throw_If_Pattern_Is_Null()
            {
                // Given
                const string pattern = null;
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
                const string pattern = "foo";
                const IIssue issue = null;

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
            [InlineData("foo {Run} bar", "foo Run bar")]
            [InlineData("foo {Identifier} bar", "foo Identifier Foo bar")]
            [InlineData("foo {Priority} bar", "foo 400 bar")]
            [InlineData("foo {PriorityName} bar", "foo Error bar")]
            [InlineData("foo {ProjectPath} bar", "foo src/Cake.Issues/Cake.Issues.csproj bar")]
            [InlineData("foo {ProjectDirectory} bar", "foo src/Cake.Issues bar")]
            [InlineData("foo {ProjectName} bar", "foo Cake.Issues bar")]
            [InlineData("foo {FilePath} bar", "foo src/Cake.Issues/foo.cs bar")]
            [InlineData("foo {FileDirectory} bar", "foo src/Cake.Issues bar")]
            [InlineData("foo {FileName} bar", "foo foo.cs bar")]
            [InlineData("foo {Line} bar", "foo 42 bar")]
            [InlineData("foo {EndLine} bar", "foo 420 bar")]
            [InlineData("foo {Column} bar", "foo 23 bar")]
            [InlineData("foo {EndColumn} bar", "foo 230 bar")]
            [InlineData("foo {FileLink} bar", "foo https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12 bar")]
            [InlineData("foo {RuleId} bar", "foo Rule Foo bar")]
            [InlineData("foo {RuleUrl} bar", "foo https://google.com/ bar")]
            [InlineData("foo {MessageText} bar", "foo MessageText Foo bar")]
            [InlineData("foo {MessageHtml} bar", "foo MessageHtml Foo bar")]
            [InlineData("foo {MessageMarkdown} bar", "foo MessageMarkdown Foo bar")]
            [InlineData("foo {AdditionalInformation:cost} bar", "foo 1000 bar")]
            [InlineData("foo {AdditionalInformation:cost} {AdditionalInformation:notExistentKey} bar", "foo 1000 {AdditionalInformation:notExistentKey} bar")]
            public void Should_Replace_Tokens(string pattern, string expectedResult)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Identifier Foo", "MessageText Foo", "ProviderType Foo", "ProviderName Foo")
                        .ForRun("Run")
                        .WithMessageInHtmlFormat("MessageHtml Foo")
                        .WithMessageInMarkdownFormat("MessageMarkdown Foo")
                        .InFile(@"src/Cake.Issues/foo.cs", 42, 420, 23, 230)
                        .InProject(@"src/Cake.Issues/Cake.Issues.csproj", "Cake.Issues")
                        .WithFileLink(new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12"))
                        .OfRule("Rule Foo", new Uri("https://google.com"))
                        .WithPriority(IssuePriority.Error)
                        .WithAdditionalInformation("cost", "1000")
                        .Create();

                // When
                var result = pattern.ReplaceIssuePattern(issue);

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
