using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Frosting;

[TaskName("Build-Solution")]
[IsDependentOn(typeof(RestoreSolutionTask))]
[IsDependeeOf(typeof(ReadIssuesTask))]
public sealed class BuildSolutionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var msBuildLogFilePath = context.Paths.OutputLogsDirectoryPath.CombineWithFilePath("build.binlog");

        var settings = new DotNetBuildSettings
        {
            NoRestore = true,
            MSBuildSettings =
                context.Parameters.Build.GetSettings()
                    .WithLogger(
                        "BinaryLogger," + context.Environment.ApplicationRoot.CombineWithFilePath("StructuredLogger.dll"),
                        "",
                        msBuildLogFilePath.FullPath)
                    .WithTarget("Rebuild"),
        };

        context.DotNetBuild(context.Paths.SolutionFilePath.FullPath, settings);

        // Read issues
        context.Parameters.InputFiles.AddMsBuildBinaryLogFilePath(msBuildLogFilePath);
    }
}
