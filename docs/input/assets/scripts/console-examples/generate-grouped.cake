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

Task("Print-Issues-Grouped")
    .Does(() =>
{
    Information("Running Console Report with Grouped by Rule");
    
    var issues = DeserializeIssuesFromJsonFile("sample-issues.json");
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

Task("Default")
    .IsDependentOn("Print-Issues-Grouped");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);