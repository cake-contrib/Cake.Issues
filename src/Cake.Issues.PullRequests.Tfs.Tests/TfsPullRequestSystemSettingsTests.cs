namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using System;
    using Testing;
    using Xunit;

    public sealed class TfsPullRequestSystemSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_SourceBranch_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(null, "foo", null));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_SourceBranch_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(new Uri("http://example.com"), null, null));

                // Then
                result.IsArgumentNullException("sourceBranch");
            }

            [Fact]
            public void Should_Throw_If_SourceBranch_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(new Uri("http://example.com"), string.Empty, null));

                // Then
                result.IsArgumentOutOfRangeException("sourceBranch");
            }

            [Fact]
            public void Should_Throw_If_SourceBranch_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(new Uri("http://example.com"), " ", null));

                // Then
                result.IsArgumentOutOfRangeException("sourceBranch");
            }

            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_PullRequestId_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(null, 0, null));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_Credentials_For_PullRequestId_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(new Uri("http://example.com"), 42, null));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Throw_If_Credentials_For_SourceBranch_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSystemSettings(new Uri("http://example.com"), "feature/foo", null));

                // Then
                result.IsArgumentNullException("credentials");
            }
        }
    }
}
