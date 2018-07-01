namespace Cake.Issues.Reporting.Generic
{
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
        public static string GetCssFileName(this DevExtremeTheme theme)
        {
            switch (theme)
            {
                case DevExtremeTheme.Light:
                    return "dx.light.css";
                case DevExtremeTheme.Dark:
                    return "dx.dark.css";
                case DevExtremeTheme.Contrast:
                    return "dx.contrast.css";
                case DevExtremeTheme.Carmine:
                    return "dx.carmine.css";
                case DevExtremeTheme.DarkMoon:
                    return "dx.darkmoon.css";
                case DevExtremeTheme.SoftBlue:
                    return "dx.softblue.css";
                case DevExtremeTheme.DarkViolet:
                    return "dx.darkviolet.css";
                case DevExtremeTheme.GreenMist:
                    return "dx.greenmist.css";
                case DevExtremeTheme.LightCompact:
                    return "dx.light.compact.css";
                case DevExtremeTheme.DarkCompact:
                    return "dx.dark.compact.css";
                case DevExtremeTheme.ContrastCompact:
                    return "dx.contrast.compact.css";
                case DevExtremeTheme.MaterialBlueLight:
                    return "dx.material.blue.light.css";
                case DevExtremeTheme.MaterialLimeLight:
                    return "dx.material.lime.light.css";
                case DevExtremeTheme.MaterialOrangeLight:
                    return "dx.material.orange.light.css";
                case DevExtremeTheme.MaterialPurpleLight:
                    return "dx.material.purple.light.css";
                case DevExtremeTheme.MaterialTealLight:
                    return "dx.material.teal.light.css";
                default:
                    throw new ArgumentException("Unknown enumeration value", nameof(theme));
            }
        }
    }
}
