namespace Cake.Issues.PullRequests.Tfs.Capabilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseDiscussionThreadsCapability{T}"/> for <see cref="TfsPullRequestSystem"/>.
    /// </summary>
    internal class TfsDiscussionThreadsCapability : BaseDiscussionThreadsCapability<ITfsPullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsDiscussionThreadsCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public TfsDiscussionThreadsCapability(ICakeLog log, ITfsPullRequestSystem pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<IPullRequestDiscussionThread> InternalFetchDiscussionThreads(string commentSource)
        {
            var threads = this.PullRequestSystem.TfsPullRequest.GetCommentThreads();

            var threadList = new List<IPullRequestDiscussionThread>();
            foreach (var thread in threads)
            {
                if (!thread.IsCommentSource(commentSource))
                {
                    continue;
                }

                var pullRequestThread = thread.ToPullRequestDiscussionThread();

                // Assuming that the first comment is the one written by this addin, we replace the content
                // containing additional formatting done by this addin with the original issue message to
                // allow Cake.Issues.PullRequests to do a proper comparison to find out which issues already were posted.
                pullRequestThread.Comments.First().Content = thread.GetIssueMessage();

                threadList.Add(pullRequestThread);
            }

            this.Log.Verbose("Found {0} discussion thread(s)", threadList.Count);
            return threadList;
        }

        /// <inheritdoc />
        protected override void InternalResolveDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            threads.NotNull(nameof(threads));

            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var thread in threads)
            {
                this.PullRequestSystem.TfsPullRequest.ResolveCommentThread(thread.Id);
            }
        }

        /// <inheritdoc />
        protected override void InternalReopenDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            threads.NotNull(nameof(threads));

            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var thread in threads)
            {
                this.PullRequestSystem.TfsPullRequest.ActivateCommentThread(thread.Id);
            }
        }
    }
}
