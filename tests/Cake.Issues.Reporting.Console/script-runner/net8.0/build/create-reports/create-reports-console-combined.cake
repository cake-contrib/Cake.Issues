Task("Create-Reports-Console-Combined")
    .Description("Creates the combined Console report example")
    .Does(() =>
{
    Information("Running Console Report with All Features Enabled");

    var issues = DeserializeIssuesFromJsonFile(@"build/create-reports/sample-issues.json");
    Information("Read {0} issues", issues.Count());

    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true,
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        @".",
        string.Empty);
});
