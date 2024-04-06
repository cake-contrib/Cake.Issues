namespace Cake.Issues.Reporting.Generic
{
    using System;

    /// <summary>
    /// Extension methods for the <see cref="HtmlDxPivotGridArea"/> enumeration.
    /// </summary>
    public static class HtmlDxPivotGridAreaExtensions
    {
        /// <summary>
        /// Returns the short identifier of the pivot grid area.
        /// </summary>
        /// <param name="area">Area for which the identifier should be returned.</param>
        /// <returns>Short identifier of the area.</returns>
        public static string ToJavaScriptIdentifier(this HtmlDxPivotGridArea area)
        {
            switch (area)
            {
                case HtmlDxPivotGridArea.Row:
                    return "row";
                case HtmlDxPivotGridArea.Column:
                    return "column";
                case HtmlDxPivotGridArea.Data:
                    return "data";
                case HtmlDxPivotGridArea.Filter:
                    return "filter";
                default:
                    throw new ArgumentException("Unknown enumeration value", nameof(area));
            }
        }
    }
}
