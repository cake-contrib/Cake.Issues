namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Default templates provided by this addin.
    /// </summary>
    public enum GenericIssueReportTemplate
    {
        /// <summary>
        /// Template for a HTML report containing a list of all issues with all properties.
        /// </summary>
        HtmlDiagnostic,

        /// <summary>
        /// Template for a HTML report containing a rich data table view with sorting and search functionality.
        /// </summary>
        HtmlDataTable,

        /// <summary>
        /// Template for a HTML report containing a rich data grid with sorting, filtering, grouping and search capabilities.
        /// See <see cref="HtmlDxDataGridOption"/> for template specific options.
        /// </summary>
        HtmlDxDataGrid
    }
}
