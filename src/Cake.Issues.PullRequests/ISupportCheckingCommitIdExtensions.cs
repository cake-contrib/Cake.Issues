namespace Cake.Issues.PullRequests
{
    using System;

    /// <summary>
    /// Extensions for the <see cref="ISupportCheckingCommitId"/> interface.
    /// </summary>
    public static class ISupportCheckingCommitIdExtensions
    {
        /// <summary>
        /// Checks if a specific commit hash is the last commit on the source branch of a pull request.
        /// </summary>
        /// <param name="capability">Pull request system capability.</param>
        /// <param name="commitId">Hash of the commit ID to check.</param>
        /// <returns>True if the commit ID is the last commit on the source branch.</returns>
        public static bool IsCurrentCommitId(this ISupportCheckingCommitId capability, string commitId)
        {
            capability.NotNull();
            commitId.NotNullOrWhiteSpace();

            var lastSourceCommitId = capability.GetLastSourceCommitId();

            return
                !string.IsNullOrWhiteSpace(lastSourceCommitId) &&
                lastSourceCommitId.Equals(commitId, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
