using Cake.Common.IO;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Pack;
using Cake.Frosting;

[TaskName("Create-NuGet-Packages")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class CreateNuGetPackagesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.EnsureDirectoryExists(context.Paths.Packages);

        foreach (var nuspec in context.GetFiles(context.Paths.Nuspec + "/*.nuspec"))
        {
            context.NuGetPack(
                nuspec,
                new NuGetPackSettings
                {
                    Version = context.State.Version.SemVer,
                    OutputDirectory = context.Paths.Packages,
                });
        }
    }
}
