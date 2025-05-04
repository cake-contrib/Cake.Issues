using Cake.Common.IO;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Pack;
using Cake.Frosting;

[TaskName("Create-NuGetPackages")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class CreateNuGetPackagesTask(IServiceProvider provider) : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // renovate: depName=NuGet.CommandLine
        const string nuGetVersion = "6.12.2";
        provider.InstallTool("NuGet.CommandLine", nuGetVersion);

        var nuspecFiles = context.GetFiles(context.Paths.NuspecDirectoryPath + "/**/*.nuspec");
        foreach (var nuspecFile in nuspecFiles)
        {
            context.EnsureDirectoryExists(context.Paths.OutputPackagesNuGetDirectoryPath);

            var settings = new NuGetPackSettings
            {
                Version = context.State.Version.SemVer,
                OutputDirectory = context.Paths.OutputPackagesNuGetDirectoryPath,
            };

            context.NuGetPack(nuspecFile, settings);
        }
    }
}
