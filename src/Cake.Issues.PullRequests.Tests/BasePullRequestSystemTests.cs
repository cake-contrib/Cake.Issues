namespace Cake.Issues.PullRequests.Tests;

using Cake.Core.Diagnostics;

public sealed class BasePullRequestSystemTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            const ICakeLog log = null;

            // When
            var result = Record.Exception(() => new FakePullRequestSystem(log));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Set_Log()
        {
            // Given
            var log = new FakeLog();

            // When
            var pullRequestSystem = new FakePullRequestSystem(log);

            // Then
            pullRequestSystem.Log.ShouldBe(log);
        }
    }

    public sealed class TheAddCapabilityMethod
    {
        [Fact]
        public void Should_Throw_If_Capability_Is_Null()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);
            const IPullRequestSystemCapability capability = null;

            // When
            var result = Record.Exception(() => pullRequestSystem.AddCapability(capability));

            // Then
            result.IsArgumentNullException("capability");
        }

        [Fact]
        public void Should_Add_Capability()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);

            // When
            pullRequestSystem.AddCapability(capability);

            // Then
            pullRequestSystem.HasCapability<FakePullRequestSystemCapability>().ShouldBeTrue();
        }
    }

    public sealed class TheHasCapabilityMethod
    {
        [Fact]
        public void Should_Return_True_If_PullRequestSystem_Has_Capability()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);

            // When
            pullRequestSystem.AddCapability(capability);

            // Then
            pullRequestSystem.HasCapability<FakePullRequestSystemCapability>().ShouldBeTrue();
        }

        [Fact]
        public void Should_Return_False_If_PullRequestSystem_Does_Not_Have_Capability()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);

            // When
            pullRequestSystem.AddCapability(capability);

            // Then
            pullRequestSystem.HasCapability<FakeCheckingCommitIdCapability>().ShouldBeFalse();
        }
    }

    public sealed class TheGetCapabilityMethod
    {
        [Fact]
        public void Should_Return_Capability_If_Capability_Exists()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);
            pullRequestSystem.AddCapability(capability);

            // When
            var result = pullRequestSystem.GetCapability<FakePullRequestSystemCapability>();

            // Then
            result.ShouldBe(capability);
        }

        [Fact]
        public void Should_Return_Null_If_Capability_Does_Not_Exist()
        {
            // Given
            var log = new FakeLog();
            var pullRequestSystem = new FakePullRequestSystem(log);
            var capability = new FakePullRequestSystemCapability(log, pullRequestSystem);
            pullRequestSystem.AddCapability(capability);

            // When
            var result = pullRequestSystem.GetCapability<FakeCheckingCommitIdCapability>();

            // Then
            result.ShouldBeNull();
        }
    }

    public sealed class ThePostDiscussionThreadsMethod
    {
        [Fact]
        public void Should_Throw_If_Settings_Is_Null()
        {
            // Given
            var pullRequestSystem = new FakePullRequestSystem(new FakeLog());

            // When
            var result = Record.Exception(() => pullRequestSystem.PostDiscussionThreads([], "Foo"));

            // Then
            result.IsInvalidOperationException("Initialize needs to be called first.");
        }
    }
}
