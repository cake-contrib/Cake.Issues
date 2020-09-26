#load "buildData.cake"

#addin "Cake.Markdownlint"
#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.MsBuild&prerelease"
#addin "Cake.Issues.PullRequests&prerelease"
#addin "Cake.Issues.PullRequests.GitHubActions&prerelease"

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////
// SETUP / TEARDOWN
//////////////////////////////////////////////////

Setup<BuildData>(setupContext =>
{
    return new BuildData();
});

var repoRootFolder = MakeAbsolute(Directory("./"));
var logPath = repoRootFolder.Combine("BuildArtifacts").Combine("logs");

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("Build")
    .Does<BuildData>((data) =>
{
    var msBuildLogPath = logPath.CombineWithFilePath("msbuild.binlog");
    var solutionFile =
        repoRootFolder
            .Combine("src")
            .CombineWithFilePath("ClassLibrary1.sln");

#if NETCOREAPP
    DotNetCoreRestore(solutionFile.FullPath);

    var settings =
        new DotNetCoreMSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                "",
                msBuildLogPath.FullPath
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
                msBuildLogPath.FullPath);
    MSBuild(solutionFile, settings);
#endif

    data.AddIssues(
        ReadIssues(
            MsBuildIssuesFromFilePath(
                msBuildLogPath,
                MsBuildBinaryLogFileFormat),
            repoRootFolder)
    );
});

Task("Report-IssuesToBuildServer")
    .Does<BuildData>((data) =>
{
    ReportIssuesToPullRequest(
        data.Issues,
        GitHubActionsBuilds(),
        repoRootFolder);
});

Task("Lint")
    .IsDependentOn("Build");

// Run issues task by default.
Task("Default")
    .IsDependentOn("Lint")
    .IsDependentOn("Report-IssuesToBuildServer");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);