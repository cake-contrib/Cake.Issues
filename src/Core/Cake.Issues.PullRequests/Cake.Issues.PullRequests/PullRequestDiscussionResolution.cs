namespace Cake.Issues.PullRequests;

/// <summary>
/// Resolution of discussions in pull requests.
/// </summary>
public enum PullRequestDiscussionResolution
{
    /// <summary>
    /// Unknown discussion resolution.
    /// </summary>
    Unknown,

    /// <summary>
    /// Discussion is resolved.
    /// </summary>
    Resolved,

    /// <summary>
    /// Discussion is marked as won't fix.
    /// </summary>
    WontFix,
}
