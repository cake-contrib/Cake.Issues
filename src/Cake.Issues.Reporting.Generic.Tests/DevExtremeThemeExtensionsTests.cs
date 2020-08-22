namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
    public sealed class DevExtremeThemeExtensionsTests
    {
        public sealed class TheGetCssFileNameMethod
        {
            public static IEnumerable<object[]> DevExtremeThemes()
            {
                foreach (var number in Enum.GetValues(typeof(DevExtremeTheme)))
                {
                    yield return new[] { number };
                }
            }

            [Theory]
            [MemberData(nameof(DevExtremeThemes))]
            public void Should_Return_FileName(DevExtremeTheme theme)
            {
                // Given

                // When
                var result = theme.GetCssFileName();

                // Then
                result.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}
