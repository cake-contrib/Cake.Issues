using Cake.Common.Build;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Frosting;

[TaskName("Restore-Solution")]
public sealed class RestoreSolutionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // Enforce up to date lock files in CI builds.
        // On local builds lock files will be updated if necessary.
        var settings = new DotNetRestoreSettings
        {
            LockedMode = !context.BuildSystem().IsLocalBuild,
            MSBuildSettings = context.Parameters.Build.GetSettings(),
        };

        context.DotNetRestore(context.Paths.SolutionFilePath.FullPath, settings);
    }
}
