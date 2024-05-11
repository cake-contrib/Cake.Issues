namespace Cake.Issues
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.IO;

    /// <summary>
    /// Base class for an issue.
    /// </summary>
    public class Issue : IIssue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        /// <param name="identifier">The identifier of the issue.
        /// The identifier needs to be identical across multiple runs of an issue provider for the same issue.</param>
        /// <param name="projectFileRelativePath">The path to the project to which the file affected by the issue belongs.
        /// The path needs to be relative to the repository root.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <param name="projectName">The name of the project to which the file affected by the issue belongs.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <param name="affectedFileRelativePath">The path to the file affected by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues have occurred.
        /// <c>null</c> if the issue affects the whole file or an assembly.</param>
        /// <param name="endLine">The end of the line range in the file where the issues have occurred.
        /// <c>null</c> if the issue affects the whole file, an assembly or only a single line.</param>
        /// <param name="column">The column in the file where the issues have occurred.
        /// <c>null</c> if the issue affects the whole file or an assembly.</param>
        /// <param name="endColumn">The end of the column range in the file where the issues have occurred.
        /// <c>null</c> if the issue affects the whole file, an assembly or only a single column.</param>
        /// <param name="fileLink">Link to the position in the file where the issue occurred.
        /// <c>null</c> if no link is available.</param>
        /// <param name="messageText">The message of the issue in plain text format.</param>
        /// <param name="messageHtml">The message of the issue in Html format.</param>
        /// <param name="messageMarkdown">The message of the issue in Markdown format.</param>
        /// <param name="priority">The priority of the message.
        /// <c>null</c> if no priority was assigned.</param>
        /// <param name="priorityName">The human friendly name of the priority.
        /// <c>null</c> or <see cref="string.Empty"/> if no priority was assigned.</param>
        /// <param name="ruleId">The ID of the rule of the issue.
        /// <c>null</c> or <see cref="string.Empty"/> if issue has no specific rule.</param>
        /// <param name="ruleName">The name of the rule of the issue.
        /// <c>null</c> or <see cref="string.Empty"/> if issue has no specific rule.</param>
        /// <param name="ruleUrl">The URL containing information about the failing rule.
        /// <c>null</c> if no URL is available.</param>
        /// <param name="run">Gets the description of the run.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        /// <param name="additionalInformation">Custom information regarding the issue.</param>
        public Issue(
            string identifier,
            string projectFileRelativePath,
            string projectName,
            string affectedFileRelativePath,
            int? line,
            int? endLine,
            int? column,
            int? endColumn,
            Uri fileLink,
            string messageText,
            string messageHtml,
            string messageMarkdown,
            int? priority,
            string priorityName,
            string ruleId,
            string ruleName,
            Uri ruleUrl,
            string run,
            string providerType,
            string providerName,
            IReadOnlyDictionary<string, string> additionalInformation)
        {
            identifier.NotNullOrWhiteSpace();
            line?.NotNegativeOrZero();
            endLine?.NotNegativeOrZero();
            column?.NotNegativeOrZero();
            endColumn?.NotNegativeOrZero();
            messageText.NotNullOrWhiteSpace();
            providerType.NotNullOrWhiteSpace();
            providerName.NotNullOrWhiteSpace();

            // File path needs to be relative to the repository root.
            if (!string.IsNullOrWhiteSpace(projectFileRelativePath))
            {
                if (!projectFileRelativePath.IsValidPath())
                {
                    throw new ArgumentException($"Invalid path '{projectFileRelativePath}'", nameof(projectFileRelativePath));
                }

                this.ProjectFileRelativePath = projectFileRelativePath;

                if (!this.ProjectFileRelativePath.IsRelative)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(projectFileRelativePath),
                        $"Project file path '{this.ProjectFileRelativePath}' needs to be relative to the repository root.");
                }
            }

            // File path needs to be relative to the repository root.
            if (!string.IsNullOrWhiteSpace(affectedFileRelativePath))
            {
                if (!affectedFileRelativePath.IsValidPath())
                {
                    throw new ArgumentException($"Invalid path '{affectedFileRelativePath}'", nameof(affectedFileRelativePath));
                }

                this.AffectedFileRelativePath = affectedFileRelativePath;

                if (!this.AffectedFileRelativePath.IsRelative)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(affectedFileRelativePath),
                        $"File path '{this.AffectedFileRelativePath}' needs to be relative to the repository root.");
                }
            }

            if (this.AffectedFileRelativePath == null && line.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(line), "Cannot specify a line while not specifying a file.");
            }

            if (!line.HasValue && (column.HasValue || endColumn.HasValue))
            {
                throw new ArgumentOutOfRangeException(nameof(column), "Cannot specify a column while not specifying a line.");
            }

            if (!line.HasValue && endLine.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(endLine), "Cannot specify the end of line range while not specifying start of line range.");
            }

            if (line.HasValue && endLine.HasValue && line.Value > endLine.Value)
            {
                throw new ArgumentOutOfRangeException(nameof(endLine), "Line range needs to end after start of range.");
            }

            if (!column.HasValue && endColumn.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(endColumn), "Cannot specify the end of column range while not specifying start of column range.");
            }

            // End column is not allowed to be before start column, except if issue is across multiple lines.
            if (column.HasValue && endColumn.HasValue && (!endLine.HasValue || endLine.Value == line.Value) && column.Value > endColumn.Value)
            {
                throw new ArgumentOutOfRangeException(nameof(endColumn), "Column range needs to end after start of range.");
            }

            this.Identifier = identifier;
            this.ProjectName = projectName;
            this.Line = line;
            this.EndLine = endLine;
            this.Column = column;
            this.EndColumn = endColumn;
            this.FileLink = fileLink;
            this.MessageText = messageText;
            this.MessageHtml = messageHtml;
            this.MessageMarkdown = messageMarkdown;
            this.Priority = priority;
            this.PriorityName = priorityName;
            this.RuleId = ruleId;
            this.RuleName = ruleName;
            this.RuleUrl = ruleUrl;
            this.Run = run;
            this.ProviderType = providerType;
            this.ProviderName = providerName;
            this.AdditionalInformation = additionalInformation ?? new Dictionary<string, string>();
        }

        /// <inheritdoc/>
        public string Identifier { get; }

        /// <inheritdoc/>
        public FilePath ProjectFileRelativePath { get; }

        /// <inheritdoc/>
        public string ProjectName { get; }

        /// <inheritdoc/>
        public FilePath AffectedFileRelativePath { get; }

        /// <inheritdoc/>
        public int? Line { get; }

        /// <inheritdoc/>
        public int? EndLine { get; }

        /// <inheritdoc/>
        public int? Column { get; }

        /// <inheritdoc/>
        public int? EndColumn { get; }

        /// <inheritdoc/>
        public Uri FileLink { get; set; }

        /// <inheritdoc/>
        public string MessageText { get; }

        /// <inheritdoc/>
        public string MessageHtml { get; }

        /// <inheritdoc/>
        public string MessageMarkdown { get; }

        /// <inheritdoc/>
        public int? Priority { get; }

        /// <inheritdoc/>
        public string PriorityName { get; }

        /// <inheritdoc/>
        public string RuleId { get; }

        /// <inheritdoc/>
        public string RuleName { get; }

        /// <inheritdoc/>
        public Uri RuleUrl { get; }

        /// <inheritdoc/>
        public string Run { get; set; }

        /// <inheritdoc/>
        public string ProviderType { get; }

        /// <inheritdoc/>
        public string ProviderName { get; }

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, string> AdditionalInformation { get; }
    }
}
