Task("Create-Reports-Console-Grouped")
    .Description("Creates the grouped Console report example")
    .Does(() =>
{
    Information("Running Console Report with Grouped by Rule");

    var issues = DeserializeIssuesFromJsonFile(@"build/create-reports/sample-issues.json");
    Information("Read {0} issues", issues.Count());

    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true
            }),
        @".",
        string.Empty);
});
