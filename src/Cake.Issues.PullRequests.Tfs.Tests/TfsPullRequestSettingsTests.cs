namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using System;
    using Issues.Testing;
    using Xunit;

    public class TfsPullRequestSettingsTests
    {
        public sealed class TheTfsPullRequestSettings
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_SourceBranch_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSettings(null, "foo", null));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_SourceBranch_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSettings(new Uri("http://example.com"), null, null));

                // Then
                result.IsArgumentNullException("sourceBranch");
            }

            [Fact]
            public void Should_Throw_If_SourceBranch_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSettings(new Uri("http://example.com"), string.Empty, null));

                // Then
                result.IsArgumentOutOfRangeException("sourceBranch");
            }

            [Fact]
            public void Should_Throw_If_SourceBranch_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSettings(new Uri("http://example.com"), " ", null));

                // Then
                result.IsArgumentOutOfRangeException("sourceBranch");
            }

            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_PullRequestId_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsPullRequestSettings(null, 0, null));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }
        }
    }
}
