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

            var report = new Report(new FileSystemRepository(this.Settings));
            foreach (var issue in issues)
            {
                report.AddDiagnostic(new IssueDiagnostic(issue));
            }

            report.Render(AnsiConsole.Console);

            return null;
        }
    }
}