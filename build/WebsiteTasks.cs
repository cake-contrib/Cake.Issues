using Cake.Common;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

[TaskName("Install-WebsiteDependencies")]
public sealed class InstallWebsiteDependenciesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        RunProcess(context, "pip", "install -r requirements.txt");
    }

    internal static void RunProcess(BuildContext context, string process, string arguments)
    {
        var exitCode = context.StartProcess(
            process,
            new ProcessSettings
            {
                Arguments = arguments,
                WorkingDirectory = context.State.RepositoryRootDirectory.Combine("docs"),
            });

        if (exitCode != 0)
        {
            throw new CakeException($"{process} exited with code {exitCode}.");
        }
    }
}

[TaskName("Website")]
[IsDependentOn(typeof(InstallWebsiteDependenciesTask))]
public sealed class WebsiteTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        InstallWebsiteDependenciesTask.RunProcess(context, "mkdocs", "serve");
    }
}
