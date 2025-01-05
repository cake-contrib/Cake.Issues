using Cake.Codecov;
using Cake.Common.IO;
using Cake.Frosting;

[TaskName("Publish-CodeCoverage")]
[IsDependentOn(typeof(TestSolutionTask))]
public sealed class PublishCodeCoverageTask(IServiceProvider provider) : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // renovate: depName=CodecovUploader
        const string nuGetVersion = "0.8.0";
        provider.InstallTool("CodecovUploader", nuGetVersion);

        var coverageFiles = context.GetFiles(context.Paths.OutputTestResultsDirectoryPath + "/**/coverage.cobertura.xml");
        foreach (var coverageFile in coverageFiles)
        {
            var settings = new CodecovSettings
            {
                Files = coverageFiles.Select(f => f.FullPath),
                NonZero = true,
                Token = context.Parameters.CodeCoverage.CodecovRepoToken
            };

            context.Codecov(settings);
        }
    }
}
