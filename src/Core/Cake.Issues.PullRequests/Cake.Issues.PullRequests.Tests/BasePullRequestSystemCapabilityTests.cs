namespace Cake.Issues.PullRequests.Tests;

using Cake.Core.Diagnostics;

public sealed class BasePullRequestSystemCapabilityTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            const ICakeLog log = null;
            var pullRequestSystem = new FakePullRequestSystem(new FakeLog());

            // When
            var result = Record.Exception(() => new FakePullRequestSystemCapability(log, pullRequestSystem));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_PullRequestSystem_Is_Null()
        {
            // Given
            var log = new FakeLog();
            const FakePullRequestSystem pullRequestSystem = null;

            // When
            var result = Record.Exception(() => new FakePullRequestSystemCapability(log, pullRequestSystem));

            // Then
            result.IsArgumentNullException("pullRequestSystem");
        }

        [Fact]
        public void Should_Set_Log()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);

            // When
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);

            // Then
            capability.Log.ShouldBe(log);
        }

        [Fact]
        public void Should_Set_PullRequestSystem()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);

            // When
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);

            // Then
            capability.PullRequestSystem.ShouldBe(pullRequestSystem);
        }
    }
}
