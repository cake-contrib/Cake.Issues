Task("Create-Reports-Console-Default")
    .Description("Creates the default Console report example")
    .Does(() =>
{
    CreateConsoleExample(new ConsoleIssueReportFormatSettings());
});
