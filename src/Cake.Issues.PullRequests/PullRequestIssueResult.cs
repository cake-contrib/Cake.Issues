namespace Cake.Issues.PullRequests;

using System.Collections.Generic;

/// <summary>
/// Result from reporting issues to a pull request.
/// </summary>
public class PullRequestIssueResult
{
    private readonly List<IIssue> reportedIssues = [];
    private readonly List<IIssue> postedIssues = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="PullRequestIssueResult"/> class.
    /// </summary>
    /// <param name="reportedIssues">Issues reported by the issue providers.</param>
    /// <param name="postedIssues">Issues posted to the pull request.</param>
    public PullRequestIssueResult(
        IEnumerable<IIssue> reportedIssues,
        IEnumerable<IIssue> postedIssues)
    {
        reportedIssues.NotNull();
        postedIssues.NotNull();

        this.reportedIssues.AddRange(reportedIssues);
        this.postedIssues.AddRange(postedIssues);
    }

    /// <summary>
    /// Gets all issues reported by the issue providers.
    /// </summary>
    public IEnumerable<IIssue> ReportedIssues => this.reportedIssues.AsReadOnly();

    /// <summary>
    /// Gets the issues posted to the pull request.
    /// </summary>
    public IEnumerable<IIssue> PostedIssues => this.postedIssues.AsReadOnly();
}
