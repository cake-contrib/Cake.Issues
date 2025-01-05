using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;
using Spectre.Console;

public class BuildLifetime : FrostingLifetime<BuildContext>
{
    public override void Setup(BuildContext context, ISetupContext info)
    {
        AnsiConsole.Write(new FigletText("Cake.Issues").Centered());

        // Determine version
        DetermineVersion(context);

        // Cleanup "dist" folder
        context.CleanDirectory(context.Parameters.OutputDirectory);
    }

    public override void Teardown(BuildContext context, ITeardownContext info)
    {
    }

    private static void DetermineVersion(BuildContext context)
    {
        var settings = new GitVersionSettings
        {
            ToolPath = context.Tools.Resolve("dotnet-gitversion.exe")
        };

        if (settings.ToolPath == null)
        {
            settings.ToolPath = context.Tools.Resolve("dotnet-gitversion");
        }

        if (!context.BuildSystem().IsLocalBuild)
        { 
            settings.UpdateAssemblyInfo = true;
            settings.UpdateAssemblyInfoFilePath =
                context.State.RepositoryRootDirectory
                    .GetRelativePath(context.Paths.SrcDirectoryPath)
                    .CombineWithFilePath("SolutionInfo.cs");
        }

        context.State.Version = context.GitVersion(settings);

        context.Information("Building version {0}", context.State.Version.FullSemVer);
    }
}
