namespace Cake.Issues.PullRequests.Tfs
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseCheckingCommitIdCapability{T}"/> for <see cref="TfsPullRequestSystem"/>.
    /// </summary>
    internal class TfsCheckingCommitIdCapability : BaseCheckingCommitIdCapability<ITfsPullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsCheckingCommitIdCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public TfsCheckingCommitIdCapability(ICakeLog log, ITfsPullRequestSystem pullRequestSystem)
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

            return this.PullRequestSystem.TfsPullRequest.LastSourceCommitId;
        }
    }
}
