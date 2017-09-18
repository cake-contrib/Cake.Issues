namespace Cake.Issues.Reporting.Generic
{
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality for creating issue reports in any text based format (HTML, Markdown, ...).
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class GenericIssueReportFormatAliases
    {
        /// <summary>
        /// Gets an instance of a the generic report format using an embedded template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="template">Template to use for generating the report.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormatFromEmbeddedTemplate(
            this ICakeContext context,
            GenericIssueReportTemplate template)
        {
            context.NotNull(nameof(context));

            return context.GenericIssueReportFormat(GenericIssueReportFormatSettings.FromEmbeddedTemplate(template));
        }

        /// <summary>
        /// Gets an instance of a the generic report format using a local template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templatePath">Path to the template to use for generating the report.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report from local template file:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromFilePath(@"c:\ReportTemplate.cshtml"),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormatFromFilePath(
            this ICakeContext context,
            FilePath templatePath)
        {
            context.NotNull(nameof(context));
            templatePath.NotNull(nameof(templatePath));

            return context.GenericIssueReportFormat(GenericIssueReportFormatSettings.FromFilePath(templatePath));
        }

        /// <summary>
        /// Gets an instance of a the generic report format using a template string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateContent">Content of the template to use for generating the report.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report from a template string:</para>
        /// <code>
        /// <![CDATA[
        ///     template =
        ///         "<ul>@foreach(var issue in Model){<li>@issue.Message</li>}</ul>";
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromContent(template),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormatFromContent(
            this ICakeContext context,
            string templateContent)
        {
            context.NotNull(nameof(context));
            templateContent.NotNullOrWhiteSpace(nameof(templateContent));

            return context.GenericIssueReportFormat(GenericIssueReportFormatSettings.FromContent(templateContent));
        }

        /// <summary>
        /// Gets an instance of a the generic report format using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the MSBuild log.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         GenericIssueReportFormatSettings.FromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic);
        ///
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormat(settings),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormat(
            this ICakeContext context,
            GenericIssueReportFormatSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new GenericIssueReportGenerator(context.Log, settings);
        }
    }
}
