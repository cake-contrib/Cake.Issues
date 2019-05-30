namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public sealed class DevExtremeThemeExtensionsTests
    {
        public sealed class TheGetCssFileNameMethod
        {
            public static IEnumerable<object[]> DevExtremeThemes()
            {
                foreach (var number in Enum.GetValues(typeof(DevExtremeTheme)))
                {
                    yield return new object[] { number };
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
