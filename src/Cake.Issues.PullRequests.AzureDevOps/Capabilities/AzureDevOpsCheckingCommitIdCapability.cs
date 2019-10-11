namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseCheckingCommitIdCapability{T}"/> for <see cref="AzureDevOpsPullRequestSystem"/>.
    /// </summary>
    internal class AzureDevOpsCheckingCommitIdCapability : BaseCheckingCommitIdCapability<IAzureDevOpsPullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsCheckingCommitIdCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public AzureDevOpsCheckingCommitIdCapability(ICakeLog log, IAzureDevOpsPullRequestSystem pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc />
        public override string GetLastSourceCommitId()
        {
            if (!this.PullRequestSystem.ValidatePullRequest())
            {
                return string.Empty;
            }

            return this.PullRequestSystem.AzureDevOpsPullRequest.LastSourceCommitId;
        }
    }
}
