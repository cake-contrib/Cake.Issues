Task("Run-InspectCode")
    .Description("Runs JetBrains InspectCode analysis")
    .WithCriteria((context) => context.IsRunningOnWindows(), "InspectCode is only supported on Windows.")
    .Does<BuildData>(data =>
{
    var inspectCodeLogFilePath =
        data.OutputFolder.CombineWithFilePath("inspectCode.log");

    // Run InspectCode
    var settings = new InspectCodeSettings() {
        OutputFile = inspectCodeLogFilePath
    };

    InspectCode(data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln"), settings);

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif"),
                "develop",
                "tests"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            InspectCodeIssuesFromFilePath(inspectCodeLogFilePath),
            readIssuesSettings));
});