namespace Cake.Issues.Reporting.Generic.Tests
{
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IIssueExtensionTests
    {
        public sealed class TheProjectPathExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ProjectPath());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var projectPath = @"src\Cake.Issues.Reporting.Generic.Tests\Cake.Issues.Reporting.Generic.Tests.csproj";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InProjectFile(projectPath)
                        .Create();

                // When
                var result = issue.ProjectPath();

                // Then
                result.ShouldBe(@"src/Cake.Issues.Reporting.Generic.Tests/Cake.Issues.Reporting.Generic.Tests.csproj");
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

        public sealed class TheFilePathExtension
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FilePath());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                var result = issue.FilePath();

                // Then
                result.ShouldBe(@"src/Cake.Issues.Reporting.Generic.Tests/Foo.cs");
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
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FileDirectory());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(filePath)
                        .Create();

                // When
                var result = issue.FileDirectory();

                // Then
                result.ShouldBe(@"src/Cake.Issues.Reporting.Generic.Tests");
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
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.FileName());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Return_Full_Path()
            {
                // Given
                var filePath = @"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs";
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
    }
}
