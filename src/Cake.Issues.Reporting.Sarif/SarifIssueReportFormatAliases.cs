namespace Cake.Issues.Reporting.Sarif
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.Reporting;

    /// <summary>
    /// Contains functionality to generate SARIF compatible files.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class SarifIssueReportFormatAliases
    {
        /// <summary>
        /// Gets an instance of the SARIF report format using default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance of a SARIF report format.</returns>
        /// <example>
        /// <para>Create SARIF compatible file:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         SarifIssueReportFormat(),
        ///         @"c:\repo",
        ///         @"c:\export.sarif");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat SarifIssueReportFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return context.SarifIssueReportFormat(new SarifIssueReportFormatSettings());
        }

        /// <summary>
        /// Gets an instance of the SARIF report format using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for generating the report.</param>
        /// <returns>Instance of a SARIF report format.</returns>
        /// <example>
        /// <para>Create SARIF compatible file:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new SarifIssueReportFormatSettings();
        ///
        ///     CreateIssueReport(
        ///         issues,
        ///         SarifIssueReportFormat(settings),
        ///         @"c:\repo",
        ///         @"c:\export.sarif");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat SarifIssueReportFormat(
            this ICakeContext context,
            SarifIssueReportFormatSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new SarifIssueReportGenerator(context.Log, settings);
        }
    }
}
