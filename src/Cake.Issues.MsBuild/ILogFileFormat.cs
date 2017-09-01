namespace Cake.Issues.MsBuild
{
    using System.Collections.Generic;
    using IssueProvider;

    /// <summary>
    /// Definition of a MsBuild log file format.
    /// </summary>
    public interface ILogFileFormat
    {
        /// <summary>
        /// Gets all code analysis issues.
        /// </summary>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="msBuildIssuesSettings">Settings for code analysis provider to use.</param>
        /// <returns>List of code analysis issues</returns>
        IEnumerable<IIssue> ReadIssues(
            RepositorySettings repositorySettings,
            MsBuildIssuesSettings msBuildIssuesSettings);
    }
}
