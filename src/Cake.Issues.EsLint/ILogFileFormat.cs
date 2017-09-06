namespace Cake.Issues.EsLint
{
    using System.Collections.Generic;

    /// <summary>
    /// Definition of a ESLint log file format.
    /// </summary>
    public interface ILogFileFormat
    {
        /// <summary>
        /// Gets all issues.
        /// </summary>
        /// <param name="repositorySettings">General settings to use.</param>
        /// <param name="esLintSettings">Settings for issue provider to use.</param>
        /// <returns>List of issues</returns>
        IEnumerable<IIssue> ReadIssues(
            RepositorySettings repositorySettings,
            EsLintIssuesSettings esLintSettings);
    }
}
