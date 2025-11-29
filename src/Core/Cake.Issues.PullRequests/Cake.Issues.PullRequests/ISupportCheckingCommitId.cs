namespace Cake.Issues.PullRequests;

/// <summary>
/// Interface describing that a pull request system supports checking if commit ID is still up-to-date.
/// </summary>
public interface ISupportCheckingCommitId
    : IPullRequestSystemCapability
{
    /// <summary>
    /// Gets the hash of the latest commit on the source branch.
    /// </summary>
    /// <returns>The hash of the latest commit on the source branch, null or <see cref="string.Empty"/>
    /// if no pull request could be found.</returns>
    string GetLastSourceCommitId();
}
