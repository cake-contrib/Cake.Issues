namespace Cake.Issues.PullRequests;

/// <summary>
/// Status of discussions in pull requests.
/// </summary>
public enum PullRequestDiscussionStatus
{
    /// <summary>
    /// Unknown discussion status.
    /// </summary>
    Unknown,

    /// <summary>
    /// Discussion is active.
    /// </summary>
    Active,

    /// <summary>
    /// Discussion is resolved.
    /// </summary>
    Resolved,
}
