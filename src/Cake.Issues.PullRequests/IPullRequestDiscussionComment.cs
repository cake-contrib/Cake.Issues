namespace Cake.Issues.PullRequests;

/// <summary>
/// Description of a comment on a pull request.
/// </summary>
public interface IPullRequestDiscussionComment
{
    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    string Content { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the comment is deleted or not.
    /// </summary>
    bool IsDeleted { get; set; }
}
