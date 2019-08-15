Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeDarkCompact")
    .Description("Creates HtmlDxDataGrid demo report for Material Orange Dark Compact theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialOrangeDarkCompact)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialorangedarkcompact.html"));
});