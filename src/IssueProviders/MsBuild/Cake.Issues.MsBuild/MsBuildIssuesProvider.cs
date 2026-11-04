namespace Cake.Issues.MsBuild;

using Cake.Core.Diagnostics;

/// <summary>
/// Provider for issues reported as MsBuild warnings.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="settings">Settings for reading the log file.</param>
public class MsBuildIssuesProvider(ICakeLog log, MsBuildIssuesSettings settings) : BaseMultiFormatIssueProvider<MsBuildIssuesSettings, MsBuildIssuesProvider>(log, settings)
{
    /// <summary>
    /// Gets the name of the MsBuild issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    public static string ProviderTypeName => typeof(MsBuildIssuesProvider).FullName;

    /// <inheritdoc />
    public override string ProviderName => "MSBuild";
}
