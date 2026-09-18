using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Frosting.Issues.Recipe;

[TaskName("Restore-Solution")]
public sealed class RestoreSolutionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetRestore(
            context.Paths.Solution.FullPath,
            new DotNetRestoreSettings
            {
                LockedMode = !context.BuildSystem().IsLocalBuild,
                MSBuildSettings = context.Parameters.CreateMsBuildSettings(context),
            });
    }
}

[TaskName("Build-Solution")]
[IsDependentOn(typeof(RestoreSolutionTask))]
[IsDependeeOf(typeof(ReadIssuesTask))]
public sealed class BuildSolutionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var logFile = context.Paths.Logs.CombineWithFilePath("build.binlog");
        var settings = new DotNetBuildSettings
        {
            NoRestore = true,
            MSBuildSettings = context.Parameters.CreateMsBuildSettings(context),
            ArgumentCustomization = args => args.AppendSwitchQuoted("/bl:", string.Empty, logFile.FullPath),
        };

        context.DotNetBuild(context.Paths.Solution.FullPath, settings);
        context.Parameters.InputFiles.AddMsBuildBinaryLogFilePath(logFile);
    }
}

[TaskName("DotNet-Build")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class DotNetBuildTask : FrostingTask
{
}
