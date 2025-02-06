using Cake.Common.IO;
using Cake.Core.IO;

public class Paths
{
    public FilePath SolutionFilePath { get; }

    public DirectoryPath SrcDirectoryPath { get; }

    public DirectoryPath NuspecDirectoryPath { get; }

    public DirectoryPath OutputLogsDirectoryPath { get; }

    public DirectoryPath OutputPackagesNuGetDirectoryPath { get; }

    public DirectoryPath OutputTestResultsDirectoryPath { get; }

    public Paths(BuildContext context)
    {
        this.SrcDirectoryPath = context.State.RepositoryRootDirectory.Combine("src");
        this.NuspecDirectoryPath = context.State.RepositoryRootDirectory.Combine("nuspec").Combine("nuget");

        this.OutputLogsDirectoryPath = context.Parameters.OutputDirectory.Combine("logs");
        this.OutputPackagesNuGetDirectoryPath = context.Parameters.OutputDirectory.Combine("Packages").Combine("NuGet");
        this.OutputTestResultsDirectoryPath = context.Parameters.OutputDirectory.Combine("TestResults");

        this.SolutionFilePath = this.SrcDirectoryPath.CombineWithFilePath("Cake.Issues.sln");

        context.EnsureDirectoryExists(context.Parameters.OutputDirectory);
    }
}
