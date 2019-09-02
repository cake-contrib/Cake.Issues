Task("Create-Reports-HtmlDxDataGrid-Enable-Exporting")
    .Description("Creates HtmlDxDataGrid demo report showing how to enable exporting functionality")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.EnableExporting, true)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-enableexporting.html"));
});