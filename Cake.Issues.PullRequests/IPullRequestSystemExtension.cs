namespace Cake.Issues.PullRequests
{
    using System;

    /// <summary>
    /// Interface describing a pull request server.
    /// </summary>
    public static class IPullRequestSystemExtension
    {
        /// <summary>
        /// Checks if a specific commit hash is the last commit on the source branch of a pull request.
        /// </summary>
        /// <param name="pullRequestSystem">Pull request system instance.</param>
        /// <param name="commitId">Hash of the commit ID to check.</param>
        /// <returns>True if the commit ID is the last commit on the source branch.</returns>
        public static bool IsCurrentCommitId(this IPullRequestSystem pullRequestSystem, string commitId)
        {
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            commitId.NotNullOrWhiteSpace(nameof(commitId));

            var lastSourceCommitId = pullRequestSystem.GetLastSourceCommitId();

            return
                !string.IsNullOrWhiteSpace(lastSourceCommitId) &&
                lastSourceCommitId.Equals(commitId, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
