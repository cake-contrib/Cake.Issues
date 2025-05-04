using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Test;
using Cake.Frosting;

[TaskName("Test-Solution")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class TestSolutionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var settings = new DotNetTestSettings
        {
            Configuration = context.Parameters.Build.Configuration,
            NoRestore = true,
            NoBuild = true,
            Collectors = ["XPlat Code Coverage"],
            ResultsDirectory = context.Paths.OutputTestResultsDirectoryPath,
        };

        context.DotNetTest(context.Paths.SolutionFilePath.FullPath, settings);
    }
}
