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
        Func<FakeBuildServerBuilder, IReportIssuesToBuildServerSettings, FakeBuildServerBuilder> buildServerSettings)
    {
        buildServerSettings.NotNull();

        this.Log = new FakeLog { Verbosity = Verbosity.Normal };

        this.Console = new TestConsole();

        this.Settings =
            new ReportIssuesToBuildServerSettings(
                @"c:\Source\Cake.Issues");

        var buildServerBuilder = FakeBuildServerBuilder.NewBuildServer(this.Log);
        buildServerBuilder =
            buildServerSettings(buildServerBuilder, this.Settings);
        this.BuildServer = buildServerBuilder.Create();
    }

    public FakeLog Log { get; set; }

    public TestConsole Console { get; set; }

    public FakeBuildServer BuildServer { get; set; }

    public IReportIssuesToBuildServerSettings Settings { get; set; }

    public BuildServerIssueResult RunOrchestrator(IEnumerable<IIssue> issues)
    {
        var orchestrator =
            new BuildServerOrchestrator(
                this.Log,
                this.Console,
                this.BuildServer);
        return orchestrator.Run(
            issues,
            this.Settings);
    }
}
