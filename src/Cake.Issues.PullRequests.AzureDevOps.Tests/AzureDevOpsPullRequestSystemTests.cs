namespace Cake.Issues.PullRequests.AzureDevOps.Tests;

using Cake.AzureDevOps.Authentication;
using Cake.Core.Diagnostics;

public sealed class AzureDevOpsPullRequestSystemTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            const ICakeLog log = null;
            var settings =
                new AzureDevOpsPullRequestSystemSettings(
                    new Uri("https://google.com"),
                    123,
                    new AzureDevOpsNtlmCredentials());

            // When
            var result = Record.Exception(() => new AzureDevOpsPullRequestSystem(log, settings));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given
            var log = new FakeLog();
            const AzureDevOpsPullRequestSystemSettings settings = null;

            // When
            var result = Record.Exception(() => new AzureDevOpsPullRequestSystem(log, settings));

            // Then
            result.IsArgumentNullException("settings");
        }
    }
}
