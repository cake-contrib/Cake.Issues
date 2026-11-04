namespace Cake.Issues.Tap;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Issues.Tap.LogFileFormat;

/// <content>
/// Aliases for provider to read issues in Test Anything Protocol format.
/// </content>
public static partial class TapIssuesAliases
{
    /// <summary>
    /// Gets an instance for the log format for any file compatible with Test Anything Protocol format.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Instance for the Test Anything Protocol format.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static BaseTapLogFileFormat GenericLogFileFormat(
        this ICakeContext context)
    {
        context.NotNull();

        return new GenericLogFileFormat(context.Log);
    }
}
