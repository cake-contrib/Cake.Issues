Task("Create-Reports-Console-Summaries")
    .Description("Creates the Console report example with summaries")
    .Does(() =>
{
    CreateConsoleExample(new ConsoleIssueReportFormatSettings
    {
        ShowProviderSummary = true,
        ShowPrioritySummary = true,
    });
});
