namespace Cake.Issues.Shared;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// Class for filtering issues for build servers.
/// </summary>
public class BasicIssueFilterer
{
    private readonly ICakeLog log;
    private readonly IHasIssueFiltering settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIssueFilterer"/> class.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    /// <param name="settings">Settings to use.</param>
    public BasicIssueFilterer(
        ICakeLog log,
        IHasIssueFiltering settings)
    {
        log.NotNull();
        settings.NotNull();

        this.log = log;
        this.settings = settings;
    }

    /// <summary>
    /// Filters all issues which should not be logged.
    /// </summary>
    /// <param name="issues">Found issues.</param>
    /// <returns>List of filtered issues.</returns>
    public IEnumerable<IIssue> FilterIssues(IEnumerable<IIssue> issues)
    {
        issues.NotNull();

        this.log.Verbose("Filtering issues before posting...");

        var result = this.FilterIssuesByNumber(issues as IList<IIssue> ?? issues.ToList());

        // Apply custom filters.
        foreach (var filterer in this.settings.IssueFilters)
        {
            var countBefore = result.Count;

            result = filterer(result).ToList();

            var commentsFiltered = countBefore - result.Count;

            this.log.Information(
                "{0} issue(s) were filtered by custom filter.",
                commentsFiltered);
        }

        return result;
    }

    /// <summary>
    /// Limits the number of issues to not overload the build server with too many issues.
    /// </summary>
    /// <param name="issues">List of issues which should be filtered.</param>
    /// <returns>List of issues limited to the maximum number of issues to post.</returns>
    private IList<IIssue> FilterIssuesByNumber(IList<IIssue> issues)
    {
        if (!issues.Any())
        {
            return issues;
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var totalIssuesFilteredCount = 0;

        // Apply issue limits per issue provider
        var result = new List<IIssue>();
        if (this.settings.MaxIssuesToPostForEachIssueProvider.HasValue)
        {
            foreach (var group in issues.GroupBy(x => x.ProviderType))
            {
                var countBefore = group.Count();
                var issuesFiltered =
                    group
                        .SortWithDefaultPrioritization()
                        .Take(this.settings.MaxIssuesToPostForEachIssueProvider.Value)
                        .ToList();
                var issuesFilteredCount = countBefore - issuesFiltered.Count;
                totalIssuesFilteredCount += issuesFilteredCount;

                this.log.Information(
                    "{0} issue(s) of type {1} were filtered to match the maximum of {2} issues which should be reported for each issue provider",
                    issuesFilteredCount,
                    group.Key,
                    this.settings.MaxIssuesToPostForEachIssueProvider);

                result.AddRange(issuesFiltered);
            }
        }
        else
        {
            result.AddRange(issues);
        }

        // Apply global issue limit
        if (this.settings.MaxIssuesToPost.HasValue)
        {
            var countBefore = result.Count;
            result =
                result
                    .SortWithDefaultPrioritization()
                    .Take(this.settings.MaxIssuesToPost.Value)
                    .ToList();
            var issuesFilteredCount = countBefore - result.Count;
            totalIssuesFilteredCount += issuesFilteredCount;

            this.log.Information(
                "{0} issue(s) were filtered to match the global issue limit of {1}",
                issuesFilteredCount,
                this.settings.MaxIssuesToPost);
        }

        this.log.Verbose(
            "Filtering out {0} issues to match limits took {1} ms",
            totalIssuesFilteredCount,
            stopwatch.ElapsedMilliseconds);

        return result;
    }
}