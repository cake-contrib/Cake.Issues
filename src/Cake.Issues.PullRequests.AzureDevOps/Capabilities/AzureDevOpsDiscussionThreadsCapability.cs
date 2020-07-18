namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseDiscussionThreadsCapability{T}"/> for <see cref="AzureDevOpsPullRequestSystem"/>.
    /// </summary>
    internal class AzureDevOpsDiscussionThreadsCapability : BaseDiscussionThreadsCapability<IAzureDevOpsPullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsDiscussionThreadsCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public AzureDevOpsDiscussionThreadsCapability(ICakeLog log, IAzureDevOpsPullRequestSystem pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<IPullRequestDiscussionThread> InternalFetchDiscussionThreads(string commentSource)
        {
            var threads = this.PullRequestSystem.AzureDevOpsPullRequest.GetCommentThreads();

            var threadList = new List<IPullRequestDiscussionThread>();
            foreach (var thread in threads)
            {
                if (!thread.IsCommentSource(commentSource))
                {
                    continue;
                }

                var pullRequestThread = thread.ToPullRequestDiscussionThread();

                // Comment identifier was introduced with Cake.Issues 0.9.0.
                // To also support pull request written by previous versions of Cake.Issues
                // we return the message without additional formatting in case no
                // identifier was set on the thread.
                if (string.IsNullOrEmpty(pullRequestThread.CommentIdentifier))
                {
                    pullRequestThread.CommentIdentifier = thread.GetIssueMessage();
                }

                // Assuming that the first comment is the one written by this addin, we replace the content
                // containing additional formatting done by this addin with the original issue message.
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
                this.PullRequestSystem.AzureDevOpsPullRequest.ResolveCommentThread(thread.Id);
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
                this.PullRequestSystem.AzureDevOpsPullRequest.ActivateCommentThread(thread.Id);
            }
        }
    }
}
