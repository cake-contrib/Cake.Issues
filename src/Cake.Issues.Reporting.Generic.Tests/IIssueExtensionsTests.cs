namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Xunit;

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
            public void Should_Set_Message_If_Flag_Is_Set()
            {
                // Given
                var message = "Message Foo";
                var issue =
                    IssueBuilder
                        .NewIssue(message, "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic result = issue.GetExpandoObject(addMessage: true);

                // Then
                Assert.Equal(result.Message, message);
            }

            [Fact]
            public void Should_Not_Set_Message_If_Flag_Is_Not_Set()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .Create();

                // When
                dynamic expando = issue.GetExpandoObject(addMessage: false);
                var result = Record.Exception(() => expando.Message);

                // Then
                result.IsRuntimeBinderException("'System.Dynamic.ExpandoObject' does not contain a definition for 'Message'");
            }
        }
    }
}
