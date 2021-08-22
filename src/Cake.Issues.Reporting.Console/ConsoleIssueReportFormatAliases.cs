namespace Cake.Issues.Reporting.Console
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.Reporting;

    /// <summary>
    /// Contains functionality to report issues to the console.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class ConsoleIssueReportFormatAliases
    {
        /// <summary>
        /// Gets an instance of the console report format using default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance of a console report format.</returns>
        /// <example>
        /// <para>Report issues to console:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         ConsoleIssueReportFormat(),
        ///         @"c:\repo",
        ///         string.Empty);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat ConsoleIssueReportFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return context.ConsoleIssueReportFormat(new ConsoleIssueReportFormatSettings());
        }

        /// <summary>
        /// Gets an instance of the console report format using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for generating the report.</param>
        /// <returns>Instance of a console report format.</returns>
        /// <example>
        /// <para>Report issues to console:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new ConsoleIssueReportFormatSettings();
        ///
        ///     CreateIssueReport(
        ///         issues,
        ///         ConsoleIssueReportFormat(settings),
        ///         @"c:\repo",
        ///         string.Empty);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat ConsoleIssueReportFormat(
            this ICakeContext context,
            ConsoleIssueReportFormatSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new ConsoleIssueReportGenerator(context.Log, settings);
        }
    }
}
