namespace Cake.Issues.PullRequests.Tfs.Tests.Capabilities
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.PullRequests.Tfs.Capabilities;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using NSubstitute;
    using Xunit;

    public sealed class TfsFilteringByModifiedFilesCapabilityTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var pullRequestSystem = Substitute.For<ITfsPullRequestSystem>();

                // When
                var result = Record.Exception(() => new TfsFilteringByModifiedFilesCapability(log, pullRequestSystem));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_PullRequestSystem_Is_Null()
            {
                // Given
                var log = new FakeLog();
                TfsPullRequestSystem pullRequestSystem = null;

                // When
                var result = Record.Exception(() => new TfsFilteringByModifiedFilesCapability(log, pullRequestSystem));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }
        }
    }
}