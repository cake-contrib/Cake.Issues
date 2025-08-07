#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Console&prerelease"

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("Print-Issues-Summaries")
    .Does(() =>
{
    Information("Running Console Report with Provider and Priority Summaries");
    
    var issues = DeserializeIssuesFromJsonFile("sample-issues.json");
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

Task("Default")
    .IsDependentOn("Print-Issues-Summaries");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);