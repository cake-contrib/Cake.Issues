namespace Cake.Issues.PullRequests.AzureDevOps.Capabilities
{
    using Cake.AzureDevOps.PullRequest.CommentThread;

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
            switch (status)
            {
                case AzureDevOpsCommentThreadStatus.Unknown:
                    return PullRequestDiscussionStatus.Unknown;
                case AzureDevOpsCommentThreadStatus.Active:
                case AzureDevOpsCommentThreadStatus.Pending:
                    return PullRequestDiscussionStatus.Active;
                case AzureDevOpsCommentThreadStatus.Fixed:
                case AzureDevOpsCommentThreadStatus.WontFix:
                case AzureDevOpsCommentThreadStatus.Closed:
                case AzureDevOpsCommentThreadStatus.ByDesign:
                    return PullRequestDiscussionStatus.Resolved;
                default:
                    throw new PullRequestIssuesException("Unknown enumeration value");
            }
        }

        /// <summary>
        /// Converts a <see cref="AzureDevOpsCommentThreadStatus"/> from Azure DevOps to a <see cref="PullRequestDiscussionResolution"/> as used in this addin.
        /// </summary>
        /// <param name="status">Azure DevOps status to convert.</param>
        /// <returns>Converted status.</returns>
        public static PullRequestDiscussionResolution ToPullRequestDiscussionResolution(this AzureDevOpsCommentThreadStatus status)
        {
            switch (status)
            {
                case AzureDevOpsCommentThreadStatus.Unknown:
                case AzureDevOpsCommentThreadStatus.Active:
                case AzureDevOpsCommentThreadStatus.Pending:
                    return PullRequestDiscussionResolution.Unknown;
                case AzureDevOpsCommentThreadStatus.Fixed:
                case AzureDevOpsCommentThreadStatus.Closed:
                case AzureDevOpsCommentThreadStatus.ByDesign:
                    return PullRequestDiscussionResolution.Resolved;
                case AzureDevOpsCommentThreadStatus.WontFix:
                    return PullRequestDiscussionResolution.WontFix;
                default:
                    throw new PullRequestIssuesException("Unknown enumeration value");
            }
        }
    }
}
