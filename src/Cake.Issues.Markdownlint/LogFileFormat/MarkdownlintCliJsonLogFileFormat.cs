namespace Cake.Issues.Markdownlint.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Logfile format as written by markdownlint-cli with <c>--json</c> parameter.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    internal class MarkdownlintCliJsonLogFileFormat(ICakeLog log)
        : BaseMarkdownlintLogFileFormat(log)
    {
        /// <inheritdoc />
        public override IEnumerable<IIssue> ReadIssues(
            MarkdownlintIssuesProvider issueProvider,
            IRepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            markdownlintIssuesSettings.NotNull(nameof(markdownlintIssuesSettings));

            if (markdownlintIssuesSettings.LogFileContent.Length == 0)
            {
                return new List<IIssue>();
            }

            IEnumerable<LogFileEntry> logFileEntries = null;
            using (var ms = new MemoryStream(markdownlintIssuesSettings.LogFileContent.ToStringUsingEncoding(true).ToByteArray()))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(LogFileEntry[]));
                logFileEntries = jsonSerializer.ReadObject(ms) as LogFileEntry[];
            }

            if (logFileEntries == null)
            {
                return new List<IIssue>();
            }

            return logFileEntries.Select(x => GetIssue(x, issueProvider, repositorySettings));
        }

        /// <summary>
        /// Returns the issue for a message from the log file.
        /// </summary>
        /// <param name="logFileEntry">Issue reported by markdownlint.</param>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>Issue instance.</returns>
        private static IIssue GetIssue(
            LogFileEntry logFileEntry,
            MarkdownlintIssuesProvider issueProvider,
            IRepositorySettings repositorySettings)
        {
            var message = logFileEntry.ruleDescription;

            if (!string.IsNullOrEmpty(logFileEntry.errorDetail))
            {
                message += $": {logFileEntry.errorDetail}";
            }

            return
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .InFile(
                        GetFilePath(logFileEntry.fileName, repositorySettings),
                        logFileEntry.lineNumber,
                        logFileEntry.errorRange != null ? logFileEntry.lineNumber : null,
                        logFileEntry.errorRange?[0],
                        logFileEntry.errorRange != null ? logFileEntry.errorRange[0] + logFileEntry.errorRange[1] : null)
                    .WithPriority(IssuePriority.Warning)
                    .OfRule(logFileEntry.ruleNames.First(), new Uri(logFileEntry.ruleInformation))
                    .Create();
        }

        /// <summary>
        /// Returns the file path of the issue.
        /// </summary>
        /// <param name="filePath">File path as reported by markdownlint.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>File path absolute to repository root.</returns>
        private static string GetFilePath(
            string filePath,
            IRepositorySettings repositorySettings)
        {
            var directoryPath = new DirectoryPath(filePath);

            if (directoryPath.IsRelative)
            {
                return filePath;
            }

            // Absolute paths need to be made relative to repository root.
            return repositorySettings.RepositoryRoot.GetRelativePath(directoryPath).FullPath;
        }

#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1401 // Fields must be private
#pragma warning disable CS0649 // Field 'field' is never assigned to, and will always have its default value 'value'

        [DataContract]
        private class LogFileEntry
        {
            [DataMember]
            public string fileName;

            [DataMember]
            public int lineNumber;

            [DataMember]
            public string[] ruleNames;

            [DataMember]
            public string ruleDescription;

            [DataMember]
            public string ruleInformation;

            [DataMember]
            public string errorDetail;

            [DataMember]
            public string errorContext;

            [DataMember]
            public int[] errorRange;
        }

#pragma warning restore SA1401 // Fields must be private
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
#pragma warning restore CS0649 // Field 'field' is never assigned to, and will always have its default value 'value'
    }
}
