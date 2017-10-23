namespace Cake.Issues.EsLint
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
    internal class JsonFormat : LogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public JsonFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc />
        public override IEnumerable<IIssue> ReadIssues(
            RepositorySettings repositorySettings,
            EsLintIssuesSettings esLintsettings)
        {
            repositorySettings.NotNull(nameof(repositorySettings));
            esLintsettings.NotNull(nameof(esLintsettings));

            IEnumerable<LogFile> logFileEntries = null;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(esLintsettings.LogFileContent)))
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
                        new Issue<EsLintIssuesProvider>(
                            GetRelativeFilePath(file.filePath, repositorySettings),
                            message.line,
                            message.message,
                            message.severity,
                            rule,
                            EsLintRuleUrlResolver.Instance.ResolveRuleUrl(rule));
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
    }
}
