namespace Cake.Issues.PullRequests
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Capability to post issues only if pull request is for a specific commit.
    /// </summary>
    /// <typeparam name="T">Type of the pull request system to which this capability belongs.</typeparam>
    public abstract class BaseCheckingCommitIdCapability<T>
        : BasePullRequestSystemCapability<T>, ISupportCheckingCommitId
        where T : class, IPullRequestSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCheckingCommitIdCapability{T}"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        protected BaseCheckingCommitIdCapability(ICakeLog log, T pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc/>
        public abstract string GetLastSourceCommitId();
    }
}
