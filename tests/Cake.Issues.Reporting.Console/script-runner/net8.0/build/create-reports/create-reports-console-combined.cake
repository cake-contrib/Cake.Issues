Task("Create-Reports-Console-Combined")
    .Description("Creates the combined Console report example")
    .Does(() =>
{
    CreateConsoleExample(new ConsoleIssueReportFormatSettings
    {
        GroupByRule = true,
        ShowProviderSummary = true,
        ShowPrioritySummary = true,
    });
});
