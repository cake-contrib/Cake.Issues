namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Capability to read, resolve and reopen discussion threads.
    /// </summary>
    /// <typeparam name="T">Type of the pull request system to which this capability belongs.</typeparam>
    public abstract class BaseDiscussionThreadsCapability<T>
        : BasePullRequestSystemCapability<T>, ISupportDiscussionThreads
        where T : class, IPullRequestSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDiscussionThreadsCapability{T}"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public BaseDiscussionThreadsCapability(ICakeLog log, T pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<IPullRequestDiscussionThread> FetchDiscussionThreads(string commentSource)
        {
            this.PullRequestSystem.AssertInitialized();

            return this.InternalFetchDiscussionThreads(commentSource);
        }

        /// <inheritdoc/>
        public void ResolveDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            this.PullRequestSystem.AssertInitialized();

            this.InternalResolveDiscussionThreads(threads);
        }

        /// <inheritdoc/>
        public void ReopenDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            this.PullRequestSystem.AssertInitialized();

            this.InternalReopenDiscussionThreads(threads);
        }

        /// <summary>
        /// Returns a list of all discussion threads.
        /// Compared to <see cref="FetchDiscussionThreads"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="commentSource">Value used to indicate threads created by this addin.</param>
        /// <returns>List of all discussion threads.</returns>
        protected abstract IEnumerable<IPullRequestDiscussionThread> InternalFetchDiscussionThreads(string commentSource);

        /// <summary>
        /// Marks a list of discussion threads as resolved.
        /// Compared to <see cref="ResolveDiscussionThreads"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="threads">Threads to mark as fixed.</param>
        protected abstract void InternalResolveDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads);

        /// <summary>
        /// Marks a list of discussion threads as active.
        /// Compared to <see cref="ReopenDiscussionThreads"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="threads">Threads to mark as active.</param>
        protected abstract void InternalReopenDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads);
    }
}
