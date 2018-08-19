namespace Cake.Issues.PullRequests.Tfs
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using TfsUrlParser;

    /// <summary>
    /// Interface for writing issues to Team Foundation Server or Visual Studio Team Services pull requests.
    /// </summary>
    internal interface ITfsPullRequestSystem : IPullRequestSystem
    {
        /// <summary>
        /// Gets the description of the repository.
        /// </summary>
        RepositoryDescription RepositoryDescription { get; }

        /// <summary>
        /// Gets the object to access the pull request.
        /// </summary>
        GitPullRequest PullRequest { get; }

        /// <summary>
        /// Validates if a pull request could be found.
        /// Depending on <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestDoesNotExist"/>
        /// the pull request instance can be null for subsequent calls.
        /// </summary>
        /// <returns>True if a valid pull request instance exists.</returns>
        bool ValidatePullRequest();

        /// <summary>
        /// Creates a client object for communicating with TFS.
        /// </summary>
        /// <returns>Client object for communicating with TFS</returns>
        GitHttpClient CreateGitClient();

        /// <summary>
        /// Votes for the pull request.
        /// </summary>
        /// <param name="vote">The vote for the pull request.</param>
        void Vote(TfsPullRequestVote vote);
    }
}
