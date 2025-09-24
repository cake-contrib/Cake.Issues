namespace Cake.Issues.Serialization;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Cake.Core.IO;

/// <summary>
/// Extensions for serializing an <see cref="IIssue"/> to the latest serialization format.
/// </summary>
public static class IssueSerializationExtensions
{
    /// <summary>
    /// Serializes an <see cref="IIssue"/> to a JSON string.
    /// </summary>
    /// <param name="issue">Issue which should be serialized.</param>
    /// <returns>Serialized issue.</returns>
    public static string SerializeToJsonString(this IIssue issue)
    {
        issue.NotNull();

        return JsonSerializer.Serialize(issue.ToSerializableIssue());
    }

    /// <summary>
    /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON string.
    /// </summary>
    /// <param name="issues">Issues which should be serialized.</param>
    /// <returns>Serialized issues.</returns>
    public static string SerializeToJsonString(this IEnumerable<IIssue> issues)
    {
        issues.NotNull();

        return JsonSerializer.Serialize(issues.Select(x => x.ToSerializableIssue()).ToArray());
    }

    /// <summary>
    /// Serializes an <see cref="IIssue"/> to a JSON file.
    /// </summary>
    /// <param name="issue">Issue which should be serialized.</param>
    /// <param name="filePath">Path to the file.</param>
    public static void SerializeToJsonFile(this IIssue issue, FilePath filePath)
    {
        issue.NotNull();
        filePath.NotNull();

        using (var stream = File.Open(filePath.FullPath, FileMode.Create))
        {
            JsonSerializer.Serialize(stream, issue.ToSerializableIssue());
        }
    }

    /// <summary>
    /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON file.
    /// </summary>
    /// <param name="issues">Issues which should be serialized.</param>
    /// <param name="filePath">Path to the file.</param>
    public static void SerializeToJsonFile(this IEnumerable<IIssue> issues, FilePath filePath)
    {
        issues.NotNull();
        filePath.NotNull();

        using (var stream = File.Open(filePath.FullPath, FileMode.Create))
        {
            JsonSerializer.Serialize(stream, issues.Select(x => x.ToSerializableIssue()).ToArray());
        }
    }

    /// <summary>
    /// Converts an <see cref="IIssue"/> to a <see cref="SerializableIssueV4"/>.
    /// </summary>
    /// <param name="issue">Issue which should be converted.</param>
    /// <returns>Converted issue.</returns>
    internal static SerializableIssueV5 ToSerializableIssue(this IIssue issue)
    {
        issue.NotNull();

        return new SerializableIssueV5
        {
            Identifier = issue.Identifier,
            ProjectFileRelativePath = issue.ProjectFileRelativePath?.FullPath,
            ProjectName = issue.ProjectName,
            AffectedFileRelativePath = issue.AffectedFileRelativePath?.FullPath,
            Line = issue.Line,
            EndLine = issue.EndLine,
            Column = issue.Column,
            EndColumn = issue.EndColumn,
            FileLink = issue.FileLink?.ToString(),
            MessageText = issue.MessageText,
            MessageMarkdown = issue.MessageMarkdown,
            MessageHtml = issue.MessageHtml,
            Priority = issue.Priority,
            PriorityName = issue.PriorityName,
            RuleId = issue.RuleId,
            RuleName = issue.RuleName,
            RuleUrl = issue.RuleUrl?.ToString(),
            Run = issue.Run,
            ProviderType = issue.ProviderType,
            ProviderName = issue.ProviderName,
            AdditionalInformation = issue.AdditionalInformation.ToDictionary(p => p.Key, p => p.Value),
            Snippet = issue.Snippet,
            SourceLanguage = issue.SourceLanguage,
        };
    }
}
