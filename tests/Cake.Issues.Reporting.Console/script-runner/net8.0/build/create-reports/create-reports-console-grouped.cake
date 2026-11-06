Task("Create-Reports-Console-Grouped")
    .Description("Creates the grouped Console report example")
    .Does(() =>
{
    CreateConsoleExample(new ConsoleIssueReportFormatSettings
    {
        GroupByRule = true,
    });
});
