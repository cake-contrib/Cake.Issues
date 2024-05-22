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

        var breaker = new BuildBreaker();
        breaker.BreakBuildOnIssues(issues);
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

        var breaker = new BuildBreaker();
        breaker.BreakBuildOnIssues(issues, priority);
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

        var breaker = new BuildBreaker();
        breaker.BreakBuildOnIssues(issues, providerType);
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
    ///     BreakBuildOnIssues(issues, IssuePriority.Error);
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

        var breaker = new BuildBreaker();
        breaker.BreakBuildOnIssues(issues, predicate);
    }
}
