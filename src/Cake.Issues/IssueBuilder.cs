namespace Cake.Issues
{
    using System;

    /// <summary>
    /// Class to create instances of <see cref="Issue"/> with a fluent API.
    /// </summary>
    public class IssueBuilder
    {
        private readonly string message;
        private readonly string providerType;
        private readonly string providerName;
        private string project;
        private string filePath;
        private int? line;
        private int priority;
        private string priorityName;
        private string rule;
        private Uri ruleUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueBuilder"/> class.
        /// </summary>
        /// <param name="message">The message of the issue.</param>
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

            this.message = message;
            this.providerType = providerType;
            this.providerName = providerName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Issue"/> class.
        /// </summary>
        /// <param name="message">The message of the issue.</param>
        /// <param name="providerType">The type of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        /// <returns>New issue</returns>
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
        /// Creates a new instance of the <see cref="Issue"/> class.
        /// </summary>
        /// <typeparam name="T">Type of the issue provider which has the issue created.</typeparam>
        /// <param name="message">The message of the issue.</param>
        /// <param name="issueProvider">Issue provider which has the issue created.</param>
        /// <returns>New issue</returns>
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
        /// Sets the name of the project to which the file affected by the issue belongs.
        /// </summary>
        /// <param name="name">Name of the project to which the file affected by the issue belongs.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder InProject(string name)
        {
            this.project = name;

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
        /// Sets the priority of the message.
        /// </summary>
        /// <param name="priority">The priority of the message.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithPriority(IssuePriority priority)
        {
            return this.WithPriority((int)priority, priority.ToString());
        }

        /// <summary>
        /// Sets the priority of the message.
        /// </summary>
        /// <param name="value">The priority of the message.
        /// <c>null</c> if no priority should be assigned.</param>
        /// <param name="name">The human friendly name of the priority.
        /// <c>null</c> or <see cref="string.Empty"/> if no priority should be assigned.</param>
        /// <returns>Issue Builder instance.</returns>
        public IssueBuilder WithPriority(int value, string name)
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
        /// Creates a new <see cref="Issue"/>.
        /// </summary>
        /// <returns>New issue object.</returns>
        public IIssue Create()
        {
            return
                new Issue(
                    this.project,
                    this.filePath,
                    this.line,
                    this.message,
                    this.priority,
                    this.priorityName,
                    this.rule,
                    this.ruleUrl,
                    this.providerType,
                    this.providerName);
        }
    }
}
