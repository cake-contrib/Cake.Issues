namespace Cake.Issues.GitRepository.Tests;

using Cake.Core.IO;

internal class GitRepositoryIssuesProviderFixture : BaseIssueProviderFixture<GitRepositoryIssuesProvider>
{
    private readonly GitRunnerFixture git = new();
    private readonly GitProcessRunner processRunner = new();

    public GitRepositoryIssuesSettings Settings { get; } = new();

    public string[] TaggedFiles { get; set; } = [];

    public string[] TextFiles { get; set; } = [];

    public string[] EmptyFiles { get; set; } = [];

    public string[] LfsFiles { get; set; } = [];

    protected override IList<object> GetCreateIssueProviderArguments()
    {
        this.processRunner.Fixture = this;
        var result = base.GetCreateIssueProviderArguments();
        result.Add(this.git.FileSystem);
        result.Add(this.git.Environment);
        result.Add(this.processRunner);
        result.Add(this.git.Tools);
        result.Add(this.Settings);
        return result;
    }

    private sealed class GitProcessRunner : IProcessRunner
    {
        public GitRepositoryIssuesProviderFixture Fixture { get; set; }

        public IProcess Start(FilePath filePath, ProcessSettings settings)
        {
            var process = new FakeProcess();
            process.SetStandardOutput(settings.Arguments.Render() switch
            {
                "ls-files -z" => [string.Join('\0', this.Fixture.TaggedFiles.Select(file => file[2..])) + '\0'],
                "ls-files -t -z" => [string.Join('\0', this.Fixture.TaggedFiles) + '\0'],
                "grep -Il ." => this.Fixture.TextFiles,
                "grep -IL ." => this.Fixture.EmptyFiles,
                "lfs ls-files -n" => this.Fixture.LfsFiles,
                var command => throw new InvalidOperationException($"Unexpected Git command: {command}"),
            });
            return process;
        }
    }
}
