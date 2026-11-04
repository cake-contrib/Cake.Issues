Task("Create-Reports-Console-Summaries")
    .Description("Creates the Console report example with summaries")
    .Does(() =>
{
    Information("Running Console Report with Provider and Priority Summaries");

    var issues = DeserializeIssuesFromJsonFile(@"build/create-reports/sample-issues.json");
    Information("Read {0} issues", issues.Count());

    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        @".",
        string.Empty);
});
