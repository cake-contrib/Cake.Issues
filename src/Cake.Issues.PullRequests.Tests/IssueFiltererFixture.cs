namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;

    internal class IssueFiltererFixture
    {
        public IssueFiltererFixture()
            : this((builder, _) => builder)
        {
        }

        public IssueFiltererFixture(
            Func<FakePullRequestSystemBuilder, IReportIssuesToPullRequestSettings, FakePullRequestSystemBuilder> pullRequestSettings)
        {
            pullRequestSettings.NotNull();

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
            IDictionary<IIssue, IssueCommentInfo> issueComments) =>
                this
                    .GetIssueFilterer()
                    .FilterIssues(issues, issueComments, null);

        public IEnumerable<IIssue> FilterIssues(
            IEnumerable<IIssue> issues,
            IDictionary<IIssue, IssueCommentInfo> issueComments,
            IReadOnlyCollection<IPullRequestDiscussionThread> existingThreads) =>
                this
                    .GetIssueFilterer()
                    .FilterIssues(issues, issueComments, existingThreads);

        private IssueFilterer GetIssueFilterer()
        {
            _ = (this.PullRequestSystem?.Initialize(this.Settings));

            return
                new IssueFilterer(
                    this.Log,
                    this.PullRequestSystem,
                    this.Settings);
        }
    }
}
