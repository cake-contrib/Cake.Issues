namespace Cake.Issues.Reporting
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Class for creating issue reports.
    /// </summary>
    internal class IssueReportCreator
    {
        private readonly ICakeLog log;
        private readonly CreateIssueReportSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueReportCreator"/> class.
        /// </summary>
        /// <param name="log">Cake log instance.</param>
        /// <param name="settings">Settings to use.</param>
        public IssueReportCreator(
            ICakeLog log,
            CreateIssueReportSettings settings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));

            this.log = log;
            this.settings = settings;
        }

        /// <summary>
        /// Creates a report from a list of issues.
        /// </summary>
        /// <param name="issueProviders">Issue providers which should be used to get the issues.</param>
        /// <param name="reportFormat">Report format to use.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        public FilePath CreateReport(IEnumerable<IIssueProvider> issueProviders, IIssueReportFormat reportFormat)
        {
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));
            reportFormat.NotNull(nameof(reportFormat));

            var issuesReader = new IssuesReader(this.log, issueProviders, this.settings);
            var issues = issuesReader.ReadIssues(IssueCommentFormat.PlainText);

            return this.CreateReport(issues, reportFormat);
        }

        /// <summary>
        /// Creates a report from a list of issues.
        /// </summary>
        /// <param name="issues">Issues for which the report should be created.</param>
        /// <param name="reportFormat">Report format to use.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        public FilePath CreateReport(IEnumerable<IIssue> issues, IIssueReportFormat reportFormat)
        {
            issues.NotNullOrEmptyElement(nameof(issues));
            reportFormat.NotNull(nameof(reportFormat));

            var reportFormatName = reportFormat.GetType().Name;
            this.log.Verbose("Initialize report format {0}...", reportFormatName);
            if (!reportFormat.Initialize(this.settings))
            {
                this.log.Warning("Error initializing report format {0}.", reportFormatName);
                return null;
            }

            this.log.Verbose("Creating report {0}...", reportFormatName);
            return reportFormat.CreateReport(issues);
        }
    }
}
