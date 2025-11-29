namespace Cake.Issues.PullRequests;

using System.Collections.Generic;

/// <summary>
/// Interface describing a pull request server.
/// </summary>
public interface IPullRequestSystem : IBaseIssueComponent<IReportIssuesToPullRequestSettings>
{
    /// <summary>
    /// Adds a capability.
    /// </summary>
    /// <param name="capability">Capability to add.</param>
    void AddCapability(IPullRequestSystemCapability capability);

    /// <summary>
    /// Checks if a pull request has a specific capability.
    /// </summary>
    /// <typeparam name="T">Type of the capability.</typeparam>
    /// <returns>True if pull request system has the specific capability.</returns>
    bool HasCapability<T>()
        where T : IPullRequestSystemCapability;

    /// <summary>
    /// Returns the instance of a specific capability.
    /// </summary>
    /// <typeparam name="T">Type of the capability.</typeparam>
    /// <returns>Instance of the specific capability.</returns>
    T GetCapability<T>()
        where T : IPullRequestSystemCapability;

    /// <summary>
    /// Posts discussion threads for all issues which need to be posted.
    /// </summary>
    /// <param name="issues">Issues which need to be posted.</param>
    /// <param name="commentSource">Value used to decorate comments created by this add-in.</param>
    void PostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource);
}
