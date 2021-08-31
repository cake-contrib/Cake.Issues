namespace Cake.Issues.Markdownlint.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Logfile format as written by markdownlint-cli with <c>--json</c> parameter.
    /// </summary>
    internal class MarkdownlintCliJsonLogFileFormat : BaseMarkdownlintLogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintCliJsonLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public MarkdownlintCliJsonLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc />
        public override IEnumerable<IIssue> ReadIssues(
            MarkdownlintIssuesProvider issueProvider,
            IRepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            markdownlintIssuesSettings.NotNull(nameof(markdownlintIssuesSettings));

            if (!markdownlintIssuesSettings.LogFileContent.Any())
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

            return logFileEntries.Select(x => GetIssue(x, issueProvider));
        }

        /// <summary>
        /// Returns the issue for a message from the log file.
        /// </summary>
        /// <param name="logFileEntry">Issue reported by markdownlint.</param>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <returns>Issue instance.</returns>
        private static IIssue GetIssue(
            LogFileEntry logFileEntry,
            MarkdownlintIssuesProvider issueProvider)
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
                        logFileEntry.fileName,
                        logFileEntry.lineNumber,
                        logFileEntry.errorRange != null ? logFileEntry.lineNumber : null,
                        logFileEntry.errorRange != null ? logFileEntry.errorRange[0] : null,
                        logFileEntry.errorRange != null ? logFileEntry.errorRange[0] + logFileEntry.errorRange[1] : null)
                    .WithPriority(IssuePriority.Warning)
                    .OfRule(logFileEntry.ruleNames.First(), new Uri(logFileEntry.ruleInformation))
                    .Create();
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
