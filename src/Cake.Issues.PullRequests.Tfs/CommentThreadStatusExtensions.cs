namespace Cake.Issues.PullRequests.Tfs
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for <see cref="CommentThreadStatus"/> enumeration.
    /// </summary>
    internal static class CommentThreadStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="CommentThreadStatus"/> from TFS to a <see cref="PullRequestDiscussionStatus"/> as used in this addin.
        /// </summary>
        /// <param name="status">TFS status to convert.</param>
        /// <returns>Converted status.</returns>
        public static PullRequestDiscussionStatus ToPullRequestDiscussionStatus(this CommentThreadStatus status)
        {
            switch (status)
            {
                case CommentThreadStatus.Unknown:
                    return PullRequestDiscussionStatus.Unknown;
                case CommentThreadStatus.Active:
                case CommentThreadStatus.Pending:
                    return PullRequestDiscussionStatus.Active;
                case CommentThreadStatus.Fixed:
                case CommentThreadStatus.WontFix:
                case CommentThreadStatus.Closed:
                case CommentThreadStatus.ByDesign:
                    return PullRequestDiscussionStatus.Resolved;
                default:
                    throw new PullRequestIssuesException("Unknown enumeration value");
            }
        }
    }
}
