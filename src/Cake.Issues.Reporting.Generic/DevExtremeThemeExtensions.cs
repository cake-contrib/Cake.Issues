namespace Cake.Issues.Reporting.Generic;

using System;

/// <summary>
/// Extension methods for the <see cref="DevExtremeTheme"/> enumeration.
/// </summary>
public static class DevExtremeThemeExtensions
{
    /// <summary>
    /// Returns the CSS file name for a specific DevExtreme theme.
    /// </summary>
    /// <param name="theme">Theme for which the CSS file name should be returned.</param>
    /// <returns>CSS file name of the DevExtreme theme.</returns>
    public static string GetCssFileName(this DevExtremeTheme theme) =>
        theme switch
        {
            DevExtremeTheme.Light => "dx.light.css",
            DevExtremeTheme.Dark => "dx.dark.css",
            DevExtremeTheme.Contrast => "dx.contrast.css",
            DevExtremeTheme.Carmine => "dx.carmine.css",
            DevExtremeTheme.DarkMoon => "dx.darkmoon.css",
            DevExtremeTheme.SoftBlue => "dx.softblue.css",
            DevExtremeTheme.DarkViolet => "dx.darkviolet.css",
            DevExtremeTheme.GreenMist => "dx.greenmist.css",
            DevExtremeTheme.LightCompact => "dx.light.compact.css",
            DevExtremeTheme.DarkCompact => "dx.dark.compact.css",
            DevExtremeTheme.ContrastCompact => "dx.contrast.compact.css",
            DevExtremeTheme.MaterialBlueLight => "dx.material.blue.light.css",
            DevExtremeTheme.MaterialLimeLight => "dx.material.lime.light.css",
            DevExtremeTheme.MaterialOrangeLight => "dx.material.orange.light.css",
            DevExtremeTheme.MaterialPurpleLight => "dx.material.purple.light.css",
            DevExtremeTheme.MaterialTealLight => "dx.material.teal.light.css",
            DevExtremeTheme.MaterialBlueDark => "dx.material.blue.dark.css",
            DevExtremeTheme.MaterialLimeDark => "dx.material.lime.dark.css",
            DevExtremeTheme.MaterialOrangeDark => "dx.material.orange.dark.css",
            DevExtremeTheme.MaterialPurpleDark => "dx.material.purple.dark.css",
            DevExtremeTheme.MaterialTealDark => "dx.material.teal.dark.css",
            DevExtremeTheme.MaterialBlueLightCompact => "dx.material.blue.light.compact.css",
            DevExtremeTheme.MaterialLimeLightCompact => "dx.material.lime.light.compact.css",
            DevExtremeTheme.MaterialOrangeLightCompact => "dx.material.orange.light.compact.css",
            DevExtremeTheme.MaterialPurpleLightCompact => "dx.material.purple.light.compact.css",
            DevExtremeTheme.MaterialTealLightCompact => "dx.material.teal.light.compact.css",
            DevExtremeTheme.MaterialBlueDarkCompact => "dx.material.blue.dark.compact.css",
            DevExtremeTheme.MaterialLimeDarkCompact => "dx.material.lime.dark.compact.css",
            DevExtremeTheme.MaterialOrangeDarkCompact => "dx.material.orange.dark.compact.css",
            DevExtremeTheme.MaterialPurpleDarkCompact => "dx.material.purple.dark.compact.css",
            DevExtremeTheme.MaterialTealDarkCompact => "dx.material.teal.dark.compact.css",
            DevExtremeTheme.FluentBlueLight => "dx.fluent.blue.light.css",
            DevExtremeTheme.FluentSaaSLight => "dx.fluent.saas.light.css",
            DevExtremeTheme.FluentBlueLightCompact => "dx.fluent.blue.light.compact.css",
            DevExtremeTheme.FluentSaaSLightCompact => "dx.fluent.saas.light.compact.css",
            DevExtremeTheme.FluentBlueDark => "dx.fluent.blue.dark.css",
            DevExtremeTheme.FluentSaaSDark => "dx.fluent.saas.dark.css",
            DevExtremeTheme.FluentBlueDarkCompact => "dx.fluent.blue.dark.compact.css",
            DevExtremeTheme.FluentSaaSDarkCompact => "dx.fluent.saas.dark.compact.css",
            _ => throw new ArgumentException("Unknown enumeration value", nameof(theme)),
        };
}
