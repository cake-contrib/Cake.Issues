namespace Cake.Issues.Build;

using System;
using System.Collections.Generic;
using Cake.Core.IO;

/// <summary>
/// Settings affecting how issues are reported to build servers.
/// </summary>
/// <param name="repositoryRoot">Root path of the repository.</param>
public class ReportIssuesToBuildServerSettings(DirectoryPath repositoryRoot) : RepositorySettings(repositoryRoot), IReportIssuesToBuildServerSettings
{
    private readonly List<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> issueFilters = [];

    /// <inheritdoc />
    public int? MaxIssuesToPost { get; set; }

    /// <inheritdoc />
    public int? MaxIssuesToPostForEachIssueProvider { get; set; } = 100;

    /// <inheritdoc />
    public Dictionary<string, int> ProviderIssueLimits { get; } = [];

    /// <inheritdoc />
    public IList<Func<IEnumerable<IIssue>, IEnumerable<IIssue>>> IssueFilters => this.issueFilters;
}