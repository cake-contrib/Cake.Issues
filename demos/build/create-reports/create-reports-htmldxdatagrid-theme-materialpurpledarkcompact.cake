Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialPurpleDarkCompact")
    .Description("Creates HtmlDxDataGrid demo report for Material Purple Dark Compact theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialPurpleDarkCompact)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialpurpledarkcompact.html"));
});