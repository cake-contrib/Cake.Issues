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
    public static void BreakBuildOnIssues(IEnumerable<IIssue> issues)
    {
        issues.NotNull();

        if (issues.Any())
        {
            BreakBuild(issues);
        }
    }

    /// <summary>
    /// Fails build if any issues of certain minimum priority are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="priority">Minimum priority of issues which should be considered.</param>
    public static void BreakBuildOnIssues(IEnumerable<IIssue> issues, IssuePriority priority)
    {
        issues.NotNull();

        BreakBuildOnIssues(issues, x => x.Priority >= (int)priority);
    }

    /// <summary>
    /// Fails build if any issues from a specific issue provider are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="providerType">Type of the issue provider.</param>
    public static void BreakBuildOnIssues(IEnumerable<IIssue> issues, string providerType)
    {
        issues.NotNull();
        providerType.NotNullOrWhiteSpace();

        BreakBuildOnIssues(issues, x => x.ProviderType == providerType);
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
    public static void BreakBuildOnIssues(
        IEnumerable<IIssue> issues,
        IssuePriority minimumPriority,
        IEnumerable<string> issueProvidersToConsider,
        IEnumerable<string> issueProvidersToIgnore)
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
            });
    }

    /// <summary>
    /// Fails build if any issues are found matching a specific predicate.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="predicate">Predicate to .</param>
    public static void BreakBuildOnIssues(IEnumerable<IIssue> issues, Func<IIssue, bool> predicate)
    {
        issues.NotNull();
        predicate.NotNull();

        if (issues.Any(predicate))
        {
            BreakBuild(issues);
        }
    }

    private static void BreakBuild(IEnumerable<IIssue> issues) =>
        throw new IssuesFoundException(issues.Count());
}
