Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialBlueDarkCompact")
    .Description("Creates HtmlDxDataGrid demo report for Material Blue Dark Compact theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialBlueDarkCompact)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialbluedarkcompact.html"));
});