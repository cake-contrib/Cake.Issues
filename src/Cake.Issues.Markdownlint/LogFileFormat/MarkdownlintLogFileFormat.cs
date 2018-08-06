namespace Cake.Issues.Markdownlint.LogFileFormat
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
            RepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            markdownlintIssuesSettings.NotNull(nameof(markdownlintIssuesSettings));

            var logFileEntries =
                JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JToken>>>(
                    markdownlintIssuesSettings.LogFileContent.ToStringUsingEncoding(true));

            return
                from file in logFileEntries
                from entry in file.Value
                let
                    rule = (string)entry.SelectToken("ruleName")
                select
                    IssueBuilder
                        .NewIssue((string)entry.SelectToken("ruleDescription"), issueProvider)
                        .InFile(file.Key, (int)entry.SelectToken("lineNumber"))
                        .WithPriority(IssuePriority.Warning)
                        .OfRule(rule, MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(rule))
                        .Create();
        }
    }
}
