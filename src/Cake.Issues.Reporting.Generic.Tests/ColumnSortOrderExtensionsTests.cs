namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
    public sealed class ColumnSortOrderExtensionsTests
    {
        public sealed class TheToShortStringMethod
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Return_Identifier(ColumnSortOrder sortOrder)
            {
                // Given

                // When
                var result = sortOrder.ToShortString();

                // Then
                result.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}
