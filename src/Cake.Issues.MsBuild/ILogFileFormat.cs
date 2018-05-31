namespace Cake.Issues.MsBuild
{
    using System.Collections.Generic;

    /// <summary>
    /// Definition of a MsBuild log file format.
    /// </summary>
    public interface ILogFileFormat
    {
        /// <summary>
        /// Gets all issues.
        /// </summary>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="msBuildIssuesSettings">Settings for issue provider to use.</param>
        /// <returns>List of issues</returns>
        IEnumerable<IIssue> ReadIssues(
            MsBuildIssuesProvider issueProvider,
            RepositorySettings repositorySettings,
            MsBuildIssuesSettings msBuildIssuesSettings);
    }
}
