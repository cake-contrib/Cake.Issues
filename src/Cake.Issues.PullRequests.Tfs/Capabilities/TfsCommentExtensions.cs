namespace Cake.Issues.PullRequests.Tfs.Capabilities
{
    using Cake.Tfs.PullRequest.CommentThread;

    /// <summary>
    /// Extensions for <see cref="TfsComment"/>.
    /// </summary>
    internal static class TfsCommentExtensions
    {
        /// <summary>
        /// Converts a <see cref="TfsComment"/> from TFS to a <see cref="IPullRequestDiscussionComment"/> as used in this addin.
        /// </summary>
        /// <param name="comment">TFS comment to convert.</param>
        /// <returns>Converted comment.</returns>
        public static IPullRequestDiscussionComment ToPullRequestDiscussionComment(this TfsComment comment)
        {
            comment.NotNull(nameof(comment));

            return new PullRequestDiscussionComment()
            {
                Content = comment.Content,
                IsDeleted = comment.IsDeleted,
            };
        }
    }
}
