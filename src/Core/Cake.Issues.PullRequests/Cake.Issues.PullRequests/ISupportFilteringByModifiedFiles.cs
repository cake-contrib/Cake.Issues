namespace Cake.Issues.PullRequests;

using System.Collections.Generic;
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
    IEnumerable<FilePath> GetModifiedFilesInPullRequest();
}
