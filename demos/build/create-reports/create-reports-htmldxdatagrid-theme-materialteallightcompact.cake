Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialTealLightCompact")
    .Description("Creates HtmlDxDataGrid demo report for Material Teal Light Compact theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialTealLightCompact)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialteallightcompact.html"));
});