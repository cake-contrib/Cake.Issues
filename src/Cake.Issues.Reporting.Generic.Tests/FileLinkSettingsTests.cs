namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class FileLinkSettingsTests
    {
        public sealed class TheGitHubMethod
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
                    FileLinkSettings.GitHub(repositoryUrl, branch, rootPath));

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
                    FileLinkSettings.GitHub(repositoryUrl, branch, rootPath));

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
                    FileLinkSettings.GitHub(repositoryUrl, branch, rootPath));

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
                    FileLinkSettings.GitHub(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("branch");
            }

            [Theory]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", null, "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website/", "master", null, "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "foo", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "/foo", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "foo/", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/{FilePath}#L{Line}")]
            [InlineData("https://github.com/cake-contrib/Cake.Issues.Website", "master", "foo/bar", "https://github.com/cake-contrib/Cake.Issues.Website/blob/master/foo/bar/{FilePath}#L{Line}")]
            public void Should_Set_Correct_FileLinkPattern(string repositoryUrl, string branch, string rootPath, string expectedPattern)
            {
                // Given

                // When
                var result = FileLinkSettings.GitHub(new Uri(repositoryUrl), branch, rootPath);

                // Then
                result.FileLinkPattern.ShouldBe(expectedPattern);
            }
        }

        public sealed class TheTeamFoundationServerMethod
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
                    FileLinkSettings.TeamFoundationServer(repositoryUrl, branch, rootPath));

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
                    FileLinkSettings.TeamFoundationServer(repositoryUrl, branch, rootPath));

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
                    FileLinkSettings.TeamFoundationServer(repositoryUrl, branch, rootPath));

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
                    FileLinkSettings.TeamFoundationServer(repositoryUrl, branch, rootPath));

                // Then
                result.IsArgumentOutOfRangeException("branch");
            }

            [Theory]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", null, "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path={FilePath}&version=GBmaster&line={Line}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository/", "master", null, "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path={FilePath}&version=GBmaster&line={Line}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "foo", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/{FilePath}&version=GBmaster&line={Line}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "/foo", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/{FilePath}&version=GBmaster&line={Line}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "foo/", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/{FilePath}&version=GBmaster&line={Line}")]
            [InlineData("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository", "master", "foo/bar", "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository?path=foo/bar/{FilePath}&version=GBmaster&line={Line}")]
            public void Should_Set_Correct_FileLinkPattern(string repositoryUrl, string branch, string rootPath, string expectedPattern)
            {
                // Given

                // When
                var result = FileLinkSettings.TeamFoundationServer(new Uri(repositoryUrl), branch, rootPath);

                // Then
                result.FileLinkPattern.ShouldBe(expectedPattern);
            }
        }
    }
}
