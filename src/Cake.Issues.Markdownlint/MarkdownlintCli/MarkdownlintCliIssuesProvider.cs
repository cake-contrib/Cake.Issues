namespace Cake.Issues.Markdownlint.MarkdownlintCli
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by markdownlint-cli.
    /// </summary>
    internal class MarkdownlintCliIssuesProvider : IssueProvider
    {
        private readonly MarkdownlintCliIssuesSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintCliIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public MarkdownlintCliIssuesProvider(ICakeLog log, MarkdownlintCliIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            var regex = new Regex(@"(.*): (\d*): (MD\d*)/((?:\w*-*)*) (.*)");

            foreach (var line in this.settings.LogFileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList().Where(s => !string.IsNullOrEmpty(s)))
            {
                var groups = regex.Match(line).Groups;

                // Read affected file from the line.
                if (!this.TryGetFile(groups, this.Settings, out string fileName))
                {
                    continue;
                }

                var lineNumber = int.Parse(groups[2].Value);
                var rule = groups[3].Value;
                var ruleDescription = groups[5].Value;

                yield return
                    new Issue<MarkdownlintCliIssuesProvider>(
                        fileName,
                        lineNumber,
                        ruleDescription,
                        0,
                        rule,
                        MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(rule));
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