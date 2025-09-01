namespace Cake.Issues.Build;

using System.Collections.Generic;
using Cake.Issues.Shared;

/// <summary>
/// Interface for settings affecting how issues are reported to build servers.
/// </summary>
public interface IReportIssuesToBuildServerSettings : IRepositorySettings, IHasIssueFiltering
{
    /// <summary>
    /// Gets the issue limits for individual <see cref="IIssueProvider"/>.
    /// The key must be the <see cref="IIssue.ProviderType"/> of a specific provider to which the limits should be applied to.
    /// Use <see cref="IHasIssueFiltering.MaxIssuesToPostForEachIssueProvider"/> to set the same limit to all issue providers.
    /// </summary>
    Dictionary<string, int> ProviderIssueLimits { get; }
}