namespace Cake.Issues.EsLint.LogFileFormat
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Core.Diagnostics;

    /// <summary>
    /// ESLint Json format.
    /// </summary>
    internal class JsonLogFileFormat : BaseEsLintLogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public JsonLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc />
        public override IEnumerable<IIssue> ReadIssues(
            EsLintIssuesProvider issueProvider,
            RepositorySettings repositorySettings,
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
                    let
                        rule = message.ruleId
                    select
                        IssueBuilder
                            .NewIssue(message.message, issueProvider)
                            .InFile(GetRelativeFilePath(file.filePath, repositorySettings), message.line)
                            .OfRule(rule, EsLintRuleUrlResolver.Instance.ResolveRuleUrl(rule))
                            .WithPriority(GetPriority(message.severity))
                            .Create();
            }

            return new List<IIssue>();
        }

        private static string GetRelativeFilePath(
            string absoluteFilePath,
            RepositorySettings repositorySettings)
        {
            // Make path relative to repository root.
            var relativeFilePath = absoluteFilePath.Substring(repositorySettings.RepositoryRoot.FullPath.Length);

            // Remove leading directory separator.
            if (relativeFilePath.StartsWith(Path.DirectorySeparatorChar.ToString()))
            {
                relativeFilePath = relativeFilePath.Substring(1);
            }

            return relativeFilePath;
        }

        /// <summary>
        /// Converts the severity level to a priority.
        /// </summary>
        /// <param name="severity">Severity level as reported by ESLint.</param>
        /// <returns>Priority</returns>
        private static IssuePriority GetPriority(int severity)
        {
            switch (severity)
            {
                case 0:
                    return IssuePriority.Hint;

                case 1:
                    return IssuePriority.Warning;

                case 2:
                    return IssuePriority.Error;

                default:
                    return IssuePriority.Undefined;
            }
        }
    }
}
