namespace Cake.Issues.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class FileLinkSettingsTests
    {
        public sealed class TheGitHubBranchMethod
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                Uri repositoryUrl = null;
                var branch = "master";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_Branch_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("https://github.com/cake-contrib/Cake.Issues.Website");
                string branch = null;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentNullException("branch");
            }

            [Fact]
            public void Should_Throw_If_Branch_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("https://github.com/cake-contrib/Cake.Issues.Website");
                var branch = string.Empty;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("branch");
            }

            [Fact]
            public void Should_Throw_If_Branch_Is_Whitespace()
            {
                // Given
                var repositoryUrl = new Uri("https://github.com/cake-contrib/Cake.Issues.Website");
                var branch = " ";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("branch");
            }

            [Theory]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", null, "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/{FilePath}#L{Line}-L{EndLine}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website/", "master", null, "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/{FilePath}#L{Line}-L{EndLine}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "foo", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/{FilePath}#L{Line}-L{EndLine}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "/foo", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/{FilePath}#L{Line}-L{EndLine}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "foo/", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/{FilePath}#L{Line}-L{EndLine}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "foo/bar", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/bar/{FilePath}#L{Line}-L{EndLine}")]
            public void Should_Set_Correct_FileLinkPattern(string repositoryUrl, string branch, string rootPath, string expectedPattern)
            {
                // Given

                // When
                var result = FileLinkSettings.GitHubBranch(new Uri(repositoryUrl), branch, rootPath);

                // Then
                result.FileLinkPattern.ShouldBe(expectedPattern);
            }
        }

        public sealed class TheGitHubCommitMethod
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                Uri repositoryUrl = null;
                var commitId = "87eaf0a0cf1746179e8714eae4114d10";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("https://github.com/cake-contrib/Cake.Issues.Website");
                string commitId = null;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentNullException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("https://github.com/cake-contrib/Cake.Issues.Website");
                var commitId = string.Empty;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Whitespace()
            {
                // Given
                var repositoryUrl = new Uri("https://github.com/cake-contrib/Cake.Issues.Website");
                var commitId = " ";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.GitHubCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Theory]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "87eaf0a0cf1746179e8714eae4114d10", null, "https://github.com/cake-contrib/Cake.Issues.Website/blob/87eaf0a0cf1746179e8714eae4114d10/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website/", "87eaf0a0cf1746179e8714eae4114d10", null, "https://github.com/cake-contrib/Cake.Issues.Website/blob/87eaf0a0cf1746179e8714eae4114d10/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "87eaf0a0cf1746179e8714eae4114d10", "foo", "https://github.com/cake-contrib/Cake.Issues.Website/blob/87eaf0a0cf1746179e8714eae4114d10/foo/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "87eaf0a0cf1746179e8714eae4114d10", "/foo", "https://github.com/cake-contrib/Cake.Issues.Website/blob/87eaf0a0cf1746179e8714eae4114d10/foo/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "87eaf0a0cf1746179e8714eae4114d10", "foo/", "https://github.com/cake-contrib/Cake.Issues.Website/blob/87eaf0a0cf1746179e8714eae4114d10/foo/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "87eaf0a0cf1746179e8714eae4114d10", "foo/bar", "https://github.com/cake-contrib/Cake.Issues.Website/blob/87eaf0a0cf1746179e8714eae4114d10/foo/bar/{FilePath}#L{Line}")]
            public void Should_Set_Correct_FileLinkPattern(string repositoryUrl, string commitId, string rootPath, string expectedPattern)
            {
                // Given

                // When
                var result = FileLinkSettings.GitHubCommit(new Uri(repositoryUrl), commitId, rootPath);

                // Then
                result.FileLinkPattern.ShouldBe(expectedPattern);
            }
        }

        public sealed class TheAzureDevOpsBranchMethod
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                Uri repositoryUrl = null;
                var branch = "master";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_Branch_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");
                string branch = null;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentNullException("branch");
            }

            [Fact]
            public void Should_Throw_If_Branch_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");
                var branch = string.Empty;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("branch");
            }

            [Fact]
            public void Should_Throw_If_Branch_Is_Whitespace()
            {
                // Given
                var repositoryUrl = new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");
                var branch = " ";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("branch");
            }

            [Theory]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", null, "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path={FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository/", "master", null, "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path={FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "foo", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/{FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "/foo", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/{FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "foo/", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/{FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "foo/bar", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/bar/{FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("https://dev.azure.com/myorganization/_git/myrepo", "master", "foo/bar", "https://dev.azure.com/myorganization/_git/myrepo?path=foo/bar/{FilePath}&version=GBmaster&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            public void Should_Set_Correct_FileLinkPattern(string repositoryUrl, string branch, string rootPath, string expectedPattern)
            {
                // Given

                // When
                var result = FileLinkSettings.AzureDevOpsBranch(new Uri(repositoryUrl), branch, rootPath);

                // Then
                result.FileLinkPattern.ShouldBe(expectedPattern);
            }
        }

        public sealed class TheAzureDevOpsCommitMethod
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                Uri repositoryUrl = null;
                var branch = "master";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsBranch(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");
                string commitId = null;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentNullException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");
                var commitId = string.Empty;
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Whitespace()
            {
                // Given
                var repositoryUrl = new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");
                var commitId = " ";
                var rootPath = string.Empty;

                // When
                var result = Record.Exception(() =>
                    FileLinkSettings.AzureDevOpsCommit(repositoryUrl, commitId, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Theory]
            [InlineData("https://dev.azure.com/myorganization/_git/myrepo", "daf015105140e98d6f296e0db2a80d28b84e7f59", null, "https://dev.azure.com/myorganization/_git/myrepo?path={FilePath}&version=GCdaf015105140e98d6f296e0db2a80d28b84e7f59&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            [InlineData("https://dev.azure.com/myorganization/_git/myrepo", "daf015105140e98d6f296e0db2a80d28b84e7f59", "foo/bar", "https://dev.azure.com/myorganization/_git/myrepo?path=foo/bar/{FilePath}&version=GCdaf015105140e98d6f296e0db2a80d28b84e7f59&line={Line}&lineEnd={EndLine}&lineStartColumn={Column}&lineEndColumn={EndColumn}")]
            public void Should_Set_Correct_FileLinkPattern(string repositoryUrl, string commitId, string rootPath, string expectedPattern)
            {
                // Given

                // When
                var result = FileLinkSettings.AzureDevOpsCommit(new Uri(repositoryUrl), commitId, rootPath);

                // Then
                result.FileLinkPattern.ShouldBe(expectedPattern);
            }
        }
    }
}
