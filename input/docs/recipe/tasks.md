---
Order: 60
Title: Tasks
Description: Tasks provided by Cake.Issues.Recipe.
---

Cake.Issues.Recipe provides the following tasks to your build script:

| Task                         | Description                                  | Task instance                                    |
|------------------------------|----------------------------------------------|--------------------------------------------------|
| `Issues`                     | Main tasks for issue management integration. | `IssuesBuildTasks.IssuesTask`                    |
| `Read-Issues`                | Reads issues from the provided log files.    | `IssuesBuildTasks.ReadIssuesTask`                |
| `Create-FullIssuesReport`    | Creates issue report.                        | `IssuesBuildTasks.CreateFullIssuesReportTask`    |
| `Publish-IssuesArtifacts`    | Publish artifacts to build server.           | `IssuesBuildTasks.PublishIssuesArtifactsTask`    |
| `Create-SummaryIssuesReport` | Creates a summary issue report.              | `IssuesBuildTasks.CreateSummaryIssuesReportTask` |
| `Report-IssuesToPullRequest` | Report issues to pull request.               | `IssuesBuildTasks.ReportIssuesToPullRequestTask` |
| `Set-PullRequestIssuesState` | Set pull request status.                     | `IssuesBuildTasks.SetPullRequestIssuesStateTask` |