Task("Build")
    .Description("Builds the solution")
    .Does<BuildData>(data =>
{
    var solutionFile = data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln");
    var msBuildLogFilePath = data.RepoRootFolder.CombineWithFilePath("msbuild.binlog");

#if NETCOREAPP
    DotNetRestore(solutionFile.FullPath);

    var settings =
        new DotNetMSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                "",
                msBuildLogFilePath.FullPath
            );

    DotNetBuild(
        solutionFile.FullPath,
        new DotNetBuildSettings
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
                msBuildLogFilePath.FullPath);
    MSBuild(solutionFile, settings);
#endif

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues.Reporting.Generic"),
                "develop",
                "demos/script-runner"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            MsBuildIssuesFromFilePath(
                msBuildLogFilePath,
                MsBuildBinaryLogFileFormat),
            readIssuesSettings));
});