namespace Cake.Issues.Reporting;

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
    /// Creates a report for the issues from an issue provider in the specified format.
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
        context.NotNull();

        issueProvider.NotNull();

        reportFormat.NotNull();
        repositoryRoot.NotNull();
        outputFilePath.NotNull();

        return
            context.CreateIssueReport(
                issueProvider,
                reportFormat,
                new CreateIssueReportFromIssueProviderSettings(repositoryRoot, outputFilePath));
    }

    /// <summary>
    /// Creates a report for the issues from an issue provider with the specific settings.
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
    ///         new CreateIssueReportFromIssueProviderSettings(
    ///             @"c:\repo",
    ///             @"c:\report.html");
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
        ICreateIssueReportFromIssueProviderSettings settings)
    {
        context.NotNull();
        reportFormat.NotNull();

        issueProvider.NotNull();

        var issueReportCreator = new IssueReportCreator(context.Log);

        return issueReportCreator.CreateReport(
            [issueProvider],
            reportFormat,
            settings);
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
        context.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        reportFormat.NotNull();
        repositoryRoot.NotNull();
        outputFilePath.NotNull();

        return
            context.CreateIssueReport(
                issueProviders,
                reportFormat,
                new CreateIssueReportFromIssueProviderSettings(repositoryRoot, outputFilePath));
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
    ///         new CreateIssueReportFromIssueProviderSettings(
    ///             @"c:\repo",
    ///             @"c:\report.html");
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
        ICreateIssueReportFromIssueProviderSettings settings)
    {
        context.NotNull();
        reportFormat.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        var issueReportCreator = new IssueReportCreator(context.Log);

        return issueReportCreator.CreateReport(issueProviders, reportFormat, settings);
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
        context.NotNull();

        issues.NotNullOrEmptyElement();

        reportFormat.NotNull();
        repositoryRoot.NotNull();
        outputFilePath.NotNull();

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
        ICreateIssueReportSettings settings)
    {
        context.NotNull();

        issues.NotNullOrEmptyElement();

        reportFormat.NotNull();
        settings.NotNull();

        var issueReportCreator = new IssueReportCreator(context.Log);

        return issueReportCreator.CreateReport(issues, reportFormat, settings);
    }
}
