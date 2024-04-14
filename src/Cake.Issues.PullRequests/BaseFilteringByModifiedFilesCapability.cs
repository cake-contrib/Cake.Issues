namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Capability to filter issues to only those affecting files modified in the pull request.
    /// </summary>
    /// <typeparam name="T">Type of the pull request system to which this capability belongs.</typeparam>
    /// <param name="log">The Cake log context.</param>
    /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
    public abstract class BaseFilteringByModifiedFilesCapability<T>(ICakeLog log, T pullRequestSystem)
        : BasePullRequestSystemCapability<T>(log, pullRequestSystem), ISupportFilteringByModifiedFiles
        where T : class, IPullRequestSystem
    {
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
