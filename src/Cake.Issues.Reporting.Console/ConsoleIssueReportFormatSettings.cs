namespace Cake.Issues.Reporting.Console
{
    /// <summary>
    /// Settings for <see cref="ConsoleIssueReportFormatAliases"/>.
    /// </summary>
    public class ConsoleIssueReportFormatSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether issues should be grouped by rule or if
        /// for every issue a separate diagnostic should be created.
        /// </summary>
        public bool GroupByRule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a summary of issues by provider / run
        /// should be shown.
        /// </summary>
        public bool ShowProviderSummary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a summary of issues by provider / run
        /// with the number of issues for each priority should be shown.
        /// </summary>
        public bool ShowPrioritySummary { get; set; }
    }
}
