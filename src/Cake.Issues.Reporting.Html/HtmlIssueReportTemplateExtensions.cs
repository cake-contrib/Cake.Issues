namespace Cake.Issues.Reporting.Html
{
    using System;

    /// <summary>
    /// Extensions for <see cref="HtmlIssueReportTemplate"/> enumeration.
    /// </summary>
    internal static class HtmlIssueReportTemplateExtensions
    {
        /// <summary>
        /// Returns the name of the embedded template file.
        /// </summary>
        /// <param name="template">Template for which the embedded file name should be returned.</param>
        /// <returns>Name of the template file</returns>
        public static string GetTemplateResourceName(this HtmlIssueReportTemplate template)
        {
            switch (template)
            {
                case HtmlIssueReportTemplate.Diagnostic:
                    return "Diagnostic.cshtml";

                default:
                    throw new ArgumentOutOfRangeException(nameof(template));
            }
        }
    }
}
