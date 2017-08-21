namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using Core.Diagnostics;
    using Core.IO;
    using Issues;
    using Issues.IssueProvider;
    using Issues.PullRequests;
    using Issues.PullRequests.PullRequestSystem;

    /// <summary>
    /// Implementation of a <see cref="Issues.PullRequests.PullRequestSystem.PullRequestSystem"/> for use in test cases.
    /// </summary>
    public class FakePullRequestSystem : PullRequestSystem
    {
        private readonly List<IPullRequestDiscussionThread> discussionThreads = new List<IPullRequestDiscussionThread>();
        private readonly List<FilePath> modifiedFiles = new List<FilePath>();
        private readonly List<IPullRequestDiscussionThread> threadsMarkedAsFixed = new List<IPullRequestDiscussionThread>();
        private readonly List<IIssue> postedIssues = new List<IIssue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePullRequestSystem"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance</param>
        public FakePullRequestSystem(ICakeLog log)
            : base(log)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePullRequestSystem"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance</param>
        /// <param name="discussionThreads">Discussion threads which the pull request system should return.</param>
        /// <param name="modifiedFiles">List of modified files which the pull request system should return.</param>
        public FakePullRequestSystem(
            ICakeLog log,
            IEnumerable<IPullRequestDiscussionThread> discussionThreads,
            IEnumerable<FilePath> modifiedFiles)
            : base(log)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            discussionThreads.NotNull(nameof(discussionThreads));

            // ReSharper disable once PossibleMultipleEnumeration
            modifiedFiles.NotNull(nameof(modifiedFiles));

            // ReSharper disable once PossibleMultipleEnumeration
            this.discussionThreads.AddRange(discussionThreads);

            // ReSharper disable once PossibleMultipleEnumeration
            this.modifiedFiles.AddRange(modifiedFiles);

            this.Initialize();
        }

        public new ICakeLog Log => base.Log;

        public new ReportIssuesToPullRequestSettings Settings => base.Settings;

        /// <summary>
        /// Gets the discussion threads marked as fixed.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionThread> ThreadsMarkedAsFixed => this.threadsMarkedAsFixed;

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
        protected override IEnumerable<IPullRequestDiscussionThread> InternalFetchActiveDiscussionThreads(string commentSource)
        {
            return this.discussionThreads;
        }

        /// <inheritdoc />
        protected override IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest()
        {
            return this.modifiedFiles;
        }

        /// <inheritdoc />
        protected override void InternalMarkThreadsAsFixed(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            threads.NotNull(nameof(threads));

            // ReSharper disable once PossibleMultipleEnumeration
            this.threadsMarkedAsFixed.AddRange(threads);
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
