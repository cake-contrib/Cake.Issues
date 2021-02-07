﻿namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseDiscussionThreadsCapability{T}"/> for use in test cases.
    /// </summary>
    public class FakeDiscussionThreadsCapability : BaseDiscussionThreadsCapability<FakePullRequestSystem>
    {
        private readonly List<IPullRequestDiscussionThread> discussionThreads = new List<IPullRequestDiscussionThread>();
        private readonly List<IPullRequestDiscussionThread> resolvedThreads = new List<IPullRequestDiscussionThread>();
        private readonly List<IPullRequestDiscussionThread> reopenedThreads = new List<IPullRequestDiscussionThread>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDiscussionThreadsCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        /// <param name="discussionThreads">Discussion threads which the pull request system capability should return.</param>
        public FakeDiscussionThreadsCapability(
            ICakeLog log,
            FakePullRequestSystem pullRequestSystem,
            IEnumerable<IPullRequestDiscussionThread> discussionThreads)
            : base(log, pullRequestSystem)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            discussionThreads.NotNull(nameof(discussionThreads));

            // ReSharper disable once PossibleMultipleEnumeration
            this.discussionThreads.AddRange(discussionThreads);
        }

        /// <summary>
        /// Gets the Cake log context.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <summary>
        /// Gets the pull request system to which the capability belongs.
        /// </summary>
        public new FakePullRequestSystem PullRequestSystem => base.PullRequestSystem;

        /// <summary>
        /// Gets the discussion threads on the pull request.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionThread> DiscussionThreads => this.discussionThreads;

        /// <summary>
        /// Gets the discussion threads marked as fixed.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionThread> ResolvedThreads => this.resolvedThreads;

        /// <summary>
        /// Gets the discussion threads marked as active.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionThread> ReopenedThreads => this.reopenedThreads;

        /// <inheritdoc />
        protected override IEnumerable<IPullRequestDiscussionThread> InternalFetchDiscussionThreads(string commentSource)
        {
            return this.discussionThreads;
        }

        /// <inheritdoc />
        protected override void InternalResolveDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            threads.NotNull(nameof(threads));

            // ReSharper disable once PossibleMultipleEnumeration
            this.resolvedThreads.AddRange(threads);
        }

        /// <inheritdoc />
        protected override void InternalReopenDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            threads.NotNull(nameof(threads));

            // ReSharper disable once PossibleMultipleEnumeration
            this.reopenedThreads.AddRange(threads);
        }
    }
}
