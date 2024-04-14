#load nuget:?package=Cake.Recipe&version=3.1.1

//*************************************************************************************************
// Settings
//*************************************************************************************************

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues",
    appVeyorAccountName: "cakecontrib",
    shouldGenerateDocumentation: false, // Documentation is generated from Cake.Issues.Website
    shouldRunCoveralls: false,  // Disabled because it's currently failing
    shouldPostToGitter: false); // Disabled because it's currently failing

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolPreprocessorDirectives(
    gitReleaseManagerGlobalTool: "#tool dotnet:?package=GitReleaseManager.Tool&version=0.17.0"
);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]LitJson.* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunDotNetCore();
