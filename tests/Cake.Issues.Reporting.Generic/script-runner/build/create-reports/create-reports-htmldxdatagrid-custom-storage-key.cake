Task("Create-Reports-HtmlDxDataGrid-Custom-Storage-Key")
    .Description("Creates HtmlDxDataGrid demo report showing how to use a custom key for state persistence.")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.StorageKey, "CustomStorageKey")),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-custom-storage-key.html"));
});