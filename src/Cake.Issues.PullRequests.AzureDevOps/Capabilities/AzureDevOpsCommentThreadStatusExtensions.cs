namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using Cake.AzureDevOps.Repos.PullRequest.CommentThread;

    /// <summary>
    /// Extensions for <see cref="AzureDevOpsCommentThreadStatus"/> enumeration.
    /// </summary>
    internal static class AzureDevOpsCommentThreadStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="AzureDevOpsCommentThreadStatus"/> from Azure DevOps to a <see cref="PullRequestDiscussionStatus"/> as used in this addin.
        /// </summary>
        /// <param name="status">Azure DevOps status to convert.</param>
        /// <returns>Converted status.</returns>
        public static PullRequestDiscussionStatus ToPullRequestDiscussionStatus(this AzureDevOpsCommentThreadStatus status)
        {
            return status switch
            {
                AzureDevOpsCommentThreadStatus.Unknown =>
                    PullRequestDiscussionStatus.Unknown,
                AzureDevOpsCommentThreadStatus.Active or
                AzureDevOpsCommentThreadStatus.Pending =>
                    PullRequestDiscussionStatus.Active,
                AzureDevOpsCommentThreadStatus.Fixed or
                AzureDevOpsCommentThreadStatus.WontFix or
                AzureDevOpsCommentThreadStatus.Closed or
                AzureDevOpsCommentThreadStatus.ByDesign =>
                    PullRequestDiscussionStatus.Resolved,
                _ => throw new PullRequestIssuesException("Unknown enumeration value"),
            };
        }

        /// <summary>
        /// Converts a <see cref="AzureDevOpsCommentThreadStatus"/> from Azure DevOps to a <see cref="PullRequestDiscussionResolution"/> as used in this addin.
        /// </summary>
        /// <param name="status">Azure DevOps status to convert.</param>
        /// <returns>Converted status.</returns>
        public static PullRequestDiscussionResolution ToPullRequestDiscussionResolution(this AzureDevOpsCommentThreadStatus status)
        {
            return status switch
            {
                AzureDevOpsCommentThreadStatus.Unknown or
                AzureDevOpsCommentThreadStatus.Active or
                AzureDevOpsCommentThreadStatus.Pending =>
                    PullRequestDiscussionResolution.Unknown,
                AzureDevOpsCommentThreadStatus.Fixed or
                AzureDevOpsCommentThreadStatus.Closed or
                AzureDevOpsCommentThreadStatus.ByDesign =>
                    PullRequestDiscussionResolution.Resolved,
                AzureDevOpsCommentThreadStatus.WontFix =>
                    PullRequestDiscussionResolution.WontFix,
                _ => throw new PullRequestIssuesException("Unknown enumeration value"),
            };
        }
    }
}
