Task("Create-Reports-HtmlDxDataGrid-Theme-DarkViolet")
    .Description("Creates HtmlDxDataGrid demo report for dark violet theme")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.DarkViolet)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-darkviolet.html"));
});