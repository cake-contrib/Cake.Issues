namespace Cake.Issues.PullRequests.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Testing;

    internal class IssueFiltererFixture
    {
        public IssueFiltererFixture()
            : this((builder, settings) => builder)
        {
        }

        public IssueFiltererFixture(
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

        public IEnumerable<IIssue> FilterIssues(
            IEnumerable<IIssue> issues,
            IDictionary<IIssue, IssueCommentInfo> issueComments)
        {
            return
                this
                    .GetIssueFilterer()
                    .FilterIssues(issues, issueComments, null);
        }

        public IEnumerable<IIssue> FilterIssues(
            IEnumerable<IIssue> issues,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads)
        {
            return
                this
                    .GetIssueFilterer()
                    .FilterIssues(issues, issueComments, existingThreads);
        }

        private IssueFilterer GetIssueFilterer()
        {
            this.PullRequestSystem?.Initialize(this.Settings);

            return
                new IssueFilterer(
                    this.Log,
                    this.PullRequestSystem,
                    this.Settings);
        }
    }
}
