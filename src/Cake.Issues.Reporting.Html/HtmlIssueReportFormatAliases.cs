namespace Cake.Issues.Reporting.Html
{
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality for creating issue reports int HTML format.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class HtmlIssueReportFormatAliases
    {
        /// <summary>
        /// Gets an instance of a the HTML report format using an embedded template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="template">Template to use for generating the HTML report.</param>
        /// <returns>Instance of a the HTML report format.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         HtmlIssueReportFormatFromEmbeddedTemplate(HtmlIssueReportTemplate.Diagnostic),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat HtmlIssueReportFormatFromEmbeddedTemplate(
            this ICakeContext context,
            HtmlIssueReportTemplate template)
        {
            context.NotNull(nameof(context));

            return context.HtmlIssueReportFormat(HtmlIssueReportFormatSettings.FromEmbeddedTemplate(template));
        }

        /// <summary>
        /// Gets an instance of a the HTML report format using a local template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templatePath">Path to the template to use for generating the HTML report.</param>
        /// <returns>Instance of a the HTML report format.</returns>
        /// <example>
        /// <para>Create HTML report from local template file:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         HtmlIssueReportFormatFromEmbeddedTemplate(@"c:\ReportTemplate.cshtml"),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat HtmlIssueReportFormatFromFilePath(
            this ICakeContext context,
            FilePath templatePath)
        {
            context.NotNull(nameof(context));
            templatePath.NotNull(nameof(templatePath));

            return context.HtmlIssueReportFormat(HtmlIssueReportFormatSettings.FromFilePath(templatePath));
        }

        /// <summary>
        /// Gets an instance of a the HTML report format using a template string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateContent">Content of the template to use for generating the HTML report.</param>
        /// <returns>Instance of a the HTML report format.</returns>
        /// <example>
        /// <para>Create HTML report from a template string:</para>
        /// <code>
        /// <![CDATA[
        ///     template =
        ///         "<ul>@foreach(var issue in Model){<li>@issue.Message</li>}</ul>";
        ///     CreateIssueReport(
        ///         issues,
        ///         HtmlIssueReportFormatFromEmbeddedTemplate(template),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat HtmlIssueReportFormatFromFilePath(
            this ICakeContext context,
            string templateContent)
        {
            context.NotNull(nameof(context));
            templateContent.NotNullOrWhiteSpace(nameof(templateContent));

            return context.HtmlIssueReportFormat(HtmlIssueReportFormatSettings.FromContent(templateContent));
        }

        /// <summary>
        /// Gets an instance of a the HTML report format using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the MSBuild log.</param>
        /// <returns>Instance of a the HTML report format.</returns>
        /// <example>
        /// <para>Create HTML report:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         HtmlIssueReportFormatFromEmbeddedTemplate(HtmlIssueReportTemplate.ByRule);
        ///
        ///     CreateIssueReport(
        ///         issues,
        ///         HtmlIssueReportFormat(settings),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat HtmlIssueReportFormat(
            this ICakeContext context,
            HtmlIssueReportFormatSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new HtmlIssueReportGenerator(context.Log, settings);
        }
    }
}
