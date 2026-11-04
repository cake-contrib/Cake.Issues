namespace Cake.Issues.PullRequests;

using System.Collections.Generic;

/// <summary>
/// Interface describing that a pull request system supports discussion threads.
/// </summary>
public interface ISupportDiscussionThreads
    : IPullRequestSystemCapability
{
    /// <summary>
    /// Returns a list of all discussion threads.
    /// </summary>
    /// <param name="commentSource">Value used to indicate threads created by this add-in.</param>
    /// <returns>List of all discussion threads.</returns>
    IEnumerable<IPullRequestDiscussionThread> FetchDiscussionThreads(string commentSource);

    /// <summary>
    /// Marks a list of discussion threads as resolved.
    /// </summary>
    /// <param name="threads">Threads to mark as fixed.</param>
    void ResolveDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads);

    /// <summary>
    /// Marks a list of discussion threads as active.
    /// </summary>
    /// <param name="threads">Threads to mark as active.</param>
    void ReopenDiscussionThreads(IEnumerable<IPullRequestDiscussionThread> threads);
}
