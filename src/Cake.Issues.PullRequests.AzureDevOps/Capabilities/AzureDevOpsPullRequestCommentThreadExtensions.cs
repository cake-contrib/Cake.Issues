namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using System.Linq;
    using Cake.AzureDevOps.Repos.PullRequest.CommentThread;

    /// <summary>
    /// Extensions for <see cref="AzureDevOpsPullRequestCommentThread"/>.
    /// </summary>
    internal static class AzureDevOpsPullRequestCommentThreadExtensions
    {
        private const string CommentSourcePropertyName = "CakeIssuesCommentSource";
        private const string CommentIdentifierPropertyName = "CakeIssuesCommentIdentifier";
        private const string IssueMessagePropertyName = "CakeIssuesIssueMessage";
        private const string ProviderTypePropertyName = "CakeIssuesProviderType";

        /// <summary>
        /// Converts a <see cref="AzureDevOpsPullRequestCommentThread"/> from Azure DevOps to a <see cref="IPullRequestDiscussionThread"/> as used in this addin.
        /// </summary>
        /// <param name="thread">Azure DevOps thread to convert.</param>
        /// <returns>Converted thread.</returns>
        public static IPullRequestDiscussionThread ToPullRequestDiscussionThread(this AzureDevOpsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return new PullRequestDiscussionThread(
                thread.Id,
                thread.Status.ToPullRequestDiscussionStatus(),
                thread.FilePath,
                thread.Comments.Select(x => x.ToPullRequestDiscussionComment()))
            {
                CommentSource = thread.GetCommentSource(),
                CommentIdentifier = thread.GetCommentIdentifier(),
                ProviderType = thread.GetProviderType(),
                Resolution = thread.Status.ToPullRequestDiscussionResolution(),
            };
        }

        /// <summary>
        /// Gets the comment source value used to decorate comments created by this add-in.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Comment source value.</returns>
        public static string GetCommentSource(this AzureDevOpsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.GetValue<string>(CommentSourcePropertyName);
        }

        /// <summary>
        /// Sets the comment source value used to decorate comments created by this addin.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as comment source.</param>
        public static void SetCommentSource(this AzureDevOpsPullRequestCommentThread thread, string value)
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
        public static bool IsCommentSource(this AzureDevOpsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            return thread.GetCommentSource() == value;
        }

        /// <summary>
        /// Gets the comment identifier to identify the issue for which the comment was created.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Comment identifier value.</returns>
        public static string GetCommentIdentifier(this AzureDevOpsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.GetValue<string>(CommentIdentifierPropertyName);
        }

        /// <summary>
        /// Sets the comment identifier value used to identify the issue for which the comment was created.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as comment identifier.</param>
        public static void SetCommentIdentifier(this AzureDevOpsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(CommentIdentifierPropertyName, value);
        }

        /// <summary>
        /// Gets the original message of the issue as provided by Cake.Issues.PullRequests,
        /// without any formatting done by this addin.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Original message of the issue.</returns>
        public static string GetIssueMessage(this AzureDevOpsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.GetValue<string>(IssueMessagePropertyName);
        }

        /// <summary>
        /// Sets the original message of the issue as provided by Cake.Issues.PullRequests.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as the original message.</param>
        public static void SetIssueMessage(this AzureDevOpsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(IssueMessagePropertyName, value);
        }

        /// <summary>
        /// Gets the provider type value used to identify specific provider origins later on when reading back existing issues.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Comment source value.</returns>
        public static string GetProviderType(this AzureDevOpsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.GetValue<string>(ProviderTypePropertyName);
        }

        /// <summary>
        /// Sets the provider type value used to identify specific provider origins later on when reading back existing issues.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as comment source.</param>
        public static void SetProviderType(this AzureDevOpsPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(ProviderTypePropertyName, value);
        }
    }
}
