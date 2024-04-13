namespace Cake.Issues.PullRequests.GitHubActions
{
    /// <summary>
    /// Settings for <see cref="GitHubActionsBuildsAliases"/>.
    /// </summary>
    public class GitHubActionsBuildSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether issues should be grouped by issue provider and run information.
        /// Enabled by default.
        /// </summary>
        public bool GroupIssues { get; set; } = true;
    }
}