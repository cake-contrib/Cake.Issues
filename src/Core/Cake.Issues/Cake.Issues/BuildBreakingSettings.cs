namespace Cake.Issues;

using System.Collections.Generic;

/// <summary>
/// Settings for build breaking aliases.
/// </summary>
public class BuildBreakingSettings
{
    /// <summary>
    /// Gets or sets the minimum minimum priority of issues which should be considered.
    /// If set to <see cref="IssuePriority.Undefined"/>, all issues are considered.
    /// Default value is <see cref="IssuePriority.Undefined"/>.
    /// </summary>
    public IssuePriority MinimumPriority { get; set; } = IssuePriority.Undefined;

    /// <summary>
    /// Gets or sets the issue provider types to consider.
    /// If empty, all providers are considered.
    /// Default value is empty.
    /// </summary>
    public IList<string> IssueProvidersToConsider { get; set; } = [];

    /// <summary>
    /// Gets or sets the issue provider types to ignore.
    /// Default value is empty.
    /// </summary>
    public IList<string> IssueProvidersToIgnore { get; set; } = [];
}
