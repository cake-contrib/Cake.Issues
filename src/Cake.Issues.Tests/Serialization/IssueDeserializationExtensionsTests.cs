namespace Cake.Issues.Tests.Serialization
{
    using Cake.Core.IO;
    using Cake.Issues.Serialization;

    public sealed class IssueDeserializationExtensionsTests
    {
        public sealed class TheDeserializeToIssueExtensionForAJsonString
        {
            [Fact]
            public void Should_Throw_If_JsonString_Is_Null()
            {
                // Given
                const string jsonString = null;

                // When
                var result = Record.Exception(() => jsonString.DeserializeToIssue());

                // Then
                result.IsArgumentNullException("jsonString");
            }

            [Fact]
            public void Should_Throw_If_JsonString_Has_Unknown_Version()
            {
                // Given
                const string jsonString = "{\"Version\": -1}";

                // When
                var result = Record.Exception(() => jsonString.DeserializeToIssue());

                // Then
                result.ShouldNotBeNull();
                result.Message.ShouldBe("Not supported issue serialization format -1");
            }
        }

        public sealed class TheDeserializeToIssuesExtensionForAJsonString
        {
            [Fact]
            public void Should_Throw_If_JsonString_Is_Null()
            {
                // Given
                const string jsonString = null;

                // When
                var result = Record.Exception(() => jsonString.DeserializeToIssues());

                // Then
                result.IsArgumentNullException("jsonString");
            }

            [Fact]
            public void Should_Return_An_Empty_List_For_An_Empty_Array()
            {
                // Given
                const string jsonString = "[]";

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
                const FilePath filePath = null;

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

            [Fact]
            public void Should_Return_IssueV2()
            {
                // Given
                var filePath = new FilePath("Testfiles/issueV2.json");

                // When
                var result = filePath.DeserializeToIssue();

                // Then
                IssueChecker.Check(
                    result,
                    IssueBuilder.NewIssue(
                        "Something went wrong.",
                        "TestProvider",
                        "Test Provider")
                        .WithMessageInHtmlFormat("Something went <b>wrong</b>.")
                        .WithMessageInMarkdownFormat("Something went **wrong**.")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar.cs", 42)
                        .OfRule("Rule", new Uri("https://google.com"))
                        .WithPriority(IssuePriority.Warning));
            }

            [Fact]
            public void Should_Return_IssueV3()
            {
                // Given
                var filePath = new FilePath("Testfiles/issueV3.json");

                // When
                var result = filePath.DeserializeToIssue();

                // Then
                IssueChecker.Check(
                    result,
                    IssueBuilder.NewIssue(
                        "Identifier",
                        "Something went wrong.",
                        "TestProvider",
                        "Test Provider")
                        .ForRun("TestRun")
                        .WithMessageInHtmlFormat("Something went <b>wrong</b>.")
                        .WithMessageInMarkdownFormat("Something went **wrong**.")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar.cs", 42, 420, 23, 230)
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
                const FilePath filePath = null;

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

            [Fact]
            public void Should_Return_List_Of_IssuesV2()
            {
                // Given
                var filePath = new FilePath("Testfiles/issuesV2.json");

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
                        .WithMessageInHtmlFormat("Something went <b>wrong</b>.")
                        .WithMessageInMarkdownFormat("Something went **wrong**.")
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
                        .WithMessageInHtmlFormat("Something went <b>wrong</b> again.")
                        .WithMessageInMarkdownFormat("Something went **wrong** again.")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar2.cs")
                        .WithPriority(IssuePriority.Warning));
            }

            [Fact]
            public void Should_Return_List_Of_IssuesV3()
            {
                // Given
                var filePath = new FilePath("Testfiles/issuesV3.json");

                // When
                var result = filePath.DeserializeToIssues().ToList();

                // Then
                result.Count.ShouldBe(2);
                IssueChecker.Check(
                    result[0],
                    IssueBuilder.NewIssue(
                        "Identifier1",
                        "Something went wrong.",
                        "TestProvider",
                        "Test Provider")
                        .WithMessageInHtmlFormat("Something went <b>wrong</b>.")
                        .WithMessageInMarkdownFormat("Something went **wrong**.")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar.cs", 42, 420, 23, 230)
                        .OfRule("Rule", new Uri("https://google.com"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    result[1],
                    IssueBuilder.NewIssue(
                        "Identifier2",
                        "Something went wrong again.",
                        "TestProvider",
                        "Test Provider")
                        .WithMessageInHtmlFormat("Something went <b>wrong</b> again.")
                        .WithMessageInMarkdownFormat("Something went **wrong** again.")
                        .InProject(@"src\Foo\Bar.csproj", "Bar")
                        .InFile(@"src\Foo\Bar2.cs")
                        .WithPriority(IssuePriority.Warning));
            }
        }
    }
}