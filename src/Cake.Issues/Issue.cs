namespace Cake.Issues
{
    using System;
    using Cake.Core.IO;

    /// <summary>
    /// Base class for an issue.
    /// </summary>
    public class Issue : IIssue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        /// <param name="projectFileRelativePath">The path to the project to which the file affected by the issue belongs.
        /// The path needs to be relative to the repository root.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <param name="projectName">The name of the project to which the file affected by the issue belongs.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <param name="affectedFileRelativePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <param name="column">The column in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <param name="messageText">The message of the issue in plain text format.</param>
        /// <param name="messageHtml">The message of the issue in Html format.</param>
        /// <param name="messageMarkdown">The message of the issue in Markdown format.</param>
        /// <param name="priority">The priority of the message.
        /// <c>null</c> if no priority was assigned.</param>
        /// <param name="priorityName">The human friendly name of the priority.
        /// <c>null</c> or <see cref="string.Empty"/> if no priority was assigned.</param>
        /// <param name="rule">The rule of the issue.
        /// <c>null</c> or <see cref="string.Empty"/> if issue has no specific rule ID.</param>
        /// <param name="ruleUrl">The URL containing information about the failing rule.
        /// <c>null</c> if no URL is available.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        public Issue(
            string projectFileRelativePath,
            string projectName,
            string affectedFileRelativePath,
            int? line,
            int? column,
            string messageText,
            string messageHtml,
            string messageMarkdown,
            int? priority,
            string priorityName,
            string rule,
            Uri ruleUrl,
            string providerType,
            string providerName)
        {
            line?.NotNegativeOrZero(nameof(line));
            column?.NotNegativeOrZero(nameof(column));
            messageText.NotNullOrWhiteSpace(nameof(messageText));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

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

            if (!line.HasValue && column.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(column), $"Cannot specify a column while not specifying a line.");
            }

            this.ProjectName = projectName;
            this.Line = line;
            this.Column = column;
            this.MessageText = messageText;
            this.MessageHtml = messageHtml;
            this.MessageMarkdown = messageMarkdown;
            this.Priority = priority;
            this.PriorityName = priorityName;
            this.Rule = rule;
            this.RuleUrl = ruleUrl;
            this.ProviderType = providerType;
            this.ProviderName = providerName;
        }

        /// <inheritdoc/>
        public FilePath ProjectFileRelativePath { get; }

        /// <inheritdoc/>
        public string ProjectName { get; }

        /// <inheritdoc/>
        public FilePath AffectedFileRelativePath { get; }

        /// <inheritdoc/>
        public int? Line { get; }

        /// <inheritdoc/>
        public int? Column { get; }

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
        public string Rule { get; }

        /// <inheritdoc/>
        public Uri RuleUrl { get; }

        /// <inheritdoc/>
        public string ProviderType { get; }

        /// <inheritdoc/>
        public string ProviderName { get; }
    }
}
