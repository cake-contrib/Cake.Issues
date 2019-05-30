namespace Cake.Issues.Reporting
{
    using System.Collections.Generic;
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;

    /// <summary>
    /// Contains functionality related to creating issue reports.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class Aliases
    {
        /// <summary>
        /// Creates a report for a the issues from an issue provider in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProvider">Issue provider for whose issues the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <param name="outputFilePath">Path of the generated report file.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         InspectCodeIssuesFromFilePath(
        ///             @"C:\build\inspectcode.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IIssueProvider issueProvider,
            IIssueReportFormat reportFormat,
            DirectoryPath repositoryRoot,
            FilePath outputFilePath)
        {
            context.NotNull(nameof(context));

            issueProvider.NotNull(nameof(issueProvider));

            reportFormat.NotNull(nameof(reportFormat));
            repositoryRoot.NotNull(nameof(repositoryRoot));
            outputFilePath.NotNull(nameof(outputFilePath));

            return
                context.CreateIssueReport(
                    issueProvider,
                    reportFormat,
                    new CreateIssueReportSettings(repositoryRoot, outputFilePath));
        }

        /// <summary>
        /// Creates a report for a the issues from an issue provider with the specific settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProvider">Issue provider for whose issues the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new CreateIssueReportSettings(@"c:\repo", @"c:\report.html");
        ///
        ///     CreateIssueReport(
        ///         new List<IIssueProvider>
        ///         InspectCodeIssuesFromFilePath(
        ///             @"C:\build\inspectcode.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        ///         settings);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IIssueProvider issueProvider,
            IIssueReportFormat reportFormat,
            CreateIssueReportSettings settings)
        {
            context.NotNull(nameof(context));
            reportFormat.NotNull(nameof(reportFormat));

            issueProvider.NotNull(nameof(issueProvider));

            var issueReportCreator = new IssueReportCreator(context.Log, settings);

            return issueReportCreator.CreateReport(new List<IIssueProvider> { issueProvider }, reportFormat);
        }

        /// <summary>
        /// Creates a report for a list of issues in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">Issue providers for whose issues the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <param name="outputFilePath">Path of the generated report file.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     CreateIssueReport(
        ///         new List<IIssueProvider>
        ///         {
        ///             MsBuildIssuesFromFilePath(
        ///                 @"C:\build\msbuild.log",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             InspectCodeIssuesFromFilePath(
        ///                 @"C:\build\inspectcode.log",
        ///                 MsBuildXmlFileLoggerFormat)
        ///         },
        ///         GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        ///         @"c:\repo",
        ///         @"c:\report.html");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            IIssueReportFormat reportFormat,
            DirectoryPath repositoryRoot,
            FilePath outputFilePath)
        {
            context.NotNull(nameof(context));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            reportFormat.NotNull(nameof(reportFormat));
            repositoryRoot.NotNull(nameof(repositoryRoot));
            outputFilePath.NotNull(nameof(outputFilePath));

            // ReSharper disable once PossibleMultipleEnumeration
            return
                context.CreateIssueReport(
                    issueProviders,
                    reportFormat,
                    new CreateIssueReportSettings(repositoryRoot, outputFilePath));
        }

        /// <summary>
        /// Creates a report for a list of issues with the specific settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">Issue providers for whose issues the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new CreateIssueReportSettings(@"c:\repo", @"c:\report.html");
        ///
        ///     CreateIssueReport(
        ///         new List<IIssueProvider>
        ///         {
        ///             MsBuildIssuesFromFilePath(
        ///                 @"C:\build\msbuild.log",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             InspectCodeIssuesFromFilePath(
        ///                 @"C:\build\inspectcode.log",
        ///                 MsBuildXmlFileLoggerFormat)
        ///         },
        ///         GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        ///         settings);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            IIssueReportFormat reportFormat,
            CreateIssueReportSettings settings)
        {
            context.NotNull(nameof(context));
            reportFormat.NotNull(nameof(reportFormat));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            var issueReportCreator = new IssueReportCreator(context.Log, settings);

            // ReSharper disable once PossibleMultipleEnumeration
            return issueReportCreator.CreateReport(issueProviders, reportFormat);
        }

        /// <summary>
        /// Creates a report for a list of issues in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issues">Issues for which the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <param name="outputFilePath">Path of the generated report file.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
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
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssue> issues,
            IIssueReportFormat reportFormat,
            DirectoryPath repositoryRoot,
            FilePath outputFilePath)
        {
            context.NotNull(nameof(context));

            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNullOrEmptyElement(nameof(issues));

            reportFormat.NotNull(nameof(reportFormat));
            repositoryRoot.NotNull(nameof(repositoryRoot));
            outputFilePath.NotNull(nameof(outputFilePath));

            // ReSharper disable once PossibleMultipleEnumeration
            return
                context.CreateIssueReport(
                    issues,
                    reportFormat,
                    new CreateIssueReportSettings(repositoryRoot, outputFilePath));
        }

        /// <summary>
        /// Creates a report for a list of issues with the specific settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issues">Issues for which the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
        /// <example>
        /// <para>Create HTML report using the diagnostic template:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new CreateIssueReportSettings(@"c:\repo", @"c:\report.html");
        ///
        ///     CreateIssueReport(
        ///         issues,
        ///         GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        ///         settings);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssue> issues,
            IIssueReportFormat reportFormat,
            CreateIssueReportSettings settings)
        {
            context.NotNull(nameof(context));

            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNullOrEmptyElement(nameof(issues));

            reportFormat.NotNull(nameof(reportFormat));
            settings.NotNull(nameof(settings));

            var issueReportCreator = new IssueReportCreator(context.Log, settings);

            // ReSharper disable once PossibleMultipleEnumeration
            return issueReportCreator.CreateReport(issues, reportFormat);
        }
    }
}
