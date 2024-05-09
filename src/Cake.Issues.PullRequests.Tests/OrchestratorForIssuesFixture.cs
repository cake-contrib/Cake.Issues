namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;

    internal class OrchestratorForIssuesFixture
    {
        public OrchestratorForIssuesFixture()
            : this((builder, _) => builder)
        {
        }

        public OrchestratorForIssuesFixture(
            Func<FakePullRequestSystemBuilder, IReportIssuesToPullRequestSettings, FakePullRequestSystemBuilder> pullRequestSettings)
        {
            pullRequestSettings.NotNull(nameof(pullRequestSettings));

            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            this.Settings =
                new ReportIssuesToPullRequestSettings(
                    @"c:\Source\Cake.Issues");

            var pullRequestSystemBuilder = FakePullRequestSystemBuilder.NewPullRequestSystem(this.Log);
            pullRequestSystemBuilder =
                pullRequestSettings(pullRequestSystemBuilder, this.Settings);
            this.PullRequestSystem = pullRequestSystemBuilder.Create();
        }

        public FakeLog Log { get; set; }

        public FakePullRequestSystem PullRequestSystem { get; set; }

        public IReportIssuesToPullRequestSettings Settings { get; set; }

        public PullRequestIssueResult RunOrchestrator(IEnumerable<IIssue> issues)
        {
            var orchestrator =
                new Orchestrator(
                    this.Log,
                    this.PullRequestSystem);
            return orchestrator.Run(
                issues,
                this.Settings);
        }
    }
}
