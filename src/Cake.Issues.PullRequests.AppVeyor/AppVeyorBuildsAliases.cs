namespace Cake.Issues.PullRequests.AppVeyor
{
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <summary>
    /// Contains functionality related to writing code analysis issues to AppVeyor builds.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class AppVeyorBuildsAliases
    {
        /// <summary>
        /// Gets an object for writing issues to AppVeyor builds using the default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Object for writing issues to AppVeyor builds.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an AppVeyor build:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AppVeyorBuilds(),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AppVeyorBuilds(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new AppVeyorPullRequestSystem(context, new AppVeyorBuildSettings());
        }

        /// <summary>
        /// Gets an object for writing issues to AppVeyor builds using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing AppVeyor.</param>
        /// <returns>Object for writing issues to AppVeyor builds.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to an AppVeyor build:</para>
        /// <code>
        /// <![CDATA[
        ///     var appVeyorSettings =
        ///         new AppVeyorBuildSettings
        ///         {
        ///             MessagePattern = "Project: {ProjectName}, File: {FilePath}, Line: {Line}"
        ///         };
        ///
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         MsBuildCodeAnalysis(
        ///             @"c:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         AppVeyorBuilds(appVeyorSettings),
        ///         @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static IPullRequestSystem AppVeyorBuilds(
            this ICakeContext context,
            AppVeyorBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new AppVeyorPullRequestSystem(context, settings);
        }
    }
}
