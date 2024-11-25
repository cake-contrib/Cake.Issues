namespace Cake.Issues.Tap;

using Cake.Core.Diagnostics;
using Cake.Issues;

/// <summary>
/// Provider for issues in TAP compatible format.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="issueProviderSettings">Settings for the issue provider.</param>
public class TapIssuesProvider(ICakeLog log, TapIssuesSettings issueProviderSettings)
    : BaseMultiFormatIssueProvider<TapIssuesSettings, TapIssuesProvider>(log, issueProviderSettings)
{
    /// <summary>
    /// Gets the name of the Markdownlint issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    public static string ProviderTypeName => typeof(TapIssuesProvider).FullName;

    /// <inheritdoc />
    public override string ProviderName => "TAP";
}