namespace Cake.Issues;

using System;
using System.Collections.Generic;
using Cake.Core.IO;

/// <summary>
/// Description of an issue.
/// </summary>
public interface IIssue
{
    /// <summary>
    /// Gets the identifier for the message.
    /// The identifier can be used to identify the same issue across multiple runs.
    /// </summary>
    string Identifier { get; }

    /// <summary>
    /// Gets the path to the project to which the file affected by the issue belongs.
    /// The path is relative to the repository root.
    /// Can be <c>null</c> if issue is not related to a project.
    /// </summary>
    FilePath ProjectFileRelativePath { get; }

    /// <summary>
    /// Gets the name of the project to which the file affected by the issue belongs.
    /// Can be <c>null</c> or <see cref="string.Empty"/> if issue is not related to a project.
    /// </summary>
    string ProjectName { get; }

    /// <summary>
    /// Gets the path to the file affected by the issue.
    /// The path is relative to the repository root.
    /// Can be <c>null</c> if issue is not related to a change in a file.
    /// </summary>
    FilePath AffectedFileRelativePath { get; }

    /// <summary>
    /// Gets the line in the file where the issues have occurred.
    /// <c>null</c> if the issue affects the whole file or an assembly.
    /// </summary>
    int? Line { get; }

    /// <summary>
    /// Gets the end of the line range in the file where the issues have occurred.
    /// <c>null</c> if the issue affects the whole file, an assembly or only a single line.
    /// </summary>
    int? EndLine { get; }

    /// <summary>
    /// Gets the column in the file where the issues have occurred.
    /// <c>null</c> if the issue affects the whole file or an assembly.
    /// </summary>
    int? Column { get; }

    /// <summary>
    /// Gets the end of the column range in the file where the issues have occurred.
    /// <c>null</c> if the issue affects the whole file, an assembly or only a single column.
    /// </summary>
    int? EndColumn { get; }

    /// <summary>
    /// Gets or sets a link to the position in the file where the issue occurred.
    /// <c>null</c> if <see cref="IReadIssuesSettings.FileLinkSettings"/> was not set while reading issue.
    /// </summary>
    Uri FileLink { get; set; }

    /// <summary>
    /// Gets the message of the issue in text format.
    /// </summary>
    string MessageText { get; }

    /// <summary>
    /// Gets the message of the issue in HTML format.
    /// </summary>
    string MessageHtml { get; }

    /// <summary>
    /// Gets the message of the issue in Markdown format.
    /// </summary>
    string MessageMarkdown { get; }

    /// <summary>
    /// Gets the priority of the message. A higher value indicates a higher priority.
    /// <c>null</c> if no priority was assigned.
    /// </summary>
    int? Priority { get; }

    /// <summary>
    /// Gets the human friendly name of the priority.
    /// <c>null</c> or <see cref="string.Empty"/> if no priority was assigned.
    /// </summary>
    string PriorityName { get; }

    /// <summary>
    /// Gets the id of the rule of the issue.
    /// Can be <c>null</c> or <see cref="string.Empty"/> if the issue provider provides no rule.
    /// </summary>
    string RuleId { get; }

    /// <summary>
    /// Gets the name of the rule of the issue.
    /// Can be <c>null</c> or <see cref="string.Empty"/> if the issue provider provides no rule.
    /// </summary>
    string RuleName { get; }

    /// <summary>
    /// Gets the URL containing information about the failing rule.
    /// Can be <c>null</c> if the issue provider provides no URL.
    /// </summary>
    Uri RuleUrl { get; }

    /// <summary>
    /// Gets or sets the description of the run.
    /// Can be <c>null</c> or <see cref="string.Empty"/> if no run information is provided.
    /// </summary>
    string Run { get; set; }

    /// <summary>
    /// Gets the type of the issue provider.
    /// </summary>
    string ProviderType { get; }

    /// <summary>
    /// Gets the human friendly name of the issue provider.
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    /// Gets a dictionary with additional information regarding the issue.
    /// </summary>
    IReadOnlyDictionary<string, string> AdditionalInformation { get; }

    /// <summary>
    /// Gets the source code snippet where the issue occurred.
    /// <c>null</c> or <see cref="string.Empty"/> if no snippet is available.
    /// </summary>
    string Snippet { get; }

    /// <summary>
    /// Gets the source language of the file where the issue occurred.
    /// <c>null</c> or <see cref="string.Empty"/> if the source language is not known.
    /// </summary>
    string SourceLanguage { get; }
}