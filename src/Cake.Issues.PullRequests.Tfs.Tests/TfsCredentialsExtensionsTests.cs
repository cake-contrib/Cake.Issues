namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using Cake.Issues.PullRequests.Tfs;
    using Cake.Issues.Testing;
    using Cake.Tfs.Authentication;
    using Shouldly;
    using Xunit;

    public sealed class TfsCredentialsExtensionsTests
    {
        public sealed class TheToVssCredentialsExtension
        {
            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                ITfsCredentials credentials = null;

                // When
                var result = Record.Exception(() => credentials.ToVssCredentials());

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Return_Ntlm_Credentials()
            {
                // Given
                var credentials = new TfsNtlmCredentials();

                // When
                var result = credentials.ToVssCredentials();

                // Then
                result.ShouldNotBeNull();
            }

            [Fact]
            public void Should_Return_Basic_Credentials()
            {
                // Given
                var credentials = new TfsBasicCredentials("foo", "bar");

                // When
                var result = credentials.ToVssCredentials();

                // Then
                result.ShouldNotBeNull();
            }

            [Fact]
            public void Should_Return_OAuth_Credentials()
            {
                // Given
                var credentials = new TfsOAuthCredentials("foo");

                // When
                var result = credentials.ToVssCredentials();

                // Then
                result.ShouldNotBeNull();
            }

            [Fact]
            public void Should_Return_Aad_Credentials()
            {
                // Given
                var credentials = new TfsAadCredentials("foo", "bar");

                // When
                var result = credentials.ToVssCredentials();

                // Then
                result.ShouldNotBeNull();
            }
        }
    }
}
