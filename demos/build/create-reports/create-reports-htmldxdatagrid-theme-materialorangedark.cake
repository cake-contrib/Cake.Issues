Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeDark")
    .Description("Creates HtmlDxDataGrid demo report for Material Orange Dark theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialOrangeDark)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialorangedark.html"));
});