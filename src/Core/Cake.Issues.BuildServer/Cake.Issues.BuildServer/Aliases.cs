namespace Cake.Issues.BuildServer;

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
    /// <param name="buildServer">The build server implementation.</param>
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
        IBuildServer buildServer,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        buildServer.NotNull();
        repositoryRoot.NotNull();

        issues.NotNullOrEmptyElement();

        return
            context.ReportIssuesToBuildServer(
                issues,
                buildServer,
                new ReportIssuesToBuildServerSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to build server using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be reported.</param>
    /// <param name="buildServer">The build server implementation.</param>
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
        IBuildServer buildServer,
        IReportIssuesToBuildServerSettings settings)
    {
        context.NotNull();
        buildServer.NotNull();
        settings.NotNull();

        issues.NotNullOrEmptyElement();

        var orchestrator =
            new BuildServerOrchestrator(
                context.Log,
                AnsiConsole.Console,
                buildServer);

        return orchestrator.Run(issues, settings);
    }

    /// <summary>
    /// Reports issues to build server.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProvider">The provider for issues.</param>
    /// <param name="buildServer">The build server implementation.</param>
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
        IBuildServer buildServer,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        issueProvider.NotNull();
        buildServer.NotNull();
        repositoryRoot.NotNull();

        return
            context.ReportIssuesToBuildServer(
                issueProvider,
                buildServer,
                new ReportIssuesToBuildServerFromIssueProviderSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to build server.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProviders">The list of provider for issues.</param>
    /// <param name="buildServer">The build server implementation.</param>
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
        IBuildServer buildServer,
        DirectoryPath repositoryRoot)
    {
        context.NotNull();
        buildServer.NotNull();
        repositoryRoot.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        return
            context.ReportIssuesToBuildServer(
                issueProviders,
                buildServer,
                new ReportIssuesToBuildServerFromIssueProviderSettings(repositoryRoot));
    }

    /// <summary>
    /// Reports issues to build server using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProvider">The provider for issues.</param>
    /// <param name="buildServer">The build server implementation.</param>
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
        IBuildServer buildServer,
        IReportIssuesToBuildServerFromIssueProviderSettings settings)
    {
        context.NotNull();
        issueProvider.NotNull();
        buildServer.NotNull();
        settings.NotNull();

        return
            context.ReportIssuesToBuildServer(
                [issueProvider],
                buildServer,
                settings);
    }

    /// <summary>
    /// Reports issues to build server using the specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issueProviders">The list of provider for issues.</param>
    /// <param name="buildServer">The build server implementation.</param>
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
        IBuildServer buildServer,
        IReportIssuesToBuildServerFromIssueProviderSettings settings)
    {
        context.NotNull();
        buildServer.NotNull();
        settings.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        var orchestrator =
            new BuildServerOrchestrator(
                context.Log,
                AnsiConsole.Console,
                buildServer);

        return orchestrator.Run(issueProviders, settings);
    }
}