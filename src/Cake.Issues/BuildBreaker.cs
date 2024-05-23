namespace Cake.Issues;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class for breaking builds.
/// </summary>
internal class BuildBreaker()
{
    /// <summary>
    /// Fails build if any issues are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    public void BreakBuildOnIssues(IEnumerable<IIssue> issues)
    {
        issues.NotNull();

        if (issues.Any())
        {
            this.BreakBuild(issues);
        }
    }

    /// <summary>
    /// Fails build if any issues of certain minimum priority are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="priority">Minimum priority of issues which should be considered.</param>
    public void BreakBuildOnIssues(IEnumerable<IIssue> issues, IssuePriority priority)
    {
        issues.NotNull();

        this.BreakBuildOnIssues(issues, x => x.Priority >= (int)priority);
    }

    /// <summary>
    /// Fails build if any issues from a specific issue provider are found.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="providerType">Type of the issue provider.</param>
    public void BreakBuildOnIssues(IEnumerable<IIssue> issues, string providerType)
    {
        issues.NotNull();
        providerType.NotNullOrWhiteSpace();

        this.BreakBuildOnIssues(issues, x => x.ProviderType == providerType);
    }

    /// <summary>
    /// Fails build if any issues are found matching a specific predicate.
    /// </summary>
    /// <param name="issues">Issues which should be checked.</param>
    /// <param name="predicate">Predicate to .</param>
    public void BreakBuildOnIssues(IEnumerable<IIssue> issues, Func<IIssue, bool> predicate)
    {
        issues.NotNull();
        predicate.NotNull();

        if (issues.Any(predicate))
        {
            this.BreakBuild(issues);
        }
    }

    private void BreakBuild(IEnumerable<IIssue> issues) =>
        throw new IssuesFoundException(issues.Count());
}
