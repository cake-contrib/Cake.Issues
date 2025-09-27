namespace Cake.Issues.Build;

using System.Collections.Generic;

/// <summary>
/// Interface describing a build server system.
/// </summary>
public interface IBuildServerSystem : IBaseIssueComponent<IReportIssuesToBuildServerSettings>
{
    /// <summary>
    /// Posts issues to the build server.
    /// </summary>
    /// <param name="issues">Issues to post.</param>
    void PostIssues(IEnumerable<IIssue> issues);
}