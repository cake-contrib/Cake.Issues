namespace Cake.Issues.JUnit;

using Cake.Core.Diagnostics;

/// <summary>
/// Provider for issues in JUnit XML format.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="issueProviderSettings">Settings for the issue provider.</param>
public class JUnitIssuesProvider(ICakeLog log, JUnitIssuesSettings issueProviderSettings)
    : BaseMultiFormatIssueProvider<JUnitIssuesSettings, JUnitIssuesProvider>(log, issueProviderSettings)
{
    /// <summary>
    /// Gets the name of the JUnit issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    public static string ProviderTypeName => typeof(JUnitIssuesProvider).FullName;

    /// <inheritdoc />
    public override string ProviderName => "JUnit";
}