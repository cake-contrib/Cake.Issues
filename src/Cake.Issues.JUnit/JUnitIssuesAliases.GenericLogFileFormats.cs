namespace Cake.Issues.JUnit;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Issues.JUnit.LogFileFormat;

/// <content>
/// Aliases for provider to read issues in JUnit file format.
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
}