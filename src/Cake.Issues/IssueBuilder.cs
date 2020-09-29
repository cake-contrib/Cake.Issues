﻿namespace Cake.Issues
{
    using System;

    /// <summary>
    /// Class to create instances of <see cref="IIssue"/> with a fluent API.
    /// </summary>
    public class IssueBuilder
    {
        private readonly string identifier;
        private readonly string providerType;
        private readonly string providerName;
        private readonly string messageText;
        private string messageHtml;
        private string messageMarkdown;
        private string projectFileRelativePath;
        private string projectName;
        private string filePath;
        private int? line;
        private int? endLine;
        private int? column;
        private int? endColumn;
        private Uri fileLink;
        private int? priority;
        private string priorityName;
        private string rule;
        private Uri ruleUrl;
        private string run;
        private FileLinkSettings fileLinkSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueBuilder"/> class.
        /// </summary>
        /// <param name="identifier">The identifier of the message.</param>
        /// <param name="message">The message of the issue in plain text format.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        private IssueBuilder(
            string identifier,
            string message,
            string providerType,
            string providerName)
        {
#pragma warning disable SA1123 // Do not place regions within elements
            #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

            identifier.NotNullOrWhiteSpace(nameof(identifier));
            message.NotNullOrWhiteSpace(nameof(message));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

            #endregion

            this.identifier = identifier;
            this.messageText = message;
            this.providerType = providerType;
            this.providerName = providerName;
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/> with <paramref name="message"/>
        /// as identifier.
        /// </summary>
        /// <typeparam name="T">Type of the issue provider which has the issue created.</typeparam>
        /// <param name="message">The message of the issue in plain text format.</param>
        /// <param name="issueProvider">Issue provider which has the issue created.</param>
        /// <returns>Builder class for creating a new issue.</returns>
        public static IssueBuilder NewIssue<T>(
            string message,
            T issueProvider)
            where T : IIssueProvider
        {
            if (issueProvider == null)
            {
                throw new ArgumentNullException(nameof(issueProvider));
            }

            message.NotNullOrWhiteSpace(nameof(message));

            return NewIssue(message, message, issueProvider);
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the issue provider which has the issue created.</typeparam>
        /// <param name="identifier">The identifier of the message.</param>
        /// <param name="message">The message of the issue in plain text format.</param>
        /// <param name="issueProvider">Issue provider which has the issue created.</param>
        /// <returns>Builder class for creating a new issue.</returns>
        public static IssueBuilder NewIssue<T>(
            string identifier,
            string message,
            T issueProvider)
            where T : IIssueProvider
        {
            if (issueProvider == null)
            {
                throw new ArgumentNullException(nameof(issueProvider));
            }

            message.NotNullOrWhiteSpace(nameof(message));

            return NewIssue(identifier, message, issueProvider.ProviderType, issueProvider.ProviderName);
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/> with <paramref name="message"/> as identifier.
        /// </summary>
        /// <param name="message">The message of the issue in plain text format.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        /// <returns>Builder class for creating a new issue.</returns>
        public static IssueBuilder NewIssue(
            string message,
            string providerType,
            string providerName)
        {
            message.NotNullOrWhiteSpace(nameof(message));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

            return NewIssue(message, message, providerType, providerName);
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/>.
        /// </summary>
        /// <param name="identifier">The identifier of the message.</param>
        /// <param name="message">The message of the issue in plain text format.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        /// <returns>Builder class for creating a new issue.</returns>
        public static IssueBuilder NewIssue(
            string identifier,
            string message,
            string providerType,
            string providerName)
        {
#pragma warning disable SA1123 // Do not place regions within elements
            #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

            identifier.NotNullOrWhiteSpace(nameof(identifier));
            message.NotNullOrWhiteSpace(nameof(message));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

            #endregion

            return new IssueBuilder(identifier, message, providerType, providerName);
        }

        /// <summary>
        /// Sets the message in HTML format.
        /// </summary>
        /// <param name="message">Message in HTML format.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue doesn't have a message in HTML format.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithMessageInHtmlFormat(string message)
        {
            this.messageHtml = message;

            return this;
        }

        /// <summary>
        /// Sets the message in Markdown format.
        /// </summary>
        /// <param name="message">Message in Markdown format.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue doesn't have a message in Markdown format.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithMessageInMarkdownFormat(string message)
        {
            this.messageMarkdown = message;

            return this;
        }

        /// <summary>
        /// Sets the path of the project to which the file affected by the issue belongs.
        /// </summary>
        /// <param name="projectFileRelativePath">The path to the project to which the file affected by the issue belongs.
        /// The path needs to be relative to the repository root.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InProjectFile(string projectFileRelativePath)
        {
            this.projectFileRelativePath = projectFileRelativePath;

            return this;
        }

        /// <summary>
        /// Sets the name of the project to which the file affected by the issue belongs.
        /// </summary>
        /// <param name="projectName">Name of the project to which the file affected by the issue belongs.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InProjectOfName(string projectName)
        {
            this.projectName = projectName;

            return this;
        }

        /// <summary>
        /// Sets the project to which the file affected by the issue belongs.
        /// </summary>
        /// <param name="projectFileRelativePath">The path to the project to which the file affected by the issue belongs.
        /// The path needs to be relative to the repository root.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <param name="projectName">Name of the project to which the file affected by the issue belongs.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InProject(string projectFileRelativePath, string projectName)
        {
            this.projectFileRelativePath = projectFileRelativePath;
            this.projectName = projectName;

            return this;
        }

        /// <summary>
        /// Sets the path to the file affected by the issue.
        /// </summary>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InFile(string filePath)
        {
            this.filePath = filePath;

            return this;
        }

        /// <summary>
        /// Sets the path to the file affected by the issue and the line in the file where the issues has occurred.
        /// </summary>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InFile(string filePath, int? line)
        {
            line?.NotNegativeOrZero(nameof(line));

            this.InFile(filePath, line, null);

            return this;
        }

        /// <summary>
        /// Sets the path to the file affected by the issue and the line and column in the file where the issues has occurred.
        /// </summary>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <param name="column">The column in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InFile(string filePath, int? line, int? column)
        {
            line?.NotNegativeOrZero(nameof(line));
            column?.NotNegativeOrZero(nameof(column));

            this.InFile(filePath, line, null, column, null);

            return this;
        }

        /// <summary>
        /// Sets the path to the file affected by the issue and the line and column in the file where the issues has occurred.
        /// </summary>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="startLine">The line in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <param name="endLine">The end of the line range in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file, an asssembly or only a single line.</param>
        /// <param name="startColumn">The column in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.</param>
        /// <param name="endColumn">The end of the column range in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file, an asssembly or only a single column.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InFile(string filePath, int? startLine, int? endLine, int? startColumn, int? endColumn)
        {
            startLine?.NotNegativeOrZero(nameof(startLine));
            endLine?.NotNegativeOrZero(nameof(endLine));
            startColumn?.NotNegativeOrZero(nameof(startColumn));
            endColumn?.NotNegativeOrZero(nameof(endColumn));

            this.filePath = filePath;
            this.line = startLine;
            this.endLine = endLine;
            this.column = startColumn;
            this.endColumn = endColumn;

            return this;
        }

        /// <summary>
        /// Sets the the link to the position in the file where the issue ocurred.
        /// </summary>
        /// <param name="fileLink">Link to the position in the file where the issue ocurred.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithFileLink(Uri fileLink)
        {
            fileLink.NotNull(nameof(fileLink));

            this.fileLink = fileLink;

            return this;
        }

        /// <summary>
        /// Sets a <see cref="FileLinkSettings"/> to create the link of the position in the file where the issue ocurred.
        /// </summary>
        /// <param name="fileLinkSettings">Settings to create the link of the position in the file where the issue ocurred.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithFileLinkSettings(FileLinkSettings fileLinkSettings)
        {
            fileLinkSettings.NotNull(nameof(fileLinkSettings));

            this.fileLinkSettings = fileLinkSettings;

            return this;
        }

        /// <summary>
        /// Sets the priority of the issue.
        /// </summary>
        /// <param name="priority">The priority of the issue.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithPriority(IssuePriority priority)
        {
            return this.WithPriority((int)priority, priority.ToString());
        }

        /// <summary>
        /// Sets the priority of the issue.
        /// </summary>
        /// <param name="value">The priority of the issue.
        /// <c>null</c> if no priority should be assigned.</param>
        /// <param name="name">The human friendly name of the priority.
        /// <c>null</c> or <see cref="string.Empty"/> if no priority should be assigned.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithPriority(int? value, string name)
        {
            this.priority = value;
            this.priorityName = name;

            return this;
        }

        /// <summary>
        /// Sets the rule of the issue.
        /// </summary>
        /// <param name="name">The rule of the issue.
        /// <c>null</c> or <see cref="string.Empty"/> if issue has no specific rule ID.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder OfRule(string name)
        {
            this.rule = name;

            return this;
        }

        /// <summary>
        /// Sets the rule of the issue.
        /// </summary>
        /// <param name="name">The rule of the issue.
        /// <c>null</c> or <see cref="string.Empty"/> if issue has no specific rule ID.</param>
        /// <param name="uri">The URL containing information about the failing rule.
        /// <c>null</c> if no URL is available.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder OfRule(string name, Uri uri)
        {
            this.rule = name;
            this.ruleUrl = uri;

            return this;
        }

        /// <summary>
        /// Sets the name of the run where the issue was reported.
        /// </summary>
        /// <param name="run">The name of the run where the issue was reported.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder ForRun(string run)
        {
            run.NotNullOrWhiteSpace(nameof(run));

            this.run = run;

            return this;
        }

        /// <summary>
        /// Creates a new <see cref="IIssue"/>.
        /// </summary>
        /// <returns>New issue object.</returns>
        public IIssue Create()
        {
            var issue = this.CreateIssue(this.fileLink);

            if (this.fileLink != null || this.fileLinkSettings == null)
            {
                return issue;
            }

            // Recreate the issue here to pass the resolved file link this time.
            return this.CreateIssue(this.fileLinkSettings.GetFileLink(issue));
        }

        private Issue CreateIssue(Uri fileLink)
        {
            return new Issue(
                this.identifier,
                this.projectFileRelativePath,
                this.projectName,
                this.filePath,
                this.line,
                this.endLine,
                this.column,
                this.endColumn,
                fileLink,
                this.messageText,
                this.messageHtml,
                this.messageMarkdown,
                this.priority,
                this.priorityName,
                this.rule,
                this.ruleUrl,
                this.run,
                this.providerType,
                this.providerName);
        }
    }
}
