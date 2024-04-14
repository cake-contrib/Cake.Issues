namespace Cake.Issues.PullRequests.AppVeyor.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

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
}