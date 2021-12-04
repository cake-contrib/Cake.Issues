#load nuget:?package=Cake.Recipe&version=2.0.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues",
    appVeyorAccountName: "cakecontrib");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[]
    {
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues*/**/*.AssemblyInfo.cs",
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues*/Serialization/LitJson/*.cs",
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Tests/**/*.cs",
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues.PullRequests.Tests/**/*.cs",
        BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Reporting.Tests/**/*.cs"
    },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]LitJson.* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

// Workaround until https://github.com/cake-contrib/Cake.Recipe/issues/862 has been fixed in Cake.Recipe
ToolSettings.SetToolPreprocessorDirectives(
    reSharperTools: "#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2021.2.0");

Build.RunDotNetCore();
