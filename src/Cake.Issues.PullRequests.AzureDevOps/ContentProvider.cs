namespace Cake.Issues.PullRequests.AzureDevOps
{
    /// <summary>
    /// Class for providing the content for a pull request comment.
    /// </summary>
    internal static class ContentProvider
    {
        /// <summary>
        /// Returns the content for an issue.
        /// </summary>
        /// <param name="issue">Issue for which the content should be returned.</param>
        /// <returns>Comment content for the issue.</returns>
        public static string GetContent(IIssue issue)
        {
            var result = issue.Message(IssueCommentFormat.Markdown);
            if (string.IsNullOrWhiteSpace(issue.RuleId))
            {
                return result;
            }

            var ruleContent = issue.RuleId;
            if (issue.RuleUrl != null)
            {
                ruleContent = $"[{issue.RuleId}]({issue.RuleUrl})";
            }

            result = $"{ruleContent}: {result}";

            return result;
        }
    }
}
