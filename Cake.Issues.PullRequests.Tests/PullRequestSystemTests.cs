namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class PullRequestSystemTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new FakePullRequestSystem(null));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();

                // When
                var prSystem = new FakePullRequestSystem(log);

                // Then
                prSystem.Log.ShouldBe(log);
            }
        }

        public sealed class TheGetPreferredCommentFormatMethod
        {
            [Fact]
            public void Should_Return_PlainText()
            {
                // Given
                var prSystem = new FakePullRequestSystem(new FakeLog());

                // When
                var result = prSystem.GetPreferredCommentFormat();

                // Then
                result.ShouldBe(IssueCommentFormat.PlainText);
            }
        }

        public sealed class TheFetchActiveDiscussionThreadsMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var prSystem = new FakePullRequestSystem(new FakeLog());

                // When
                var result = Record.Exception(() => prSystem.FetchDiscussionThreads("Foo"));

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }
        }

        public sealed class TheGetModifiedFilesInPullRequestMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var prSystem = new FakePullRequestSystem(new FakeLog());

                // When
                var result = Record.Exception(() => prSystem.GetModifiedFilesInPullRequest());

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }
        }

        public sealed class TheMarkThreadsAsFixedMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var prSystem = new FakePullRequestSystem(new FakeLog());

                // When
                var result = Record.Exception(() => prSystem.ResolveDiscussionThreads(new List<IPullRequestDiscussionThread>()));

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }
        }

        public sealed class ThePostDiscussionThreadsMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var prSystem = new FakePullRequestSystem(new FakeLog());

                // When
                var result = Record.Exception(() => prSystem.PostDiscussionThreads(new List<IIssue>(), "Foo"));

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }
        }
    }
}
