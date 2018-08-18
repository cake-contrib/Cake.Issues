namespace Cake.Issues.PullRequests.Tfs
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
            if (!this.PullRequestSystem.ValidatePullRequest())
            {
                return new List<FilePath>();
            }

            this.Log.Verbose("Computing the list of files changed in this pull request...");

            var targetVersionDescriptor = new GitTargetVersionDescriptor
            {
                VersionType = GitVersionType.Commit,
                Version = this.PullRequestSystem.PullRequest.LastMergeSourceCommit.CommitId
            };

            var baseVersionDescriptor = new GitBaseVersionDescriptor
            {
                VersionType = GitVersionType.Commit,
                Version = this.PullRequestSystem.PullRequest.LastMergeTargetCommit.CommitId
            };

            using (var gitClient = this.PullRequestSystem.CreateGitClient())
            {
                var commitDiffs = gitClient.GetCommitDiffsAsync(
                    this.PullRequestSystem.RepositoryDescription.ProjectName,
                    this.PullRequestSystem.RepositoryDescription.RepositoryName,
                    true, // bool? diffCommonCommit
                    null, // int? top
                    null, // int? skip
                    baseVersionDescriptor,
                    targetVersionDescriptor,
                    null, // object userState
                    CancellationToken.None).Result;

                if (!commitDiffs.ChangeCounts.Any())
                {
                    return new List<FilePath>();
                }

                this.Log.Verbose(
                    "Found {0} changed file(s) in the pull request",
                    commitDiffs.Changes.Count());

                return
                    from change in commitDiffs.Changes
                    where
                        change != null &&
                        !change.Item.IsFolder
                    select
                        new FilePath(change.Item.Path.TrimStart('/'));
            }
        }
    }
}
