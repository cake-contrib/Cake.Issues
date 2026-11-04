namespace Cake.Issues.EsLint;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Issues.EsLint.LogFileFormat;

/// <content>
/// Contains functionality related to <see cref="JsonLogFileFormat"/>.
/// </content>
public static partial class EsLintIssuesAliases
{
    /// <summary>
    /// Gets an instance for the ESLint JSON log format as written by the JSON formatter.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Instance for the ESLint JSON log format.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static BaseEsLintLogFileFormat EsLintJsonFormat(
        this ICakeContext context)
    {
        context.NotNull();

        return new JsonLogFileFormat(context.Log);
    }
}
