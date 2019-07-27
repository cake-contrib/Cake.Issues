namespace Cake.Issues.GitRepository
{
    /// <summary>
    /// Settings for <see cref="GitRepositoryIssuesAliases"/>.
    /// </summary>
    public class GitRepositoryIssuesSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the repository should be checked for
        /// binary files not tracked by Git LFS.
        /// Requires Git and Git-LFS to be available through Cake tool resolution.
        /// </summary>
        public bool CheckBinaryFilesTrackedByLfs { get; set; }
    }
}
