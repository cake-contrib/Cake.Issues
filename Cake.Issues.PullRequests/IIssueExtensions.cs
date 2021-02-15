﻿namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions for <see cref="IIssue"/>s.
    /// </summary>
    internal static class IIssueExtensions
    {
        /// <summary>
        /// Sorts issues based on the following criteria:
        /// <list type="number">
        /// <description><see cref="IIssue.Priority"/> descending</description>
        /// <description><see cref="IIssue.AffectedFileRelativePath"/> is null</description>
        /// <description> The <see cref="IIssue.AffectedFileRelativePath"/> FullPath.</description>
        /// </list>
        /// </summary>
        /// <param name="issues">Issues to be sorted.</param>
        /// <returns>The sorted issues.</returns>
        public static IEnumerable<IIssue> SortWithDefaultPriorization(this IEnumerable<IIssue> issues)
        {
            issues.NotNull(nameof(issues));

            return issues
                .OrderByDescending(x => x.Priority)
                .ThenBy(x => x.AffectedFileRelativePath is null)
                .ThenBy(x => x.AffectedFileRelativePath?.FullPath);
        }
    }
}