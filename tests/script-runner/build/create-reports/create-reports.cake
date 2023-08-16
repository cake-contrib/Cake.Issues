Task("Create-Reports")
    .Description("Creates all demo reports")
    .IsDependentOn("Create-Reports-Default");

Task("Create-Reports-Default")
    .Description("Creates default SARIF report")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        SarifIssueReportFormat(),
        data.RepoRootFolder,
        data.OutputFolder.CombineWithFilePath("report.sarif"));
});