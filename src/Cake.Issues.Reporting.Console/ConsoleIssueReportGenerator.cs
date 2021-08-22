namespace Cake.Issues.Reporting.Console
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

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
            return null;
        }
    }
}