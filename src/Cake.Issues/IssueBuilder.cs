namespace Cake.Issues
{
    using System;

    /// <summary>
    /// Class to create instances of <see cref="IIssue"/> with a fluent API.
    /// </summary>
    public class IssueBuilder
    {
        private readonly string providerType;
        private readonly string providerName;
        private readonly string messageText;
        private string messageHtml;
        private string messageMarkdown;
        private string projectFileRelativePath;
        private string projectName;
        private string filePath;
        private int? line;
        private int? priority;
        private string priorityName;
        private string rule;
        private Uri ruleUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueBuilder"/> class.
        /// </summary>
        /// <param name="message">The message of the issue in plain text format.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        private IssueBuilder(
            string message,
            string providerType,
            string providerName)
        {
            message.NotNullOrWhiteSpace(nameof(message));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

            this.messageText = message;
            this.providerType = providerType;
            this.providerName = providerName;
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/>.
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

            return new IssueBuilder(message, providerType, providerName);
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/>.
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

            return new IssueBuilder(message, typeof(T).FullName, issueProvider.ProviderName);
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

            this.filePath = filePath;
            this.line = line;

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
        /// Creates a new <see cref="IIssue"/>.
        /// </summary>
        /// <returns>New issue object.</returns>
        public IIssue Create()
        {
            return
                new Issue(
                    this.projectFileRelativePath,
                    this.projectName,
                    this.filePath,
                    this.line,
                    this.messageText,
                    this.messageHtml,
                    this.messageMarkdown,
                    this.priority,
                    this.priorityName,
                    this.rule,
                    this.ruleUrl,
                    this.providerType,
                    this.providerName);
        }
    }
}
