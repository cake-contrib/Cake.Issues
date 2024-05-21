namespace Cake.Issues.Tests.FileLinking;

using Cake.Issues.FileLinking;

public sealed class AzureDevOpsFileLinkSettingsBuilderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_RepositoryUrl_Is_Null()
        {
            // Given
            const Uri repositoryUrl = null;

            // When
            var result = Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(repositoryUrl));

            // Then
            result.IsArgumentNullException("repositoryUrl");
        }
    }

    public sealed class TheBranchMethod
    {
        [Fact]
        public void Should_Throw_If_BranchName_Is_Null()
        {
            // Given
            const string branch = null;

            // When
            var result =
                Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(new Uri("https://github.com")).Branch(branch));

            // Then
            result.IsArgumentNullException("branchName");
        }

        [Fact]
        public void Should_Throw_If_BranchName_Is_Empty()
        {
            // Given
            var branch = string.Empty;

            // When
            var result =
                Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(new Uri("https://github.com")).Branch(branch));

            // Then
            result.IsArgumentOutOfRangeException("branchName");
        }

        [Fact]
        public void Should_Throw_If_BranchName_Is_WhiteSpace()
        {
            // Given
            var branch = " ";

            // When
            var result =
                Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(new Uri("https://github.com")).Branch(branch));

            // Then
            result.IsArgumentOutOfRangeException("branchName");
        }

        [Theory]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            30,
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=30")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo/",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            30,
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=30")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            null,
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=2147483647")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            null,
            null,
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster&line=10&lineEnd=12&lineStartColumn=1&lineEndColumn=2147483647")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            null,
            null,
            null,
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster&line=10&lineEnd=10&lineStartColumn=1&lineEndColumn=2147483647")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            null,
            null,
            null,
            null,
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster")]
        [InlineData(
            "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            30,
            "master",
            "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=30")]
        public void Should_Return_The_Correct_Link(
            string repositoryUrl,
            string filePath,
            int? line,
            int? endLine,
            int? column,
            int? endColumn,
            string branch,
            string expectedLink)
        {
            // Given
            var issue =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath, line, endLine, column, endColumn)
                    .Create();

            // When
            var result =
                new AzureDevOpsFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Branch(branch)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }

        [Theory]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "foo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "/foo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "foo/",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "foo/bar",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/bar/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            null,
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "master",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GBmaster")]
        public void Should_Return_The_Correct_Link_For_RootPath(
            string repositoryUrl,
            string rootPath,
            string filePath,
            string branch,
            string expectedLink)
        {
            // Given
            var issue =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath)
                    .Create();

            // When
            var result =
                new AzureDevOpsFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Branch(branch)
                    .WithRootPath(rootPath)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }
    }

    public sealed class TheCommitMethod
    {
        [Fact]
        public void Should_Throw_If_CommitId_Is_Null()
        {
            // Given
            const string commitId = null;

            // When
            var result =
                Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(new Uri("https://github.com")).Commit(commitId));

            // Then
            result.IsArgumentNullException("commitId");
        }

        [Fact]
        public void Should_Throw_If_CommitId_Is_Empty()
        {
            // Given
            var commitId = string.Empty;

            // When
            var result =
                Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(new Uri("https://github.com")).Commit(commitId));

            // Then
            result.IsArgumentOutOfRangeException("commitId");
        }

        [Fact]
        public void Should_Throw_If_CommitId_Is_WhiteSpace()
        {
            // Given
            var commitId = " ";

            // When
            var result =
                Record.Exception(() => new AzureDevOpsFileLinkSettingsBuilder(new Uri("https://github.com")).Commit(commitId));

            // Then
            result.IsArgumentOutOfRangeException("commitId");
        }

        [Theory]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            30,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=30")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo/",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            30,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=30")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            null,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=2147483647")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            null,
            null,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466&line=10&lineEnd=12&lineStartColumn=1&lineEndColumn=2147483647")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            null,
            null,
            null,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466&line=10&lineEnd=10&lineStartColumn=1&lineEndColumn=2147483647")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            null,
            null,
            null,
            null,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466")]
        [InlineData(
            "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            10,
            12,
            20,
            30,
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466&line=10&lineEnd=12&lineStartColumn=20&lineEndColumn=30")]
        public void Should_Return_The_Correct_Link(
            string repositoryUrl,
            string filePath,
            int? line,
            int? endLine,
            int? column,
            int? endColumn,
            string commitId,
            string expectedLink)
        {
            // Given
            var issue =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath, line, endLine, column, endColumn)
                    .Create();

            // When
            var result =
                new AzureDevOpsFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Commit(commitId)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }

        [Theory]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "foo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "/foo",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "foo/",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            "foo/bar",
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/foo/bar/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466")]
        [InlineData(
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo",
            null,
            @"src\ClassLibrary1\ClassLibrary1.csproj",
            "734bd70b03e45741426ed2916d1fa72c6ff20466",
            "https://dev.azure.com/pberger/_git/Cake.Issues-Demo?path=/src/ClassLibrary1/ClassLibrary1.csproj&version=GC734bd70b03e45741426ed2916d1fa72c6ff20466")]
        public void Should_Return_The_Correct_Link_For_RootPath(
            string repositoryUrl,
            string rootPath,
            string filePath,
            string commitId,
            string expectedLink)
        {
            // Given
            var issue =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath)
                    .Create();

            // When
            var result =
                new AzureDevOpsFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Commit(commitId)
                    .WithRootPath(rootPath)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }
    }
}
