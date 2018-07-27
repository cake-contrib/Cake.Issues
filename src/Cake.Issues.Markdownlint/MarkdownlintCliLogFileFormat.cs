namespace Cake.Issues.Markdownlint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Logfile format as written by markdownlint-cli.
    /// </summary>
    internal class MarkdownlintCliLogFileFormat : LogFileFormat
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
            RepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            markdownlintIssuesSettings.NotNull(nameof(markdownlintIssuesSettings));

            var regex = new Regex(@"(.*): (\d*): (MD\d*)/((?:\w*-*/*)*) (.*)");

            foreach (var line in markdownlintIssuesSettings.LogFileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList().Where(s => !string.IsNullOrEmpty(s)))
            {
                var groups = regex.Match(line).Groups;

                // Read affected file from the line.
                if (!this.TryGetFile(groups, repositorySettings, out string fileName))
                {
                    continue;
                }

                var lineNumber = int.Parse(groups[2].Value);
                var rule = groups[3].Value;
                var ruleDescription = groups[5].Value;

                yield return
                    IssueBuilder
                        .NewIssue(ruleDescription, issueProvider)
                        .InFile(fileName, lineNumber)
                        .WithPriority(IssuePriority.Warning)
                        .OfRule(rule, MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(rule))
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
            RepositorySettings repositorySettings,
            out string fileName)
        {
            fileName = values[1].Value;

            // Make path relative to repository root.
            fileName = fileName.Substring(repositorySettings.RepositoryRoot.FullPath.Length);

            // Remove leading directory separator.
            if (fileName.StartsWith("/"))
            {
                fileName = fileName.Substring(1);
            }

            return true;
        }
    }
}
