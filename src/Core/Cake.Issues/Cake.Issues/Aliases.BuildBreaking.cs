namespace Cake.Issues;

using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

/// <content>
/// Contains functionality for breaking builds.
/// </content>
public static partial class Aliases
{
    /// <summary>
    /// Fails build if any issues are found.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <example>
    /// <para>Fails build if issues are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(issues);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues)
    {
        context.NotNull();
        issues.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, null);
    }

    /// <summary>
    /// Fails build if any issues are found.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="handler">Optional handler to call if <paramref name="issues"/> contains items.</param>
    /// <example>
    /// <para>Fails build if issues are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         x => Information("{0} issues found", x.Count()));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        Action<IEnumerable<IIssue>> handler)
    {
        context.NotNull();
        issues.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, handler);
    }

    /// <summary>
    /// Fails build if any issues of certain minimum priority are found.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="priority">Minimum priority of issues which should be considered.</param>
    /// <example>
    /// <para>Fails build if errors are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(issues, IssuePriority.Error);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        IssuePriority priority)
    {
        context.NotNull();
        issues.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, priority, null);
    }

    /// <summary>
    /// Fails build if any issues of certain minimum priority are found.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="priority">Minimum priority of issues which should be considered.</param>
    /// <param name="handler">Optional handler to call when issues of <paramref name="priority"/> are found.</param>
    /// <example>
    /// <para>Fails build if errors are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         IssuePriority.Error,
    ///         x => Information("{0} issues found", x.Count()));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        IssuePriority priority,
        Action<IEnumerable<IIssue>> handler)
    {
        context.NotNull();
        issues.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, priority, handler);
    }

    /// <summary>
    /// Fails build if any issues from a specific issue provider are found.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="providerType">Type of the issue provider.</param>
    /// <example>
    /// <para>Fails build if issues from MsBuild are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(issues, MsBuildIssuesProviderTypeName);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        string providerType)
    {
        context.NotNull();
        issues.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, providerType, null);
    }

    /// <summary>
    /// Fails build if any issues from a specific issue provider are found.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="providerType">Type of the issue provider.</param>
    /// <param name="handler">Optional handler to call when issues from <paramref name="providerType"/> are found.</param>
    /// <example>
    /// <para>Fails build if issues from MsBuild are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         MsBuildIssuesProviderTypeName,
    ///         x => Information("{0} issues found", x.Count()));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        string providerType,
        Action<IEnumerable<IIssue>> handler)
    {
        context.NotNull();
        issues.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, providerType, handler);
    }

    /// <summary>
    /// Fails build if any issues are found with settings to limit to priority and issue provider types.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="settings">Settings to apply.</param>
    /// <example>
    /// <para>Fails build if issues with severity warning or higher from MsBuild are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         new BuildBreakingSettings
    ///         {
    ///             MinimumPriority = IssuePriority.Warning
    ///             IssueProvidersToConsider = [MsBuildIssuesProviderTypeName]
    ///         });
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// <para>Fails build if issues with severity warning or higher are found, ignoring MsBuild issues:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         new BuildBreakingSettings
    ///         {
    ///             MinimumPriority = IssuePriority.Warning
    ///             IssueProvidersToIgnore = [MsBuildIssuesProviderTypeName]
    ///         });
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        BuildBreakingSettings settings)
    {
        context.NotNull();
        issues.NotNull();
        settings.NotNull();

        BuildBreaker.BreakBuildOnIssues(
            issues,
            settings.MinimumPriority,
            settings.IssueProvidersToConsider,
            settings.IssueProvidersToIgnore,
            null);
    }

    /// <summary>
    /// Fails build if any issues are found with settings to limit to priority and issue provider types.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="settings">Settings to apply.</param>
    /// <param name="handler">Optional handler to call when issues matching parameters are found.</param>
    /// <example>
    /// <para>Fails build if issues with severity warning or higher from MsBuild are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         new BuildBreakingSettings
    ///         {
    ///             MinimumPriority = IssuePriority.Warning
    ///             IssueProvidersToConsider = [MsBuildIssuesProviderTypeName]
    ///         },
    ///         x => Information("{0} issues found", x.Count()));
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// <para>Fails build if issues with severity warning or higher are found, ignoring MsBuild issues:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         new BuildBreakingSettings
    ///         {
    ///             MinimumPriority = IssuePriority.Warning
    ///             IssueProvidersToIgnore = [MsBuildIssuesProviderTypeName]
    ///         },
    ///         x => Information("{0} issues found", x.Count()));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        BuildBreakingSettings settings,
        Action<IEnumerable<IIssue>> handler)
    {
        context.NotNull();
        issues.NotNull();
        settings.NotNull();

        BuildBreaker.BreakBuildOnIssues(
            issues,
            settings.MinimumPriority,
            settings.IssueProvidersToConsider,
            settings.IssueProvidersToIgnore,
            handler);
    }

    /// <summary>
    /// Fails build if any issues are found matching a specific predicate.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="predicate">Predicate to .</param>
    /// <example>
    /// <para>Fails build if errors are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         x => x.Priority >= (int)IssuePriority.Error);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        Func<IIssue, bool> predicate)
    {
        context.NotNull();
        issues.NotNull();
        predicate.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, predicate, null);
    }

    /// <summary>
    /// Fails build if any issues are found matching a specific predicate.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="predicate">Predicate to .</param>
    /// <param name="handler">Optional handler to call when issues matching <paramref name="predicate"/> are found.</param>
    /// <example>
    /// <para>Fails build if errors are found:</para>
    /// <code>
    /// <![CDATA[
    ///     BreakBuildOnIssues(
    ///         issues,
    ///         x => x.Priority >= (int)IssuePriority.Error,
    ///         x => Information("{0} issues found", x.Count()));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.BuildBreakingCakeAliasCategory)]
    public static void BreakBuildOnIssues(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        Func<IIssue, bool> predicate,
        Action<IEnumerable<IIssue>> handler)
    {
        context.NotNull();
        issues.NotNull();
        predicate.NotNull();

        BuildBreaker.BreakBuildOnIssues(issues, predicate, handler);
    }
}
