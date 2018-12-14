namespace Cake.Issues.PullRequests.Tfs.Capabilities
{
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Tfs.PullRequest.CommentThread;

    /// <summary>
    /// Extensions for <see cref="TfsPullRequestCommentThread"/>.
    /// </summary>
    internal static class TfsPullRequestCommentThreadExtensions
    {
        private const string CommentSourcePropertyName = "CakeIssuesCommentSource";
        private const string IssueMessagePropertyName = "CakeIssuesIssueMessage";

        /// <summary>
        /// Converts a <see cref="TfsPullRequestCommentThread"/> from TFS to a <see cref="IPullRequestDiscussionThread"/> as used in this addin.
        /// </summary>
        /// <param name="thread">TFS thread to convert.</param>
        /// <returns>Converted thread.</returns>
        public static IPullRequestDiscussionThread ToPullRequestDiscussionThread(this TfsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return new PullRequestDiscussionThread(
                thread.Id,
                thread.Status.ToPullRequestDiscussionStatus(),
                thread.FilePath,
                thread.Comments.Select(x => x.ToPullRequestDiscussionComment()))
            {
                CommentSource = thread.GetCommentSource(),
                Resolution = thread.Status.ToPullRequestDiscussionResolution()
            };
        }

        /// <summary>
        /// Gets the comment source value used to decorate comments created by this addin.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Comment source value.</returns>
        public static string GetCommentSource(this TfsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.GetValue<string>(CommentSourcePropertyName);
        }

        /// <summary>
        /// Sets the comment sourc e value used to decorate comments created by this addin.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as comment source.</param>
        public static void SetCommentSource(this TfsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(CommentSourcePropertyName, value);
        }

        /// <summary>
        /// Checks if the custom comment source value used to decorate comments created by this addin
        /// has a specific value.
        /// </summary>
        /// <param name="thread">Thread to check.</param>
        /// <param name="value">Value to check for.</param>
        /// <returns><c>True</c> if the value is identical, <c>False</c> otherwise.</returns>
        public static bool IsCommentSource(this TfsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            return thread.GetCommentSource() == value;
        }

        /// <summary>
        /// Gets the original message of the issue as provided by Cake.Issues.PullRequests,
        /// without any formatting done by this addin.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Original message of the issue.</returns>
        public static string GetIssueMessage(this TfsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.GetValue<string>(IssueMessagePropertyName);
        }

        /// <summary>
        /// Sets the original message of the issue as provided by Cake.Issues.PullRequests.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as the original message.</param>
        public static void SetIssueMessage(this TfsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(IssueMessagePropertyName, value);
        }
    }
}
