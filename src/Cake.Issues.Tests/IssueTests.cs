namespace Cake.Issues.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueTests
    {
        public sealed class TheCtor
        {
            public sealed class TheIdentifierArgument
            {
                [Fact]
                public void Should_Throw_If_Identifier_Is_Null()
                {
                    // Given
                    string identifier = null;
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentNullException("identifier");
                }

                [Fact]
                public void Should_Throw_If_Identifier_Is_Empty()
                {
                    // Given
                    var identifier = string.Empty;
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("identifier");
                }

                [Fact]
                public void Should_Throw_If_Identifier_Is_WhiteSpace()
                {
                    // Given
                    var identifier = " ";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("identifier");
                }

                [Theory]
                [InlineData("identifier")]
                public void Should_Set_Identifier(string identifier)
                {
                    // Given
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Identifier.ShouldBe(identifier);
                }
            }

            public sealed class TheProjectFileRelativePathArgument
            {
                [Theory]
                [InlineData("foo\tbar")]
                public void Should_Throw_If_Project_Path_Is_Invalid(string projectPath)
                {
                    // Given
                    var identifier = "identifier";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentException("projectFileRelativePath");
                }

                [Theory]
                [InlineData(@"c:\src\foo.cs")]
                [InlineData(@"/foo")]
                [InlineData(@"\foo")]
                public void Should_Throw_If_File_Path_Is_Absolute(string projectPath)
                {
                    // Given
                    var identifier = "identifier";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("projectFileRelativePath");
                }

                [Fact]
                public void Should_Handle_Project_Paths_Which_Are_Null()
                {
                    // Given
                    var identifier = "identifier";
                    string projectPath = null;
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectFileRelativePath.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_Project_Paths_Which_Are_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = string.Empty;
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectFileRelativePath.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_Project_Paths_Which_Are_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = " ";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectFileRelativePath.ShouldBe(null);
                }

                [Theory]
                [InlineData("project")]
                public void Should_Set_ProjectFileRelativePath(string projectPath)
                {
                    // Given
                    var identifier = "identifier";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectFileRelativePath.ToString().ShouldBe(projectPath);
                }
            }

            public sealed class TheProjectNameArgument
            {
                [Fact]
                public void Should_Handle_Projects_Which_Are_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    string projectName = null;
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectName.ShouldBe(projectName);
                }

                [Fact]
                public void Should_Handle_Projects_Which_Are_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = string.Empty;
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectName.ShouldBe(projectName);
                }

                [Fact]
                public void Should_Handle_Projects_Which_Are_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = " ";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectName.ShouldBe(projectName);
                }

                [Theory]
                [InlineData("project")]
                public void Should_Set_ProjectName(string projectName)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProjectName.ShouldBe(projectName);
                }
            }

            public sealed class TheAffectedFileRelativePathArgument
            {
                [Theory]
                [InlineData("foo\tbar")]
                public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentException("affectedFileRelativePath");
                }

                [Theory]
                [InlineData(@"c:\src\foo.cs")]
                [InlineData(@"/foo")]
                [InlineData(@"\foo")]
                public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("affectedFileRelativePath");
                }

                [Fact]
                public void Should_Handle_File_Paths_Which_Are_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    string filePath = null;
                    int? line = null;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    Uri fileLink = null;
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.AffectedFileRelativePath.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_File_Paths_Which_Are_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = string.Empty;
                    int? line = null;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    Uri fileLink = null;
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.AffectedFileRelativePath.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = " ";
                    int? line = null;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    Uri fileLink = null;
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.AffectedFileRelativePath.ShouldBe(null);
                }

                [Theory]
                [InlineData(@"foo", @"foo")]
                [InlineData(@"foo\bar", @"foo/bar")]
                [InlineData(@"foo/bar", @"foo/bar")]
                [InlineData(@"foo\bar\", @"foo/bar")]
                [InlineData(@"foo/bar/", @"foo/bar")]
                [InlineData(@".\foo", @"foo")]
                [InlineData(@"./foo", @"foo")]
                [InlineData(@"foo\..\bar", @"foo/../bar")]
                [InlineData(@"foo/../bar", @"foo/../bar")]
                public void Should_Set_File_Path(string filePath, string expectedFilePath)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                    issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
                }
            }

            public sealed class TheLineArgument
            {
                [Fact]
                public void Should_Throw_If_Line_Is_Negative()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = -1;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("line");
                }

                [Fact]
                public void Should_Throw_If_Line_Is_Zero()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 0;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("line");
                }

                [Fact]
                public void Should_Throw_If_Line_Is_Set_But_No_File()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    string filePath = null;
                    var line = 10;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    Uri fileLink = null;
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("line");
                }

                [Fact]
                public void Should_Handle_Line_Which_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    int? line = null;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Line.ShouldBe(line);
                }

                [Theory]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_Line(int line)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    int? endLine = null;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Line.ShouldBe(line);
                }
            }

            public sealed class TheEndLineArgument
            {
                [Fact]
                public void Should_Throw_If_EndLine_Is_Negative()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = -1;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endLine");
                }

                [Fact]
                public void Should_Throw_If_EndLine_Is_Zero()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 0;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endLine");
                }

                [Fact]
                public void Should_Throw_If_EndLine_Is_Set_But_No_Line()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    int? line = null;
                    var endLine = 12;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endLine");
                }

                [Fact]
                public void Should_Throw_If_EndLine_Is_Smaller_Line()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 100;
                    var endLine = 12;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endLine");
                }

                [Fact]
                public void Should_Handle_EndLine_Which_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    int? endLine = null;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.EndLine.ShouldBe(endLine);
                }

                [Fact]
                public void Should_Handle_EndLine_Which_Is_Equals_Line()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 10;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.EndLine.ShouldBe(endLine);
                }

                [Theory]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_EndLine(int endLine)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 1;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.EndLine.ShouldBe(endLine);
                }
            }

            public sealed class TheColumnArgument
            {
                [Fact]
                public void Should_Throw_If_Column_Is_Negative()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = -1;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("column");
                }

                [Fact]
                public void Should_Throw_If_Column_Is_Zero()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 0;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("column");
                }

                [Fact]
                public void Should_Throw_If_Column_Is_Set_But_No_Line()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    int? line = null;
                    int? endLine = null;
                    var column = 50;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("column");
                }

                [Fact]
                public void Should_Handle_Column_Which_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Column.ShouldBe(column);
                }

                [Theory]
                [InlineData(null)]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_Column(int? column)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Column.ShouldBe(column);
                }
            }

            public sealed class TheEndColumnArgument
            {
                [Fact]
                public void Should_Throw_If_EndColumn_Is_Negative()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = -1;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endColumn");
                }

                [Fact]
                public void Should_Throw_If_EndColumn_Is_Zero()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 0;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endColumn");
                }

                [Fact]
                public void Should_Throw_If_EndColumn_Is_Set_But_No_Column()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    int? column = null;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endColumn");
                }

                [Fact]
                public void Should_Throw_If_EndColumn_Is_Smaller_Column()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 5;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("endColumn");
                }

                [Fact]
                public void Should_Handle_EndColumn_Which_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    int? column = null;
                    int? endColumn = null;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.EndColumn.ShouldBe(endColumn);
                }

                [Fact]
                public void Should_Handle_EndColumn_Which_Is_Equals_Column()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 50;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.EndColumn.ShouldBe(endColumn);
                }

                [Theory]
                [InlineData(null)]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_EndColumn(int? endColumn)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 1;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.EndColumn.ShouldBe(endColumn);
                }
            }

            public sealed class TheFileLinkArgument
            {
                [Fact]
                public void Should_Set_FileLink()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.FileLink.ShouldBe(fileLink);
                }

                [Fact]
                public void Should_Set_FileLink_If_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    Uri fileLink = null;
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.FileLink.ShouldBe(fileLink);
                }
            }

            public sealed class TheMessageTextArgument
            {
                [Fact]
                public void Should_Throw_If_MessageText_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    string messageText = null;
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentNullException("messageText");
                }

                [Fact]
                public void Should_Throw_If_MessageText_Is_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = string.Empty;
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("messageText");
                }

                [Fact]
                public void Should_Throw_If_MessageText_Is_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = " ";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("messageText");
                }

                [Theory]
                [InlineData("messageText")]
                public void Should_Set_MessageText(string messageText)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.MessageText.ShouldBe(messageText);
                }
            }

            public sealed class TheMessageHtmlArgument
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                [InlineData("messageHtml")]
                public void Should_Set_MessageHtml(string messageHtml)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.MessageHtml.ShouldBe(messageHtml);
                }
            }

            public sealed class TheMessageMarkdownArgument
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                [InlineData("messageMarkdown")]
                public void Should_Set_MessageHtml(string messageMarkdown)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.MessageMarkdown.ShouldBe(messageMarkdown);
                }
            }

            public sealed class ThePriorityArgument
            {
                [Theory]
                [InlineData(null)]
                [InlineData(int.MinValue)]
                [InlineData(-1)]
                [InlineData(0)]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_Priority(int? priority)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Priority.ShouldBe(priority);
                }
            }

            public sealed class ThePriorityNameArgument
            {
                [Fact]
                public void Should_Handle_PriorityNames_Which_Are_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    string priorityName = null;
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.PriorityName.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_PriorityNames_Which_Are_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = string.Empty;
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.PriorityName.ShouldBe(string.Empty);
                }

                [Fact]
                public void Should_Handle_PriorityNames_Which_Are_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = " ";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.PriorityName.ShouldBe(" ");
                }

                [Theory]
                [InlineData("Warning")]
                public void Should_Set_Priority_Name(string priorityName)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.PriorityName.ShouldBe(priorityName);
                }
            }

            public sealed class TheRuleArgument
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData("rule")]
                public void Should_Set_Rule(string rule)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Rule.ShouldBe(rule);
                }
            }

            public sealed class TheRuleNameArgument
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData("ruleName")]
                public void Should_Set_RuleName(string ruleName)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.RuleName.ShouldBe(ruleName);
                }
            }

            public sealed class TheRuleUrlArgument
            {
                [Fact]
                public void Should_Set_Rule_Url()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.RuleUrl.ShouldBe(ruleUri);
                }

                [Fact]
                public void Should_Set_Rule_Url_If_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    Uri ruleUri = null;
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.RuleUrl.ShouldBe(ruleUri);
                }
            }

            public sealed class TheRunArgument
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData("run")]
                public void Should_Set_Run(string run)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.Run.ShouldBe(run);
                }
            }

            public sealed class TheProviderTypeArgument
            {
                [Fact]
                public void Should_Throw_If_Provider_Type_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    string providerType = null;
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentNullException("providerType");
                }

                [Fact]
                public void Should_Throw_If_Provider_Type_Is_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = string.Empty;
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("providerType");
                }

                [Fact]
                public void Should_Throw_If_Provider_Type_Is_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = " ";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("providerType");
                }

                [Theory]
                [InlineData("foo")]
                public void Should_Set_ProviderType(string providerType)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProviderType.ShouldBe(providerType);
                }
            }

            public sealed class TheProviderNameArgument
            {
                [Fact]
                public void Should_Throw_If_Provider_Name_Is_Null()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    string providerName = null;
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentNullException("providerName");
                }

                [Fact]
                public void Should_Throw_If_Provider_Name_Is_Empty()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = string.Empty;
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("providerName");
                }

                [Fact]
                public void Should_Throw_If_Provider_Name_Is_WhiteSpace()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = " ";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation));

                    // Then
                    result.IsArgumentOutOfRangeException("providerName");
                }

                [Theory]
                [InlineData("providerName")]
                public void Should_Set_ProviderName(string providerName)
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var run = "Run";
                    var additionalInformation = new Dictionary<string, string>();

                    // When
                    var issue =
                        new Issue(
                            identifier,
                            projectPath,
                            projectName,
                            filePath,
                            line,
                            endLine,
                            column,
                            endColumn,
                            fileLink,
                            messageText,
                            messageHtml,
                            messageMarkdown,
                            priority,
                            priorityName,
                            rule,
                            ruleName,
                            ruleUri,
                            run,
                            providerType,
                            providerName,
                            additionalInformation);

                    // Then
                    issue.ProviderName.ShouldBe(providerName);
                }
            }

            public sealed class TheAdditionalInformationArgument
            {
                [Fact]
                public void Should_Set_AdditionalInformation()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    var additionalInformation =
                        new Dictionary<string, string>
                        {
                            {
                                "FirstName",
                                "Larry"
                            },
                            {
                                "LastName",
                                "Fischer"
                            },
                        };

                    // When
                    var issue = new Issue(
                        identifier,
                        projectPath,
                        projectName,
                        filePath,
                        line,
                        endLine,
                        column,
                        endColumn,
                        fileLink,
                        messageText,
                        messageHtml,
                        messageMarkdown,
                        priority,
                        priorityName,
                        rule,
                        ruleName,
                        ruleUri,
                        run,
                        providerType,
                        providerName,
                        additionalInformation);

                    // Then
                    issue.AdditionalInformation.ShouldContain(new KeyValuePair<string, string>("FirstName", "Larry"));
                    issue.AdditionalInformation.ShouldContain(new KeyValuePair<string, string>("LastName", "Fischer"));
                }

                [Fact]
                public void Should_Set_AdditionalInformation_To_Empty_Dictionary()
                {
                    // Given
                    var identifier = "identifier";
                    var projectPath = @"src\foo.csproj";
                    var projectName = "foo";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var endLine = 12;
                    var column = 50;
                    var endColumn = 55;
                    var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                    var messageText = "MessageText";
                    var messageHtml = "MessageHtml";
                    var messageMarkdown = "MessageMarkdown";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleName = "Rule Name";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";
                    var run = "Run";
                    IReadOnlyDictionary<string, string> additionalInformation = null;

                    // When
                    var issue = new Issue(
                        identifier,
                        projectPath,
                        projectName,
                        filePath,
                        line,
                        endLine,
                        column,
                        endColumn,
                        fileLink,
                        messageText,
                        messageHtml,
                        messageMarkdown,
                        priority,
                        priorityName,
                        rule,
                        ruleName,
                        ruleUri,
                        run,
                        providerType,
                        providerName,
                        additionalInformation);

                    // Then
                    issue.AdditionalInformation.ShouldNotBeNull();
                    issue.AdditionalInformation.ShouldBeEmpty();
                }
            }
        }
    }
}
