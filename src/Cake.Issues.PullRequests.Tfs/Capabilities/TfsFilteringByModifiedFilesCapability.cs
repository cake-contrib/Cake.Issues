namespace Cake.Issues.PullRequests.Tfs.Capabilities
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Implementation of a <see cref="BaseFilteringByModifiedFilesCapability{T}"/> for <see cref="TfsPullRequestSystem"/>.
    /// </summary>
    internal class TfsFilteringByModifiedFilesCapability : BaseFilteringByModifiedFilesCapability<ITfsPullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsFilteringByModifiedFilesCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public TfsFilteringByModifiedFilesCapability(ICakeLog log, ITfsPullRequestSystem pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest()
        {
            this.Log.Verbose("Computing the list of files changed in this pull request...");

            return this.PullRequestSystem.TfsPullRequest.GetModifiedFiles();
        }
    }
}
