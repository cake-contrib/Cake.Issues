namespace Cake.Issues.PullRequests.GitHubActions
{
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <summary>
    /// Contains functionality related to writing code analysis issues to GitHub Actions.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class GitHubActionsBuildsAliases
    {
        /// <summary>
        /// Gets an object for writing issues to GitHub Actions using the default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Object for writing issues to GitHub Actions.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to GitHub Actions:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         GitHubActionsBuilds(),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem GitHubActionsBuilds(
            this ICakeContext context)
        {
            context.NotNull();

            return new GitHubActionsPullRequestSystem(context.Log, new GitHubActionsBuildSettings());
        }

        /// <summary>
        /// Gets an object for writing issues to GitHub Actions using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for writing issues to GitHub Actions.</param>
        /// <returns>Object for writing issues to GitHub Actions.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to GitHub Actions:</para>
        /// <code>
        /// <![CDATA[
        ///     var gitHubActionsSettings =
        ///         new GitHubActionsBuildSettings
        ///         {
        ///         };
        ///
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         GitHubActionsBuilds(gitHubActionsSettings),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem GitHubActionsBuilds(
            this ICakeContext context,
            GitHubActionsBuildSettings settings)
        {
            context.NotNull();
            settings.NotNull();

            return new GitHubActionsPullRequestSystem(context.Log, settings);
        }
    }
}
