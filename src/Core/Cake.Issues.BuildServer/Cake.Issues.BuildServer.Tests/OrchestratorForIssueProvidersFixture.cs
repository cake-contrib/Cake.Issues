namespace Cake.Issues.BuildServer.Tests;

using Cake.Core.Diagnostics;
using Spectre.Console.Testing;

internal class OrchestratorForIssueProvidersFixture
{
    public OrchestratorForIssueProvidersFixture()
        : this((builder, _) => builder)
    {
    }

    public OrchestratorForIssueProvidersFixture(
        Func<FakeBuildServerSystemBuilder, IReportIssuesToBuildServerSettings, FakeBuildServerSystemBuilder> buildServerSettings)
    {
        buildServerSettings.NotNull();

        this.Log = new FakeLog { Verbosity = Verbosity.Normal };

        this.Console = new TestConsole();

        this.IssueProviders = [new(this.Log)];

        this.Settings =
            new ReportIssuesToBuildServerFromIssueProviderSettings(
                @"c:\Source\Cake.Issues");

        var buildServerSystemBuilder = FakeBuildServerSystemBuilder.NewBuildServerSystem(this.Log);
        buildServerSystemBuilder =
            buildServerSettings(buildServerSystemBuilder, this.Settings);
        this.BuildServerSystem = buildServerSystemBuilder.Create();
    }

    public FakeLog Log { get; set; }

    public TestConsole Console { get; set; }

    public IList<FakeIssueProvider> IssueProviders { get; set; }

    public FakeBuildServerSystem BuildServerSystem { get; set; }

    public IReportIssuesToBuildServerFromIssueProviderSettings Settings { get; set; }

    public BuildServerIssueResult RunOrchestrator()
    {
        var orchestrator =
            new BuildServerOrchestrator(
                this.Log,
                this.Console,
                this.BuildServerSystem);
        return
            orchestrator.Run(
                this.IssueProviders,
                this.Settings);
    }
}
