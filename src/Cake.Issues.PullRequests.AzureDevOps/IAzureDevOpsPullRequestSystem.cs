namespace Cake.Issues.PullRequests.AzureDevOps
{
    using Cake.AzureDevOps.Repos.PullRequest;

    /// <summary>
    /// Interface for writing issues to Azure DevOps pull requests.
    /// </summary>
    internal interface IAzureDevOpsPullRequestSystem : IPullRequestSystem
    {
        /// <summary>
        /// Gets information about the pull request.
        /// </summary>
        AzureDevOpsPullRequest AzureDevOpsPullRequest { get; }

        /// <summary>
        /// Validates if a pull request could be found.
        /// Depending on <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// the pull request instance can be null for subsequent calls.
        /// </summary>
        /// <returns>True if a valid pull request instance exists.</returns>
        bool ValidatePullRequest();
    }
}
