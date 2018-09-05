namespace Cake.Issues.PullRequests.Tfs
{
    using System;
    using Cake.Tfs.Authentication;
    using Cake.Tfs.PullRequest;

    /// <summary>
    /// Settings for <see cref="TfsPullRequestSystemAliases"/>.
    /// </summary>
    public class TfsPullRequestSystemSettings : TfsPullRequestSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSystemSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceBranch">Branch for which the pull request is made.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Visual Studio Team Services.</param>
        public TfsPullRequestSystemSettings(Uri repositoryUrl, string sourceBranch, ITfsCredentials credentials)
            : base(repositoryUrl, sourceBranch, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSystemSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="pullRequestId">ID of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Visual Studio Team Services.</param>
        public TfsPullRequestSystemSettings(Uri repositoryUrl, int pullRequestId, ITfsCredentials credentials)
            : base(repositoryUrl, pullRequestId, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSystemSettings"/> class
        /// based on the instance of a <see cref="TfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public TfsPullRequestSystemSettings(TfsPullRequestSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether pull request system should check if commit Id
        /// is still up to date before posting comments.
        /// </summary>
        public bool CheckCommitId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether discussion threads should automatically be
        /// resolved oder reopened.
        /// </summary>
        public bool ManageDiscussionThreadStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pull request system should filter to issues affecting
        /// files changed in the pull request.
        /// </summary>
        public bool FilterModifiedFiles { get; set; }
    }
}
