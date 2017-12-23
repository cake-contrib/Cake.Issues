namespace Cake.Issues.PullRequests.Tests
{
    using Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IPullRequestSystemExtensionTests
    {
        public sealed class TheIsCurrentCommitIdExtension
        {
            [Fact]
            public void Should_Throw_If_PullRequestSystem_Is_Null()
            {
                // Given
                IPullRequestSystem pullRequestSystem = null;

                // When
                var result = Record.Exception(() => pullRequestSystem.IsCurrentCommitId(string.Empty));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Null()
            {
                // Given
                var fixture = new PullRequestsFixture();
                string commitId = null;

                // When
                var result = Record.Exception(() => fixture.PullRequestSystem.IsCurrentCommitId(commitId));

                // Then
                result.IsArgumentNullException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Empty()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = string.Empty;

                // When
                var result = Record.Exception(() => fixture.PullRequestSystem.IsCurrentCommitId(commitId));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Fact]
            public void Should_Throw_If_CommitId_Is_Whitespace()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = " ";

                // When
                var result = Record.Exception(() => fixture.PullRequestSystem.IsCurrentCommitId(commitId));

                // Then
                result.IsArgumentOutOfRangeException("commitId");
            }

            [Fact]
            public void Should_Return_True_If_Commit_Is_Current()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                fixture.PullRequestSystem.LastSourceCommitId = commitId;

                // When
                var result = fixture.PullRequestSystem.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBe(true);
            }

            [Fact]
            public void Should_Ignore_Casing()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                fixture.PullRequestSystem.LastSourceCommitId = commitId.ToLowerInvariant();

                // When
                var result = fixture.PullRequestSystem.IsCurrentCommitId(commitId.ToUpperInvariant());

                // Then
                result.ShouldBe(true);
            }

            [Fact]
            public void Should_Return_False_If_Commit_Is_Different()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                fixture.PullRequestSystem.LastSourceCommitId = "9ebcec39e16c39b5ffcb10f253d0c2bcf8438cf6";

                // When
                var result = fixture.PullRequestSystem.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBe(false);
            }

            [Fact]
            public void Should_Return_False_If_PullRequestSystems_Returns_Null()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                fixture.PullRequestSystem.LastSourceCommitId = null;

                // When
                var result = fixture.PullRequestSystem.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBe(false);
            }

            [Fact]
            public void Should_Return_False_If_PullRequestSystems_Returns_Empty_String()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                fixture.PullRequestSystem.LastSourceCommitId = string.Empty;

                // When
                var result = fixture.PullRequestSystem.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBe(false);
            }

            [Fact]
            public void Should_Return_False_If_PullRequestSystems_Returns_Whitespace()
            {
                // Given
                var fixture = new PullRequestsFixture();
                var commitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";
                fixture.PullRequestSystem.LastSourceCommitId = " ";

                // When
                var result = fixture.PullRequestSystem.IsCurrentCommitId(commitId);

                // Then
                result.ShouldBe(false);
            }
        }
    }
}
