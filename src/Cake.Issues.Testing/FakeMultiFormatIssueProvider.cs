namespace Cake.Issues.Testing;

using Cake.Core.Diagnostics;

/// <summary>
/// Implementation of a <see cref="BaseMultiFormatIssueProvider{TSettings, TIssueProvider} "/> for use in test cases.
/// </summary>
/// <param name="log">The Cake log instance.</param>
/// <param name="settings">The issue provider settings.</param>
public class FakeMultiFormatIssueProvider(ICakeLog log, FakeMultiFormatIssueProviderSettings settings)
            : BaseMultiFormatIssueProvider<FakeMultiFormatIssueProviderSettings, FakeMultiFormatIssueProvider>(log, settings)
{
    /// <summary>
    /// Gets the Cake log instance.
    /// </summary>
    public new ICakeLog Log => base.Log;

    /// <summary>
    /// Gets the repository settings.
    /// </summary>
    public IRepositorySettings RepositorySettings => this.Settings;

    /// <summary>
    /// Gets the issue provider settings.
    /// </summary>
    public new FakeMultiFormatIssueProviderSettings IssueProviderSettings => base.IssueProviderSettings;

    /// <summary>
    /// Gets the format in which issues should be returned.
    /// </summary>
    public IssueCommentFormat Format { get; private set; }

    /// <inheritdoc/>
    public override string ProviderName => "Fake Issue Provider";
}
