Task("Build")
    .Description("Builds the solution")
    .Does<BuildData>(data =>
{
    var solutionFile = data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln");
    var msBuildLogFilePath = data.RepoRootFolder.CombineWithFilePath("msbuild.binlog");

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

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues"),
                "develop",
                "tests/TestDataGenerator"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            MsBuildIssuesFromFilePath(
                msBuildLogFilePath,
                MsBuildBinaryLogFileFormat),
            readIssuesSettings));
});