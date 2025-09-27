namespace Cake.Issues.JUnit;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

/// <content>
/// Aliases to read issues from JUnit XML files.
/// </content>
public static partial class JUnitIssuesAliases
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

        return JUnitIssuesProvider.ProviderTypeName;
    }

    /// <summary>
    /// Gets an instance of a provider for issues in a JUnit XML file from disk.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFilePath">Path to the JUnit XML file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided JUnit XML file.</param>
    /// <returns>Instance of a provider for issues in JUnit XML format.</returns>
    /// <example>
    /// <para>Read issues from a JUnit XML file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             JUnitIssuesFromFilePath(
    ///                 @"c:\build\junit-results.xml",
    ///                 GenericJUnitLogFileFormat),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider JUnitIssuesFromFilePath(
        this ICakeContext context,
        FilePath logFilePath,
        BaseJUnitLogFileFormat format)
    {
        context.NotNull();
        logFilePath.NotNull();
        format.NotNull();

        return context.JUnitIssues(new JUnitIssuesSettings(logFilePath, format));
    }

    /// <summary>
    /// Gets an instance of a provider for issues in a JUnit XML file from memory.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFileContent">Content of the JUnit XML file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided JUnit XML file.</param>
    /// <returns>Instance of a provider for issues in JUnit XML format.</returns>
    /// <example>
    /// <para>Read issues from a JUnit XML file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             JUnitIssuesFromContent(
    ///                 logFileContent,
    ///                 GenericJUnitLogFileFormat),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider JUnitIssuesFromContent(
        this ICakeContext context,
        string logFileContent,
        BaseJUnitLogFileFormat format)
    {
        context.NotNull();
        logFileContent.NotNullOrWhiteSpace();
        format.NotNull();

        return context.JUnitIssues(new JUnitIssuesSettings(logFileContent.ToByteArray(), format));
    }

    /// <summary>
    /// Gets an instance of a provider for issues in a JUnit XML file using specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">Settings for reading the JUnit XML file.</param>
    /// <returns>Instance of a provider for issues in JUnit XML format.</returns>
    /// <example>
    /// <para>Read issues from a JUnit XML file:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new JUnitIssuesSettings(
    ///             @"C:\build\junit-results.xml",
    ///             GenericJUnitLogFileFormat);
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