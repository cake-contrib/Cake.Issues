Task("Create-Reports-Console-Default")
    .Description("Creates the default Console report example")
    .Does(() =>
{
    Information("Running Console Report with Default Settings");

    var issues = DeserializeIssuesFromJsonFile(@"build/create-reports/sample-issues.json");
    Information("Read {0} issues", issues.Count());

    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                ShowDiagnostics = true
            }),
        @".",
        string.Empty);
});
