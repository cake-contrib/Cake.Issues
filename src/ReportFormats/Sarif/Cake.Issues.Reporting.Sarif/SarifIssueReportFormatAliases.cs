namespace Cake.Issues.Reporting.Sarif;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Issues.Reporting;

/// <summary>
/// Contains functionality to generate SARIF compatible files.
///
/// NOTE: Use Cake.Issues.Reporting.Sarif addin to use these aliases with Cake Script Runners and
/// Cake.Frosting.Issues.Reporting.Sarif to use these aliases with Cake Frosting.
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
        context.NotNull();

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
        context.NotNull();
        settings.NotNull();

        return new SarifIssueReportGenerator(context.Log, settings);
    }
}
