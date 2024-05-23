namespace Cake.Issues.Tests.FileLinking;

using Cake.Issues.FileLinking;

public sealed class GitHubFileLinkSettingsBuilderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_RepositoryUrl_Is_Null()
        {
            // Given
            const Uri repositoryUrl = null;

            // When
            var result = Record.Exception(() => new GitHubFileLinkSettingsBuilder(repositoryUrl));

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
                Record.Exception(() => new GitHubFileLinkSettingsBuilder(new Uri("https://github.com")).Branch(branch));

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
                Record.Exception(() => new GitHubFileLinkSettingsBuilder(new Uri("https://github.com")).Branch(branch));

            // Then
            result.IsArgumentOutOfRangeException("branchName");
        }

        [Fact]
        public void Should_Throw_If_BranchName_Is_WhiteSpace()
        {
            // Given
            const string branch = " ";

            // When
            var result =
                Record.Exception(() => new GitHubFileLinkSettingsBuilder(new Uri("https://github.com")).Branch(branch));

            // Then
            result.IsArgumentOutOfRangeException("branchName");
        }

        [Theory]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues",
            @"src\Cake.Issues\Cake.Issues.csproj",
            10,
            12,
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/src/Cake.Issues/Cake.Issues.csproj#L10-L12")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            10,
            12,
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/src/Cake.Issues/Cake.Issues.csproj#L10-L12")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            10,
            null,
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/src/Cake.Issues/Cake.Issues.csproj#L10")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            null,
            null,
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/src/Cake.Issues/Cake.Issues.csproj")]
        public void Should_Return_The_Correct_Link(
            string repositoryUrl,
            string filePath,
            int? line,
            int? endLine,
            string branch,
            string expectedLink)
        {
            // Given
            var issue =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath, line, endLine, null, null)
                    .Create();

            // When
            var result =
                new GitHubFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Branch(branch)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }

        [Theory]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "foo",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/foo/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "/foo",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/foo/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "foo/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/foo/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "foo/bar",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/foo/bar/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            null,
            @"src\Cake.Issues\Cake.Issues.csproj",
            "master",
            "https://github.com/cake-contrib/Cake.Issues/blob/master/src/Cake.Issues/Cake.Issues.csproj")]
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
                new GitHubFileLinkSettingsBuilder(new Uri(repositoryUrl))
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
                Record.Exception(() => new GitHubFileLinkSettingsBuilder(new Uri("https://github.com")).Commit(commitId));

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
                Record.Exception(() => new GitHubFileLinkSettingsBuilder(new Uri("https://github.com")).Commit(commitId));

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
                Record.Exception(() => new GitHubFileLinkSettingsBuilder(new Uri("https://github.com")).Commit(commitId));

            // Then
            result.IsArgumentOutOfRangeException("commitId");
        }

        [Theory]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues",
            @"src\Cake.Issues\Cake.Issues.csproj",
            10,
            12,
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/src/Cake.Issues/Cake.Issues.csproj#L10-L12")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            10,
            12,
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/src/Cake.Issues/Cake.Issues.csproj#L10-L12")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            10,
            null,
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/src/Cake.Issues/Cake.Issues.csproj#L10")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            null,
            null,
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/src/Cake.Issues/Cake.Issues.csproj")]
        public void Should_Return_The_Correct_Link(
            string repositoryUrl,
            string filePath,
            int? line,
            int? endLine,
            string commitId,
            string expectedLink)
        {
            // Given
            var issue =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath, line, endLine, null, null)
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();

            // When
            var result =
                new GitHubFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Commit(commitId)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }

        [Theory]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "foo",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/foo/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "/foo",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/foo/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "foo/",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/foo/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            "foo/bar",
            @"src\Cake.Issues\Cake.Issues.csproj",
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/foo/bar/src/Cake.Issues/Cake.Issues.csproj")]
        [InlineData(
            "https://github.com/cake-contrib/Cake.Issues/",
            null,
            @"src\Cake.Issues\Cake.Issues.csproj",
            "6d035013812a4a3ab8be5e84c0f2a8b5ce60720a",
            "https://github.com/cake-contrib/Cake.Issues/blob/6d035013812a4a3ab8be5e84c0f2a8b5ce60720a/src/Cake.Issues/Cake.Issues.csproj")]
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
                new GitHubFileLinkSettingsBuilder(new Uri(repositoryUrl))
                    .Commit(commitId)
                    .WithRootPath(rootPath)
                    .GetFileLink(issue);

            // Then
            result.ToString().ShouldBe(expectedLink);
        }
    }
}
