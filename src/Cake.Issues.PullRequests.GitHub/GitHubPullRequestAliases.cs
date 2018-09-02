namespace Cake.Issues.PullRequests.GitHub
{
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <summary>
    /// Contains functionality related to writing code analysis issues to GitHub pull requests.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class GitHubPullRequestAliases
    {
        /// <summary>
        /// Gets an object for writing issues to GitHub pull requests using the default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Object for writing issues to GitHub pull requests.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a GitHub pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         GitHubPullRequests(),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem GitHubPullRequests(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new GitHubPullRequestSystem(context, new GitHubPullRequestSettings());
        }

        /// <summary>
        /// Gets an object for writing issues to GitHub pull requests using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for writing issues to GitHub pull requests.</param>
        /// <returns>Object for writing issues to GitHub pull requests.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a GitHub pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var gitHubSettings =
        ///         new GitHubPullRequestSettings
        ///         {
        ///         };
        ///
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         GitHubPullRequests(gitHubSettings),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem GitHubPullRequests(
            this ICakeContext context,
            GitHubPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new GitHubPullRequestSystem(context, settings);
        }
    }
}
