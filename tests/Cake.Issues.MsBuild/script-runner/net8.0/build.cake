#load "buildData.cake"

#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.MsBuild&prerelease"

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

    DotNetRestore(solutionFile.FullPath);

    var settings =
        new DotNetMSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/lib/net8.0/StructuredLogger.dll"),
                "",
                msBuildLogPath.FullPath
            );

    DotNetBuild(
        solutionFile.FullPath,
        new DotNetBuildSettings
        {
            MSBuildSettings = settings
        });

    data.AddIssues(
        ReadIssues(
            MsBuildIssuesFromFilePath(
                msBuildLogPath,
                MsBuildBinaryLogFileFormat),
            repoRootFolder)
    );
});

// Run Build task by default.
Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);