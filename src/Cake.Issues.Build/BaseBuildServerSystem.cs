namespace Cake.Issues.Build;

using System.Collections.Generic;
using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all build server system implementations.
/// </summary>
/// <param name="log">The Cake log context.</param>
public abstract class BaseBuildServerSystem(ICakeLog log)
    : BaseIssueComponent<IReportIssuesToBuildServerSettings>(log), IBuildServerSystem
{
    /// <inheritdoc/>
    public void PostIssues(IEnumerable<IIssue> issues)
    {
        this.AssertInitialized();

        this.InternalPostIssues(issues);
    }

    /// <summary>
    /// Posts issues to the build server.
    /// Compared to <see cref="PostIssues"/> it is safe to access Settings from this method.
    /// </summary>
    /// <param name="issues">Issues which need to be posted.</param>
    protected abstract void InternalPostIssues(IEnumerable<IIssue> issues);
}