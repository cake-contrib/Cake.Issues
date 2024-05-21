namespace Cake.Issues.PullRequests.AppVeyor.Tests;

using System.Diagnostics.CodeAnalysis;

public sealed class AppVeyorBuildSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_MessagePattern_Is_Null()
        {
            // Given
            string messagePattern = null;
            var settings = new AppVeyorBuildSettings();

            // When
            var result = Record.Exception(() => settings.MessagePattern = messagePattern);

            // Then
            result.IsArgumentNullException("value");
        }

        [Fact]
        public void Should_Throw_If_DetailsPattern_Is_Null()
        {
            // Given
            string detailsPattern = null;
            var settings = new AppVeyorBuildSettings();

            // When
            var result = Record.Exception(() => settings.DetailsPattern = detailsPattern);

            // Then
            result.IsArgumentNullException("value");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("foo")]
        [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Want to split between Given/When/Then")]
        public void Should_Set_MessagePattern(string messagePattern)
        {
            // Given
            var settings = new AppVeyorBuildSettings();

            // When
            settings.MessagePattern = messagePattern;

            // Then
            settings.MessagePattern.ShouldBe(messagePattern);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("foo")]
        [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Want to split between Given/When/Then")]
        public void Should_Set_DetailsPattern(string detailsPattern)
        {
            // Given
            var settings = new AppVeyorBuildSettings();

            // When
            settings.DetailsPattern = detailsPattern;

            // Then
            settings.DetailsPattern.ShouldBe(detailsPattern);
        }
    }
}