namespace Cake.Issues.Reporting.Generic;

/// <summary>
/// Available display modes for <see cref="GenericIssueReportTemplate.HtmlDxDataGrid"/> template.
/// </summary>
public enum HtmlDxDataGridDisplayMode
{
    /// <summary>
    /// Issues are displayed in pages with a pager in the footer.
    /// </summary>
    Paged,

    /// <summary>
    /// More issues are loaded when the user reaches the end of the grid.
    /// </summary>
    InfiniteScroll,
}
