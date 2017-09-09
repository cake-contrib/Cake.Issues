namespace Cake.Issues.Reporting.ReportFormat
{
    using System.Collections.Generic;
    using Core.IO;
    using Issues.IssueProvider;

    /// <summary>
    /// Interface describing a issue report format.
    /// </summary>
    public interface IIssueReportFormat : IBaseIssueComponent<RepositorySettings>
    {
        /// <summary>
        /// Creates a report from a list of issues.
        /// </summary>
        /// <param name="issues">Issues for which the report should be created.</param>
        /// <returns>Path to the created report.</returns>
        FilePath CreateReport(IEnumerable<IIssue> issues);
    }
}
