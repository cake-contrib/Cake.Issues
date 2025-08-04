namespace Cake.Issues.JUnit;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Issues.JUnit.LogFileFormat;

/// <content>
/// Aliases for JUnit log file formats.
/// </content>
public static partial class JUnitIssuesAliases
{
    /// <summary>
    /// Gets an instance for the log format for any file compatible with JUnit XML format.
    /// Does best effort parsing for any JUnit XML format.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Instance for the generic JUnit XML format.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static BaseJUnitLogFileFormat GenericJUnitLogFileFormat(
        this ICakeContext context)
    {
        context.NotNull();

        return new GenericJUnitLogFileFormat(context.Log);
    }

    /// <summary>
    /// Gets an instance for the log format for cpplint JUnit XML output.
    /// Optimized for cpplint's specific JUnit format where test case names represent file names.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Instance for the cpplint JUnit XML format.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static BaseJUnitLogFileFormat CppLintJUnitLogFileFormat(
        this ICakeContext context)
    {
        context.NotNull();

        return new CppLintLogFileFormat(context.Log);
    }

    /// <summary>
    /// Gets an instance for the log format for markdownlint-cli2 JUnit XML output.
    /// Optimized for markdownlint-cli2's specific JUnit format where file paths are in classname attributes.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Instance for the markdownlint-cli2 JUnit XML format.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static BaseJUnitLogFileFormat MarkdownlintCli2JUnitLogFileFormat(
        this ICakeContext context)
    {
        context.NotNull();

        return new MarkdownlintCli2LogFileFormat(context.Log);
    }
}