namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Diagnostics.CodeAnalysis;

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
