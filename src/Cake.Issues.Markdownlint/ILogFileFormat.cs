namespace Cake.Issues.Markdownlint
{
    using System.Collections.Generic;

    /// <summary>
    /// Definition of a Markdownlint log file format.
    /// </summary>
    public interface ILogFileFormat
    {
        /// <summary>
        /// Gets all issues.
        /// </summary>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="markdownlintIssuesSettings">Settings for issue provider to use.</param>
        /// <returns>List of issues</returns>
        IEnumerable<IIssue> ReadIssues(
            MarkdownlintIssuesProvider issueProvider,
            RepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings);
    }
}
