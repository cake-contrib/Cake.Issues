namespace Cake.Issues.PullRequests.Tfs.Tests.Authentication
{
    using Shouldly;
    using Tfs.Authentication;
    using Xunit;

    public class TfsNtlmCredentialsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Not_Throw()
            {
                // Given / When
                var credentials = new TfsNtlmCredentials();

                // Then
                credentials.ShouldBeOfType<TfsNtlmCredentials>();
            }
        }
    }
}
