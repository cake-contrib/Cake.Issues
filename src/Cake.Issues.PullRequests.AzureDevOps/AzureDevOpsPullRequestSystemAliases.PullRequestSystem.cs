namespace Cake.Issues.PullRequests.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to <see cref="AzureDevOpsPullRequestSystem"/>.
    /// </content>
    public static partial class AzureDevOpsPullRequestSystemAliases
    {
        /// <summary>
        /// Gets an object for writing issues to Azure DevOps pull request in a specific repository and for a
        /// specific source branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceBranch">Branch for which the pull request is made.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        /// <returns>Object for writing issues to Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an Azure DevOps pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AzureDevOpsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             AzureDevOpsAuthenticationNtlm()),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AzureDevOpsPullRequests(
            this ICakeContext context,
            Uri repositoryUrl,
            string sourceBranch,
            IAzureDevOpsCredentials credentials)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            sourceBranch.NotNullOrWhiteSpace(nameof(sourceBranch));
            credentials.NotNull(nameof(credentials));

            return context.AzureDevOpsPullRequests(new AzureDevOpsPullRequestSystemSettings(repositoryUrl, sourceBranch, credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Azure DevOps pull request in a specific repository and with a specific ID.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="pullRequestId">ID of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        /// <returns>Object for writing issues to Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an Azure DevOps Server pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AzureDevOpsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             5,
        ///             AzureDevOpsAuthenticationNtlm()),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AzureDevOpsPullRequests(
            this ICakeContext context,
            Uri repositoryUrl,
            int pullRequestId,
            IAzureDevOpsCredentials credentials)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            credentials.NotNull(nameof(credentials));

            return context.AzureDevOpsPullRequests(new AzureDevOpsPullRequestSystemSettings(repositoryUrl, pullRequestId, credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Azure DevOps pull request where all required data is taken
        /// from the environment variables set by Azure Pipelines.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        /// <returns>Object for writing issues to Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an Azure DevOps Server pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AzureDevOpsPullRequests(
        ///             AzureDevOpsAuthenticationNtlm()),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AzureDevOpsPullRequests(
            this ICakeContext context,
            IAzureDevOpsCredentials credentials)
        {
            context.NotNull(nameof(context));
            credentials.NotNull(nameof(credentials));

            return context.AzureDevOpsPullRequests(new AzureDevOpsPullRequestSystemSettings(credentials));
        }

        /// <summary>
        /// Gets an object for writing issues to Azure DevOps pull request where all required data (including authentication token)
        /// is taken from the environment variables set by Azure Pipelines.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Object for writing issues to Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an Azure DevOps pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AzureDevOpsPullRequests(),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AzureDevOpsPullRequests(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return context.AzureDevOpsPullRequests(new AzureDevOpsPullRequestSystemSettings());
        }

        /// <summary>
        /// Gets an object for writing issues to Azure DevOps pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <returns>Object for writing issues to Azure DevOps pull request.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an Azure DevOps Server pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new AzureDevOpsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             AzureDevOpsAuthenticationNtlm());
        ///
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AzureDevOpsPullRequests(pullRequestSettings),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AzureDevOpsPullRequests(
            this ICakeContext context,
            AzureDevOpsPullRequestSystemSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new AzureDevOpsPullRequestSystem(context.Log, settings);
        }
    }
}
