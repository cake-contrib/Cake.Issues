namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Capability to filter issues to only those affecting files modified in the pull request.
    /// </summary>
    /// <typeparam name="T">Type of the pull request system to which this capability belongs.</typeparam>
    public abstract class BaseFilteringByModifiedFilesCapability<T>
        : BasePullRequestSystemCapability<T>, ISupportFilteringByModifiedFiles
        where T : class, IPullRequestSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilteringByModifiedFilesCapability{T}"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        protected BaseFilteringByModifiedFilesCapability(ICakeLog log, T pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<FilePath> GetModifiedFilesInPullRequest()
        {
            this.PullRequestSystem.AssertInitialized();

            return this.InternalGetModifiedFilesInPullRequest();
        }

        /// <summary>
        /// Returns a list of all files modified in a pull request.
        /// Compared to <see cref="GetModifiedFilesInPullRequest"/> it is safe to access Settings from this method.
        /// </summary>
        /// <returns>List of all files modified in a pull request.</returns>
        protected abstract IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest();
    }
}
