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

Task("Print-Issues-Default")
    .Does(() =>
{
    Information("Running Console Report with Default Settings");
    
    var issues = DeserializeIssuesFromJsonFile("sample-issues.json");
    Information("Read {0} issues", issues.Count());
    
    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(),
        @".",
        string.Empty);
});

Task("Default")
    .IsDependentOn("Print-Issues-Default");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);