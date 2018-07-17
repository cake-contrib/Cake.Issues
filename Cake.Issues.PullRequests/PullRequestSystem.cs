namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Base class for all pull request system implementations.
    /// </summary>
    public abstract class PullRequestSystem : BaseIssueComponent<ReportIssuesToPullRequestSettings>, IPullRequestSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PullRequestSystem"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        protected PullRequestSystem(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc/>
        public virtual string GetLastSourceCommitId()
        {
            return null;
        }

        /// <inheritdoc/>
        public virtual IssueCommentFormat GetPreferredCommentFormat()
        {
            return IssueCommentFormat.PlainText;
        }

        /// <inheritdoc/>
        public IEnumerable<IPullRequestDiscussionThread> FetchActiveDiscussionThreads(string commentSource)
        {
            this.AssertSettings();

            return this.InternalFetchActiveDiscussionThreads(commentSource);
        }

        /// <inheritdoc/>
        public IEnumerable<FilePath> GetModifiedFilesInPullRequest()
        {
            this.AssertSettings();

            return this.InternalGetModifiedFilesInPullRequest();
        }

        /// <inheritdoc/>
        public void MarkThreadsAsFixed(IEnumerable<IPullRequestDiscussionThread> threads)
        {
            this.AssertSettings();

            this.InternalMarkThreadsAsFixed(threads);
        }

        /// <inheritdoc/>
        public void PostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            this.AssertSettings();

            this.InternalPostDiscussionThreads(issues, commentSource);
        }

        /// <summary>
        /// Returns a list of all active discussion threads.
        /// Compared to <see cref="FetchActiveDiscussionThreads"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="commentSource">Value used to indicate threads created by this addin.</param>
        /// <returns>List of all active discussion threads.</returns>
        protected abstract IEnumerable<IPullRequestDiscussionThread> InternalFetchActiveDiscussionThreads(string commentSource);

        /// <summary>
        /// Returns a list of all files modified in a pull request.
        /// Compared to <see cref="GetModifiedFilesInPullRequest"/> it is safe to access Settings from this method.
        /// </summary>
        /// <returns>List of all files modified in a pull request.</returns>
        protected abstract IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest();

        /// <summary>
        /// Marks a list of discussion threads as resolved.
        /// Compared to <see cref="MarkThreadsAsFixed"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="threads">Threads to mark as fixed.</param>
        protected abstract void InternalMarkThreadsAsFixed(IEnumerable<IPullRequestDiscussionThread> threads);

        /// <summary>
        /// Posts discussion threads for all issues which need to be posted.
        /// Compared to <see cref="PostDiscussionThreads"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="issues">Issues which need to be posted.</param>
        /// <param name="commentSource">Value used to decorate comments created by this addin.</param>
        protected abstract void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource);
    }
}
