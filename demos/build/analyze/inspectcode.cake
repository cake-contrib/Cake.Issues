#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"

Task("Run-InspectCode")
    .Description("Runs JetBrains InspectCode analysis")
    .WithCriteria((context) => context.IsRunningOnWindows(), "InspectCode is only supported on Windows.")
    .Does<BuildData>(data =>
{
    var inspectCodeLogFilePath =
        data.RepoRootFolder.CombineWithFilePath("inspectCode.log");

    // Run InspectCode
    var settings = new InspectCodeSettings() {
        OutputFile = inspectCodeLogFilePath
    };

    InspectCode(data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln"), settings);

    // Read issues
    data.Issues.AddRange(
        ReadIssues(
            InspectCodeIssuesFromFilePath(inspectCodeLogFilePath),
            data.RepoRootFolder));
});