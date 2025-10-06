namespace Cake.Issues.CodeClimate;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

/// <summary>
/// Contains functionality for reading issues from CodeClimate files.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class CodeClimateIssuesAliases
{
    /// <summary>
    /// Gets the name of the CodeClimate issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Name of the CodeClimate issue provider.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static string CodeClimateIssuesProviderTypeName(
        this ICakeContext context)
    {
        context.NotNull();

        return typeof(CodeClimateIssuesProvider).FullName;
    }

    /// <summary>
    /// Gets an instance of a provider for CodeClimate compatible files using a file from disk.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFilePath">Path to the CodeClimate file.</param>
    /// <returns>Instance of a provider for CodeClimate compatible files.</returns>
    /// <example>
    /// <para>Read issues from a CodeClimate compatible file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             CodeClimateIssuesFromFilePath(@"c:\build\log.json"),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider CodeClimateIssuesFromFilePath(
        this ICakeContext context,
        FilePath logFilePath)
    {
        context.NotNull();
        logFilePath.NotNull();

        return context.CodeClimateIssues(new CodeClimateIssuesSettings(logFilePath));
    }

    /// <summary>
    /// Gets an instance of a provider for CodeClimate compatible files using file content.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFileContent">Content of the CodeClimate compatible file.</param>
    /// <returns>Instance of a provider for CodeClimate compatible files.</returns>
    /// <example>
    /// <para>Read issues from a CodeClimate compatible file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         ReadIssues(
    ///             CodeClimateIssuesFromContent(logFileContent)),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider CodeClimateIssuesFromContent(
        this ICakeContext context,
        string logFileContent)
    {
        context.NotNull();
        logFileContent.NotNullOrWhiteSpace();

        return context.CodeClimateIssues(new CodeClimateIssuesSettings(logFileContent.ToByteArray()));
    }

    /// <summary>
    /// Gets an instance of a provider for CodeClimate compatible files using specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">Settings for reading the CodeClimate compatible file.</param>
    /// <returns>Instance of a provider for CodeClimate compatible files.</returns>
    /// <example>
    /// <para>Read issues from a CodeClimate compatible file:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new CodeClimateIssuesSettings(
    ///             @"c:\build\log.json"));
    ///
    ///     var issues =
    ///         ReadIssues(
    ///             CodeClimateIssues(settings),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider CodeClimateIssues(
        this ICakeContext context,
        CodeClimateIssuesSettings settings)
    {
        context.NotNull();
        settings.NotNull();

        return new CodeClimateIssuesProvider(context.Log, settings);
    }
}