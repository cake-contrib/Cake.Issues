namespace Cake.Issues.PullRequests;

using System.Collections.Generic;
using System.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all pull request system implementations.
/// </summary>
/// <param name="log">The Cake log context.</param>
public abstract class BasePullRequestSystem(ICakeLog log)
    : BaseIssueComponent<IReportIssuesToPullRequestSettings>(log), IPullRequestSystem
{
    private readonly List<IPullRequestSystemCapability> capabilities = [];

    /// <inheritdoc/>
    public void AddCapability(IPullRequestSystemCapability capability)
    {
        capability.NotNull();

        this.capabilities.Add(capability);
    }

    /// <inheritdoc/>
    public bool HasCapability<T>()
        where T : IPullRequestSystemCapability => this.capabilities.Exists(x => x is T);

    /// <inheritdoc/>
    public T GetCapability<T>()
        where T : IPullRequestSystemCapability => this.capabilities.OfType<T>().FirstOrDefault();

    /// <inheritdoc/>
    public void PostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
    {
        this.AssertInitialized();

        this.InternalPostDiscussionThreads(issues, commentSource);
    }

    /// <summary>
    /// Posts discussion threads for all issues which need to be posted.
    /// Compared to <see cref="PostDiscussionThreads"/> it is safe to access Settings from this method.
    /// </summary>
    /// <param name="issues">Issues which need to be posted.</param>
    /// <param name="commentSource">Value used to decorate comments created by this add-in.</param>
    protected abstract void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource);
}
