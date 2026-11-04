#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Console&prerelease"

#load build/create-reports/create-reports-console.cake

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("Print-Issues")
    .Does(() =>
{
    var issues = DeserializeIssuesFromJsonFile(@"../../../issues.json");
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
    .IsDependentOn("Print-Issues")
    .IsDependentOn("Create-Reports-Console-Examples");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);