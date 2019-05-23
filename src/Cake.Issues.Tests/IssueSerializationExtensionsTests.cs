namespace Cake.Issues.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.IO;
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
            public void Should_Give_Correct_Result_For_Message_After_Roundtrip()
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
                result.Message.ShouldBe(message);
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
            public void Should_Serialize_Issues()
            {
                // Given
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .Create(),
                    };

                // When
                var result = issues.SerializeToJsonString();

                // Then
                result.ShouldNotBeNull();
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
            public void Should_Give_Correct_Result_For_Message_After_Roundtrip()
            {
                // Given
                var message = "message";
                var issue =
                    IssueBuilder
                        .NewIssue(message, "providerType", "providerName")
                        .Create();
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issue.SerializeToJsonFile(filePath);
                    var result = filePath.DeserializeToIssue();

                    // Then
                    result.Message.ShouldBe(message);
                }
                finally
                {
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
                    System.IO.File.Delete(filePath.FullPath);
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
            public void Should_Serialize_Issues()
            {
                // Given
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                          .NewIssue("message1", "providerType1", "providerName1")
                            .Create(),
                        IssueBuilder
                            .NewIssue("message2", "providerType2", "providerName2")
                            .Create(),
                    };
                var filePath = new FilePath(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".json");

                try
                {
                    // When
                    issues.SerializeToJsonFile(filePath);

                    // Then
                    System.IO.File.Exists(filePath.FullPath).ShouldBeTrue();
                }
                finally
                {
                    System.IO.File.Delete(filePath.FullPath);
                }
            }
        }

        public sealed class TheDeserializeToIssueExtensionForAJsonString
        {
            [Fact]
            public void Should_Throw_If_JsonString_Is_Null()
            {
                // Given
                string jsonString = null;

                // When
                var result = Record.Exception(() => jsonString.DeserializeToIssue());

                // Then
                result.IsArgumentNullException("jsonString");
            }
        }

        public sealed class TheDeserializeToIssuesExtensionForAJsonString
        {
            [Fact]
            public void Should_Throw_If_JsonString_Is_Null()
            {
                // Given
                string jsonString = null;

                // When
                var result = Record.Exception(() => jsonString.DeserializeToIssues());

                // Then
                result.IsArgumentNullException("jsonString");
            }

            [Fact]
            public void Should_Return_An_Empty_List_For_An_Empty_Array()
            {
                // Given
                string jsonString = "[]";

                // When
                var result = jsonString.DeserializeToIssues();

                // Then
                result.ShouldBeEmpty();
            }
        }

        public sealed class TheDeserializeToIssueExtensionForAJsonFile
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                FilePath filePath = null;

                // When
                var result = Record.Exception(() => filePath.DeserializeToIssue());

                // Then
                result.IsArgumentNullException("filePath");
            }
        }

        public sealed class TheDeserializeToIssuesExtensionForAJsonFile
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                FilePath filePath = null;

                // When
                var result = Record.Exception(() => filePath.DeserializeToIssues());

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Return_An_Empty_List_For_An_Empty_Array()
            {
                // Given
                var filePath = new FilePath("Testfiles/empty-array.json");

                // When
                var result = filePath.DeserializeToIssues();

                // Then
                result.ShouldBeEmpty();
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

        public sealed class TheToIssueExtension
        {
            [Fact]
            public void Should_Throw_If_SerializableIssue_Is_Null()
            {
                // Given
                SerializableIssue serializableIssue = null;

                // When
                var result = Record.Exception(() => serializableIssue.ToIssue());

                // Then
                result.IsArgumentNullException("serializableIssue");
            }
        }
    }
}