namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using NSubstitute;
    using Xunit;

    public sealed class TfsDiscussionThreadsCapabilityTests
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
                var result = Record.Exception(() => new TfsDiscussionThreadsCapability(log, pullRequestSystem));

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
                var result = Record.Exception(() => new TfsDiscussionThreadsCapability(log, pullRequestSystem));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }
        }
    }
}