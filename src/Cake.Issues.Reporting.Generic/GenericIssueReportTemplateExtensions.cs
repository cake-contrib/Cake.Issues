namespace Cake.Issues.Reporting.Generic
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="GenericIssueReportTemplate"/> enumeration.
    /// </summary>
    internal static class GenericIssueReportTemplateExtensions
    {
        /// <summary>
        /// Returns the name of the embedded template file.
        /// </summary>
        /// <param name="template">Template for which the embedded file name should be returned.</param>
        /// <returns>Name of the template file.</returns>
        public static string GetTemplateResourceName(this GenericIssueReportTemplate template)
        {
            return template switch
            {
                GenericIssueReportTemplate.HtmlDiagnostic => "Diagnostic.cshtml",
                GenericIssueReportTemplate.HtmlDataTable => "DataTable.cshtml",
                GenericIssueReportTemplate.HtmlDxDataGrid => "DxDataGrid.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(template)),
            };
        }
    }
}
