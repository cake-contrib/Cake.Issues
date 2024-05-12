namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Information about comments of an issue.
    /// </summary>
    internal class IssueCommentInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueCommentInfo"/> class.
        /// </summary>
        /// <param name="activeComments">Active comments.</param>
        /// <param name="wontFixComments">Comments from threads marked as won't fix.</param>
        /// <param name="resolvedComments">Comments from threads marked as resolved.</param>
        public IssueCommentInfo(
            IEnumerable<IPullRequestDiscussionComment> activeComments,
            IEnumerable<IPullRequestDiscussionComment> wontFixComments,
            IEnumerable<IPullRequestDiscussionComment> resolvedComments)
        {
            activeComments.NotNull();
            wontFixComments.NotNull();
            resolvedComments.NotNull();

            this.ActiveComments = activeComments.ToList();
            this.WontFixComments = wontFixComments.ToList();
            this.ResolvedComments = resolvedComments.ToList();
        }

        /// <summary>
        /// Gets active comments.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionComment> ActiveComments { get; }

        /// <summary>
        /// Gets comments from threads marked as won't fix.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionComment> WontFixComments { get; }

        /// <summary>
        /// Gets comments from threads marked as resolved.
        /// </summary>
        public IEnumerable<IPullRequestDiscussionComment> ResolvedComments { get; }
    }
}
