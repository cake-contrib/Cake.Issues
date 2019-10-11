namespace Cake.Issues.PullRequests.AzureDevOps.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Xunit;

    public sealed class AzureDevOpsPullRequestSystemSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_SourceRefName_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(null, "foo", null));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), null, null));

                // Then
                result.IsArgumentNullException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), string.Empty, null));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), " ", null));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_PullRequestId_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(null, 0, null));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_Credentials_For_PullRequestId_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), 42, null));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Throw_If_Credentials_For_SourceBranch_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), "feature/foo", null));

                // Then
                result.IsArgumentNullException("credentials");
            }
        }
    }
}
