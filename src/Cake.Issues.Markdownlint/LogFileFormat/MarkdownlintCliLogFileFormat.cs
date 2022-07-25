namespace Cake.Issues.Markdownlint.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Logfile format as written by markdownlint-cli.
    /// </summary>
    internal class MarkdownlintCliLogFileFormat : BaseMarkdownlintLogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintCliLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public MarkdownlintCliLogFileFormat(ICakeLog log)
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

            var regex = new Regex(@"(?<filePath>.*[^:\d+]): ?(?<lineNumber>\d+):?(?<columnNumber>\d+)? (?<ruleId>MD\d+)/(?<ruleName>(?:\w*-*/*)*) (?<message>.*)");

            foreach (var line in markdownlintIssuesSettings.LogFileContent.ToStringUsingEncoding().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList().Where(s => !string.IsNullOrEmpty(s)))
            {
                var groups = regex.Match(line).Groups;

                // Read affected file from the line.
                if (!this.TryGetFile(groups, repositorySettings, out string fileName))
                {
                    continue;
                }

                var lineNumber = int.Parse(groups["lineNumber"].Value);
                int? columnNumber = null;
                if (!string.IsNullOrEmpty(groups["columnNumber"].Value))
                {
                    columnNumber = int.Parse(groups["columnNumber"].Value);
                }
                var ruleId = groups["ruleId"].Value;
                var message = groups["message"].Value;

                yield return
                    IssueBuilder
                        .NewIssue(message, issueProvider)
                        .InFile(fileName, lineNumber, columnNumber)
                        .WithPriority(IssuePriority.Warning)
                        .OfRule(ruleId, MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(ruleId))
                        .Create();
            }
        }

        /// <summary>
        /// Reads the affected file path from a parsed entry.
        /// </summary>
        /// <param name="values">Parsed values of a line in the log file.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="fileName">Returns the full path to the affected file.</param>
        /// <returns>True if the file path could be parsed.</returns>
        private bool TryGetFile(
            GroupCollection values,
            IRepositorySettings repositorySettings,
            out string fileName)
        {
            values.NotNull(nameof(values));
            repositorySettings.NotNull(nameof(repositorySettings));

            var filePath = values["filePath"].Value;

            // Validate file path and make relative to repository root.
            bool result;
            (result, fileName) = this.ValidateFilePath(filePath, repositorySettings);
            return result;
        }
    }
}
