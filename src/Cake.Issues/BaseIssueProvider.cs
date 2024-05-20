namespace Cake.Issues;

using System.Collections.Generic;
using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all issue provider implementations.
/// </summary>
/// <param name="log">The Cake log context.</param>
public abstract class BaseIssueProvider(ICakeLog log)
    : BaseIssueComponent<IReadIssuesSettings>(log), IIssueProvider
{
    /// <inheritdoc/>
    public abstract string ProviderName { get; }

    /// <inheritdoc/>
    public virtual string ProviderType => this.GetType().FullName;

    /// <inheritdoc/>
    public IEnumerable<IIssue> ReadIssues()
    {
        this.AssertInitialized();

        return this.InternalReadIssues();
    }

    /// <summary>
    /// Gets all issues.
    /// Compared to <see cref="ReadIssues"/> it is safe to access Settings from this method.
    /// </summary>
    /// <returns>List of issues.</returns>
    protected abstract IEnumerable<IIssue> InternalReadIssues();
}
