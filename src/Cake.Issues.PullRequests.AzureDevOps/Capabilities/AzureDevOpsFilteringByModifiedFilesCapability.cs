namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Implementation of a <see cref="BaseFilteringByModifiedFilesCapability{T}"/> for <see cref="AzureDevOpsPullRequestSystem"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AzureDevOpsFilteringByModifiedFilesCapability"/> class.
    /// </remarks>
    /// <param name="log">The Cake log context.</param>
    /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
    internal class AzureDevOpsFilteringByModifiedFilesCapability(ICakeLog log, IAzureDevOpsPullRequestSystem pullRequestSystem)
        : BaseFilteringByModifiedFilesCapability<IAzureDevOpsPullRequestSystem>(log, pullRequestSystem)
    {
        /// <inheritdoc />
        protected override IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest()
        {
            this.Log.Verbose("Computing the list of files changed in this pull request...");

            return this.PullRequestSystem.AzureDevOpsPullRequest.GetModifiedFiles();
        }
    }
}
