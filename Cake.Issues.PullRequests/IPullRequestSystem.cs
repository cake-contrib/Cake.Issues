namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Cake.Core.IO;

    /// <summary>
    /// Interface describing a pull request server.
    /// </summary>
    public interface IPullRequestSystem : IBaseIssueComponent<ReportIssuesToPullRequestSettings>
    {
        /// <summary>
        /// Gets the hash of the latest commit on the source branch.
        /// </summary>
        /// <returns>The hash of the latest commit on the source branch, null or <see cref="string.Empty"/>
        /// if no pull request could be found.</returns>
        string GetLastSourceCommitId();

        /// <summary>
        /// Returns the preferred format for pull request comments.
        /// </summary>
        /// <returns>The preferred format for pull request comments.</returns>
        IssueCommentFormat GetPreferredCommentFormat();

        /// <summary>
        /// Returns a list of all discussion threads.
        /// </summary>
        /// <param name="commentSource">Value used to indicate threads created by this addin.</param>
        /// <returns>List of all discussion threads.</returns>
        IEnumerable<IPullRequestDiscussionThread> FetchDiscussionThreads(string commentSource);

        /// <summary>
        /// Marks a list of discussion threads as resolved.
        /// </summary>
        /// <param name="threads">Threads to mark as fixed.</param>
        void ResolveDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads);

        /// <summary>
        /// Marks a list of discussion threads as active.
        /// </summary>
        /// <param name="threads">Threads to mark as active.</param>
        void ReopenDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads);

        /// <summary>
        /// Returns a list of all files modified in a pull request.
        /// </summary>
        /// <returns>List of all files modified in a pull request.</returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Most probably will make a remote call")]
        IEnumerable<FilePath> GetModifiedFilesInPullRequest();

        /// <summary>
        /// Posts discussion threads for all issues which need to be posted.
        /// </summary>
        /// <param name="issues">Issues which need to be posted.</param>
        /// <param name="commentSource">Value used to decorate comments created by this addin.</param>
        void PostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource);
    }
}
