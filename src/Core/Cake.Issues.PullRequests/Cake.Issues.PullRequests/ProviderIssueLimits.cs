namespace Cake.Issues.PullRequests;

/// <inheritdoc />
/// <param name="maxIssuesToPost">Maximum amount of issues to be posted in a single run.</param>
/// <param name="maxIssuesToPostAcrossRuns">Maximum amount of issues to be posted across all runs.</param>
public class ProviderIssueLimits(
    int? maxIssuesToPost = null,
    int? maxIssuesToPostAcrossRuns = null) : IProviderIssueLimits
{
    /// <inheritdoc />
    public int? MaxIssuesToPost { get; set; } = maxIssuesToPost;

    /// <inheritdoc />
    public int? MaxIssuesToPostAcrossRuns { get; set; } = maxIssuesToPostAcrossRuns;
}
