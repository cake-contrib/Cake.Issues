namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cake.Issues.Testing;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Follows name of the class which is tested")]
    public sealed class IIssueExtensionsTests
    {
        public sealed class TheGetExpandoObjectExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.GetExpandoObject());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Set_ProviderType_If_Flag_Is_Set()
            {
                // Given
                var providerType = "ProviderType Foo";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", providerType, "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addProviderType: true);

                // Then
                Assert.Equal(result.ProviderType, providerType);
            }

            [Fact]
            public void Should_Not_Set_ProviderType_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addProviderType: false);
                var result = Record.Exception(() => expando.ProviderType);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'ProviderType'");
            }

            [Fact]
            public void Should_Set_ProviderName_If_Flag_Is_Set()
            {
                // Given
                var providerName = "ProviderName Foo";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", providerName)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addProviderName: true);

                // Then
                Assert.Equal(result.ProviderName, providerName);
            }

            [Fact]
            public void Should_Not_Set_ProviderName_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addProviderName: false);
                var result = Record.Exception(() => expando.ProviderName);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'ProviderName'");
            }

            [Fact]
            public void Should_Set_Priority_If_Flag_Is_Set()
            {
                // Given
                var priority = IssuePriority.Error;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .WithPriority(priority)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addPriority: true);

                // Then
                Assert.Equal(result.Priority, (int)priority);
            }

            [Fact]
            public void Should_Not_Set_Priority_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addPriority: false);
                var result = Record.Exception(() => expando.Priority);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'Priority'");
            }

            [Fact]
            public void Should_Set_PriorityName_If_Flag_Is_Set()
            {
                // Given
                var priorityName = "Foo";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .WithPriority(0, priorityName)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addPriorityName: true);

                // Then
                Assert.Equal(result.PriorityName, priorityName);
            }

            [Fact]
            public void Should_Not_Set_PriorityName_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addPriorityName: false);
                var result = Record.Exception(() => expando.PriorityName);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'PriorityName'");
            }

            [Fact]
            public void Should_Set_ProjectPath_If_Flag_Is_Set()
            {
                // Given
                var projectPath = @"src\Cake.Issues.Reporting.Generic.Tests\Cake.Issues.Reporting.Generic.Tests.csproj";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InProjectFile(projectPath)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addProjectPath: true);

                // Then
                Assert.Equal(result.ProjectPath, @"src/Cake.Issues.Reporting.Generic.Tests/Cake.Issues.Reporting.Generic.Tests.csproj");
            }

            [Fact]
            public void Should_Not_Set_ProjectPath_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addProjectPath: false);
                var result = Record.Exception(() => expando.ProjectPath);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'ProjectPath'");
            }

            [Fact]
            public void Should_Set_ProjectName_If_Flag_Is_Set()
            {
                // Given
                var projectName = "Cake.Issues.Reporting.Generic.Tests";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InProjectOfName(projectName)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addProjectName: true);

                // Then
                Assert.Equal(result.ProjectName, projectName);
            }

            [Fact]
            public void Should_Not_Set_ProjectName_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addProjectName: false);
                var result = Record.Exception(() => expando.ProjectName);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'ProjectName'");
            }

            [Fact]
            public void Should_Set_FilePath_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addFilePath: true);

                // Then
                Assert.Equal(result.FilePath, "src/Cake.Issues.Reporting.Generic.Tests/Foo.cs");
            }

            [Fact]
            public void Should_Not_Set_FilePath_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addFilePath: false);
                var result = Record.Exception(() => expando.FilePath);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'FilePath'");
            }

            [Fact]
            public void Should_Set_FileDirectory_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addFileDirectory: true);

                // Then
                Assert.Equal(result.FileDirectory, "src/Cake.Issues.Reporting.Generic.Tests");
            }

            [Fact]
            public void Should_Not_Set_FileDirectory_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addFileDirectory: false);
                var result = Record.Exception(() => expando.FileDirectory);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'FileDirectory'");
            }

            [Fact]
            public void Should_Set_FileName_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addFileName: true);

                // Then
                Assert.Equal(result.FileName, "Foo.cs");
            }

            [Fact]
            public void Should_Not_Set_FileName_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addFileName: false);
                var result = Record.Exception(() => expando.FileName);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'FileName'");
            }

            [Fact]
            public void Should_Set_FileLink_If_Flag_Is_Set()
            {
                // Given
                var fileLink = new Uri("https://github.com");
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .WithFileLink(fileLink)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addFileLink: true);

                // Then
                Assert.Equal(result.FileLink.ToString(), fileLink.ToString());
            }

            [Fact]
            public void Should_Not_Set_FileLink_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addFileLink: false);
                var result = Record.Exception(() => expando.FileLink);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'FileLink'");
            }

            [Fact]
            public void Should_Set_Line_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var line = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath, 42)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addLine: true);

                // Then
                Assert.Equal(result.Line, line);
            }

            [Fact]
            public void Should_Not_Set_Line_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addLine: false);
                var result = Record.Exception(() => expando.Line);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'Line'");
            }

            [Fact]
            public void Should_Set_EndLine_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var line = 23;
                var endLine = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath, line, endLine, null, null)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addEndLine: true);

                // Then
                Assert.Equal(result.EndLine, endLine);
            }

            [Fact]
            public void Should_Not_Set_EndLine_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addEndLine: false);
                var result = Record.Exception(() => expando.EndLine);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'EndLine'");
            }

            [Fact]
            public void Should_Set_Column_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var line = 23;
                var endLine = 42;
                var column = 5;
                var endColumn = 10;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath, line, endLine, column, endColumn)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addColumn: true);

                // Then
                Assert.Equal(result.Column, column);
            }

            [Fact]
            public void Should_Not_Set_Column_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addColumn: false);
                var result = Record.Exception(() => expando.Column);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'Column'");
            }

            [Fact]
            public void Should_Set_EndColumn_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var line = 23;
                var endLine = 42;
                var column = 5;
                var endColumn = 10;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath, line, endLine, column, endColumn)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addEndColumn: true);

                // Then
                Assert.Equal(result.EndColumn, endColumn);
            }

            [Fact]
            public void Should_Not_Set_EndColumn_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addEndColumn: false);
                var result = Record.Exception(() => expando.EndColumn);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'EndColumn'");
            }

            [Fact]
            public void Should_Set_Location_If_Flag_Is_Set()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var line = 23;
                var endLine = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath, line, endLine, null, null)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addLocation: true);

                // Then
                Assert.Equal(result.Location, $"{line}-{endLine}");
            }

            [Fact]
            public void Should_Not_Set_Location_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addLocation: false);
                var result = Record.Exception(() => expando.Location);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'Location'");
            }

            [Fact]
            public void Should_Set_Rule_If_Flag_Is_Set()
            {
                // Given
                var rule = "foo";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule(rule)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addRule: true);

                // Then
                Assert.Equal(result.Rule, rule);
            }

            [Fact]
            public void Should_Not_Set_Rule_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addRule: false);
                var result = Record.Exception(() => expando.Rule);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'Rule'");
            }

            [Fact]
            public void Should_Set_RuleUrl_If_Flag_Is_Set()
            {
                // Given
                var rule = "foo";
                var ruleUrl = new Uri("https://google.com");
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule(rule, ruleUrl)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addRuleUrl: true);

                // Then
                Assert.Equal(result.RuleUrl.ToString(), ruleUrl.ToString());
            }

            [Fact]
            public void Should_Not_Set_RuleUrl_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addRuleUrl: false);
                var result = Record.Exception(() => expando.RuleUrl);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'RuleUrl'");
            }

            [Fact]
            public void Should_Set_MessageText_If_Flag_Is_Set()
            {
                // Given
                var message = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(message, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageText: true);

                // Then
                Assert.Equal(result.MessageText, message);
            }

            [Fact]
            public void Should_Not_Set_MessageText_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addMessageText: false);
                var result = Record.Exception(() => expando.MessageText);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'MessageText'");
            }

            [Fact]
            public void Should_Set_MessageHtml_If_Flag_Is_Set()
            {
                // Given
                var messageHtml = "Message Foo HTML";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .WithMessageInHtmlFormat(messageHtml)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageHtml: true);

                // Then
                Assert.Equal(result.MessageHtml, messageHtml);
            }

            [Fact]
            public void Should_Not_Set_MessageHtml_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addMessageHtml: false);
                var result = Record.Exception(() => expando.MessageHtml);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'MessageHtml'");
            }

            [Fact]
            public void Should_Fallback_To_MessageText_If_MessageHtml_Is_Not_Available()
            {
                // Given
                var messageText = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageHtml: true);

                // Then
                Assert.Equal(result.MessageHtml, messageText);
            }

            [Fact]
            public void Should_Fallback_To_MessageText_If_MessageHtml_Is_Not_Available_And_Flag_Is_Set()
            {
                // Given
                var messageText = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageHtml: true, fallbackToTextMessageIfHtmlMessageNotAvailable: true);

                // Then
                Assert.Equal(result.MessageHtml, messageText);
            }

            [Fact]
            public void Should_Not_Fallback_To_MessageText_If_MessageHtml_Is_Not_Available_And_Flag_Is_Not_Set()
            {
                // Given
                var messageText = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageHtml: true, fallbackToTextMessageIfHtmlMessageNotAvailable: false);

                // Then
                Assert.Equal(result.MessageHtml, null);
            }

            [Fact]
            public void Should_Set_MessageMarkdown_If_Flag_Is_Set()
            {
                // Given
                var messageMarkdown = "Message Foo Markdown";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .WithMessageInMarkdownFormat(messageMarkdown)
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageMarkdown: true);

                // Then
                Assert.Equal(result.MessageMarkdown, messageMarkdown);
            }

            [Fact]
            public void Should_Not_Set_MessageMarkdown_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addMessageMarkdown: false);
                var result = Record.Exception(() => expando.MessageMarkdown);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'MessageMarkdown'");
            }

            [Fact]
            public void Should_Fallback_To_MessageText_If_MessageMarkdown_Is_Not_Available()
            {
                // Given
                var messageText = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageMarkdown: true);

                // Then
                Assert.Equal(result.MessageMarkdown, messageText);
            }

            [Fact]
            public void Should_Fallback_To_MessageText_If_MessageMarkdown_Is_Not_Available_And_Flag_Is_Set()
            {
                // Given
                var messageText = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageMarkdown: true, fallbackToTextMessageIfMarkdownMessageNotAvailable: true);

                // Then
                Assert.Equal(result.MessageMarkdown, messageText);
            }

            [Fact]
            public void Should_Not_Fallback_To_MessageText_If_MessageMarkdown_Is_Not_Available_And_Flag_Is_Not_Set()
            {
                // Given
                var messageText = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessageMarkdown: true, fallbackToTextMessageIfMarkdownMessageNotAvailable: false);

                // Then
                Assert.Equal(result.MessageMarkdown, null);
            }
        }
    }
}
