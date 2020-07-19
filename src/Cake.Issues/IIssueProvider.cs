namespace Cake.Issues
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface describing a provider for issues.
    /// </summary>
    public interface IIssueProvider : IBaseIssueComponent<IReadIssuesSettings>
    {
        /// <summary>
        /// Gets the human friendly name of the issue provider.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Gets all issues.
        /// </summary>
        /// <returns>List of issues.</returns>
        IEnumerable<IIssue> ReadIssues();
    }
}
