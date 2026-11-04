namespace Cake.Issues.PullRequests;

using System;
using System.Collections.Generic;

/// <summary>
/// Interface for settings affecting how issues are reported to pull requests.
/// </summary>
public interface IReportIssuesToPullRequestSettings : IRepositorySettings
{
    /// <summary>
    /// Gets or sets the hash of the commit for which the issues were reported.
    /// </summary>
    string CommitId { get; set; }

    /// <summary>
    /// Gets or sets the global number of issues which should be posted at maximum over all
    /// <see cref="IIssueProvider"/>.
    /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
    /// are prioritized.
    /// Default is <c>null</c> which won't set a global limit.
    /// Use <see cref="MaxIssuesToPostAcrossRuns"/> to set a limit across multiple runs.
    /// </summary>
    int? MaxIssuesToPost { get; set; }

    /// <summary>
    /// Gets or sets the global number of issues which should be posted at maximum over all
    /// <see cref="IIssueProvider"/> and across multiple runs.
    /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
    /// are prioritized.
    /// Default is <c>null</c> which won't set a limit across multiple runs.
    /// Use <see cref="MaxIssuesToPost"/> to set a limit for a single run.
    /// </summary>
    int? MaxIssuesToPostAcrossRuns { get; set; }

    /// <summary>
    /// Gets or sets the number of issues which should be posted at maximum for each
    /// <see cref="IIssueProvider"/>.
    /// Issues are filtered by <see cref="IIssue.Priority"/> and issues with an <see cref="IIssue.AffectedFileRelativePath"/>
    /// are prioritized.
    /// <c>null</c> won't limit issues per issue provider.
    /// Default is to filter to 100 issues for each issue provider.
    /// Use <see cref="ProviderIssueLimits"/> to set limits for individual issue providers.
    /// </summary>
    int? MaxIssuesToPostForEachIssueProvider { get; set; }

    /// <summary>
    /// Gets the issue limits for individual <see cref="IIssueProvider"/>.
    /// The key must be the <see cref="IIssue.ProviderType"/> of a specific provider to which the limits should be applied to.
    /// Use <see cref="MaxIssuesToPostForEachIssueProvider"/> to set the same limit to all issue providers.
    /// </summary>
    Dictionary<string, IProviderIssueLimits> ProviderIssueLimits { get; }

    /// <summary>
    /// Gets or sets a value used to decorate comments created by this add-in.
    /// Only comments with the same source will be resolved.
    /// </summary>
    string CommentSource { get; set; }

    /// <summary>
    /// Gets list of filter functions which should be applied before posting issues to pull requests.
    /// </summary>
    IList<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> IssueFilters { get; }
}
