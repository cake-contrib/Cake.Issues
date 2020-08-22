Task("Create-Reports-HtmlDataTable-Default")
    .Description("Creates HtmlDataTable default demo report")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDataTable),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldatatable-demo-default.html"));
});