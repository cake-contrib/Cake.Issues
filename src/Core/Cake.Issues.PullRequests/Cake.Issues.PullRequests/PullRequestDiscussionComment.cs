namespace Cake.Issues.PullRequests;

/// <summary>
/// Base class for a pull request comment.
/// </summary>
public class PullRequestDiscussionComment : IPullRequestDiscussionComment
{
    /// <inheritdoc/>
    public string Content { get; set; }

    /// <inheritdoc/>
    public bool IsDeleted { get; set; }
}
