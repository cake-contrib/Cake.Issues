namespace Cake.Issues.PullRequests.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;

    internal class OrchestratorForIssueProvidersFixture
    {
        public OrchestratorForIssueProvidersFixture()
            : this((builder, _) => builder)
        {
        }

        public OrchestratorForIssueProvidersFixture(
            Func<FakePullRequestSystemBuilder, IReportIssuesToPullRequestSettings, FakePullRequestSystemBuilder> pullRequestSettings)
        {
            pullRequestSettings.NotNull(nameof(pullRequestSettings));

            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            this.IssueProviders = new List<FakeIssueProvider> { new(this.Log) };

            this.Settings =
                new ReportIssuesToPullRequestFromIssueProviderSettings(
                    @"c:\Source\Cake.Issues");

            var pullRequestSystemBuilder = FakePullRequestSystemBuilder.NewPullRequestSystem(this.Log);
            pullRequestSystemBuilder =
                pullRequestSettings(pullRequestSystemBuilder, this.Settings);
            this.PullRequestSystem = pullRequestSystemBuilder.Create();
        }

        public FakeLog Log { get; set; }

        public IList<FakeIssueProvider> IssueProviders { get; set; }

        public FakePullRequestSystem PullRequestSystem { get; set; }

        public IReportIssuesToPullRequestFromIssueProviderSettings Settings { get; set; }

        public PullRequestIssueResult RunOrchestrator()
        {
            var orchestrator =
                new Orchestrator(
                    this.Log,
                    this.PullRequestSystem);
            return
                orchestrator.Run(
                    this.IssueProviders,
                    this.Settings);
        }
    }
}
