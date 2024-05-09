namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    public sealed class BaseFilteringByModifiedFilesCapabilityTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;
                var pullRequestSystem = new FakePullRequestSystem(new FakeLog());
                var modifiedFiles = new List<FilePath>();

                // When
                var result =
                    Record.Exception(() =>
                        new FakeFilteringByModifiedFilesCapability(log, pullRequestSystem, modifiedFiles));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_PullRequestSystem_Is_Null()
            {
                // Given
                var log = new FakeLog();
                const FakePullRequestSystem pullRequestSystem = null;
                var modifiedFiles = new List<FilePath>();

                // When
                var result =
                    Record.Exception(() =>
                        new FakeFilteringByModifiedFilesCapability(log, pullRequestSystem, modifiedFiles));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var modifiedFiles = new List<FilePath>();

                // When
                var capability = new FakeFilteringByModifiedFilesCapability(log, pullRequestSystem, modifiedFiles);

                // Then
                capability.Log.ShouldBe(log);
            }

            [Fact]
            public void Should_Set_PullRequestSystem()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var modifiedFiles = new List<FilePath>();

                // When
                var capability = new FakeFilteringByModifiedFilesCapability(log, pullRequestSystem, modifiedFiles);

                // Then
                capability.PullRequestSystem.ShouldBe(pullRequestSystem);
            }
        }

        public sealed class TheGetModifiedFilesInPullRequestMethod
        {
            [Fact]
            public void Should_Throw_If_Not_Initialized()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var modifiedFiles = new List<FilePath>();
                var capability = new FakeFilteringByModifiedFilesCapability(log, pullRequestSystem, modifiedFiles);

                // When
                var result =
                    Record.Exception(() =>
                        capability.GetModifiedFilesInPullRequest());

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }

            [Fact]
            public void Should_Call_InternalGetModifiedFilesInPullRequest()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var settings = new ReportIssuesToPullRequestSettings(@"c:\repo");
                var modifiedFiles = new List<FilePath> { "foo.cs" };
                var capability = new FakeFilteringByModifiedFilesCapability(log, pullRequestSystem, modifiedFiles);

                // When
                pullRequestSystem.Initialize(settings);
                var result = capability.GetModifiedFilesInPullRequest();

                // Then
                result.ShouldBe(modifiedFiles);
            }
        }
    }
}
