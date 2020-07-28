Task("Build")
    .Description("Builds the solution")
    .Does<BuildData>(data =>
{
    var solutionFile = data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln");

#if NETCOREAPP
    DotNetCoreRestore(solutionFile.FullPath);

    var settings =
        new DotNetCoreMSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                "",
                data.MsBuildLogFilePath.FullPath
            );

    DotNetCoreBuild(
        solutionFile.FullPath,
        new DotNetCoreBuildSettings
        {
            MSBuildSettings = settings
        });
#else
    NuGetRestore(solutionFile);

    var settings =
        new MSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll").FullPath,
                "BinaryLogger",
                data.MsBuildLogFilePath.FullPath);
    MSBuild(solutionFile, settings);
#endif
});