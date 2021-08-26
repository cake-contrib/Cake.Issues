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
    }
}
