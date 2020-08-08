﻿namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Name of columns in a report.
    /// The value can be used for default positions of the columns.
    /// </summary>
    public enum ReportColumn
    {
        /// <summary>
        /// Column for the <see cref="IIssue.ProviderType"/> field.
        /// </summary>
        ProviderType = 100,

        /// <summary>
        /// Column for the <see cref="IIssue.ProviderName"/> field.
        /// </summary>
        ProviderName = 200,

        /// <summary>
        /// Column for the <see cref="IIssue.Priority"/> field.
        /// </summary>
        Priority = 300,

        /// <summary>
        /// Column for the <see cref="IIssue.PriorityName"/> field.
        /// </summary>
        PriorityName = 400,

        /// <summary>
        /// Column for the <see cref="IIssue.ProjectFileRelativePath"/> field.
        /// </summary>
        ProjectPath = 500,

        /// <summary>
        /// Column for the <see cref="IIssue.ProjectName"/> field.
        /// </summary>
        ProjectName = 600,

        /// <summary>
        /// Column for the <see cref="IIssue.AffectedFileRelativePath"/> field.
        /// </summary>
        FilePath = 700,

        /// <summary>
        /// Column for the value returned by <see cref="Cake.Issues.IIssueExtensions.FilePath(IIssue)"/>.
        /// </summary>
        FileDirectory = 800,

        /// <summary>
        /// Column for the value returned by <see cref="Cake.Issues.IIssueExtensions.FileName(IIssue)"/>.
        /// </summary>
        FileName = 900,

        /// <summary>
        /// Column for the <see cref="IIssue.Line"/> field.
        /// </summary>
        Line = 1000,

        /// <summary>
        /// Column for the <see cref="IIssue.Rule"/> field.
        /// </summary>
        Rule = 1100,

        /// <summary>
        /// Column for the <see cref="IIssue.RuleUrl"/> field.
        /// </summary>
        RuleUrl = 1200,

        /// <summary>
        /// Column for the message. This can either be the <see cref="IIssue.MessageText"/>,
        /// <see cref="IIssue.MessageHtml"/> or <see cref="IIssue.MessageMarkdown"/> field,
        /// depending on the report.
        /// </summary>
        Message = 1300,

        /// <summary>
        /// Column for the <see cref="IIssue.Run"/> field.
        /// </summary>
        Run = 1400,
    }
}
