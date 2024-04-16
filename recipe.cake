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
    shouldUseDeterministicBuilds: true,
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
// Setup
//*************************************************************************************************

Setup(context =>
{
    // Addins are backwards compatible to latest major version.
    // Since we are using project references, we need to fix assembly version to the latest
    // major version to avoid requiring exact minor versions at runtime.
    var settings = context.Data.Get<DotNetCoreMSBuildSettings>();
    var version = new Version(settings.Properties["AssemblyVersion"].First());
    settings.Properties["AssemblyVersion"] = new List<string> { $"{version.Major}.0.0.0" };

    context.Log.Information("AssemblyVersion changed to {0}", settings.Properties["AssemblyVersion"].First());
});

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunDotNetCore();
