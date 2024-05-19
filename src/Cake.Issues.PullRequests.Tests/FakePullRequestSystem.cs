namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BasePullRequestSystem"/> for use in test cases.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    public class FakePullRequestSystem(ICakeLog log) : BasePullRequestSystem(log)
    {
        private readonly List<IIssue> postedIssues = [];

        /// <summary>
        /// Gets the log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <summary>
        /// Gets the settings which should be used.
        /// </summary>
        public new IReportIssuesToPullRequestSettings Settings => base.Settings;

        /// <summary>
        /// Gets the <see cref="FakeCheckingCommitIdCapability"/> if it is enabled or null.
        /// </summary>
        public FakeCheckingCommitIdCapability CheckingCommitIdCapability =>
            this.GetCapability<FakeCheckingCommitIdCapability>();

        /// <summary>
        /// Gets the <see cref="FakeDiscussionThreadsCapability"/> if it is enabled or null.
        /// </summary>
        public FakeDiscussionThreadsCapability DiscussionThreadsCapability =>
            this.GetCapability<FakeDiscussionThreadsCapability>();

        /// <summary>
        /// Gets the <see cref="FakeFilteringByModifiedFilesCapability"/> if it is enabled or null.
        /// </summary>
        public FakeFilteringByModifiedFilesCapability FilteringByModifiedFilesCapability =>
            this.GetCapability<FakeFilteringByModifiedFilesCapability>();

        /// <summary>
        /// Gets the issues posted to the pull request.
        /// </summary>
        public IEnumerable<IIssue> PostedIssues => this.postedIssues;

        /// <summary>
        /// Gets or sets a value indicating whether the pull request system should return false during <see cref="Initialize"/>.
        /// </summary>
        public bool ShouldFailOnInitialization { get; set; }

        /// <inheritdoc />
        public override bool Initialize(IReportIssuesToPullRequestSettings settings)
        {
            var result = base.Initialize(settings);

            return result && !this.ShouldFailOnInitialization;
        }

        /// <inheritdoc />
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            issues.NotNull();

            this.postedIssues.AddRange(issues);
        }
    }
}
