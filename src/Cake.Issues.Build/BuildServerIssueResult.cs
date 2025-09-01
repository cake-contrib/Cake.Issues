namespace Cake.Issues.Build;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Information about the result of the operation for reporting issues to a build server.
/// </summary>
/// <param name="reportedIssues">Issues which were reported.</param>
/// <param name="postedIssues">Issues which were posted.</param>
public class BuildServerIssueResult(
    IEnumerable<IIssue> reportedIssues,
    IEnumerable<IIssue> postedIssues)
{
    /// <summary>
    /// Gets all issues which were part of the report.
    /// This can be more than the issues posted to the build server,
    /// since there can be filtering in place which removes some issues before posting to the build server.
    /// </summary>
    public IEnumerable<IIssue> ReportedIssues { get; } = reportedIssues.ToList();

    /// <summary>
    /// Gets all issues posted to the build server.
    /// </summary>
    public IEnumerable<IIssue> PostedIssues { get; } = postedIssues.ToList();
}