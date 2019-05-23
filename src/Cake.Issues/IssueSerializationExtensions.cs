namespace Cake.Issues
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Cake.Core.IO;

    /// <summary>
    /// Extensions for serializing and deserializing an <see cref="IIssue"/>.
    /// </summary>
    internal static class IssueSerializationExtensions
    {
        /// <summary>
        /// Serializes an <see cref="IIssue"/> to a JSON string.
        /// </summary>
        /// <param name="issue">Issue which should be serialized.</param>
        /// <returns>Serialized issue.</returns>
        public static string SerializeToJsonString(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            var serializer = new DataContractJsonSerializer(typeof(SerializableIssue));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, issue.ToSerializableIssue());
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON string.
        /// </summary>
        /// <param name="issues">Issues which should be serialized.</param>
        /// <returns>Serialized issues.</returns>
        public static string SerializeToJsonString(this IEnumerable<IIssue> issues)
        {
            issues.NotNull(nameof(issues));

            var serializer = new DataContractJsonSerializer(typeof(IEnumerable<SerializableIssue>));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, issues.Select(x => x.ToSerializableIssue()));
                return Encoding.UTF8.GetString(stream.ToArray());
            }
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

            var serializer = new DataContractJsonSerializer(typeof(SerializableIssue));

            using (var stream = File.Open(filePath.FullPath, FileMode.Create))
            {
                serializer.WriteObject(stream, issue.ToSerializableIssue());
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

            var serializer = new DataContractJsonSerializer(typeof(IEnumerable<SerializableIssue>));

            using (var stream = File.Open(filePath.FullPath, FileMode.Create))
            {
                serializer.WriteObject(stream, issues.Select(x => x.ToSerializableIssue()));
            }
        }

        /// <summary>
        /// Deserializes an <see cref="Issue"/> from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON representation of the issue.</param>
        /// <returns>Instance of the issue.</returns>
        public static Issue DeserializeToIssue(this string jsonString)
        {
            jsonString.NotNullOrWhiteSpace(nameof(jsonString));

            var serializer = new DataContractJsonSerializer(typeof(SerializableIssue));

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonString)))
            {
                var deserializedIssue = serializer.ReadObject(stream) as SerializableIssue;

                if (deserializedIssue == null)
                {
                    return null;
                }

                return deserializedIssue.ToIssue();
            }
        }

        /// <summary>
        /// Deserializes an <see cref="IEnumerable{IIssue}"/> from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON representation of the issues.</param>
        /// <returns>List of issues.</returns>
        public static IEnumerable<Issue> DeserializeToIssues(this string jsonString)
        {
            jsonString.NotNullOrWhiteSpace(nameof(jsonString));

            var serializer = new DataContractJsonSerializer(typeof(SerializableIssue));

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonString)))
            {
                var deserializedIssue = serializer.ReadObject(stream) as IEnumerable<SerializableIssue>;

                if (deserializedIssue == null)
                {
                    return new List<Issue>();
                }

                return deserializedIssue.Select(x => x.ToIssue());
            }
        }

        /// <summary>
        /// Deserializes an <see cref="Issue"/> from a JSON file.
        /// </summary>
        /// <param name="filePath">Path to the JSON file.</param>
        /// <returns>Instance of the issue.</returns>
        public static Issue DeserializeToIssue(this FilePath filePath)
        {
            filePath.NotNull(nameof(filePath));

            var serializer = new DataContractJsonSerializer(typeof(SerializableIssue));

            using (var stream = File.Open(filePath.FullPath, FileMode.Open))
            {
                var deserializedIssue = serializer.ReadObject(stream) as SerializableIssue;

                if (deserializedIssue == null)
                {
                    return null;
                }

                return deserializedIssue.ToIssue();
            }
        }

        /// <summary>
        /// Deserializes an <see cref="IEnumerable{IIssue}"/> from a JSON file.
        /// </summary>
        /// <param name="filePath">Path to the JSON file.</param>
        /// <returns>List of issues.</returns>
        public static IEnumerable<Issue> DeserializeToIssues(this FilePath filePath)
        {
            filePath.NotNull(nameof(filePath));

            var serializer = new DataContractJsonSerializer(typeof(SerializableIssue));

            using (var stream = File.Open(filePath.FullPath, FileMode.Open))
            {
                var deserializedIssue = serializer.ReadObject(stream) as IEnumerable<SerializableIssue>;

                if (deserializedIssue == null)
                {
                    return new List<Issue>();
                }

                return deserializedIssue.Select(x => x.ToIssue());
            }
        }

        /// <summary>
        /// Converts an <see cref="IIssue"/> to a <see cref="SerializableIssue"/>.
        /// </summary>
        /// <param name="issue">Issue which should be converted.</param>
        /// <returns>Converted issue.</returns>
        internal static SerializableIssue ToSerializableIssue(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return new SerializableIssue
            {
                ProjectFileRelativePath = issue.ProjectFileRelativePath?.FullPath,
                ProjectName = issue.ProjectName,
                AffectedFileRelativePath = issue.AffectedFileRelativePath?.FullPath,
                Line = issue.Line,
                Message = issue.Message,
                Priority = issue.Priority,
                PriorityName = issue.PriorityName,
                Rule = issue.Rule,
                RuleUrl = issue.RuleUrl?.ToString(),
                ProviderType = issue.ProviderType,
                ProviderName = issue.ProviderName,
            };
        }

        /// <summary>
        /// Converts a <see cref="SerializableIssue"/> to an <see cref="Issue"/>.
        /// </summary>
        /// <param name="serializableIssue">Issue which should be converted.</param>
        /// <returns>Converted issue.</returns>
        internal static Issue ToIssue(this SerializableIssue serializableIssue)
        {
            serializableIssue.NotNull(nameof(serializableIssue));

            Uri ruleUrl = null;
            if (!string.IsNullOrWhiteSpace(serializableIssue.RuleUrl))
            {
                ruleUrl = new Uri(serializableIssue.RuleUrl);
            }

            return new Issue(
                serializableIssue.ProjectFileRelativePath,
                serializableIssue.ProjectName,
                serializableIssue.AffectedFileRelativePath,
                serializableIssue.Line,
                serializableIssue.Message,
                serializableIssue.Priority,
                serializableIssue.PriorityName,
                serializableIssue.Rule,
                ruleUrl,
                serializableIssue.ProviderType,
                serializableIssue.ProviderName);
        }
    }
}
