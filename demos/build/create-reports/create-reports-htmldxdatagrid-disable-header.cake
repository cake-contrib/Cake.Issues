Task("Create-Reports-HtmlDxDataGrid-Disable-Header")
    .Description("Creates HtmlDxDataGrid demo report showing how to disable the report header")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.ShowHeader, false)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-disableheader.html"));
});