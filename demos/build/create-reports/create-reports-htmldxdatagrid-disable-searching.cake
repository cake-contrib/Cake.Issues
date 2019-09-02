Task("Create-Reports-HtmlDxDataGrid-Disable-Searching")
    .Description("Creates HtmlDxDataGrid demo report showing how to disable searching functionality")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.EnableSearching, false)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-disablesearching.html"));
});