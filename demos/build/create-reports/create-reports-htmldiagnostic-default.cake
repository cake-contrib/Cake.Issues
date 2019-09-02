Task("Create-Reports-HtmlDiagnostic-Default")
    .Description("Creates HtmlDiagnostic default demo report")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldiagnostic-demo-default.html"));
});