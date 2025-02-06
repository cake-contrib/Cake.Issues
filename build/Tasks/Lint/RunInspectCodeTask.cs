using Cake.Common.Tools.InspectCode;
using Cake.Core;
using Cake.Frosting;

[TaskName("Run-InspectCodeTask")]
[IsDependeeOf(typeof(ReadIssuesTask))]
public sealed class RunInspectCodeTask(IServiceProvider provider) : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return context.Environment.Platform.Family == PlatformFamily.Windows;
    }

    public override void Run(BuildContext context)
    {
        // renovate: depName=JetBrains.ReSharper.CommandLineTools
        const string inspectCodeVersion = "2024.3.3";
        provider.InstallTool("JetBrains.ReSharper.CommandLineTools", inspectCodeVersion);

        var inspectCodeLogFilePath = context.Paths.OutputLogsDirectoryPath.CombineWithFilePath("inspectcode.sarif");

        // Run InspectCode.
        var settings = new InspectCodeSettings()
        {
            SolutionWideAnalysis = true,
            OutputFile = inspectCodeLogFilePath,
            SkipOutputAnalysis = true,
        };
        context.InspectCode(
            context.Paths.SolutionFilePath,
            settings);

        // Read issues
        context.Parameters.InputFiles.AddSarifLogFilePath(
            inspectCodeLogFilePath,
            new ReadIssuesSettings(context.State.RepositoryRootDirectory)
            {
                Run = "InspectCode",
            });
    }
}
