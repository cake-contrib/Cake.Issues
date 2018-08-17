namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BasePullRequestSystem"/> for use in test cases.
    /// </summary>
    public class FakePullRequestSystem : BasePullRequestSystem
    {
        private readonly List<IIssue> postedIssues = new List<IIssue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePullRequestSystem"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public FakePullRequestSystem(ICakeLog log)
            : base(log)
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets the log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <summary>
        /// Gets the settings which should be used.
        /// </summary>
        public new ReportIssuesToPullRequestSettings Settings => base.Settings;

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
        /// Gets or sets the preferred comment format returned with <see cref="GetPreferredCommentFormat"/>.
        /// </summary>
        public IssueCommentFormat CommentFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the pull request system should return false during <see cref="Initialize"/>.
        /// </summary>
        public bool ShouldFailOnInitialization { get; set; } = false;

        /// <inheritdoc />
        public override IssueCommentFormat GetPreferredCommentFormat()
        {
            return this.CommentFormat;
        }

        /// <inheritdoc />
        public override bool Initialize(ReportIssuesToPullRequestSettings settings)
        {
            var result = base.Initialize(settings);

            return result && !this.ShouldFailOnInitialization;
        }

        /// <inheritdoc />
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            // ReSharper disable once PossibleMultipleEnumeration
            this.postedIssues.AddRange(issues);
        }

        private void Initialize()
        {
            this.CommentFormat = base.GetPreferredCommentFormat();
        }
    }
}
