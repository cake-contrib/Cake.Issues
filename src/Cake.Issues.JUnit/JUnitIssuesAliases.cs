namespace Cake.Issues.JUnit;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

/// <summary>
/// Contains functionality for reading issues from JUnit XML files.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class JUnitIssuesAliases
{
    /// <summary>
    /// Gets the name of the JUnit issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Name of the JUnit issue provider.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static string JUnitIssuesProviderTypeName(
        this ICakeContext context)
    {
        context.NotNull();

        return typeof(JUnitIssuesProvider).FullName;
    }

    /// <summary>
    /// Gets an instance of a provider for issues reported in JUnit XML format using the log file from disk.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFilePath">Path to the JUnit log file.</param>
    /// <returns>Instance of a provider for issues reported in JUnit XML format.</returns>
    /// <example>
    /// <para>Read issues from a JUnit XML file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             JUnitIssuesFromFilePath(@"c:\build\junit-results.xml"),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider JUnitIssuesFromFilePath(
        this ICakeContext context,
        FilePath logFilePath)
    {
        context.NotNull();
        logFilePath.NotNull();

        return context.JUnitIssues(new JUnitIssuesSettings(logFilePath));
    }

    /// <summary>
    /// Gets an instance of a provider for issues reported in JUnit XML format using log file content.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFileContent">Content of the JUnit log file.</param>
    /// <returns>Instance of a provider for issues reported in JUnit XML format.</returns>
    /// <example>
    /// <para>Read issues the content of JUnit XML file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             JUnitIssuesFromContent(junitLogContent),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider JUnitIssuesFromContent(
        this ICakeContext context,
        string logFileContent)
    {
        context.NotNull();
        logFileContent.NotNullOrWhiteSpace();

        return context.JUnitIssues(new JUnitIssuesSettings(logFileContent.ToByteArray()));
    }

    /// <summary>
    /// Gets an instance of a provider for issues reported in JUnit XML format using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">Settings for reading the JUnit log.</param>
    /// <returns>Instance of a provider for issues reported in JUnit XML format.</returns>
    /// <example>
    /// <para>Read issues from a JUnit XML file:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         JUnitIssuesSettings.FromFilePath(@"c:\build\junit-results.xml");
    ///
    ///     var issues =
    ///         ReadIssues(
    ///             JUnitIssues(settings),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider JUnitIssues(
        this ICakeContext context,
        JUnitIssuesSettings settings)
    {
        context.NotNull();
        settings.NotNull();

        return new JUnitIssuesProvider(context.Log, settings);
    }
}