namespace Cake.Issues.BuildServer.Tests;

using Cake.Core.Diagnostics;
using Spectre.Console.Testing;

internal class OrchestratorForIssuesFixture
{
    public OrchestratorForIssuesFixture()
        : this((builder, _) => builder)
    {
    }

    public OrchestratorForIssuesFixture(
        Func<FakeBuildServerSystemBuilder, IReportIssuesToBuildServerSettings, FakeBuildServerSystemBuilder> buildServerSettings)
    {
        buildServerSettings.NotNull();

        this.Log = new FakeLog { Verbosity = Verbosity.Normal };

        this.Console = new TestConsole();

        this.Settings =
            new ReportIssuesToBuildServerSettings(
                @"c:\Source\Cake.Issues");

        var buildServerSystemBuilder = FakeBuildServerSystemBuilder.NewBuildServerSystem(this.Log);
        buildServerSystemBuilder =
            buildServerSettings(buildServerSystemBuilder, this.Settings);
        this.BuildServerSystem = buildServerSystemBuilder.Create();
    }

    public FakeLog Log { get; set; }

    public TestConsole Console { get; set; }

    public FakeBuildServerSystem BuildServerSystem { get; set; }

    public IReportIssuesToBuildServerSettings Settings { get; set; }

    public BuildServerIssueResult RunOrchestrator(IEnumerable<IIssue> issues)
    {
        var orchestrator =
            new BuildServerOrchestrator(
                this.Log,
                this.Console,
                this.BuildServerSystem);
        return orchestrator.Run(
            issues,
            this.Settings);
    }
}
