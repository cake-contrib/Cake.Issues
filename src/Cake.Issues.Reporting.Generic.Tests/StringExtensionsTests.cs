namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
    public sealed class StringExtensionsTests
    {
        public sealed class TheSanitizeHtmlIdAttributeExtension
        {
            [Theory]
            [InlineData(null, "")]
            [InlineData("", "")]
            [InlineData(" ", "")]
            [InlineData("foo", "foo")]
            [InlineData("foo123", "foo123")]
            [InlineData("foo:bar", "foo:bar")]
            [InlineData("foo-bar", "foo-bar")]
            [InlineData("foo_bar", "foo_bar")]
            [InlineData("foo.bar", "foo.bar")]
            [InlineData("foo bar", "foo-bar")]
            [InlineData("foo@bar", "foo-bar")]
            [InlineData("foo🐱bar", "foo-bar")]
            [InlineData("123foo", "foo")]
            public void Should_Sanitize_Input(string input, string expectedId)
            {
                // Given

                // When
                var result = input.SanitizeHtmlIdAttribute();

                // Then
                result.ShouldBe(expectedId);
            }
        }
    }
}
