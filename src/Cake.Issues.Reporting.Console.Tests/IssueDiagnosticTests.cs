namespace Cake.Issues.Reporting.Console.Tests;

using System;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Xunit;

public sealed class IssueDiagnosticTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            ICakeLog log = null;
            var repositoryRoot = @"/repo";
            var issue = IssueBuilder
                .NewIssue("Test message", "TestProvider", "TestProviderName")
                .InFile("test.cs", 10, 5)
                .InProject("TestProject.csproj", "TestProject")
                .OfRule("TestRule")
                .WithPriority(IssuePriority.Warning)
                .Create();


            // When
            var result = Record.Exception(() => new IssueDiagnostic(log, repositoryRoot, issue));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_RepositoryRoot_Is_Null()
        {
            // Given
            var log = new FakeLog();
            DirectoryPath repositoryRoot = null;
            var issue = IssueBuilder
                .NewIssue("Test message", "TestProvider", "TestProviderName")
                .InFile("test.cs", 10, 5)
                .InProject("TestProject.csproj", "TestProject")
                .OfRule("TestRule")
                .WithPriority(IssuePriority.Warning)
                .Create();


            // When
            var result = Record.Exception(() => new IssueDiagnostic(log, repositoryRoot, issue));

            // Then
            result.IsArgumentNullException("repositoryRoot");
        }

        [Fact]
        public void Should_Include_Project_Information_In_Note_When_Available()
        {
            // Given
            var log = new FakeLog();
            var repositoryRoot = @"/repo";
            var issue = IssueBuilder
                .NewIssue("Test message", "TestProvider", "TestProviderName")
                .InFile("test.cs", 10, 5)
                .InProject("TestProject.csproj", "TestProject")
                .OfRule("TestRule")
                .WithPriority(IssuePriority.Warning)
                .Create();

            // When
            var diagnostic = new IssueDiagnostic(log, repositoryRoot, issue);

            // Then
            diagnostic.Note.ShouldContain("Project: TestProject.csproj");
        }

        [Fact]
        public void Should_Include_Project_Name_When_No_Project_File_Path()
        {
            // Given
            var log = new FakeLog();
            var repositoryRoot = @"/repo";
            var issue = IssueBuilder
                .NewIssue("Test message", "TestProvider", "TestProviderName")
                .InFile("test.cs", 10, 5)
                .InProject(null, "TestProject")
                .OfRule("TestRule")
                .WithPriority(IssuePriority.Warning)
                .Create();

            // When
            var diagnostic = new IssueDiagnostic(log, repositoryRoot, issue);

            // Then
            diagnostic.Note.ShouldContain("Project: TestProject");
        }

        [Fact]
        public void Should_Include_Both_Project_And_Rule_Info_When_Available()
        {
            // Given
            var log = new FakeLog();
            var repositoryRoot = @"/repo";
            var issue = IssueBuilder
                .NewIssue("Test message", "TestProvider", "TestProviderName")
                .InFile("test.cs", 10, 5)
                .InProject("TestProject.csproj", "TestProject")
                .OfRule("TestRule", new Uri("http://example.com/rule"))
                .WithPriority(IssuePriority.Warning)
                .Create();

            // When
            var diagnostic = new IssueDiagnostic(log, repositoryRoot, issue);

            // Then
            diagnostic.Note.ShouldContain("Project: TestProject.csproj");
            diagnostic.Note.ShouldContain("See http://example.com/rule for more information");
        }

        [Fact]
        public void Should_Not_Include_Project_When_Not_Available()
        {
            // Given
            var log = new FakeLog();
            var repositoryRoot = @"/repo";
            var issue = IssueBuilder
                .NewIssue("Test message", "TestProvider", "TestProviderName")
                .InFile("test.cs", 10, 5)
                .OfRule("TestRule", new Uri("http://example.com/rule"))
                .WithPriority(IssuePriority.Warning)
                .Create();

            // When
            var diagnostic = new IssueDiagnostic(log, repositoryRoot, issue);

            // Then
            if (diagnostic.Note != null)
            {
                diagnostic.Note.ShouldNotContain("Project:");
                diagnostic.Note.ShouldContain("See http://example.com/rule for more information");
            }
            else
            {
                // If note is null, that means no rule URL was set, which means there's an issue with the test setup
                Assert.Fail("Note should not be null when rule URL is provided");
            }
        }
    }
}