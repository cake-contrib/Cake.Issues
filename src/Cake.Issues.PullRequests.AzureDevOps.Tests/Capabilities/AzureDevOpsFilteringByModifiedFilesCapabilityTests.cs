namespace Cake.Issues.PullRequests.AzureDevOps.Tests.Capabilities
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.PullRequests.AzureDevOps.Capabilities;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using NSubstitute;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class AzureDevOpsFilteringByModifiedFilesCapabilityTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;
                var pullRequestSystem = Substitute.For<IAzureDevOpsPullRequestSystem>();

                // When
                var result = Record.Exception(() => new AzureDevOpsFilteringByModifiedFilesCapability(log, pullRequestSystem));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_PullRequestSystem_Is_Null()
            {
                // Given
                var log = new FakeLog();
                const AzureDevOpsPullRequestSystem pullRequestSystem = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsFilteringByModifiedFilesCapability(log, pullRequestSystem));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }
        }
    }
}