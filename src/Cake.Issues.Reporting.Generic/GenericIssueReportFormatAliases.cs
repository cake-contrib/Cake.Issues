namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;

    /// <summary>
    /// Contains functionality for creating issue reports in any text based format (HTML, Markdown, ...).
    ///
    /// NOTE: Use Cake.Issues.Reporting.Generic addin to use these aliases with Cake Script Runners and
    /// Cake.Frosting.Issues.Reporting.Generic to use these aliases with Cake Frosting.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Aliases are not used in addin code")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Aliases are called by Cake scripts")]
    [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Class will be loaded by Cake")]
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
        /// Gets an instance of a the generic report format using an embedded template with custom settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="template">Template to use for generating the report.</param>
        /// <param name="configurator">Action for defining the settings.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report using the HtmlDxDataGrid template with custom title:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromEmbeddedTemplate(
        ///             GenericIssueReportTemplate.HtmlDxDataGrid,
        ///             x => x.WithOption(HtmlDxDataGridOption.Title, "My Issue Report")),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormatFromEmbeddedTemplate(
            this ICakeContext context,
            GenericIssueReportTemplate template,
            Action<GenericIssueReportFormatSettings> configurator)
        {
            context.NotNull(nameof(context));
            configurator.NotNull(nameof(configurator));

            var settings = GenericIssueReportFormatSettings.FromEmbeddedTemplate(template);
            configurator(settings);
            return context.GenericIssueReportFormat(settings);
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
        /// Gets an instance of a the generic report format using a local template with custom settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templatePath">Path to the template to use for generating the report.</param>
        /// <param name="configurator">Action for defining the settings.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report from local template file with custom title:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromFilePath(
        ///             @"c:\ReportTemplate.cshtml",
        ///             x => x.WithOption("Title", "My Issue Report")),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormatFromFilePath(
            this ICakeContext context,
            FilePath templatePath,
            Action<GenericIssueReportFormatSettings> configurator)
        {
            context.NotNull(nameof(context));
            templatePath.NotNull(nameof(templatePath));
            configurator.NotNull(nameof(configurator));

            var settings = GenericIssueReportFormatSettings.FromFilePath(templatePath);
            configurator(settings);
            return context.GenericIssueReportFormat(settings);
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
        /// Gets an instance of a the generic report format using a template string with custom settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateContent">Content of the template to use for generating the report.</param>
        /// <param name="configurator">Action for defining the settings.</param>
        /// <returns>Instance of a the generic report format.</returns>
        /// <example>
        /// <para>Create HTML report from a template string with custom title:</para>
        /// <code>
        /// <![CDATA[
        ///     template =
        ///         "<h1>@ViewBag.Title</h1><ul>@foreach(var issue in Model){<li>@issue.Message</li>}</ul>";
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromContent(
        ///             template,
        ///             x => x.WithOption("Title", "My Issue Report")),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat GenericIssueReportFormatFromContent(
            this ICakeContext context,
            string templateContent,
            Action<GenericIssueReportFormatSettings> configurator)
        {
            context.NotNull(nameof(context));
            templateContent.NotNullOrWhiteSpace(nameof(templateContent));
            configurator.NotNull(nameof(configurator));

            var settings = GenericIssueReportFormatSettings.FromContent(templateContent);
            configurator(settings);
            return context.GenericIssueReportFormat(settings);
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
        ///         GenericIssueReportFormatSettings
        ///             .FromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic);
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
