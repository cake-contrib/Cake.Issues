namespace Cake.Issues.PullRequests.AzureDevOps.Tests
{
    using System;
    using Cake.AzureDevOps.Authentication;
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
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(null, "foo", new AzureDevOpsNtlmCredentials()));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), null, new AzureDevOpsNtlmCredentials()));

                // Then
                result.IsArgumentNullException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), string.Empty, new AzureDevOpsNtlmCredentials()));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(new Uri("http://example.com"), " ", new AzureDevOpsNtlmCredentials()));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_RepositoryUrl_For_PullRequestId_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSystemSettings(null, 0, new AzureDevOpsNtlmCredentials()));

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
