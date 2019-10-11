namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Implementation of a <see cref="BaseFilteringByModifiedFilesCapability{T}"/> for <see cref="AzureDevOpsPullRequestSystem"/>.
    /// </summary>
    internal class AzureDevOpsFilteringByModifiedFilesCapability : BaseFilteringByModifiedFilesCapability<IAzureDevOpsPullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsFilteringByModifiedFilesCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public AzureDevOpsFilteringByModifiedFilesCapability(ICakeLog log, IAzureDevOpsPullRequestSystem pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest()
        {
            this.Log.Verbose("Computing the list of files changed in this pull request...");

            return this.PullRequestSystem.AzureDevOpsPullRequest.GetModifiedFiles();
        }
    }
}
