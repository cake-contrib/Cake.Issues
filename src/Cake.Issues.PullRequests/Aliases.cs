namespace Cake.Issues.PullRequests;

using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

/// <summary>
/// Contains functionality related to reporting issues to pull requests.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class Aliases
{
    /// <summary>
    /// Reports issues to pull requests.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be reported.</param>
    /// <param name="pullRequestSystem">The pull request system.</param>
    /// <param name="repositoryRoot">Root path of the repository.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a TFS pull request:</para>
    /// <code>
    /// <![CDATA[
    ///     ReportIssuesToPullRequest(
    ///         issues,
    ///         TfsPullRequests(
    ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
    ///             "refs/heads/feature/myfeature",
    ///             TfsAuthenticationNtlm()),
    ///         @"C:\repo"));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(PullRequestsAliasConstants.ReportIssuesToPullRequestCakeAliasCategory)]
    public static PullRequestIssueResult ReportIssuesToPullRequest(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        IPullRequestSystem pullRequestSystem,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        pullRequestSystem.NotNull();
        repositoryRoot.NotNull();

        issues.NotNullOrEmptyElement();

        return
            context.ReportIssuesToPullRequest(
                issues,
                pullRequestSystem,
                new ReportIssuesToPullRequestSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to pull requests using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be reported.</param>
    /// <param name="pullRequestSystem">The pull request system.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a TFS pull request and limit number of comments to ten:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new ReportIssuesToPullRequestSettings(@"C:\repo")
    ///         {
    ///             MaxIssuesToPost = 10
    ///         };
    ///
    ///     ReportIssuesToPullRequest(
    ///         issues,
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
        IEnumerable<IIssue> issues,
        IPullRequestSystem pullRequestSystem,
        IReportIssuesToPullRequestSettings settings)
    {
        context.NotNull();
        pullRequestSystem.NotNull();
        settings.NotNull();

        issues.NotNullOrEmptyElement();

        var orchestrator =
            new Orchestrator(
                context.Log,
                pullRequestSystem);

        return orchestrator.Run(issues, settings);
    }

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
    ///         @"C:\repo");
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
        context.NotNull();
        issueProvider.NotNull();
        pullRequestSystem.NotNull();
        repositoryRoot.NotNull();

        return
            context.ReportIssuesToPullRequest(
                issueProvider,
                pullRequestSystem,
                new ReportIssuesToPullRequestFromIssueProviderSettings(repositoryRoot));
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
    ///         @"C:\repo");
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
        context.NotNull();
        pullRequestSystem.NotNull();
        repositoryRoot.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        return
            context.ReportIssuesToPullRequest(
                issueProviders,
                pullRequestSystem,
                new ReportIssuesToPullRequestFromIssueProviderSettings(repositoryRoot));
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
    ///         new ReportIssuesToPullRequestFromIssueProviderSettings(@"C:\repo")
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
        IReportIssuesToPullRequestFromIssueProviderSettings settings)
    {
        context.NotNull();
        issueProvider.NotNull();
        pullRequestSystem.NotNull();
        settings.NotNull();

        return
            context.ReportIssuesToPullRequest(
                [issueProvider],
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
    ///         new ReportIssuesToPullRequestFromIssueProviderSettings(@"C:\repo")
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
        IReportIssuesToPullRequestFromIssueProviderSettings settings)
    {
        context.NotNull();
        pullRequestSystem.NotNull();
        settings.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        var orchestrator =
            new Orchestrator(
                context.Log,
                pullRequestSystem);

        return orchestrator.Run(issueProviders, settings);
    }
}
