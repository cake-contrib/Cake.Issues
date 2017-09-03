namespace Cake.Issues
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface describing a provider for issues.
    /// </summary>
    public interface IIssueProvider : IBaseIssueComponent<RepositorySettings>
    {
        /// <summary>
        /// Gets all issues.
        /// </summary>
        /// <param name="format">Preferred format of the comments.</param>
        /// <returns>List of issues.</returns>
        IEnumerable<IIssue> ReadIssues(IssueCommentFormat format);
    }
}
