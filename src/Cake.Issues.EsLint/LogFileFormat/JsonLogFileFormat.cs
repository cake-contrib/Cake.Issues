namespace Cake.Issues.EsLint.LogFileFormat
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization.Json;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// ESLint Json format.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    internal class JsonLogFileFormat(ICakeLog log)
        : BaseEsLintLogFileFormat(log)
    {
        /// <inheritdoc />
        public override IEnumerable<IIssue> ReadIssues(
            EsLintIssuesProvider issueProvider,
            IRepositorySettings repositorySettings,
            EsLintIssuesSettings esLintsettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            esLintsettings.NotNull(nameof(esLintsettings));

            IEnumerable<LogFile> logFileEntries = null;
            using (var ms = new MemoryStream(esLintsettings.LogFileContent.ToStringUsingEncoding(true).ToByteArray()))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(LogFile[]));
                logFileEntries = jsonSerializer.ReadObject(ms) as LogFile[];
            }

            if (logFileEntries != null)
            {
                return
                    from file in logFileEntries
                    from message in file.messages
                    select
                        GetIssue(file, message, issueProvider, repositorySettings);
            }

            return new List<IIssue>();
        }

        /// <summary>
        /// Returns the issue for a message from the log file.
        /// </summary>
        /// <param name="file">File for which the issue was reported.</param>
        /// <param name="message">Message reported by ESLint.</param>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>Issue instance.</returns>
        private static IIssue GetIssue(
            LogFile file,
            Message message,
            EsLintIssuesProvider issueProvider,
            IRepositorySettings repositorySettings)
        {
            var issueBuilder =
                IssueBuilder
                    .NewIssue(message.message, issueProvider)
                    .InFile(
                        file.filePath.MakeFilePathRelativeToRepositoryRoot(repositorySettings),
                        message.line <= 0 ? null : message.line,
                        message.column <= 0 ? null : message.column)
                    .WithPriority(GetPriority(message.severity));

            if (!string.IsNullOrWhiteSpace(message.ruleId))
            {
                issueBuilder =
                    issueBuilder
                        .OfRule(message.ruleId, EsLintRuleUrlResolver.Instance.ResolveRuleUrl(message.ruleId));
            }

            return issueBuilder.Create();
        }

        /// <summary>
        /// Converts the severity level to a priority.
        /// </summary>
        /// <param name="severity">Severity level as reported by ESLint.</param>
        /// <returns>Priority.</returns>
        private static IssuePriority GetPriority(int severity)
        {
            return severity switch
            {
                0 => IssuePriority.Hint,
                1 => IssuePriority.Warning,
                2 => IssuePriority.Error,
                _ => IssuePriority.Undefined,
            };
        }
    }
}
