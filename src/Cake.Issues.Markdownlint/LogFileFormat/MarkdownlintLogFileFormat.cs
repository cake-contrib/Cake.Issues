namespace Cake.Issues.Markdownlint.LogFileFormat
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Logfile format as written by Markdownlint.
    /// </summary>
    internal class MarkdownlintLogFileFormat : BaseMarkdownlintLogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public MarkdownlintLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc />
        public override IEnumerable<IIssue> ReadIssues(
            MarkdownlintIssuesProvider issueProvider,
            IssueCommentFormat format,
            RepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            markdownlintIssuesSettings.NotNull(nameof(markdownlintIssuesSettings));

            Dictionary<string, IEnumerable<Issue>> logFileEntries;
            using (var ms = new MemoryStream(markdownlintIssuesSettings.LogFileContent.RemovePreamble()))
            {
                var jsonSerializer = new DataContractJsonSerializer(
                    typeof(Dictionary<string, IEnumerable<Issue>>),
                    settings: new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });

                logFileEntries = jsonSerializer.ReadObject(ms) as Dictionary<string, IEnumerable<Issue>>;
            }

            return
                from file in logFileEntries
                from entry in file.Value
                let
                    rule = entry.ruleName
                select
                    IssueBuilder
                        .NewIssue(entry.ruleDescription, issueProvider)
                        .InFile(file.Key, entry.lineNumber)
                        .WithPriority(IssuePriority.Warning)
                        .OfRule(rule, MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(rule))
                        .Create();
        }
    }
}
