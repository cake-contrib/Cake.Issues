namespace Cake.Issues.PullRequests.Tfs.Capabilities
{
    using Cake.Tfs.PullRequest.CommentThread;

    /// <summary>
    /// Extensions for <see cref="TfsCommentThreadStatus"/> enumeration.
    /// </summary>
    internal static class CommentThreadStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="TfsCommentThreadStatus"/> from TFS to a <see cref="PullRequestDiscussionStatus"/> as used in this addin.
        /// </summary>
        /// <param name="status">TFS status to convert.</param>
        /// <returns>Converted status.</returns>
        public static PullRequestDiscussionStatus ToPullRequestDiscussionStatus(this TfsCommentThreadStatus status)
        {
            switch (status)
            {
                case TfsCommentThreadStatus.Unknown:
                    return PullRequestDiscussionStatus.Unknown;
                case TfsCommentThreadStatus.Active:
                case TfsCommentThreadStatus.Pending:
                    return PullRequestDiscussionStatus.Active;
                case TfsCommentThreadStatus.Fixed:
                case TfsCommentThreadStatus.WontFix:
                case TfsCommentThreadStatus.Closed:
                case TfsCommentThreadStatus.ByDesign:
                    return PullRequestDiscussionStatus.Resolved;
                default:
                    throw new PullRequestIssuesException("Unknown enumeration value");
            }
        }

        /// <summary>
        /// Converts a <see cref="TfsCommentThreadStatus"/> from TFS to a <see cref="PullRequestDiscussionResolution"/> as used in this addin.
        /// </summary>
        /// <param name="status">TFS status to convert.</param>
        /// <returns>Converted status.</returns>
        public static PullRequestDiscussionResolution ToPullRequestDiscussionResolution(this TfsCommentThreadStatus status)
        {
            switch (status)
            {
                case TfsCommentThreadStatus.Unknown:
                case TfsCommentThreadStatus.Active:
                case TfsCommentThreadStatus.Pending:
                    return PullRequestDiscussionResolution.Unknown;
                case TfsCommentThreadStatus.Fixed:
                case TfsCommentThreadStatus.Closed:
                case TfsCommentThreadStatus.ByDesign:
                    return PullRequestDiscussionResolution.Resolved;
                case TfsCommentThreadStatus.WontFix:
                    return PullRequestDiscussionResolution.WontFix;
                default:
                    throw new PullRequestIssuesException("Unknown enumeration value");
            }
        }
    }
}
