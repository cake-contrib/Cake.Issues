namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using Cake.AzureDevOps.Repos.PullRequest.CommentThread;

    /// <summary>
    /// Extensions for <see cref="AzureDevOpsComment"/>.
    /// </summary>
    internal static class AzureDevOpsCommentExtensions
    {
        /// <summary>
        /// Converts a <see cref="AzureDevOpsComment"/> from Azure DevOps to a <see cref="IPullRequestDiscussionComment"/> as used in this addin.
        /// </summary>
        /// <param name="comment">Azure DevOps comment to convert.</param>
        /// <returns>Converted comment.</returns>
        public static IPullRequestDiscussionComment ToPullRequestDiscussionComment(this AzureDevOpsComment comment)
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
