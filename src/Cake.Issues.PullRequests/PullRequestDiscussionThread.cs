namespace Cake.Issues.PullRequests
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.IO;

    /// <summary>
    /// Base class for a collection of pull request comments relating to each other.
    /// </summary>
    public class PullRequestDiscussionThread : IPullRequestDiscussionThread
    {
        private readonly List<IPullRequestDiscussionComment> comments = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PullRequestDiscussionThread"/> class.
        /// </summary>
        /// <param name="id">ID of the discussion thread.</param>
        /// <param name="status">A value if the thread is active or already fixed.</param>
        /// <param name="filePath">The path to the file where the message should be posted.
        /// The path needs to be relative to the repository root.
        /// Can be <c>null</c> if discussion is not related to a change in a file.</param>
        /// <param name="comments">All the comments of this thread.</param>
        public PullRequestDiscussionThread(
            int id,
            PullRequestDiscussionStatus status,
            FilePath filePath,
            IEnumerable<IPullRequestDiscussionComment> comments)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            comments.NotNull();

            // File path needs to be relative to the repository root.
            if (filePath != null)
            {
                if (!filePath.IsRelative)
                {
                    throw new ArgumentOutOfRangeException(nameof(filePath), "File path needs to be relative to the repository root.");
                }

                this.AffectedFileRelativePath = filePath;
            }

            this.Id = id;
            this.Status = status;

            // ReSharper disable once PossibleMultipleEnumeration
            this.comments.AddRange(comments);
        }

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public PullRequestDiscussionStatus Status { get; set; }

        /// <inheritdoc/>
        public PullRequestDiscussionResolution Resolution { get; set; }

        /// <inheritdoc/>
        public FilePath AffectedFileRelativePath { get; set; }

        /// <inheritdoc/>
        public IList<IPullRequestDiscussionComment> Comments => this.comments;

        /// <inheritdoc/>
        public string CommentSource { get; set; }

        /// <inheritdoc/>
        public string ProviderType { get; set; }

        /// <inheritdoc/>
        public string CommentIdentifier { get; set; }
    }
}
