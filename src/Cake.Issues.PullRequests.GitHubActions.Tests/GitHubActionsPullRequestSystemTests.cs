namespace Cake.Issues.PullRequests.GitHubActions.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Xunit;

    public sealed class GitHubActionsPullRequestSystemTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;
                var settings =
                    new GitHubActionsBuildSettings();

                // When
                var result = Record.Exception(() => new GitHubActionsPullRequestSystem(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                const GitHubActionsBuildSettings settings = null;

                // When
                var result = Record.Exception(() => new GitHubActionsPullRequestSystem(log, settings));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
