namespace Cake.Issues.Reporting.Generic.Tests
{
    using Shouldly;
    using Xunit;

    public sealed class HtmlDxPivotGridAreaExtensionsTests
    {
        public sealed class TheToJavaScriptIdentifierMethod
        {
            [Theory]
            [InlineData(HtmlDxPivotGridArea.Column)]
            [InlineData(HtmlDxPivotGridArea.Row)]
            [InlineData(HtmlDxPivotGridArea.Filter)]
            [InlineData(HtmlDxPivotGridArea.Data)]
            public void Should_Return_Identifier(HtmlDxPivotGridArea area)
            {
                // Given

                // When
                var result = area.ToJavaScriptIdentifier();

                // Then
                result.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}
