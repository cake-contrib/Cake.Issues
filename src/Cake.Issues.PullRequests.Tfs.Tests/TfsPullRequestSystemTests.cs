namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using System;
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Cake.Tfs.Authentication;
    using Xunit;

    public sealed class TfsPullRequestSystemTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var settings =
                    new TfsPullRequestSystemSettings(
                        new Uri("https://google.com"),
                        123,
                        new TfsNtlmCredentials());

                // When
                var result = Record.Exception(() => new TfsPullRequestSystem(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                TfsPullRequestSystemSettings settings = null;

                // When
                var result = Record.Exception(() => new TfsPullRequestSystem(log, settings));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
