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
    }
}
