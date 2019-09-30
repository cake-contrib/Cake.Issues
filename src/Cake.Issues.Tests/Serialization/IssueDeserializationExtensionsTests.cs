namespace Cake.Issues.Tests.Serialization
{
    using System;
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Issues.Serialization;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueDeserializationExtensionsTests
    {
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

            [Fact]
            public void Should_Throw_If_JsonString_Has_Unknown_Version()
            {
                // Given
                var jsonString = "{\"Version\": -1}";

                // When
                var result = Record.Exception(() => jsonString.DeserializeToIssue());

                // Then
                result.Message.ShouldBe("Not supported issue serialization format -1");
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

            [Fact]
            public void Should_Return_Issue()
            {
                // Given
                var filePath = new FilePath("Testfiles/issue.json");

                // When
                var result = filePath.DeserializeToIssue();

                // Then
                IssueChecker.Check(
                    result,
                    IssueBuilder.NewIssue(
                        "Something went wrong.",
                        "TestProvider",
                        "Test Provider")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar.cs", 42)
                        .OfRule("Rule", new Uri("https://google.com"))
                        .WithPriority(IssuePriority.Warning));
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

            [Fact]
            public void Should_Return_List_Of_Issues()
            {
                // Given
                var filePath = new FilePath("Testfiles/issues.json");

                // When
                var result = filePath.DeserializeToIssues().ToList();

                // Then
                result.Count.ShouldBe(2);
                IssueChecker.Check(
                    result[0],
                    IssueBuilder.NewIssue(
                        "Something went wrong.",
                        "TestProvider",
                        "Test Provider")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar.cs", 42)
                        .OfRule("Rule", new Uri("https://google.com"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    result[1],
                    IssueBuilder.NewIssue(
                        "Something went wrong again.",
                        "TestProvider",
                        "Test Provider")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar2.cs")
                        .WithPriority(IssuePriority.Warning));
            }
        }
    }
}