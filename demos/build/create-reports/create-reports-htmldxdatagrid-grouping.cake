Task("Create-Reports-HtmlDxDataGrid-Grouping")
    .Description("Creates HtmlDxDataGrid demo report showing how to define column grouping")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.GroupedColumns, new List<ReportColumn> { ReportColumn.Rule })),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-grouping.html"));
});