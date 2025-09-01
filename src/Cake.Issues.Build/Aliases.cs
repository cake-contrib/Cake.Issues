namespace Cake.Issues.Build;

using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Spectre.Console;

/// <summary>
/// Contains functionality related to reporting issues to build servers.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class Aliases
{
    /// <summary>
    /// Reports issues to build server.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be reported.</param>
    /// <param name="buildServerSystem">The build server system.</param>
    /// <param name="repositoryRoot">Root path of the repository.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a build server:</para>
    /// <code>
    /// <![CDATA[
    ///     ReportIssuesToBuildServer(
    ///         issues,
    ///         AppVeyorBuilds(),
    ///         @"C:\repo"));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.ReportIssuesToBuildServerCakeAliasCategory)]
    public static BuildServerIssueResult ReportIssuesToBuildServer(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        IBuildServerSystem buildServerSystem,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        buildServerSystem.NotNull();
        repositoryRoot.NotNull();

        issues.NotNullOrEmptyElement();

        return
            context.ReportIssuesToBuildServer(
                issues,
                buildServerSystem,
                new ReportIssuesToBuildServerSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to build server using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be reported.</param>
    /// <param name="buildServerSystem">The build server system.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a build server and limit number of issues to ten:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new ReportIssuesToBuildServerSettings(@"C:\repo")
    ///         {
    ///             MaxIssuesToPost = 10
    ///         };
    ///
    ///     ReportIssuesToBuildServer(
    ///         issues,
    ///         AppVeyorBuilds(),
    ///         settings));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.ReportIssuesToBuildServerCakeAliasCategory)]
    public static BuildServerIssueResult ReportIssuesToBuildServer(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        IBuildServerSystem buildServerSystem,
        IReportIssuesToBuildServerSettings settings)
    {
        context.NotNull();
        buildServerSystem.NotNull();
        settings.NotNull();

        issues.NotNullOrEmptyElement();

        var orchestrator =
            new BuildServerOrchestrator(
                context.Log,
                AnsiConsole.Console,
                buildServerSystem);

        return orchestrator.Run(issues, settings);
    }

    /// <summary>
    /// Reports issues to build server.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProvider">The provider for issues.</param>
    /// <param name="buildServerSystem">The build server system.</param>
    /// <param name="repositoryRoot">Root path of the repository.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a build server:</para>
    /// <code>
    /// <![CDATA[
    ///     ReportIssuesToBuildServer(
    ///         MsBuildIssuesFromFilePath(
    ///             @"C:\build\msbuild.log",
    ///             MsBuildXmlFileLoggerFormat),
    ///         AppVeyorBuilds(),
    ///         @"C:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.ReportIssuesToBuildServerCakeAliasCategory)]
    public static BuildServerIssueResult ReportIssuesToBuildServer(
        this ICakeContext context,
        IIssueProvider issueProvider,
        IBuildServerSystem buildServerSystem,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        issueProvider.NotNull();
        buildServerSystem.NotNull();
        repositoryRoot.NotNull();

        return
            context.ReportIssuesToBuildServer(
                issueProvider,
                buildServerSystem,
                new ReportIssuesToBuildServerFromIssueProviderSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to build server.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProviders">The list of provider for issues.</param>
    /// <param name="buildServerSystem">The build server system.</param>
    /// <param name="repositoryRoot">Root path of the repository.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a build server:</para>
    /// <code>
    /// <![CDATA[
    ///     ReportIssuesToBuildServer(
    ///         new List<IIssueProvider>
    ///         {
    ///             MsBuildIssuesFromFilePath(
    ///                 @"C:\build\msbuild.log",
    ///                 MsBuildXmlFileLoggerFormat),
    ///             InspectCodeFromFilePath(
    ///                 @"C:\build\inspectcode.log")
    ///         },
    ///         AppVeyorBuilds(),
    ///         @"C:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.ReportIssuesToBuildServerCakeAliasCategory)]
    public static BuildServerIssueResult ReportIssuesToBuildServer(
        this ICakeContext context,
        IEnumerable<IIssueProvider> issueProviders,
        IBuildServerSystem buildServerSystem,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        buildServerSystem.NotNull();
        repositoryRoot.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        return
            context.ReportIssuesToBuildServer(
                issueProviders,
                buildServerSystem,
                new ReportIssuesToBuildServerFromIssueProviderSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to build server using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProvider">The provider for issues.</param>
    /// <param name="buildServerSystem">The build server system.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a build server and limit number of issues to ten:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new ReportIssuesToBuildServerFromIssueProviderSettings(@"C:\repo")
    ///         {
    ///             MaxIssuesToPost = 10
    ///         };
    ///
    ///     ReportIssuesToBuildServer(
    ///         MsBuildIssuesFromFilePath(
    ///             @"C:\build\msbuild.log",
    ///             MsBuildXmlFileLoggerFormat),
    ///         AppVeyorBuilds(),
    ///         settings));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.ReportIssuesToBuildServerCakeAliasCategory)]
    public static BuildServerIssueResult ReportIssuesToBuildServer(
        this ICakeContext context,
        IIssueProvider issueProvider,
        IBuildServerSystem buildServerSystem,
        IReportIssuesToBuildServerFromIssueProviderSettings settings)
    {
        context.NotNull();
        issueProvider.NotNull();
        buildServerSystem.NotNull();
        settings.NotNull();

        return
            context.ReportIssuesToBuildServer(
                [issueProvider],
                buildServerSystem,
                settings);
    }

    /// <summary>
    /// Reports issues to build server using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProviders">The list of provider for issues.</param>
    /// <param name="buildServerSystem">The build server system.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>Information about the reported and written issues.</returns>
    /// <example>
    /// <para>Report issues reported as MsBuild warnings to a build server and limit number of issues to ten:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new ReportIssuesToBuildServerFromIssueProviderSettings(@"C:\repo")
    ///         {
    ///             MaxIssuesToPost = 10
    ///         };
    ///
    ///     ReportIssuesToBuildServer(
    ///         new List<IIssueProvider>
    ///         {
    ///             MsBuildIssuesFromFilePath(
    ///                 @"C:\build\msbuild.log",
    ///                 MsBuildXmlFileLoggerFormat),
    ///             InspectCodeFromFilePath(
    ///                 @"C:\build\inspectcode.log")
    ///         },
    ///         AppVeyorBuilds(),
    ///         settings));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.ReportIssuesToBuildServerCakeAliasCategory)]
    public static BuildServerIssueResult ReportIssuesToBuildServer(
        this ICakeContext context,
        IEnumerable<IIssueProvider> issueProviders,
        IBuildServerSystem buildServerSystem,
        IReportIssuesToBuildServerFromIssueProviderSettings settings)
    {
        context.NotNull();
        buildServerSystem.NotNull();
        settings.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        var orchestrator =
            new BuildServerOrchestrator(
                context.Log,
                AnsiConsole.Console,
                buildServerSystem);

        return orchestrator.Run(issueProviders, settings);
    }
}