namespace Cake.Issues.PullRequests.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;

    internal class PullRequestsFixture
    {
        public PullRequestsFixture()
            : this((builder, settings) => builder)
        {
        }

        public PullRequestsFixture(
            Func<FakePullRequestSystemBuilder, ReportIssuesToPullRequestSettings, FakePullRequestSystemBuilder> pullRequestSettings)
        {
            pullRequestSettings.NotNull(nameof(pullRequestSettings));

            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            this.IssueProviders = new List<FakeIssueProvider> { new FakeIssueProvider(this.Log) };

            this.Settings =
                new ReportIssuesToPullRequestSettings(
                    new Core.IO.DirectoryPath(@"c:\Source\Cake.Issues"));

            var pullRequestSystemBuilder = FakePullRequestSystemBuilder.NewPullRequestSystem(this.Log);
            pullRequestSystemBuilder =
                pullRequestSettings(pullRequestSystemBuilder, this.ReportIssuesToPullRequestSettings);
            this.PullRequestSystem = pullRequestSystemBuilder.Create();
        }

        public FakeLog Log { get; set; }

        public IList<FakeIssueProvider> IssueProviders { get; set; }

        public FakePullRequestSystem PullRequestSystem { get; set; }

        public RepositorySettings Settings { get; set; }

        public ReportIssuesToPullRequestSettings ReportIssuesToPullRequestSettings =>
            this.Settings as ReportIssuesToPullRequestSettings;

        public PullRequestIssueResult RunOrchestratorForIssueProviders()
        {
            var orchestrator =
                new Orchestrator(
                    this.Log,
                    this.PullRequestSystem,
                    this.ReportIssuesToPullRequestSettings);
            return orchestrator.Run(this.IssueProviders);
        }

        public PullRequestIssueResult RunOrchestratorForIssues(IEnumerable<IIssue> issues)
        {
            var orchestrator =
                new Orchestrator(
                    this.Log,
                    this.PullRequestSystem,
                    this.ReportIssuesToPullRequestSettings);
            return orchestrator.Run(issues);
        }

        public IEnumerable<IIssue> FilterIssues(
            IEnumerable<IIssue> issues,
            IDictionary<IIssue, IssueCommentInfo> issueComments)
        {
            this.PullRequestSystem?.Initialize(this.ReportIssuesToPullRequestSettings);

            var issueFilterer =
                new IssueFilterer(
                    this.Log,
                    this.PullRequestSystem,
                    this.ReportIssuesToPullRequestSettings);
            return issueFilterer.FilterIssues(issues, issueComments);
        }
    }
}
