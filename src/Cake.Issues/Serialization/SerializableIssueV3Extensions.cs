namespace Cake.Issues.Serialization;

using System;
using System.Collections.Generic;

/// <summary>
/// Extensions for <see cref="SerializableIssueV3"/>.
/// </summary>
internal static class SerializableIssueV3Extensions
{
    /// <summary>
    /// Converts a <see cref="SerializableIssueV3"/> to an <see cref="Issue"/>.
    /// </summary>
    /// <param name="serializableIssue">Issue which should be converted.</param>
    /// <returns>Converted issue.</returns>
    internal static Issue ToIssue(this SerializableIssueV3 serializableIssue)
    {
        serializableIssue.NotNull();

        Uri ruleUrl = null;
        if (!string.IsNullOrWhiteSpace(serializableIssue.RuleUrl))
        {
            ruleUrl = new Uri(serializableIssue.RuleUrl);
        }

        Uri fileLink = null;
        if (!string.IsNullOrWhiteSpace(serializableIssue.FileLink))
        {
            fileLink = new Uri(serializableIssue.FileLink);
        }

        return new Issue(
            serializableIssue.Identifier,
            serializableIssue.ProjectFileRelativePath,
            serializableIssue.ProjectName,
            serializableIssue.AffectedFileRelativePath,
            serializableIssue.Line,
            serializableIssue.EndLine,
            serializableIssue.Column,
            serializableIssue.EndColumn,
            null, // offset
            null, // endOffset
            fileLink,
            serializableIssue.MessageText,
            serializableIssue.MessageHtml,
            serializableIssue.MessageMarkdown,
            serializableIssue.Priority,
            serializableIssue.PriorityName,
            serializableIssue.Rule,
            null,
            ruleUrl,
            serializableIssue.Run,
            serializableIssue.ProviderType,
            serializableIssue.ProviderName,
            new Dictionary<string, string>());
    }
}
