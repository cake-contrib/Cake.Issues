namespace Cake.Issues.Reporting.Sarif
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Generator for creating SARIF compatible reports.
    /// </summary>
    internal class SarifIssueReportGenerator : IssueReportFormat
    {
        private readonly SarifIssueReportFormatSettings sarifIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SarifIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public SarifIssueReportGenerator(ICakeLog log, SarifIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.sarifIssueReportFormatSettings = settings;
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            this.Log.Information("Creating report '{0}'", this.Settings.OutputFilePath.FullPath);

            // TODO Implement
            return null;
        }
    }
}