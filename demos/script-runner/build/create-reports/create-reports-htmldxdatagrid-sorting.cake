Task("Create-Reports-HtmlDxDataGrid-Sorting")
    .Description("Creates HtmlDxDataGrid demo report showing how to change column sorting")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.SortedColumns, new List<ReportColumn> { ReportColumn.Rule })
                .WithOption(HtmlDxDataGridOption.RuleSortOrder, ColumnSortOrder.Descending)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-sorting.html"));
});