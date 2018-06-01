namespace Cake.Issues.Tests
{
    using System;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class IssueTests
    {
        public sealed class TheCtor
        {
            public sealed class TheProjectArgument
            {
                [Fact]
                public void Should_Handle_Projects_Which_Are_Null()
                {
                    // Given
                    string project = null;
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Project.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_Projects_Which_Are_Empty()
                {
                    // Given
                    var project = string.Empty;
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Project.ShouldBe(string.Empty);
                }

                [Fact]
                public void Should_Handle_Projects_Which_Are_WhiteSpace()
                {
                    // Given
                    var project = " ";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Project.ShouldBe(" ");
                }

                [Theory]
                [InlineData("project")]
                public void Should_Set_Project(string project)
                {
                    // Given
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Project.ShouldBe(project);
                }
            }

            public sealed class TheFilePathArgument
            {
                [Theory]
                [InlineData("foo\tbar")]
                public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
                {
                    // Given
                    var project = "Project";
                    var line = 100;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentException("filePath");
                }

                [Theory]
                [InlineData(@"c:\src\foo.cs")]
                [InlineData(@"/foo")]
                [InlineData(@"\foo")]
                public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
                {
                    // Given
                    var project = "Project";
                    var line = 100;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("filePath");
                }

                [Fact]
                public void Should_Handle_File_Paths_Which_Are_Null()
                {
                    // Given
                    var project = "Project";
                    string filePath = null;
                    int? line = null;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.AffectedFileRelativePath.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_File_Paths_Which_Are_Empty()
                {
                    // Given
                    var project = "Project";
                    var filePath = string.Empty;
                    int? line = null;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.AffectedFileRelativePath.ShouldBe(null);
                }

                [Fact]
                public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
                {
                    // Given
                    var project = "Project";
                    var filePath = " ";
                    int? line = null;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

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
                    var project = "Project";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

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
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = -1;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("line");
                }

                [Fact]
                public void Should_Throw_If_Line_Is_Zero()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 0;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("line");
                }

                [Fact]
                public void Should_Throw_If_Line_Is_Set_But_No_File()
                {
                    // Given
                    var project = "Project";
                    string filePath = null;
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("line");
                }

                [Theory]
                [InlineData(null)]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_Line(int? line)
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Line.ShouldBe(line);
                }
            }

            public sealed class TheMessageArgument
            {
                [Fact]
                public void Should_Throw_If_Message_Is_Null()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    string message = null;
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentNullException("message");
                }

                [Fact]
                public void Should_Throw_If_Message_Is_Empty()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = string.Empty;
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("message");
                }

                [Fact]
                public void Should_Throw_If_Message_Is_WhiteSpace()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = " ";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("message");
                }

                [Theory]
                [InlineData("message")]
                public void Should_Set_Message(string message)
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Message.ShouldBe(message);
                }
            }

            public sealed class ThePriorityArgument
            {
                [Theory]
                [InlineData(int.MinValue)]
                [InlineData(-1)]
                [InlineData(0)]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void Should_Set_Priority(int priority)
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Priority.ShouldBe(priority);
                }
            }

            public sealed class ThePriorityNameArgument
            {
                [Fact]
                public void Should_Throw_If_Priority_Name_Is_Null()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    string priorityName = null;
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentNullException("priorityName");
                }

                [Fact]
                public void Should_Throw_If_Priority_Name_Is_Empty()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = string.Empty;
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("priorityName");
                }

                [Fact]
                public void Should_Throw_If_Priority_Name_Is_WhiteSpace()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = " ";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("priorityName");
                }

                [Theory]
                [InlineData("Warning")]
                public void Should_Set_Priority_Name(string priorityName)
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

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
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.Rule.ShouldBe(rule);
                }
            }

            public sealed class TheRuleUrlArgument
            {
                [Fact]
                public void Should_Set_Rule_Url()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.RuleUrl.ShouldBe(ruleUri);
                }

                [Fact]
                public void Should_Set_Rule_Url_If_Null()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    Uri ruleUri = null;
                    var providerType = "ProviderType";
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.RuleUrl.ShouldBe(ruleUri);
                }
            }

            public sealed class TheProviderTypeArgument
            {
                [Fact]
                public void Should_Throw_If_Provider_Type_Is_Null()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    string providerType = null;
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentNullException("providerType");
                }

                [Fact]
                public void Should_Throw_If_Provider_Type_Is_Empty()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = string.Empty;
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("providerType");
                }

                [Fact]
                public void Should_Throw_If_Provider_Type_Is_WhiteSpace()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = " ";
                    var providerName = "ProviderName";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("providerType");
                }

                [Theory]
                [InlineData("foo")]
                public void Should_Set_ProviderType(string providerType)
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerName = "ProviderName";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

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
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    string providerName = null;

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentNullException("providerName");
                }

                [Fact]
                public void Should_Throw_If_Provider_Name_Is_Empty()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = string.Empty;

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("providerName");
                }

                [Fact]
                public void Should_Throw_If_Provider_Name_Is_WhiteSpace()
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";
                    var providerName = " ";

                    // When
                    var result = Record.Exception(() =>
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName));

                    // Then
                    result.IsArgumentOutOfRangeException("providerName");
                }

                [Theory]
                [InlineData("providerName")]
                public void Should_Set_ProviderName(string providerName)
                {
                    // Given
                    var project = "Project";
                    var filePath = @"src\foo.cs";
                    var line = 10;
                    var message = "Message";
                    var priority = 1;
                    var priorityName = "Warning";
                    var rule = "Rule";
                    var ruleUri = new Uri("https://google.com");
                    var providerType = "ProviderType";

                    // When
                    var issue =
                        new Issue(
                            project,
                            filePath,
                            line,
                            message,
                            priority,
                            priorityName,
                            rule,
                            ruleUri,
                            providerType,
                            providerName);

                    // Then
                    issue.ProviderName.ShouldBe(providerName);
                }
            }
        }
    }
}
