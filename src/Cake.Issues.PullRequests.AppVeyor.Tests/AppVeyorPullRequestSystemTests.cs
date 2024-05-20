namespace Cake.Issues.PullRequests.AppVeyor.Tests;

using Cake.Core;

public sealed class AppVeyorPullRequestSystemTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            const ICakeContext context = null;
            var settings = new AppVeyorBuildSettings();

            // When
            var result = Record.Exception(() => new AppVeyorPullRequestSystem(context, settings));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            const AppVeyorBuildSettings settings = null;

            // When
            var result = Record.Exception(() => new AppVeyorPullRequestSystem(context, settings));

            // Then
            result.IsArgumentNullException("settings");
        }
    }
}
