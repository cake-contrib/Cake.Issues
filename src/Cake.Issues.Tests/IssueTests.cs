namespace Cake.Issues.Tests;

using System.Runtime.InteropServices;

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
                const string identifier = null;
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = " ";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
            [InlineData("foo\0bar")]
            public void Should_Throw_If_Project_Path_Is_Invalid(string projectPath)
            {
                // Given
                const string identifier = "identifier";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
            [InlineData("/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string projectPath)
            {
                // Given
                const string identifier = "identifier";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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

            [SkippableTheory]
            [InlineData(@"c:\src\foo.cs")]
            public void Should_Throw_If_File_Path_Is_Absolute_Windows_Path(string projectPath)
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given
                const string identifier = "identifier";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = null;
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                var projectPath = string.Empty;
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = " ";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = null;
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                var projectName = string.Empty;
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = " ";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
            [InlineData("foo\0bar")]
            public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
            {
                // Given
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
            [InlineData("/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
            {
                // Given
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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

            [SkippableTheory]
            [InlineData(@"c:\src\foo.cs")]
            public void Should_Throw_If_File_Path_Is_Absolute_Windows_Path(string filePath)
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = null;
                int? line = null;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                const Uri fileLink = null;
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                var filePath = string.Empty;
                int? line = null;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                const Uri fileLink = null;
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = " ";
                int? line = null;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                const Uri fileLink = null;
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
            [InlineData("foo", "foo")]
            [InlineData(@"foo\bar", "foo/bar")]
            [InlineData("foo/bar", "foo/bar")]
            [InlineData(@"foo\bar\", "foo/bar")]
            [InlineData("foo/bar/", "foo/bar")]
            [InlineData(@".\foo", "foo")]
            [InlineData("./foo", "foo")]
            [InlineData(@"foo\..\bar", "foo/../bar")]
            [InlineData("foo/../bar", "foo/../bar")]
            public void Should_Set_File_Path(string filePath, string expectedFilePath)
            {
                // Given
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = -1;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 0;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = null;
                const int line = 10;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                Uri fileLink = null;
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                int? line = null;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                int? endLine = null;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = -1;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 0;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                int? line = null;
                const int endLine = 12;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 100;
                const int endLine = 12;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                int? endLine = null;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 10;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 1;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = -1;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 0;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                int? line = null;
                int? endLine = null;
                const int column = 50;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = -1;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 0;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                int? column = null;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 10;
                const int column = 50;
                const int endColumn = 5;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                int? column = null;
                int? endColumn = null;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 50;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
            public void Should_Handle_EndColumn_Which_Is_Smaller_Than_Column_If_EndLine_Is_Higher()
            {
                // Given
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 10;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 1;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                const Uri fileLink = null;
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = null;
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                var messageText = string.Empty;
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = " ";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = null;
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                var priorityName = string.Empty;
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = " ";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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

        public sealed class TheRuleIdArgument
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("rule")]
            public void Should_Set_RuleId(string ruleId)
            {
                // Given
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
                        fileLink,
                        messageText,
                        messageHtml,
                        messageMarkdown,
                        priority,
                        priorityName,
                        ruleId,
                        ruleName,
                        ruleUri,
                        run,
                        providerType,
                        providerName,
                        additionalInformation);

                // Then
                issue.RuleId.ShouldBe(ruleId);
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string ruleId = "Rule";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
                        fileLink,
                        messageText,
                        messageHtml,
                        messageMarkdown,
                        priority,
                        priorityName,
                        ruleId,
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                const Uri ruleUri = null;
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = null;
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                var providerType = string.Empty;
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = " ";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerName = "ProviderName";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = null;
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                var providerName = string.Empty;
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = " ";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string run = "Run";
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
                        null, // offset
                        null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
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
                    null, // offset
                    null, // endOffset
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
                const string identifier = "identifier";
                const string projectPath = @"src\foo.csproj";
                const string projectName = "foo";
                const string filePath = @"src\foo.cs";
                const int line = 10;
                const int endLine = 12;
                const int column = 50;
                const int endColumn = 55;
                var fileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
                const string messageText = "MessageText";
                const string messageHtml = "MessageHtml";
                const string messageMarkdown = "MessageMarkdown";
                const int priority = 1;
                const string priorityName = "Warning";
                const string rule = "Rule";
                const string ruleName = "Rule Name";
                var ruleUri = new Uri("https://google.com");
                const string providerType = "ProviderType";
                const string providerName = "ProviderName";
                const string run = "Run";
                const IReadOnlyDictionary<string, string> additionalInformation = null;

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
                    null, // offset
                    null, // endOffset
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
                var value = issue.AdditionalInformation.ShouldNotBeNull();
                value.ShouldBeEmpty();
            }
        }
    }
}
