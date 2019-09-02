Task("Create-Reports-HtmlDxDataGrid-Disable-Grouping")
    .Description("Creates HtmlDxDataGrid demo report showing how to disable grouping functionality")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.EnableGrouping, false)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-disablegrouping.html"));
});