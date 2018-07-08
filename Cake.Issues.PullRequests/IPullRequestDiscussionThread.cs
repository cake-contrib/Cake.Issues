namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Cake.Core.IO;

    /// <summary>
    /// Description of a collection of pull request comments relating to each other.
    /// </summary>
    public interface IPullRequestDiscussionThread
    {
        /// <summary>
        /// Gets or sets the ID of the discussion thread.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets if the thread is active or already fixed.
        /// </summary>
        PullRequestDiscussionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the resolution of the thred if <see cref="Status"/> is <see cref="PullRequestDiscussionStatus.Resolved"/>.
        /// </summary>
        PullRequestDiscussionResolution Resolution { get; set; }

        /// <summary>
        /// Gets or sets the path to the file where the message should be posted.
        /// The path needs to be relative to the repository root.
        /// Can be <c>null</c> if discussion is not related to a change in a file.
        /// </summary>
        FilePath AffectedFileRelativePath { get; set; }

        /// <summary>
        /// Gets or sets a value used to decorate comments created by this addin.
        /// </summary>
        string CommentSource { get; set; }

        /// <summary>
        /// Gets all the comments of this thread.
        /// </summary>
        IList<IPullRequestDiscussionComment> Comments { get; }
    }
}
