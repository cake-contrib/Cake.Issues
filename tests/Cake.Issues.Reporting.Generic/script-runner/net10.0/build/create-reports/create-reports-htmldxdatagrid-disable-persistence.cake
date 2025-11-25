Task("Create-Reports-HtmlDxDataGrid-Disable-Persistence")
    .Description("Creates HtmlDxDataGrid demo report showing how to disable state persistence.")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.PersistState, false)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-disable-persistence.html"));
});