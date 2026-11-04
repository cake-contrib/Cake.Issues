namespace Cake.Issues.Shared;

using System;
using System.Collections.Generic;

/// <summary>
/// Interface for settings that support issue filtering.
/// </summary>
public interface IHasIssueFiltering
{
    /// <summary>
    /// Gets or sets the global number of issues which should be posted at maximum over all
    /// <see cref="IIssueProvider"/>.
    /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
    /// are prioritized.
    /// Default is <c>null</c> which won't set a global limit.
    /// </summary>
    int? MaxIssuesToPost { get; set; }

    /// <summary>
    /// Gets or sets the number of issues which should be posted at maximum for each
    /// <see cref="IIssueProvider"/>.
    /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
    /// are prioritized.
    /// <c>null</c> won't limit issues per issue provider.
    /// Default is to filter to 100 issues for each issue provider.
    /// </summary>
    int? MaxIssuesToPostForEachIssueProvider { get; set; }

    /// <summary>
    /// Gets list of filter functions which should be applied before posting issues.
    /// </summary>
    IList<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> IssueFilters { get; }
}