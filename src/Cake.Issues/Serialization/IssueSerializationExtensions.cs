namespace Cake.Issues.Serialization
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cake.Core.IO;
    using LitJson;

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
            issue.NotNull(nameof(issue));

            return JsonMapper.ToJson(issue.ToSerializableIssue());
        }

        /// <summary>
        /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON string.
        /// </summary>
        /// <param name="issues">Issues which should be serialized.</param>
        /// <returns>Serialized issues.</returns>
        public static string SerializeToJsonString(this IEnumerable<IIssue> issues)
        {
            issues.NotNull(nameof(issues));

            return JsonMapper.ToJson(issues.Select(x => x.ToSerializableIssue()).ToArray());
        }

        /// <summary>
        /// Serializes an <see cref="IIssue"/> to a JSON file.
        /// </summary>
        /// <param name="issue">Issue which should be serialized.</param>
        /// <param name="filePath">Path to the file.</param>
        public static void SerializeToJsonFile(this IIssue issue, FilePath filePath)
        {
            issue.NotNull(nameof(issue));
            filePath.NotNull(nameof(filePath));

            using (var stream = File.Open(filePath.FullPath, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                JsonMapper.ToJson(issue.ToSerializableIssue(), new JsonWriter(writer));
            }
        }

        /// <summary>
        /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON file.
        /// </summary>
        /// <param name="issues">Issues which should be serialized.</param>
        /// <param name="filePath">Path to the file.</param>
        public static void SerializeToJsonFile(this IEnumerable<IIssue> issues, FilePath filePath)
        {
            issues.NotNull(nameof(issues));
            filePath.NotNull(nameof(filePath));

            using (var stream = File.Open(filePath.FullPath, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                JsonMapper.ToJson(issues.Select(x => x.ToSerializableIssue()).ToArray(), new JsonWriter(writer));
            }
        }

        /// <summary>
        /// Converts an <see cref="IIssue"/> to a <see cref="SerializableIssueV3"/>.
        /// </summary>
        /// <param name="issue">Issue which should be converted.</param>
        /// <returns>Converted issue.</returns>
        internal static SerializableIssueV3 ToSerializableIssue(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return new SerializableIssueV3
            {
                Identifier = issue.Identifier,
                ProjectFileRelativePath = issue.ProjectFileRelativePath?.FullPath,
                ProjectName = issue.ProjectName,
                AffectedFileRelativePath = issue.AffectedFileRelativePath?.FullPath,
                Line = issue.Line,
                Column = issue.Column,
                MessageText = issue.MessageText,
                MessageMarkdown = issue.MessageMarkdown,
                MessageHtml = issue.MessageHtml,
                Priority = issue.Priority,
                PriorityName = issue.PriorityName,
                Rule = issue.Rule,
                RuleUrl = issue.RuleUrl?.ToString(),
                Run = issue.Run,
                ProviderType = issue.ProviderType,
                ProviderName = issue.ProviderName,
            };
        }
    }
}
