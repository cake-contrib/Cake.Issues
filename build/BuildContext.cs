using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Frosting.Issues.Recipe;

public sealed class BuildContext : IssuesContext<BuildParameters, BuildState>
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }

    public BuildPaths Paths { get; private set; } = null!;

    protected override BuildParameters CreateIssuesParameters() => new();

    protected override BuildState CreateIssuesState() => new(this);

    public void Initialize()
    {
        Parameters.OutputDirectory =
            State.RepositoryRootDirectory.Combine("BuildArtifacts");
        Paths = new BuildPaths(this);
    }
}

public sealed class BuildParameters : IssuesParameters
{
    private const string CoverageFilter =
        "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*";

    public BuildParameters()
    {
        OutputDirectory = "../BuildArtifacts";
        BuildBreaking.ShouldFailBuildOnIssues = true;
        Reporting.ShouldReportIssuesToConsole = true;
    }

    public string Configuration { get; } = "Release";

    public string CodecovToken { get; set; } = string.Empty;

    public IReadOnlyList<string> CoverageFilters { get; } =
        CoverageFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    public string CoverageExcludeByAttribute { get; } = "*.ExcludeFromCodeCoverage*";

    public string CoverageExcludeByFile { get; } = "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs";

    public DotNetMSBuildSettings CreateMsBuildSettings(BuildContext context)
    {
        var assemblyVersion = $"{context.State.Version.Major}.0.0.0";

        return new DotNetMSBuildSettings()
            .SetConfiguration(Configuration)
            .WithProperty("AssemblyVersion", assemblyVersion)
            .WithProperty("Version", context.State.Version.FullSemVer)
            .WithProperty(
                "ContinuousIntegrationBuild",
                (!context.BuildSystem().IsLocalBuild).ToString());
    }
}

public sealed class BuildState : IssuesState
{
    public BuildState(BuildContext context)
        : base(context, RepositoryInfoProviderType.CakeGit)
    {
    }

    public GitVersion Version { get; set; } = null!;
}

public sealed class BuildPaths
{
    public BuildPaths(BuildContext context)
    {
        Source = context.State.RepositoryRootDirectory.Combine("src");
        Nuspec = context.State.RepositoryRootDirectory.Combine("nuspec").Combine("nuget");
        Solution = Source.CombineWithFilePath("Cake.Issues.slnx");
        Logs = context.Parameters.OutputDirectory.Combine("logs");
        Packages = context.Parameters.OutputDirectory.Combine("Packages").Combine("NuGet");
        Coverage = context.Parameters.OutputDirectory.Combine("TestCoverage").Combine("coverlet");
    }

    public DirectoryPath Source { get; }

    public DirectoryPath Nuspec { get; }

    public FilePath Solution { get; }

    public DirectoryPath Logs { get; }

    public DirectoryPath Packages { get; }

    public DirectoryPath Coverage { get; }
}

public sealed class BuildLifetime : FrostingLifetime<BuildContext>
{
    public override void Setup(BuildContext context, ISetupContext info)
    {
        context.Initialize();
        context.CleanDirectory(context.Parameters.OutputDirectory);
        context.EnsureDirectoryExists(context.Paths.Logs);

        context.State.Version = context.GitVersion(
            new GitVersionSettings
            {
                NoFetch = context.BuildSystem().IsLocalBuild,
            });

        context.Parameters.CodecovToken =
            context.EnvironmentVariable("CODECOV_REPO_TOKEN") ?? string.Empty;

        context.Information("Building Cake.Issues {0}", context.State.Version.FullSemVer);
        context.Information(
            "AssemblyVersion set to {0}.0.0.0",
            context.State.Version.Major);
    }

    public override void Teardown(BuildContext context, ITeardownContext info)
    {
    }
}
