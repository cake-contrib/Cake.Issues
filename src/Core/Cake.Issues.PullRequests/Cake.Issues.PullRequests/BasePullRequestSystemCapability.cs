namespace Cake.Issues.PullRequests;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all optional pull request system capabilities.
/// </summary>
/// <typeparam name="T">Type of the pull request system.</typeparam>
public abstract class BasePullRequestSystemCapability<T> : IPullRequestSystemCapability
    where T : class, IPullRequestSystem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasePullRequestSystemCapability{T}"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
    protected BasePullRequestSystemCapability(ICakeLog log, T pullRequestSystem)
    {
        log.NotNull();
        pullRequestSystem.NotNull();

        this.Log = log;
        this.PullRequestSystem = pullRequestSystem;
    }

    /// <summary>
    /// Gets the Cake log context.
    /// </summary>
    protected ICakeLog Log { get; }

    /// <summary>
    /// Gets the pull request system to which the capability belongs.
    /// </summary>
    protected T PullRequestSystem { get; }
}
