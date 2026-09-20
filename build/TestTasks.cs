using Cake.Codecov;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core.IO;
using Cake.Frosting;

[TaskName("Test-Solution")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class TestSolutionTask : FrostingTask<BuildContext>
{
    private static readonly string[] TargetFrameworks = ["net8.0", "net9.0", "net10.0"];

    public override void Run(BuildContext context)
    {
        context.EnsureDirectoryExists(context.Paths.Coverage);

        foreach (var project in context.GetFiles(context.Paths.Source + "/**/*Tests.csproj"))
        {
            var projectName = project.GetFilenameWithoutExtension().FullPath.Replace('.', '-');
            foreach (var targetFramework in TargetFrameworks)
            {
                var outputPrefix =
                    context.Paths.Coverage.CombineWithFilePath(
                        projectName);
                var msBuildSettings = context.Parameters.CreateMsBuildSettings(context)
                    .WithProperty("BuildInParallel", "false")
                    .WithProperty("CollectCoverage", "true")
                    .WithProperty("CoverletOutput", outputPrefix.FullPath)
                    .WithProperty("CoverletOutputFormat", "opencover")
                    .WithProperty("ExcludeByFile", context.Parameters.CoverageExcludeByFile)
                    .WithProperty("ExcludeByAttribute", context.Parameters.CoverageExcludeByAttribute)
                    .WithProperty(
                        "Include",
                        string.Join(',', context.Parameters.CoverageFilters
                            .Where(filter => filter[0] == '+')
                            .Select(filter => filter[1..])))
                    .WithProperty(
                        "Exclude",
                        string.Join(',', context.Parameters.CoverageFilters
                            .Where(filter => filter[0] == '-')
                            .Select(filter => filter[1..])))
                    .WithProperty("UseSourceLink", "true");
                msBuildSettings.MaxCpuCount = 1;
                msBuildSettings.NodeReuse = false;

                context.DotNetTest(
                    project.FullPath,
                    new DotNetTestSettings
                    {
                        Configuration = context.Parameters.Configuration,
                        Framework = targetFramework,
                        NoBuild = true,
                        NoRestore = true,
                        MSBuildSettings = msBuildSettings,
                        EnvironmentVariables =
                            new Dictionary<string, string>
                            {
                                ["MSBUILDDISABLENODEREUSE"] = "1",
                            },
                    });
            }
        }
    }
}

[TaskName("Test")]
[IsDependentOn(typeof(TestSolutionTask))]
public sealed class TestTask : FrostingTask
{
}

[TaskName("Publish-CodeCoverage")]
[IsDependentOn(typeof(TestSolutionTask))]
public sealed class PublishCodeCoverageTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context) =>
        !string.IsNullOrWhiteSpace(context.Parameters.CodecovToken) &&
        string.Equals(
            context.EnvironmentVariable("GITHUB_ACTIONS"),
            "true",
            StringComparison.OrdinalIgnoreCase) &&
        string.Equals(
            context.EnvironmentVariable("GITHUB_REPOSITORY"),
            "cake-contrib/Cake.Issues",
            StringComparison.OrdinalIgnoreCase);

    public override void Run(BuildContext context)
    {
        var coverageFiles =
            context.GetFiles(context.Paths.Coverage + "/*.net9.0.opencover.xml");

        if (coverageFiles.Count == 0)
        {
            context.Warning("No .NET 9 coverage files were found for Codecov.");
            return;
        }

        context.Codecov(
            new CodecovSettings
            {
                Files = coverageFiles.Select(file => file.FullPath),
                NonZero = true,
                Token = context.Parameters.CodecovToken,
            });
    }
}
