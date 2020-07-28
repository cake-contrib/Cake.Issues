Task("Build")
    .Description("Builds the solution")
    .Does<BuildData>(data =>
{
    var solutionFile = data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln");

    NuGetRestore(solutionFile);

    var settings =
        new MSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll").FullPath,
                "BinaryLogger",
                data.MsBuildLogFilePath.FullPath);
    MSBuild(solutionFile, settings);
});