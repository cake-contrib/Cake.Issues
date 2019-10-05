﻿namespace Cake.Issues.GitRepository
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

        /// <summary>
        /// Gets or sets a value indicating whether the repository should be checked for
        /// files path longer than <see cref="MaxFilePathLength"/>.
        /// </summary>
        public bool CheckFilesPathLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the maximum allowed length of file paths if
        /// <see cref="CheckFilesPathLength"/> is enabled.
        /// </summary>
        public int MaxFilePathLength { get; set; }
    }
}
