namespace Cake.Issues.Reporting.Generic
{
    using System;

    /// <summary>
    /// Extensions for <see cref="GenericIssueReportTemplate"/> enumeration.
    /// </summary>
    internal static class GenericIssueReportTemplateExtensions
    {
        /// <summary>
        /// Returns the name of the embedded template file.
        /// </summary>
        /// <param name="template">Template for which the embedded file name should be returned.</param>
        /// <returns>Name of the template file</returns>
        public static string GetTemplateResourceName(this GenericIssueReportTemplate template)
        {
            switch (template)
            {
                case GenericIssueReportTemplate.HtmlDiagnostic:
                    return "Diagnostic.cshtml";

                default:
                    throw new ArgumentOutOfRangeException(nameof(template));
            }
        }
    }
}
