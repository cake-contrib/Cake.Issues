namespace Cake.Issues.Markdownlint
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provider for issues reported by Markdownlint.
    /// </summary>
    internal class MarkdownlintIssuesProvider : IssueProvider
    {
        private readonly MarkdownlintIssuesSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public MarkdownlintIssuesProvider(ICakeLog log, MarkdownlintIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            var logFileEntries =
                JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JToken>>>(this.settings.LogFileContent);

            return
                from file in logFileEntries
                from entry in file.Value
                let
                    rule = (string)entry.SelectToken("ruleName")
                select
                    new Issue<MarkdownlintIssuesProvider>(
                        file.Key,
                        (int)entry.SelectToken("lineNumber"),
                        (string)entry.SelectToken("ruleDescription"),
                        0,
                        rule,
                        MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(rule));
        }
    }
}
