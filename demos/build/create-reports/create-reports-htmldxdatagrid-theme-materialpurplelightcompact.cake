Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialPurpleLightCompact")
    .Description("Creates HtmlDxDataGrid demo report for Material Purple Light Compact theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialPurpleLightCompact)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialpurplelightcompact.html"));
});