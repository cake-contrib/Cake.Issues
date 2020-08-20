﻿namespace Cake.Issues.Tests.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Issues.Serialization;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueSerializationExtensionsTests
    {
        public sealed class TheSerializeToJsonStringExtensionForASingleIssue
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.SerializeToJsonString());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Identifier_After_Roundtrip()
            {
                // Given
                var identifier = "identifier";
                var issue =
                    IssueBuilder
                        .NewIssue(identifier, "message", "providerType", "providerName")
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.Identifier.ShouldBe(identifier);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageText_After_Roundtrip()
            {
                // Given
                var message = "message";
                var issue =
                    IssueBuilder
                        .NewIssue(message, "providerType", "providerName")
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.MessageText.ShouldBe(message);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageMarkdown_After_Roundtrip()
            {
                // Given
                var messageMarkdown = "messageMarkdown";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(messageMarkdown)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.MessageMarkdown.ShouldBe(messageMarkdown);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageHtml_After_Roundtrip()
            {
                // Given
                var messageHtml = "messageHtml";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(messageHtml)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.MessageHtml.ShouldBe(messageHtml);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderType_After_Roundtrip()
            {
                // Given
                var providerType = "providerType";
                var issue =
                    IssueBuilder
                        .NewIssue("message", providerType, "providerName")
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.ProviderType.ShouldBe(providerType);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderName_After_Roundtrip()
            {
                // Given
                var providerName = "providerName";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", providerName)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.ProviderName.ShouldBe(providerName);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Run_After_Roundtrip()
            {
                // Given
                var run = "run";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .ForRun(run)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.Run.ShouldBe(run);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectFileRelativePath_After_Roundtrip()
            {
                // Given
                var projectFileRelativePath = @"src/myproj.file";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(projectFileRelativePath)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.ProjectFileRelativePath.FullPath.ShouldBe(projectFileRelativePath);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectName_After_Roundtrip()
            {
                // Given
                var projectName = "projectName";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_AffectedFileRelativePath_After_Roundtrip()
            {
                // Given
                var affectedFileRelativePath = @"src/foo.bar";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(affectedFileRelativePath)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.AffectedFileRelativePath.FullPath.ShouldBe(affectedFileRelativePath);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Line_After_Roundtrip()
            {
                // Given
                var line = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", line)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.Line.ShouldBe(line);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndLine_After_Roundtrip()
            {
                // Given
                var endLine = 420;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", 42, endLine, null, null)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.EndLine.ShouldBe(endLine);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Column_After_Roundtrip()
            {
                // Given
                var column = 23;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", 42, column)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.Column.ShouldBe(column);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndColumn_After_Roundtrip()
            {
                // Given
                var endColumn = 230;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", 42, 420, 23, endColumn)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.EndColumn.ShouldBe(endColumn);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_FileLink_After_Roundtrip()
            {
                // Given
                var fileLink = "https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithFileLink(new Uri(fileLink))
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.FileLink.ToString().ShouldBe(fileLink);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Priority_After_Roundtrip()
            {
                // Given
                var priority = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority, "priorityName")
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.Priority.ShouldBe(priority);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_PriorityName_After_Roundtrip()
            {
                // Given
                var priorityName = "priorityName";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.PriorityName.ShouldBe(priorityName);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Rule_After_Roundtrip()
            {
                // Given
                var rule = "rule";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_RuleUrl_After_Roundtrip()
            {
                // Given
                var ruleUrl = new Uri("https://rule.url");
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule("rule", ruleUrl)
                        .Create();

                // When
                var result = issue.SerializeToJsonString().DeserializeToIssue();

                // Then
                result.RuleUrl.ToString().ShouldBe(ruleUrl.ToString());
            }
        }

        public sealed class TheSerializeToJsonStringExtensionForAnEnumerableOfIssues
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IEnumerable<IIssue> issues = null;

                // When
                var result = Record.Exception(() => issues.SerializeToJsonString());

                // Then
                result.IsArgumentNullException("issues");
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Identifier_After_Roundtrip()
            {
                // Given
                var identifier1 = "identifier1";
                var identifier2 = "identifier2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue(identifier1, "messageText1", "providerType1", "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue(identifier2, "messageText2", "providerType2", "providerName2")
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().Identifier.ShouldBe(identifier1);
                result.Last().Identifier.ShouldBe(identifier2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageText_After_Roundtrip()
            {
                // Given
                var messageText1 = "messageText1";
                var messageText2 = "messageText2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue(messageText1, "providerType1", "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue(messageText2, "providerType2", "providerName2")
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().MessageText.ShouldBe(messageText1);
                result.Last().MessageText.ShouldBe(messageText2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageMarkdown_After_Roundtrip()
            {
                // Given
                var messageMarkdown1 = "messageMarkdown1";
                var messageMarkdown2 = "messageMarkdown2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("messageText1", "providerType1", "providerName1")
                            .WithMessageInMarkdownFormat(messageMarkdown1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("messageText2", "providerType2", "providerName2")
                            .WithMessageInMarkdownFormat(messageMarkdown2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().MessageMarkdown.ShouldBe(messageMarkdown1);
                result.Last().MessageMarkdown.ShouldBe(messageMarkdown2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageHtml_After_Roundtrip()
            {
                // Given
                var messageHtml1 = "messageHtml1";
                var messageHtml2 = "messageHtml2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("messageText1", "providerType1", "providerName1")
                            .WithMessageInHtmlFormat(messageHtml1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("messageText2", "providerType2", "providerName2")
                            .WithMessageInHtmlFormat(messageHtml2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().MessageHtml.ShouldBe(messageHtml1);
                result.Last().MessageHtml.ShouldBe(messageHtml2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderType_After_Roundtrip()
            {
                // Given
                var providerType1 = "providerType1";
                var providerType2 = "providerType2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", providerType1, "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", providerType2, "providerName2")
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().ProviderType.ShouldBe(providerType1);
                result.Last().ProviderType.ShouldBe(providerType2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderName_After_Roundtrip()
            {
                // Given
                var providerName1 = "providerName1";
                var providerName2 = "providerName2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", providerName1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", providerName2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().ProviderName.ShouldBe(providerName1);
                result.Last().ProviderName.ShouldBe(providerName2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Run_After_Roundtrip()
            {
                // Given
                var run1 = "run1";
                var run2 = "run2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .ForRun(run1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .ForRun(run2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().Run.ShouldBe(run1);
                result.Last().Run.ShouldBe(run2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectFileRelativePath_After_Roundtrip()
            {
                // Given
                var projectFileRelativePath1 = @"src/myproj1.file";
                var projectFileRelativePath2 = @"src/myproj2.file";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InProjectFile(projectFileRelativePath1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InProjectFile(projectFileRelativePath2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().ProjectFileRelativePath.FullPath.ShouldBe(projectFileRelativePath1);
                result.Last().ProjectFileRelativePath.FullPath.ShouldBe(projectFileRelativePath2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectName_After_Roundtrip()
            {
                // Given
                var projectName1 = "projectName1";
                var projectName2 = "projectName2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InProjectOfName(projectName1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InProjectOfName(projectName2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().ProjectName.ShouldBe(projectName1);
                result.Last().ProjectName.ShouldBe(projectName2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_AffectedFileRelativePath_After_Roundtrip()
            {
                // Given
                var affectedFileRelativePath1 = @"src/foo1.bar";
                var affectedFileRelativePath2 = @"src/foo2.bar";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(affectedFileRelativePath1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(affectedFileRelativePath2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().AffectedFileRelativePath.FullPath.ShouldBe(affectedFileRelativePath1);
                result.Last().AffectedFileRelativePath.FullPath.ShouldBe(affectedFileRelativePath2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Line_After_Roundtrip()
            {
                // Given
                var line1 = 23;
                var line2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", line1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", line2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().Line.ShouldBe(line1);
                result.Last().Line.ShouldBe(line2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndLine_After_Roundtrip()
            {
                // Given
                var endLine1 = 230;
                var endLine2 = 420;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", 23, endLine1, null, null)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", 42, endLine2, null, null)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().EndLine.ShouldBe(endLine1);
                result.Last().EndLine.ShouldBe(endLine2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Column_After_Roundtrip()
            {
                // Given
                var column1 = 23;
                var column2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", 123, column1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", 123, column2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().Column.ShouldBe(column1);
                result.Last().Column.ShouldBe(column2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndColumn_After_Roundtrip()
            {
                // Given
                var endColumn1 = 230;
                var endColumn2 = 420;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", 5, 50, 23, endColumn1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", 5, 50, 42, endColumn2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().EndColumn.ShouldBe(endColumn1);
                result.Last().EndColumn.ShouldBe(endColumn2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_FileLink_After_Roundtrip()
            {
                // Given
                var fileLink1 = "https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12";
                var fileLink2 = "https://github.com/myorg/myrepo/blob/develop/src/bar.cs#L23-L42";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .WithFileLink(new Uri(fileLink1))
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .WithFileLink(new Uri(fileLink2))
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().FileLink.ToString().ShouldBe(fileLink1);
                result.Last().FileLink.ToString().ShouldBe(fileLink2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Priority_After_Roundtrip()
            {
                // Given
                var priority1 = 23;
                var priority2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .WithPriority(priority1, "priorityName")
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .WithPriority(priority2, "priorityName")
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().Priority.ShouldBe(priority1);
                result.Last().Priority.ShouldBe(priority2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_PriorityName_After_Roundtrip()
            {
                // Given
                var priorityName1 = "priorityName1";
                var priorityName2 = "priorityName2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .WithPriority(42, priorityName1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .WithPriority(42, priorityName2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().PriorityName.ShouldBe(priorityName1);
                result.Last().PriorityName.ShouldBe(priorityName2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Rule_After_Roundtrip()
            {
                // Given
                var rule1 = "rule1";
                var rule2 = "rule2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .OfRule(rule1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .OfRule(rule2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().Rule.ShouldBe(rule1);
                result.Last().Rule.ShouldBe(rule2);
            }

            [Fact]
            public void Should_Give_Correct_Result_For_RuleUrl_After_Roundtrip()
            {
                // Given
                var ruleUrl1 = new Uri("https://rule1.url");
                var ruleUrl2 = new Uri("https://rule2.url");
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .OfRule("rule", ruleUrl1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .OfRule("rule", ruleUrl2)
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString().DeserializeToIssues();

                // Then
                result.Count().ShouldBe(2);
                result.First().RuleUrl.ToString().ShouldBe(ruleUrl1.ToString());
                result.Last().RuleUrl.ToString().ShouldBe(ruleUrl2.ToString());
            }
        }

        public sealed class TheSerializeToJsonFileExtensionForASingleIssue
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;
                var filePath = @"c:\issue.json";

                // When
                var result = Record.Exception(() => issue.SerializeToJsonFile(filePath));

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                FilePath filePath = null;

                // When
                var result = Record.Exception(() => issue.SerializeToJsonFile(filePath));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Identifier_After_Roundtrip()
            {
                // Given
                var identifier = "identifier";
                var issue =
                    IssueBuilder
                        .NewIssue(identifier, "messageText", "providerType", "providerName")
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Identifier.ShouldBe(identifier);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageText_After_Roundtrip()
            {
                // Given
                var messageText = "messageText";
                var issue =
                    IssueBuilder
                        .NewIssue(messageText, "providerType", "providerName")
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.MessageText.ShouldBe(messageText);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageMarkdown_After_Roundtrip()
            {
                // Given
                var messageMarkdown = "messageMarkdown";
                var issue =
                    IssueBuilder
                        .NewIssue("messageText", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(messageMarkdown)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.MessageMarkdown.ShouldBe(messageMarkdown);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageHtml_After_Roundtrip()
            {
                // Given
                var messageHtml = "messageHtml";
                var issue =
                    IssueBuilder
                        .NewIssue("messageText", "providerType", "providerName")
                        .WithMessageInHtmlFormat(messageHtml)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.MessageHtml.ShouldBe(messageHtml);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderType_After_Roundtrip()
            {
                // Given
                var providerType = "providerType";
                var issue =
                    IssueBuilder
                        .NewIssue("message", providerType, "providerName")
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.ProviderType.ShouldBe(providerType);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderName_After_Roundtrip()
            {
                // Given
                var providerName = "providerName";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", providerName)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.ProviderName.ShouldBe(providerName);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Run_After_Roundtrip()
            {
                // Given
                var run = "run";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .ForRun(run)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Run.ShouldBe(run);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectFileRelativePath_After_Roundtrip()
            {
                // Given
                var projectFileRelativePath = @"src/myproj.file";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(projectFileRelativePath)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.ProjectFileRelativePath.FullPath.ShouldBe(projectFileRelativePath);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectName_After_Roundtrip()
            {
                // Given
                var projectName = "projectName";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.ProjectName.ShouldBe(projectName);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_AffectedFileRelativePath_After_Roundtrip()
            {
                // Given
                var affectedFileRelativePath = @"src/foo.bar";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(affectedFileRelativePath)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.AffectedFileRelativePath.FullPath.ShouldBe(affectedFileRelativePath);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Line_After_Roundtrip()
            {
                // Given
                var line = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", line)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Line.ShouldBe(line);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndLine_After_Roundtrip()
            {
                // Given
                var endLine = 420;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", 42, endLine, null, null)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.EndLine.ShouldBe(endLine);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Column_After_Roundtrip()
            {
                // Given
                var column = 23;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", 42, column)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Column.ShouldBe(column);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndColumn_After_Roundtrip()
            {
                // Given
                var endColumn = 230;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(@"src/foo.bar", 42, 50, 1, endColumn)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.EndColumn.ShouldBe(endColumn);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_FileLink_After_Roundtrip()
            {
                // Given
                var fileLink = "https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithFileLink(new Uri(fileLink))
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.FileLink.ToString().ShouldBe(fileLink);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Priority_After_Roundtrip()
            {
                // Given
                var priority = 42;
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority, "priorityName")
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Priority.ShouldBe(priority);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_PriorityName_After_Roundtrip()
            {
                // Given
                var priorityName = "priorityName";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.PriorityName.ShouldBe(priorityName);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Rule_After_Roundtrip()
            {
                // Given
                var rule = "rule";
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Rule.ShouldBe(rule);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_RuleUrl_After_Roundtrip()
            {
                // Given
                var ruleUrl = new Uri("https://rule.url");
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule("rule", ruleUrl)
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.RuleUrl.ToString().ShouldBe(ruleUrl.ToString());
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }
        }

        public sealed class TheSerializeToJsonFileExtensionForAnEnumerableOfIssues
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IEnumerable<IIssue> issues = null;
                var filePath = @"c:\issues.json";

                // When
                var result = Record.Exception(() => issues.SerializeToJsonFile(filePath));

                // Then
                result.IsArgumentNullException("issues");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                var issues = new List<IIssue>();
                FilePath filePath = null;

                // When
                var result = Record.Exception(() => issues.SerializeToJsonFile(filePath));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Identifier_After_Roundtrip()
            {
                // Given
                var identifier1 = "identifier1";
                var identifier2 = "identifier2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue(identifier1, "messageText1", "providerType1", "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue(identifier2, "messageText2", "providerType2", "providerName2")
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().Identifier.ShouldBe(identifier1);
                    result.Last().Identifier.ShouldBe(identifier2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageText_After_Roundtrip()
            {
                // Given
                var messageText1 = "messageText1";
                var messageText2 = "messageText2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue(messageText1, "providerType1", "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue(messageText2, "providerType2", "providerName2")
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().MessageText.ShouldBe(messageText1);
                    result.Last().MessageText.ShouldBe(messageText2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageMarkdown_After_Roundtrip()
            {
                // Given
                var messageMarkdown1 = "messageMarkdown1";
                var messageMarkdown2 = "messageMarkdown2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("messageText1", "providerType1", "providerName1")
                            .WithMessageInMarkdownFormat(messageMarkdown1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("messageText2", "providerType2", "providerName2")
                            .WithMessageInMarkdownFormat(messageMarkdown2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().MessageMarkdown.ShouldBe(messageMarkdown1);
                    result.Last().MessageMarkdown.ShouldBe(messageMarkdown2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_MessageHtml_After_Roundtrip()
            {
                // Given
                var messageHtml1 = "messageHtml1";
                var messageHtml2 = "messageHtml2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("messageText1", "providerType1", "providerName1")
                            .WithMessageInHtmlFormat(messageHtml1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("messageText2", "providerType2", "providerName2")
                            .WithMessageInHtmlFormat(messageHtml2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().MessageHtml.ShouldBe(messageHtml1);
                    result.Last().MessageHtml.ShouldBe(messageHtml2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderType_After_Roundtrip()
            {
                // Given
                var providerType1 = "providerType1";
                var providerType2 = "providerType2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", providerType1, "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", providerType2, "providerName2")
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().ProviderType.ShouldBe(providerType1);
                    result.Last().ProviderType.ShouldBe(providerType2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProviderName_After_Roundtrip()
            {
                // Given
                var providerName1 = "providerName1";
                var providerName2 = "providerName2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", providerName1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", providerName2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().ProviderName.ShouldBe(providerName1);
                    result.Last().ProviderName.ShouldBe(providerName2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Run_After_Roundtrip()
            {
                // Given
                var run1 = "run1";
                var run2 = "run2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .ForRun(run1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .ForRun(run2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().Run.ShouldBe(run1);
                    result.Last().Run.ShouldBe(run2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectFileRelativePath_After_Roundtrip()
            {
                // Given
                var projectFileRelativePath1 = @"src/myproj1.file";
                var projectFileRelativePath2 = @"src/myproj2.file";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InProjectFile(projectFileRelativePath1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InProjectFile(projectFileRelativePath2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().ProjectFileRelativePath.FullPath.ShouldBe(projectFileRelativePath1);
                    result.Last().ProjectFileRelativePath.FullPath.ShouldBe(projectFileRelativePath2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_ProjectName_After_Roundtrip()
            {
                // Given
                var projectName1 = "projectName1";
                var projectName2 = "projectName2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InProjectOfName(projectName1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InProjectOfName(projectName2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().ProjectName.ShouldBe(projectName1);
                    result.Last().ProjectName.ShouldBe(projectName2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_AffectedFileRelativePath_After_Roundtrip()
            {
                // Given
                var affectedFileRelativePath1 = @"src/foo1.bar";
                var affectedFileRelativePath2 = @"src/foo2.bar";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(affectedFileRelativePath1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(affectedFileRelativePath2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().AffectedFileRelativePath.FullPath.ShouldBe(affectedFileRelativePath1);
                    result.Last().AffectedFileRelativePath.FullPath.ShouldBe(affectedFileRelativePath2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Line_After_Roundtrip()
            {
                // Given
                var line1 = 23;
                var line2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", line1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", line2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().Line.ShouldBe(line1);
                    result.Last().Line.ShouldBe(line2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndLine_After_Roundtrip()
            {
                // Given
                var endLine1 = 230;
                var endLine2 = 420;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", 23, endLine1, null, null)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", 42, endLine2, null, null)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().EndLine.ShouldBe(endLine1);
                    result.Last().EndLine.ShouldBe(endLine2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Column_After_Roundtrip()
            {
                // Given
                var column1 = 23;
                var column2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", 123, column1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", 123, column2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().Column.ShouldBe(column1);
                    result.Last().Column.ShouldBe(column2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_EndColumn_After_Roundtrip()
            {
                // Given
                var endColumn1 = 23;
                var endColumn2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .InFile(@"src/foo.bar", 5, 50, 1, endColumn1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .InFile(@"src/foo.bar", 5, 50, 1, endColumn2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().EndColumn.ShouldBe(endColumn1);
                    result.Last().EndColumn.ShouldBe(endColumn2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_FileLink_After_Roundtrip()
            {
                // Given
                var fileLink1 = "https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12";
                var fileLink2 = "https://github.com/myorg/myrepo/blob/develop/src/bar.cs#L23-L42";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .WithFileLink(new Uri(fileLink1))
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .WithFileLink(new Uri(fileLink2))
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().FileLink.ToString().ShouldBe(fileLink1);
                    result.Last().FileLink.ToString().ShouldBe(fileLink2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Priority_After_Roundtrip()
            {
                // Given
                var priority1 = 23;
                var priority2 = 42;
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .WithPriority(priority1, "priorityName")
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .WithPriority(priority2, "priorityName")
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().Priority.ShouldBe(priority1);
                    result.Last().Priority.ShouldBe(priority2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_PriorityName_After_Roundtrip()
            {
                // Given
                var priorityName1 = "priorityName1";
                var priorityName2 = "priorityName2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .WithPriority(42, priorityName1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .WithPriority(42, priorityName2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().PriorityName.ShouldBe(priorityName1);
                    result.Last().PriorityName.ShouldBe(priorityName2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_Rule_After_Roundtrip()
            {
                // Given
                var rule1 = "rule1";
                var rule2 = "rule2";
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .OfRule(rule1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .OfRule(rule2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().Rule.ShouldBe(rule1);
                    result.Last().Rule.ShouldBe(rule2);
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }

            [Fact]
            public void Should_Give_Correct_Result_For_RuleUrl_After_Roundtrip()
            {
                // Given
                var ruleUrl1 = new Uri("https://rule1.url");
                var ruleUrl2 = new Uri("https://rule2.url");
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .OfRule("rule", ruleUrl1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .OfRule("rule", ruleUrl2)
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssues();

                    // Then
                    result.Count().ShouldBe(2);
                    result.First().RuleUrl.ToString().ShouldBe(ruleUrl1.ToString());
                    result.Last().RuleUrl.ToString().ShouldBe(ruleUrl2.ToString());
                }
                finally
                {
                    if (System.IO.File.Exists(filePath.FullPath))
                    {
                        System.IO.File.Delete(filePath.FullPath);
                    }
                }
            }
        }

        public sealed class TheToSerializableIssueExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ToSerializableIssue());

                // Then
                result.IsArgumentNullException("issue");
            }
        }
    }
}