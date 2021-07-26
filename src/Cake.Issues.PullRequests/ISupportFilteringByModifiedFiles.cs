﻿namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Cake.Core.IO;

    /// <summary>
    /// Interface describing that a pull request system supports filtering by modified files.
    /// </summary>
    public interface ISupportFilteringByModifiedFiles
        : IPullRequestSystemCapability
    {
        /// <summary>
        /// Returns a list of all files modified in a pull request.
        /// </summary>
        /// <returns>List of all files modified in a pull request.</returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Most probably will make a remote call")]
        IEnumerable<FilePath> GetModifiedFilesInPullRequest();
    }
}
