using Cake.Common.IO;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Push;
using Cake.Frosting;

[TaskName("Publish-NuGetPackages")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class PublishNuGetPackagesTask(IServiceProvider provider) : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // renovate: depName=NuGet.CommandLine
        const string nuGetVersion = "6.12.2";
        provider.InstallTool("NuGet.CommandLine", nuGetVersion);

        var nuspecFiles = context.GetFiles(context.Paths.OutputPackagesNuGetDirectoryPath + "/*.nupkg");
        foreach (var nuspecFile in nuspecFiles)
        {
            var settings = new NuGetPushSettings
            {
                ApiKey = context.Parameters.Packaging.NuGetApiKey,
                Source = "https://api.nuget.org/v3/index.json",
                SkipDuplicate = true,
            };

            context.NuGetPush(nuspecFile, settings);
        }
    }
}
