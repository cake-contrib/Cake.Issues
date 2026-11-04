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
        Func<FakeBuildServerBuilder, IReportIssuesToBuildServerSettings, FakeBuildServerBuilder> buildServerSettings)
    {
        buildServerSettings.NotNull();

        this.Log = new FakeLog { Verbosity = Verbosity.Normal };

        this.Console = new TestConsole();

        this.IssueProviders = [new(this.Log)];

        this.Settings =
            new ReportIssuesToBuildServerFromIssueProviderSettings(
                @"c:\Source\Cake.Issues");

        var buildServerBuilder = FakeBuildServerBuilder.NewBuildServer(this.Log);
        buildServerBuilder =
            buildServerSettings(buildServerBuilder, this.Settings);
        this.BuildServer = buildServerBuilder.Create();
    }

    public FakeLog Log { get; set; }

    public TestConsole Console { get; set; }

    public IList<FakeIssueProvider> IssueProviders { get; set; }

    public FakeBuildServer BuildServer { get; set; }

    public IReportIssuesToBuildServerFromIssueProviderSettings Settings { get; set; }

    public BuildServerIssueResult RunOrchestrator()
    {
        var orchestrator =
            new BuildServerOrchestrator(
                this.Log,
                this.Console,
                this.BuildServer);
        return
            orchestrator.Run(
                this.IssueProviders,
                this.Settings);
    }
}
