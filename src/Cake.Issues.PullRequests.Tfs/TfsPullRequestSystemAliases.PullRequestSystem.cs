namespace Cake.Issues.PullRequests.Tfs
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Tfs.Authentication;

    /// <content>
    /// Contains functionality related to <see cref="TfsPullRequestSystem"/>.
    /// </content>
    public static partial class TfsPullRequestSystemAliases
    {
        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Azure DevOps pull request
        /// in a specific repository and for a specific source branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceBranch">Branch for which the pull request is made.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Azure DevOps pull request.</returns>
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

            return context.TfsPullRequests(new TfsPullRequestSystemSettings(repositoryUrl, sourceBranch, credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Azure DevOps pull request
        /// in a specific repository and with a specific ID.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="pullRequestId">ID of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Azure DevOps pull request.</returns>
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

            return context.TfsPullRequests(new TfsPullRequestSystemSettings(repositoryUrl, pullRequestId, credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Azure DevOps pull request
        /// where all required data is taken from the environment variables set by the Azure Pipelines / TFS build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             TfsAuthenticationNtlm()),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem TfsPullRequests(
            this ICakeContext context,
            ITfsCredentials credentials)
        {
            context.NotNull(nameof(context));
            credentials.NotNull(nameof(credentials));

            return context.TfsPullRequests(new TfsPullRequestSystemSettings(credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Azure DevOps pull request
        /// where all required data (including authentication token) is taken from the environment variables set by the Azure Pipelines / TFS build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem TfsPullRequests(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return context.TfsPullRequests(new TfsPullRequestSystemSettings());
        }

        /// <summary>
        /// Gets an object for writing issues to Team Foundation Server or Visual Studio Team Services pull request
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <returns>Object for writing issues to Team Foundation Server or Azure DevOps pull request.</returns>
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
            TfsPullRequestSystemSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new TfsPullRequestSystem(context.Log, settings);
        }
    }
}
