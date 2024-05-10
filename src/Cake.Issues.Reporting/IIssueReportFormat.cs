namespace Cake.Issues.Reporting
{
    using System.Collections.Generic;
    using Cake.Core.IO;

    /// <summary>
    /// Interface describing an issue report format.
    /// </summary>
    public interface IIssueReportFormat : IBaseIssueComponent<ICreateIssueReportSettings>
    {
        /// <summary>
        /// Creates a report from a list of issues.
        /// </summary>
        /// <param name="issues">Issues for which the report should be created.</param>
        /// <returns>Path to the created report.</returns>
        FilePath CreateReport(IEnumerable<IIssue> issues);
    }
}
