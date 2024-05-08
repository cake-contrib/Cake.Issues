namespace Cake.Issues.PullRequests.Tests
{
    public sealed class ISupportCheckingCommitIdExtensionsTests
    {
        public sealed class TheIsCurrentCommitIdExtension
        {
            [Fact]
            public void Should_Throw_If_PullRequestSystem_Is_Null()
            {
                // Given
                const ISupportCheckingCommitId capability = null;

                // When
                var result = Record.Exception(() => capability.IsCurrentCommitId(string.Empty));

                // Then
                result.IsArgumentNullException("capability");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Null()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = null;

                // When
                var result =
                    Record.Exception(() =>
                        capability.IsCurrentCommitId(commitId));

                // Then
                result.IsArgumentNullException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Empty()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                var commitId = string.Empty;

                // When
                var result =
                    Record.Exception(() =>
                        capability.IsCurrentCommitId(commitId));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Whitespace()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = " ";

                // When
                var result =
                    Record.Exception(() =>
                        capability.IsCurrentCommitId(commitId));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Fact]
            public void Should_Return_True_If_Commit_Is_Current()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                capability.LastSourceCommitId = commitId;

                // When
                var result = capability.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBeTrue();
            }

            [Fact]
            public void Should_Ignore_Casing()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                capability.LastSourceCommitId = commitId.ToLowerInvariant();

                // When
                var result = capability.IsCurrentCommitId(commitId.ToUpperInvariant());

                // Then
                result.ShouldBeTrue();
            }

            [Fact]
            public void Should_Return_False_If_Commit_Is_Different()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                capability.LastSourceCommitId = "9ebcec39e16c39b5ffcb10f253d0c2bcf8438cf6";

                // When
                var result = capability.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBeFalse();
            }

            [Fact]
            public void Should_Return_False_If_PullRequestSystems_Returns_Null()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                capability.LastSourceCommitId = null;

                // When
                var result = capability.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBeFalse();
            }

            [Fact]
            public void Should_Return_False_If_PullRequestSystems_Returns_Empty_String()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                capability.LastSourceCommitId = string.Empty;

                // When
                var result = capability.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBeFalse();
            }

            [Fact]
            public void Should_Return_False_If_PullRequestSystems_Returns_Whitespace()
            {
                // Given
                var pullRequestSystem =
                    FakePullRequestSystemBuilder
                        .NewPullRequestSystem()
                        .WithCheckingCommitIdCapability()
                        .Create();
                var capability = pullRequestSystem.CheckingCommitIdCapability;
                const string commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                capability.LastSourceCommitId = " ";

                // When
                var result = capability.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBeFalse();
            }
        }
    }
}
