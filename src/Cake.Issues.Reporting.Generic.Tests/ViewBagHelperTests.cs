namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
    public sealed class ViewBagHelperTests
    {
        public sealed class TheValueOrDefaultMethod
        {
            [Fact]
            public void Should_Return_Value_If_Not_Null()
            {
                // Given
                var value = "foo";
                var defaultValue = "bar";

                // When
                var result = ViewBagHelper.ValueOrDefault(value, defaultValue);

                // Then
                result.ShouldBe(value);
            }

            [Fact]
            public void Should_Return_Default_If_Value_Is_Null()
            {
                // Given
                string value = null;
                var defaultValue = "bar";

                // When
                var result = ViewBagHelper.ValueOrDefault(value, defaultValue);

                // Then
                result.ShouldBe(defaultValue);
            }
        }
    }
}
