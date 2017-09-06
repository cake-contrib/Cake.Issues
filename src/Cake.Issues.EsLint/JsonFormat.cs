namespace Cake.Issues.EsLint
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Core.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

            var logFileEntries =
                JsonConvert.DeserializeObject<IEnumerable<JToken>>(esLintsettings.LogFileContent);

            return
                from file in logFileEntries
                from message in file.SelectToken("messages")
                let
                    rule = (string)message.SelectToken("ruleId")
                select
                    new Issue<EsLintIssuesProvider>(
                        GetRelativeFilePath((string)file.SelectToken("filePath"), repositorySettings),
                        (int)message.SelectToken("line"),
                        (string)message.SelectToken("message"),
                        (int)message.SelectToken("severity"),
                        rule,
                        EsLintRuleUrlResolver.Instance.ResolveRuleUrl(rule));
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
