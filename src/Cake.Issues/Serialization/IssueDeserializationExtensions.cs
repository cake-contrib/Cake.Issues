namespace Cake.Issues.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Cake.Core.IO;
    using LitJson;

    /// <summary>
    /// Extensions for deserializing an <see cref="IIssue"/>.
    /// </summary>
    public static class IssueDeserializationExtensions
    {
        /// <summary>
        /// Deserializes an <see cref="Issue"/> from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON representation of the issue.</param>
        /// <returns>Instance of the issue.</returns>
        public static Issue DeserializeToIssue(this string jsonString)
        {
            jsonString.NotNullOrWhiteSpace(nameof(jsonString));

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonString)))
            {
                return DeserializeStreamToIssue(stream);
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

            using (var stream = File.Open(filePath.FullPath, FileMode.Open))
            {
                return DeserializeStreamToIssue(stream);
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

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonString)))
            {
                return DeserializeStreamToIssues(stream);
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

            using (var stream = File.Open(filePath.FullPath, FileMode.Open))
            {
                return DeserializeStreamToIssues(stream);
            }
        }

        /// <summary>
        /// Deserializes a stream containing the JSON representation of an issue to an <see cref="Issue"/>.
        /// </summary>
        /// <param name="stream">Stream whose content should be deserialized.</param>
        /// <returns>Issue instance.</returns>
        private static Issue DeserializeStreamToIssue(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var jsonContent = reader.ReadToEnd();

                var data = JsonMapper.ToObject(jsonContent);

                return DeserializeJsonDataToIssue(data);
            }
        }

        /// <summary>
        /// Deserializes a stream containing the JSON representation of an array of issues to an <see cref="IEnumerable{Issue}"/>.
        /// </summary>
        /// <param name="stream">Stream whose content should be deserialized.</param>
        /// <returns>List of issues.</returns>
        private static IEnumerable<Issue> DeserializeStreamToIssues(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var jsonContent = reader.ReadToEnd();

                var data = JsonMapper.ToObject(jsonContent);
                var issues = new List<Issue>();
                foreach (JsonData element in data)
                {
                    issues.Add(DeserializeJsonDataToIssue(element));
                }

                return issues;
            }
        }

        /// <summary>
        /// Deserializes JSON representation of an issue to an <see cref="Issue"/>.
        /// Supports serialization format of the current version of Cake.Issues as versions
        /// written with previous versions of Cake.Issues.
        /// </summary>
        /// <param name="data">JSON representation of the issue.</param>
        /// <returns>Issue instance.</returns>
        private static Issue DeserializeJsonDataToIssue(JsonData data)
        {
            if (data.ContainsKey("Version"))
            {
                var version = (int)data["Version"];
                return version switch
                {
                    2 => JsonMapper.ToObject<SerializableIssueV2>(data.ToJson()).ToIssue(),
                    3 => JsonMapper.ToObject<SerializableIssueV3>(data.ToJson()).ToIssue(),
                    4 => JsonMapper.ToObject<SerializableIssueV4>(data.ToJson()).ToIssue(),
                    5 => JsonMapper.ToObject<SerializableIssueV5>(data.ToJson()).ToIssue(),
                    _ => throw new Exception($"Not supported issue serialization format {version}"),
                };
            }
            else
            {
                // If no version is available deserialize to original format.
                return JsonMapper.ToObject<SerializableIssue>(data.ToJson()).ToIssue();
            }
        }
    }
}
