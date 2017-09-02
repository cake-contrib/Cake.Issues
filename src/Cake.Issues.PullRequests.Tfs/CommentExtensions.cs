namespace Cake.Issues.PullRequests.Tfs
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using PullRequestSystem;

    /// <summary>
    /// Extensions for <see cref="Comment"/>.
    /// </summary>
    internal static class CommentExtensions
    {
        /// <summary>
        /// Converts a <see cref="Comment"/> from TFS to a <see cref="IPullRequestDiscussionComment"/> as used in this addin.
        /// </summary>
        /// <param name="comment">TFS comment to convert.</param>
        /// <returns>Converted comment.</returns>
        public static IPullRequestDiscussionComment ToPullRequestDiscussionComment(this Comment comment)
        {
            comment.NotNull(nameof(comment));

            return new PullRequestDiscussionComment()
            {
                Content = comment.Content,
                IsDeleted = comment.IsDeleted
            };
        }
    }
}
