namespace Cake.Issues.PullRequests
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Capability to post issues only if pull request is for a specific commit.
    /// </summary>
    /// <typeparam name="T">Type of the pull request system to which this capability belongs.</typeparam>
    /// <param name="log">The Cake log context.</param>
    /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
    public abstract class BaseCheckingCommitIdCapability<T>(ICakeLog log, T pullRequestSystem)
        : BasePullRequestSystemCapability<T>(log, pullRequestSystem), ISupportCheckingCommitId
        where T : class, IPullRequestSystem
    {
        /// <inheritdoc/>
        public abstract string GetLastSourceCommitId();
    }
}
