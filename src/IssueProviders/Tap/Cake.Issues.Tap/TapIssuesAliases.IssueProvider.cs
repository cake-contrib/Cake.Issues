namespace Cake.Issues.Tap;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

/// <content>
/// Aliases to read issues from Test Anything Protocol files.
/// </content>
public static partial class TapIssuesAliases
{
    /// <summary>
    /// Gets the name of the TAP issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Name of the TAP issue provider.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static string TapIssuesProviderTypeName(
        this ICakeContext context)
    {
        context.NotNull();

        return TapIssuesProvider.ProviderTypeName;
    }

    /// <summary>
    /// Gets an instance of a provider for issues in a TAP compatible file from disk.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFilePath">Path to the TAP file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided TAP file.</param>
    /// <returns>Instance of a provider for issues in TAP format.</returns>
    /// <example>
    /// <para>Read issues in Test Anything Protocol format:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             TapIssuesFromFilePath(
    ///                 @"c:\build\output.tap",
    ///                 GeneralLogFileFormat),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider TapIssuesFromFilePath(
        this ICakeContext context,
        FilePath logFilePath,
        BaseTapLogFileFormat format)
    {
        context.NotNull();
        logFilePath.NotNull();
        format.NotNull();

        return context.TapIssues(new TapIssuesSettings(logFilePath, format));
    }

    /// <summary>
    /// Gets an instance of a provider for issues in a TAP compatible file from memory.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFileContent">Content of the TAP file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided TAP file.</param>
    /// <returns>Instance of a provider for issues in TAP format.</returns>
    /// <example>
    /// <para>Read issues in Test Anything Protocol format:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             TapIssuesFromContent(
    ///                 logFileContent,
    ///                 GenericLogFileFormat),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider TapIssuesFromContent(
        this ICakeContext context,
        string logFileContent,
        BaseTapLogFileFormat format)
    {
        context.NotNull();
        logFileContent.NotNullOrWhiteSpace();
        format.NotNull();

        return context.TapIssues(new TapIssuesSettings(logFileContent, format));
    }

    /// <summary>
    /// Gets an instance of a provider for issues in a TAP compatible file using specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">Settings for reading the TAP file.</param>
    /// <returns>Instance of a provider for issues in TAP format.</returns>
    /// <example>
    /// <para>Read issues in Test Anything Protocol format:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new TapIssuesSettings(
    ///             @"C:\build\output.tap",
    ///             GenericLogFileFormat);
    ///
    ///     var issues =
    ///         ReadIssues(
    ///             TapIssues(settings),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider TapIssues(
        this ICakeContext context,
        TapIssuesSettings settings)
    {
        context.NotNull();
        settings.NotNull();

        return new TapIssuesProvider(context.Log, settings);
    }
}
