namespace Cake.Issues.Tests;

public sealed class StringExtensionsTests
{
    public sealed class TheToStringWithNullMarkupMethod
    {
        [Fact]
        public void Should_Return_NotSetString_When_Value_Is_Null()
        {
            // Given
            object value = null;

            // When
            var result = value.ToStringWithNullMarkup();

            // Then
            result.ShouldBe("[italic grey]Not set[/]");
        }

        [Theory]
        [InlineData("foo", "foo")]
        [InlineData(1, "1")]
        public void Should_Return_Value_ToString_When_Value_Is_Not_Null(object value, string expected)
        {
            // Given

            // When
            var result = value.ToStringWithNullMarkup();

            // Then
            result.ShouldBe(expected);
        }
    }
}
