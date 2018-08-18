namespace Cake.Issues.PullRequests.Tfs
{
    using System;
    using Authentication;
    using Core;
    using Core.Annotations;

    /// <summary>
    /// Contains functionality related to writing code analysis issues to Team Foundation Server or
    /// Visual Studio Team Services pull requests.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class TfsPullRequestSystemAliases
    {
        /// <summary>
        /// Returns credentials for integrated / NTLM authentication.
        /// Can only be used for on-premise Team Foundation Server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Credentials for integrated / NTLM authentication</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static ITfsCredentials TfsAuthenticationNtlm(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return AuthenticationProvider.AuthenticationNtlm();
        }

        /// <summary>
        /// Returns credentials for basic authentication.
        /// Can only be used for on-premise Team Foundation Server configured for basic authentication.
        /// See https://www.visualstudio.com/en-us/docs/integrate/get-started/auth/tfs-basic-auth.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for basic authentication.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static ITfsCredentials TfsAuthenticationBasic(
            this ICakeContext context,
            string userName,
            string password)
        {
            context.NotNull(nameof(context));
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return AuthenticationProvider.AuthenticationBasic(userName, password);
        }

        /// <summary>
        /// Returns credentials for authentication with a personal access token.
        /// Can be used for Team Foundation Server and Visual Studio Team Services.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="personalAccessToken">Personal access token.</param>
        /// <returns>Credentials for authentication with a personal access token.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static ITfsCredentials TfsAuthenticationPersonalAccessToken(
            this ICakeContext context,
            string personalAccessToken)
        {
            context.NotNull(nameof(context));
            personalAccessToken.NotNullOrWhiteSpace(nameof(personalAccessToken));

            return AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken);
        }

        /// <summary>
        /// Returns credentials for OAuth authentication.
        /// Can only be used with Visual Studio Team Services.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="accessToken">OAuth access token.</param>
        /// <returns>Credentials for OAuth authentication.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static ITfsCredentials TfsAuthenticationOAuth(
            this ICakeContext context,
            string accessToken)
        {
            context.NotNull(nameof(context));
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            return AuthenticationProvider.AuthenticationOAuth(accessToken);
        }

        /// <summary>
        /// Returns credentials for authentication with an Azure Active Directory.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for authentication with an Azure Active Directory.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static ITfsCredentials TfsAuthenticationAzureActiveDirectory(
            this ICakeContext context,
            string userName,
            string password)
        {
            context.NotNull(nameof(context));
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password);
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Visual Studio Team Services pull request
        /// in a specific repository and for a specific source branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceBranch">Branch for which the pull request is made.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Visual Studio Team Services.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Visual Studio Team Services pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem TfsPullRequests(
            this ICakeContext context,
            Uri repositoryUrl,
            string sourceBranch,
            ITfsCredentials credentials)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            sourceBranch.NotNullOrWhiteSpace(nameof(sourceBranch));
            credentials.NotNull(nameof(credentials));

            return context.TfsPullRequests(new TfsPullRequestSettings(repositoryUrl, sourceBranch, credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Visual Studio Team Services pull request
        /// in a specific repository and with a specific ID.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="pullRequestId">ID of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Visual Studio Team Services.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Visual Studio Team Services pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             5,
        ///             TfsAuthenticationNtlm()),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem TfsPullRequests(
            this ICakeContext context,
            Uri repositoryUrl,
            int pullRequestId,
            ITfsCredentials credentials)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            credentials.NotNull(nameof(credentials));

            return context.TfsPullRequests(new TfsPullRequestSettings(repositoryUrl, pullRequestId, credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Visual Studio Team Services pull request
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Visual Studio Team Services pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(pullRequestSettings),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem TfsPullRequests(
            this ICakeContext context,
            TfsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new TfsPullRequestSystem(context.Log, settings);
        }

        /// <summary>
        /// Votes for the Team Foundation Server or Visual Studio Team Services pull request
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <param name="vote">The vote for the pull request.</param>
        /// <example>
        /// <para>Vote 'Approved' to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     TfsVotePullRequest(
        ///         pullRequestSettings,
        ///         TfsPullRequestVote.Approved);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static void TfsVotePullRequest(
            this ICakeContext context,
            TfsPullRequestSettings settings,
            TfsPullRequestVote vote)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequest = new TfsPullRequestSystem(context.Log, settings);
            pullRequest.Vote(vote);
        }

        /// <summary>
        /// Gets the last commit hash on the source branch of the Team Foundation Server or
        /// Visual Studio Team Services pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <example>
        /// <para>Get the hash of the last commit:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     var hash =
        ///         TfsPullRequestLastSourceCommit(
        ///             pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The hash of the last commit on the source branch or <see cref="string.Empty"/>
        /// if no pull request could be found.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static string TfsPullRequestLastSourceCommit(
            this ICakeContext context,
            TfsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequestSystem = new TfsPullRequestSystem(context.Log, settings);
            return pullRequestSystem.GetCapability<TfsCheckingCommitIdCapability>().GetLastSourceCommitId();
        }
    }
}
