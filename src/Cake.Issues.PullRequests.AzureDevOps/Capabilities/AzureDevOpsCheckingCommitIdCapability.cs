namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseCheckingCommitIdCapability{T}"/> for <see cref="AzureDevOpsPullRequestSystem"/>.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
    internal class AzureDevOpsCheckingCommitIdCapability(ICakeLog log, IAzureDevOpsPullRequestSystem pullRequestSystem)
        : BaseCheckingCommitIdCapability<IAzureDevOpsPullRequestSystem>(log, pullRequestSystem)
    {
        /// <inheritdoc />
        public override string GetLastSourceCommitId() =>
            this.PullRequestSystem.ValidatePullRequest()
                ? this.PullRequestSystem.AzureDevOpsPullRequest.LastSourceCommitId
                : string.Empty;
    }
}
