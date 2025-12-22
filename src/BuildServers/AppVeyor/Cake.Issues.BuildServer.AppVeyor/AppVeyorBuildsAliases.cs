namespace Cake.Issues.BuildServer.AppVeyor;

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
    ///     ReportIssuesToBuildServer(
    ///         MsBuildCodeAnalysis(
    ///             @"c:\build\msbuild.log",
    ///             MsBuildXmlFileLoggerFormat),
    ///         AppVeyorBuilds(),
    ///         @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.BuildServerCakeAliasCategory)]
    public static IBuildServer AppVeyorBuilds(
        this ICakeContext context)
    {
        context.NotNull();

        return new AppVeyorBuildServer(context, new AppVeyorBuildSettings());
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
    ///     ReportIssuesToBuildServer(
    ///         MsBuildCodeAnalysis(
    ///             @"c:\build\msbuild.log",
    ///             MsBuildXmlFileLoggerFormat),
    ///         AppVeyorBuilds(appVeyorSettings),
    ///         @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(BuildServerAliasConstants.BuildServerCakeAliasCategory)]
    public static IBuildServer AppVeyorBuilds(
        this ICakeContext context,
        AppVeyorBuildSettings settings)
    {
        context.NotNull();
        settings.NotNull();

        return new AppVeyorBuildServer(context, settings);
    }
}
