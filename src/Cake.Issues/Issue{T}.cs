namespace Cake.Issues
{
    using System;

    /// <summary>
    /// Base class for an issue.
    /// </summary>
    /// <typeparam name="T">Type of the issue provider which has raised the issue.</typeparam>
    public class Issue<T> : Issue
        where T : IIssueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Issue{T}"/> class.
        /// </summary>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// Nothing if the issue affects the whole file or an asssembly.</param>
        /// <param name="message">The message of the issue.</param>
        /// <param name="priority">The priority of the message.</param>
        /// <param name="rule">The rule of the issue.</param>
        public Issue(
            string filePath,
            int? line,
            string message,
            int priority,
            string rule)
            : base(filePath, line, message, priority, rule, typeof(T).FullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue{T}"/> class.
        /// </summary>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// Nothing if the issue affects the whole file or an asssembly.</param>
        /// <param name="message">The message of the issue.</param>
        /// <param name="priority">The priority of the message.</param>
        /// <param name="rule">The rule of the issue.</param>
        /// <param name="ruleUrl">The URL containing information about the failing rule.</param>
        public Issue(
            string filePath,
            int? line,
            string message,
            int priority,
            string rule,
            Uri ruleUrl)
            : base(filePath, line, message, priority, rule, ruleUrl, GetProviderTypeName())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue{T}"/> class.
        /// </summary>
        /// <param name="project">Name of the project to which the file affected by the issue belongs</param>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// Nothing if the issue affects the whole file or an asssembly.</param>
        /// <param name="message">The message of the issue.</param>
        /// <param name="priority">The priority of the message.</param>
        /// <param name="rule">The rule of the issue.</param>
        public Issue(
            string project,
            string filePath,
            int? line,
            string message,
            int priority,
            string rule)
            : base(project, filePath, line, message, priority, rule, null, GetProviderTypeName())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue{T}"/> class.
        /// </summary>
        /// <param name="project">Name of the project to which the file affected by the issue belongs</param>
        /// <param name="filePath">The path to the file affacted by the issue.
        /// The path needs to be relative to the repository root.
        /// <c>null</c> or <see cref="string.Empty"/> if issue is not related to a change in a file.</param>
        /// <param name="line">The line in the file where the issues has occurred.
        /// Nothing if the issue affects the whole file or an asssembly.</param>
        /// <param name="message">The message of the issue.</param>
        /// <param name="priority">The priority of the message.</param>
        /// <param name="rule">The rule of the issue.</param>
        /// <param name="ruleUrl">The URL containing information about the failing rule.</param>
        public Issue(
            string project,
            string filePath,
            int? line,
            string message,
            int priority,
            string rule,
            Uri ruleUrl)
            : base(project, filePath, line, message, priority, rule, ruleUrl, GetProviderTypeName())
        {
        }

        /// <summary>
        /// Gets the name of the issue provider as it will be set to in the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <returns>Name of the issue provider.</returns>
        public static string GetProviderTypeName()
        {
            return typeof(T).FullName;
        }
    }
}
