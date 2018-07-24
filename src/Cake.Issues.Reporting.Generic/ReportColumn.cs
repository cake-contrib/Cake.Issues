namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Name of columns in a report.
    /// </summary>
    public enum ReportColumn
    {
        /// <summary>
        /// Column for the <see cref="IIssue.ProviderType"/> field.
        /// </summary>
        ProviderType,

        /// <summary>
        /// Column for the <see cref="IIssue.ProviderName"/> field.
        /// </summary>
        ProviderName,

        /// <summary>
        /// Column for the <see cref="IIssue.Priority"/> field.
        /// </summary>
        Priority,

        /// <summary>
        /// Column for the <see cref="IIssue.PriorityName"/> field.
        /// </summary>
        PriorityName,

        /// <summary>
        /// Column for the <see cref="IIssue.ProjectFileRelativePath"/> field.
        /// </summary>
        ProjectPath,

        /// <summary>
        /// Column for the <see cref="IIssue.ProjectName"/> field.
        /// </summary>
        ProjectName,

        /// <summary>
        /// Column for the <see cref="IIssue.AffectedFileRelativePath"/> field.
        /// </summary>
        FilePath,

        /// <summary>
        /// Column for the value returned by <see cref="IIssueExtension.FilePath(IIssue)"/>.
        /// </summary>
        Path,

        /// <summary>
        /// Column for the value returned by <see cref="IIssueExtension.FileName(IIssue)"/>.
        /// </summary>
        File,

        /// <summary>
        /// Column for the <see cref="IIssue.Line"/> field.
        /// </summary>
        Line,

        /// <summary>
        /// Column for the <see cref="IIssue.Rule"/> field.
        /// </summary>
        Rule,

        /// <summary>
        /// Column for the <see cref="IIssue.RuleUrl"/> field.
        /// </summary>
        RuleUrl,

        /// <summary>
        /// Column for the <see cref="IIssue.Message"/> field.
        /// </summary>
        Message
    }
}
