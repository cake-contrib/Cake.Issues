namespace Cake.Issues;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class for breaking builds.
/// </summary>
internal static class BuildBreaker
{
    /// <summary>
    /// Fails build if any issues are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="handler">Optional handler to call if <paramref name="issues"/> contains items.</param>
    public static void BreakBuildOnIssues(IEnumerable<IIssue> issues, Action<IEnumerable<IIssue>> handler)
    {
        issues.NotNull();

        if (issues.Any())
        {
            BreakBuild(issues, handler);
        }
    }

    /// <summary>
    /// Fails build if any issues of certain minimum priority are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="priority">Minimum priority of issues which should be considered.</param>
    /// <param name="handler">Optional handler to call when issues of <paramref name="priority"/> are found.</param>
    public static void BreakBuildOnIssues(
        IEnumerable<IIssue> issues,
        IssuePriority priority,
        Action<IEnumerable<IIssue>> handler)
    {
        issues.NotNull();

        BreakBuildOnIssues(issues, x => x.Priority >= (int)priority, handler);
    }

    /// <summary>
    /// Fails build if any issues from a specific issue provider are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="providerType">Type of the issue provider.</param>
    /// <param name="handler">Optional handler to call when issues from <paramref name="providerType"/> are found.</param>
    public static void BreakBuildOnIssues(
        IEnumerable<IIssue> issues,
        string providerType,
        Action<IEnumerable<IIssue>> handler)
    {
        issues.NotNull();
        providerType.NotNullOrWhiteSpace();

        BreakBuildOnIssues(issues, x => x.ProviderType == providerType, handler);
    }

    /// <summary>
    /// Fails build if any issues are found with possibility to limit to priority and issue provider types.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="minimumPriority">Minimum priority of issues which should be considered.
    /// If set to <see cref="IssuePriority.Undefined"/>, all issues are considered.</param>
    /// <param name="issueProvidersToConsider">Issue providers to consider.
    /// If empty, all providers are considered.</param>
    /// <param name="issueProvidersToIgnore">Issue providers to ignore.</param>
    /// <param name="handler">Optional handler to call when issues matching parameters are found.</param>
    public static void BreakBuildOnIssues(
        IEnumerable<IIssue> issues,
        IssuePriority minimumPriority,
        IEnumerable<string> issueProvidersToConsider,
        IEnumerable<string> issueProvidersToIgnore,
        Action<IEnumerable<IIssue>> handler)
    {
        issues.NotNull();
        issueProvidersToConsider.NotNull();
        issueProvidersToIgnore.NotNull();

        BreakBuildOnIssues(
            issues,
            x =>
            {
                return
                    (x.Priority == null || x.Priority >= (int)minimumPriority) &&
                    (!issueProvidersToConsider.Any() || issueProvidersToConsider.Contains(x.ProviderType)) &&
                    !issueProvidersToIgnore.Contains(x.ProviderType);
            },
            handler);
    }

    /// <summary>
    /// Fails build if any issues are found matching a specific predicate.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="predicate">Predicate to .</param>
    /// <param name="handler">Optional handler to call when issues matching <paramref name="predicate"/> are found.</param>
    public static void BreakBuildOnIssues(
        IEnumerable<IIssue> issues,
        Func<IIssue, bool> predicate,
        Action<IEnumerable<IIssue>> handler)
    {
        issues.NotNull();
        predicate.NotNull();

        var issuesToCheck = issues.Where(predicate).ToList();
        if (issuesToCheck.Any(predicate))
        {
            BreakBuild(issuesToCheck, handler);
        }
    }

    private static void BreakBuild(IEnumerable<IIssue> issues, Action<IEnumerable<IIssue>> handler)
    {
        handler?.Invoke(issues);

        throw new IssuesFoundException(issues.Count());
    }
}
