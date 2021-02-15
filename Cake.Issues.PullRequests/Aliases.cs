﻿namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality related to reporting issues to pull requests.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    [CakeNamespaceImport("Cake.Issues.PullRequests.PullRequestSystem")]
    public static class Aliases
    {
        /// <summary>
        /// Reports issues to pull requests.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProvider">The provider for issues.</param>
        /// <param name="pullRequestSystem">The pull request system.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Information about the reported and written issues.</returns>
        /// <example>
        /// <para>Report issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         MsBuildIssuesFromFilePath(
        ///             @"C:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         new DirectoryPath("c:\repo"));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.ReportIssuesToPullRequestCakeAliasCategory)]
        public static PullRequestIssueResult ReportIssuesToPullRequest(
            this ICakeContext context,
            IIssueProvider issueProvider,
            IPullRequestSystem pullRequestSystem,
            DirectoryPath repositoryRoot)
        {
            context.NotNull(nameof(context));
            issueProvider.NotNull(nameof(issueProvider));
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            return
                context.ReportIssuesToPullRequest(
                    issueProvider,
                    pullRequestSystem,
                    new ReportIssuesToPullRequestSettings(repositoryRoot));
        }

        /// <summary>
        /// Reports issues to pull requests.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">The list of provider for issues.</param>
        /// <param name="pullRequestSystem">The pull request system.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Information about the reported and written issues.</returns>
        /// <example>
        /// <para>Report issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     ReportIssuesToPullRequest(
        ///         new List<IIssueProvider>
        ///         {
        ///             MsBuildIssuesFromFilePath(
        ///                 @"C:\build\msbuild.log",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             InspectCodeFromFilePath(
        ///                 @"C:\build\inspectcode.log")
        ///         },
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         new DirectoryPath("c:\repo"));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.ReportIssuesToPullRequestCakeAliasCategory)]
        public static PullRequestIssueResult ReportIssuesToPullRequest(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            IPullRequestSystem pullRequestSystem,
            DirectoryPath repositoryRoot)
        {
            context.NotNull(nameof(context));
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            // ReSharper disable once PossibleMultipleEnumeration
            return
                context.ReportIssuesToPullRequest(
                    issueProviders,
                    pullRequestSystem,
                    new ReportIssuesToPullRequestSettings(repositoryRoot));
        }

        /// <summary>
        /// Reports issues to pull requests using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProvider">The provider for issues.</param>
        /// <param name="pullRequestSystem">The pull request system.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Information about the reported and written issues.</returns>
        /// <example>
        /// <para>Report issues reported as MsBuild warnings to a TFS pull request and limit number of comments to ten:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new ReportIssuesToPullRequestSettings(new DirectoryPath("c:\repo"))
        ///         {
        ///             MaxIssuesToPost = 10
        ///         };
        ///
        ///     ReportIssuesToPullRequest(
        ///         MsBuildIssuesFromFilePath(
        ///             @"C:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         settings));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.ReportIssuesToPullRequestCakeAliasCategory)]
        public static PullRequestIssueResult ReportIssuesToPullRequest(
            this ICakeContext context,
            IIssueProvider issueProvider,
            IPullRequestSystem pullRequestSystem,
            ReportIssuesToPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            issueProvider.NotNull(nameof(issueProvider));
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            settings.NotNull(nameof(settings));

            return
                context.ReportIssuesToPullRequest(
                    new List<IIssueProvider> { issueProvider },
                    pullRequestSystem,
                    settings);
        }

        /// <summary>
        /// Reports issues to pull requests using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">The list of provider for issues.</param>
        /// <param name="pullRequestSystem">The pull request system.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Information about the reported and written issues.</returns>
        /// <example>
        /// <para>Report issues reported as MsBuild warnings to a TFS pull request and limit number of comments to ten:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new ReportIssuesToPullRequestSettings(new DirectoryPath("c:\repo"))
        ///         {
        ///             MaxIssuesToPost = 10
        ///         };
        ///
        ///     ReportIssuesToPullRequest(
        ///         new List<IIssueProvider>
        ///         {
        ///             MsBuildIssuesFromFilePath(
        ///                 @"C:\build\msbuild.log",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             InspectCodeFromFilePath(
        ///                 @"C:\build\inspectcode.log")
        ///         },
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         settings));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.ReportIssuesToPullRequestCakeAliasCategory)]
        public static PullRequestIssueResult ReportIssuesToPullRequest(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            IPullRequestSystem pullRequestSystem,
            ReportIssuesToPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            pullRequestSystem.NotNull(nameof(pullRequestSystem));
            settings.NotNull(nameof(settings));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            // ReSharper disable once PossibleMultipleEnumeration
            var orchestrator =
                new Orchestrator(
                    context.Log,
                    issueProviders,
                    pullRequestSystem,
                    settings);
            return orchestrator.Run();
        }
    }
}
