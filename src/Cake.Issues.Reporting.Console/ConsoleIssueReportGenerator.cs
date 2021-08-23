namespace Cake.Issues.Reporting.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Errata;
    using Spectre.Console;

    /// <summary>
    /// Generator for reporting issues to console.
    /// </summary>
    internal class ConsoleIssueReportGenerator : IssueReportFormat
    {
        private readonly ConsoleIssueReportFormatSettings consoleIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reporting the issues.</param>
        public ConsoleIssueReportGenerator(ICakeLog log, ConsoleIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.consoleIssueReportFormatSettings = settings;
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            // Filter to issues from existing files
            issues =
                issues
                    .Where(x =>
                        x.AffectedFileRelativePath != null &&
                        File.Exists(this.Settings.RepositoryRoot.CombineWithFilePath(x.AffectedFileRelativePath).FullPath))
                    .ToList();

            // Filter to issues with line and column information
            // Errata currently doesn't support file or line level diagnostics.
            // https://github.com/cake-contrib/Cake.Issues.Reporting.Console/issues/4
            // https://github.com/cake-contrib/Cake.Issues.Reporting.Console/issues/5
            issues =
                issues
                    .Where(x => x.Line.HasValue && x.Column.HasValue)
                    .ToList();

            var report = new Report();
            foreach (var issue in issues)
            {
                var color = issue.Priority switch
                {
                    (int)IssuePriority.Error => Color.Red,
                    (int)IssuePriority.Warning => Color.Yellow,
                    (int)IssuePriority.Suggestion or (int)IssuePriority.Hint => Color.Blue,
                    _ => throw new Exception(),
                };

                (var location, var length) = this.GetLocation(issue);
                var label =
                    new Label(
                        issue.AffectedFileRelativePath.FullPath,
                        location,
                        issue.Message(IssueCommentFormat.PlainText))
                    .WithColor(color);

                if (length > 0)
                {
                    label = label.WithLength(length);
                }

                report.AddDiagnostic(
                    new IssueDiagnostic(issue)
                        .WithLabel(label));
            }

            report.Render(
                AnsiConsole.Console,
                new FileSystemRepository(this.Settings));

            return null;
        }

        /// <summary>
        /// Returns the diagnostic location of an issue.
        /// </summary>
        /// <param name="issue">Issue for which the location should be returned.</param>
        /// <returns>Location for the diagnostic.</returns>
        private (Location location, int lenght) GetLocation(IIssue issue)
        {
            // Errata currently doesn't support file or line level diagnostics.
            if (!issue.Line.HasValue || !issue.Column.HasValue)
            {
                return default;
            }

            var location = new Location(issue.Line.Value, issue.Column.Value);

            int lenght = 0;
            if (issue.EndColumn.HasValue)
            {
                lenght = issue.EndColumn.Value - issue.Column.Value;
            }

            return (location, lenght);
        }
    }
}