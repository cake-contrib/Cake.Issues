Task("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeLight")
    .Description("Creates HtmlDxDataGrid demo report for Material Orange Light theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialOrangeLight)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materialorangelight.html"));
});