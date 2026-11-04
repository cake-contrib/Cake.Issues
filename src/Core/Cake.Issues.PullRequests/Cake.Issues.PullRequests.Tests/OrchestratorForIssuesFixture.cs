namespace Cake.Issues.PullRequests.Tests;

using Cake.Core.Diagnostics;
using Spectre.Console.Testing;

internal class OrchestratorForIssuesFixture
{
    public OrchestratorForIssuesFixture()
        : this((builder, _) => builder)
    {
    }

    public OrchestratorForIssuesFixture(
        Func<FakePullRequestSystemBuilder, IReportIssuesToPullRequestSettings, FakePullRequestSystemBuilder> pullRequestSettings)
    {
        pullRequestSettings.NotNull();

        this.Log = new FakeLog { Verbosity = Verbosity.Normal };

        this.Console = new TestConsole();

        this.Settings =
            new ReportIssuesToPullRequestSettings(
                @"c:\Source\Cake.Issues");

        var pullRequestSystemBuilder = FakePullRequestSystemBuilder.NewPullRequestSystem(this.Log);
        pullRequestSystemBuilder =
            pullRequestSettings(pullRequestSystemBuilder, this.Settings);
        this.PullRequestSystem = pullRequestSystemBuilder.Create();
    }

    public FakeLog Log { get; set; }

    public TestConsole Console { get; set; }

    public FakePullRequestSystem PullRequestSystem { get; set; }

    public IReportIssuesToPullRequestSettings Settings { get; set; }

    public PullRequestIssueResult RunOrchestrator(IEnumerable<IIssue> issues)
    {
        var orchestrator =
            new Orchestrator(
                this.Log,
                this.Console,
                this.PullRequestSystem);
        return orchestrator.Run(
            issues,
            this.Settings);
    }
}
