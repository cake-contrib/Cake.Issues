Task("Create-Reports")
    .Description("Creates all demo reports")
    .IsDependentOn("Create-Reports-Default");

Task("Create-Reports-Default")
    .Description("Creates default report")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDxDataGrid),
        data.RepoRootFolder,
        data.OutputFolder.CombineWithFilePath("report.html"));
});