#load nuget:?package=Cake.Recipe&version=1.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues",
    appVeyorAccountName: "cakecontrib",
    shouldRunCodecov: true,
    shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[]
    {
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues*/**/*.AssemblyInfo.cs",
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues*/Serialization/LitJson/*.cs",
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Tests/**/*.cs"
    },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]LitJson.* -[Shouldly]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

BuildParameters.Tasks.UploadCoverageReportTask = Task("Buildserver")
  .IsDependentOn("Default")
  .IsDependentOn("Upload-Coverage-Report");

Build.RunDotNetCore();