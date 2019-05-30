namespace Cake.Issues.Reporting.Generic.Tests
{
    using Shouldly;
    using Xunit;

    public sealed class DevExtremeThemeExtensionsTests
    {
        public sealed class TheGetCssFileNameMethod
        {
            [Theory]
            [InlineData(DevExtremeTheme.Light)]
            [InlineData(DevExtremeTheme.Dark)]
            [InlineData(DevExtremeTheme.Contrast)]
            [InlineData(DevExtremeTheme.Carmine)]
            [InlineData(DevExtremeTheme.DarkMoon)]
            [InlineData(DevExtremeTheme.SoftBlue)]
            [InlineData(DevExtremeTheme.DarkViolet)]
            [InlineData(DevExtremeTheme.GreenMist)]
            [InlineData(DevExtremeTheme.LightCompact)]
            [InlineData(DevExtremeTheme.DarkCompact)]
            [InlineData(DevExtremeTheme.ContrastCompact)]
            [InlineData(DevExtremeTheme.MaterialBlueLight)]
            [InlineData(DevExtremeTheme.MaterialLimeLight)]
            [InlineData(DevExtremeTheme.MaterialOrangeLight)]
            [InlineData(DevExtremeTheme.MaterialPurpleLight)]
            [InlineData(DevExtremeTheme.MaterialTealLight)]
            [InlineData(DevExtremeTheme.MaterialBlueDark)]
            [InlineData(DevExtremeTheme.MaterialLimeDark)]
            [InlineData(DevExtremeTheme.MaterialOrangeDark)]
            [InlineData(DevExtremeTheme.MaterialPurpleDark)]
            [InlineData(DevExtremeTheme.MaterialTealDark)]
            [InlineData(DevExtremeTheme.MaterialBlueLightCompact)]
            [InlineData(DevExtremeTheme.MaterialLimeLightCompact)]
            [InlineData(DevExtremeTheme.MaterialOrangeLightCompact)]
            [InlineData(DevExtremeTheme.MaterialPurpleLightCompact)]
            [InlineData(DevExtremeTheme.MaterialTealLightCompact)]
            [InlineData(DevExtremeTheme.MaterialBlueDarkCompact)]
            [InlineData(DevExtremeTheme.MaterialLimeDarkCompact)]
            [InlineData(DevExtremeTheme.MaterialOrangeDarkCompact)]
            [InlineData(DevExtremeTheme.MaterialPurpleDarkCompact)]
            [InlineData(DevExtremeTheme.MaterialTealDarkCompact)]
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
