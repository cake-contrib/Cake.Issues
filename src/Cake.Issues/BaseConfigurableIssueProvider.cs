namespace Cake.Issues;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all issue provider implementations with issue provider specific settings.
/// </summary>
/// <typeparam name="T">Type of the issue provider settings.</typeparam>
public abstract class BaseConfigurableIssueProvider<T> : BaseIssueProvider
    where T : IssueProviderSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseConfigurableIssueProvider{T}"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="issueProviderSettings">Settings for the issue provider.</param>
    protected BaseConfigurableIssueProvider(ICakeLog log, T issueProviderSettings)
        : base(log)
    {
        issueProviderSettings.NotNull();

        this.IssueProviderSettings = issueProviderSettings;
    }

    /// <summary>
    /// Gets the settings for the issue provider.
    /// </summary>
    protected T IssueProviderSettings { get; }
}
