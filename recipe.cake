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
    shouldRunInspectCode: false,
    shouldRunCoveralls: false,  // Disabled because it's currently failing
    shouldPostToGitter: false); // Disabled because it's currently failing

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolPreprocessorDirectives(
    gitReleaseManagerGlobalTool: "#tool dotnet:?package=GitReleaseManager.Tool&version=0.17.0",
    nugetTool: "#tool nuget:?package=NuGet.CommandLine&version=6.9.1",
    gitVersionGlobalTool: "#tool dotnet:?package=GitVersion.Tool&version=5.12.0"
);

ToolSettings.SetToolSettings(
    context: Context,
    skipDuplicatePackages: true,
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues.Reporting.Generic]LitJson.* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
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
// Task overrides
//*************************************************************************************************

// Since Cake.Recipe does not detect the correct settings when using Directory.Build.pros we need
// to override the test task
((CakeTask)BuildParameters.Tasks.DotNetCoreTestTask.Task).Actions.Clear();
BuildParameters.Tasks.DotNetCoreTestTask.Does<DotNetCoreMSBuildSettings>((context, msBuildSettings) => {
    var projects = GetFiles(BuildParameters.TestDirectoryPath + (BuildParameters.TestFilePattern ?? "/**/*Tests.csproj"));
    // We create the coverlet settings here so we don't have to create the filters several times
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage         = true,
        // It is problematic to merge the reports into one, as such we use a custom directory for coverage results
        CoverletOutputDirectory = BuildParameters.Paths.Directories.TestCoverage.Combine("coverlet"),
        CoverletOutputFormat    = CoverletOutputFormat.opencover,
        ExcludeByFile           = ToolSettings.TestCoverageExcludeByFile.Split(new [] {';' }, StringSplitOptions.None).ToList(),
        ExcludeByAttribute      = ToolSettings.TestCoverageExcludeByAttribute.Split(new [] {';' }, StringSplitOptions.None).ToList()
    };

    foreach (var filter in ToolSettings.TestCoverageFilter.Split(new [] {' ' }, StringSplitOptions.None))
    {
        if (filter[0] == '+')
        {
            coverletSettings.WithInclusion(filter.TrimStart('+'));
        }
        else if (filter[0] == '-')
        {
            coverletSettings.WithFilter(filter.TrimStart('-'));
        }
    }
    var settings = new DotNetCoreTestSettings
    {
        Configuration = BuildParameters.Configuration,
        NoBuild = true
    };

    foreach (var project in projects)
    {
        var parsedProject = ParseProject(project, BuildParameters.Configuration);

        settings.ArgumentCustomization = args => {
            args.AppendMSBuildSettings(msBuildSettings, context.Environment);
            args.Append("/p:UseSourceLink=true");
            return args;
        };

        coverletSettings.CoverletOutputName = parsedProject.RootNameSpace.Replace('.', '-');
        DotNetCoreTest(project.FullPath, settings, coverletSettings);
    }
});

// Update to latest InspectCode version which generates a SARIF file
((CakeTask)BuildParameters.Tasks.InspectCodeTask.Task).Actions.Clear();
BuildParameters.Tasks.InspectCodeTask
    .WithCriteria(() => BuildParameters.BuildAgentOperatingSystem == PlatformFamily.Windows, "Skipping due to not running on Windows")
    .WithCriteria(() => BuildParameters.ShouldRunInspectCode, "Skipping because InspectCode has been disabled")
    .Does<BuildData>(data => RequireTool(ToolSettings.ReSharperTools, () => {
        var inspectCodeLogFilePath = BuildParameters.Paths.Directories.InspectCodeTestResults.CombineWithFilePath("inspectcode.xml");

        var settings = new InspectCodeSettings() {
            SolutionWideAnalysis = true,
            OutputFile = inspectCodeLogFilePath,
            ArgumentCustomization = x => x.Append("--no-build").Append("-f=xml")
        };

        if (FileExists(BuildParameters.SourceDirectoryPath.CombineWithFilePath(BuildParameters.ResharperSettingsFileName)))
        {
            settings.Profile = BuildParameters.SourceDirectoryPath.CombineWithFilePath(BuildParameters.ResharperSettingsFileName);
        }

        InspectCode(BuildParameters.SolutionFilePath, settings);

        // Pass path to InspectCode log file to Cake.Issues.Recipe
        IssuesParameters.InputFiles.AddInspectCodeLogFile(inspectCodeLogFilePath);
    })
);

// Upload only .NET 8 coverage results to avoid issues with files being too large
((CakeTask)BuildParameters.Tasks.UploadCodecovReportTask.Task).Actions.Clear();
BuildParameters.Tasks.UploadCodecovReportTask
    .WithCriteria(() => BuildParameters.IsMainRepository, "Skipping because not running from the main repository")
    .WithCriteria(() => BuildParameters.ShouldRunCodecov, "Skipping because uploading to codecov is disabled")
    .WithCriteria(() => BuildParameters.CanPublishToCodecov, "Skipping because repo token is missing, or not running on appveyor")
    .Does<BuildVersion>((context, buildVersion) => RequireTool(BuildParameters.IsDotNetCoreBuild ? ToolSettings.CodecovGlobalTool : ToolSettings.CodecovTool, () => {
        var coverageFiles = GetFiles(BuildParameters.Paths.Directories.TestCoverage + "/coverlet/*.net8.0.opencover.xml");
        if (FileExists(BuildParameters.Paths.Files.TestCoverageOutputFilePath))
        {
            coverageFiles += BuildParameters.Paths.Files.TestCoverageOutputFilePath;
        }

        if (coverageFiles.Any())
        {
            var settings = new CodecovSettings {
                Files = coverageFiles.Select(f => f.FullPath),
                Required = true
            };
            if (buildVersion != null &&
                !string.IsNullOrEmpty(buildVersion.FullSemVersion) &&
                BuildParameters.IsRunningOnAppVeyor)
            {
                // Required to work correctly with appveyor because environment changes isn't detected until cake is done running.
                var localBuildVersion = string.Format("{0}.build.{1}",
                    buildVersion.FullSemVersion,
                    BuildSystem.AppVeyor.Environment.Build.Number);
                settings.EnvironmentVariables = new Dictionary<string, string> { { "APPVEYOR_BUILD_VERSION", localBuildVersion }};
            }

            Codecov(settings);
        }
    })
);

//*************************************************************************************************
// Custom tasks
//*************************************************************************************************

Task("BreakBuildOnIssues")
    .Description("Breaks build if any issues in the code are found.")
    .Does<IssuesData>((data) =>
{
    if (data.Issues.Any())
    {
        throw new Exception("Issues found in code.");
    }
});

IssuesBuildTasks.IssuesTask
    .IsDependentOn("BreakBuildOnIssues");

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunDotNetCore();
