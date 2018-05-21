namespace Cake.Issues
{
    using System;
    using Core.IO;

    /// <summary>
    /// Description of an issue.
    /// </summary>
    public interface IIssue
    {
        /// <summary>
        /// Gets the name of the project to which the file affected by the issue belongs.
        /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.
        /// </summary>
        string Project { get; }

        /// <summary>
        /// Gets the path to the file affacted by the issue.
        /// The path is relative to the repository root.
        /// Can be <c>null</c> if issue is not related to a change in a file.
        /// </summary>
        FilePath AffectedFileRelativePath { get; }

        /// <summary>
        /// Gets the line in the file where the issues has occurred.
        /// <c>null</c> if the issue affects the whole file or an asssembly.
        /// </summary>
        int? Line { get; }

        /// <summary>
        /// Gets the message of the issue.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the priority of the message. A higher value indicates a higher priority.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Gets the rule of the issue.
        /// Can be <see cref="string.Empty"/> if the issue provider provides no rule.
        /// </summary>
        string Rule { get; }

        /// <summary>
        /// Gets the URL containing information about the failing rule.
        /// Can be <c>null</c> if the issue provider provides no URL.
        /// </summary>
        Uri RuleUrl { get; }

        /// <summary>
        /// Gets the type of the issue provider.
        /// </summary>
        string ProviderType { get; }
    }
}