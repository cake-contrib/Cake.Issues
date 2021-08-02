---
Order: 60
Title: Tasks
Description: Tasks provided by Cake.Issues recipes.
---

Cake.Issues recipes provide the following tasks to your build script:

| Task                         | Description                                  | Cake.Issues.Recipe task instance                 | Cake.Frosting.Issues.Recipe task type                       |
|------------------------------|----------------------------------------------|--------------------------------------------------|-------------------------------------------------------------|
| `Issues`                     | Main tasks for issue management integration. | `IssuesBuildTasks.IssuesTask`                    | `Cake.Frosting.Issues.Recipe.IssuesTask`                    |
| `Read-Issues`                | Reads issues from the provided log files.    | `IssuesBuildTasks.ReadIssuesTask`                | `Cake.Frosting.Issues.Recipe.ReadIssuesTask`                |
| `Create-FullIssuesReport`    | Creates issue report.                        | `IssuesBuildTasks.CreateFullIssuesReportTask`    | `Cake.Frosting.Issues.Recipe.CreateFullIssuesReportTask`    |
| `Publish-IssuesArtifacts`    | Publish artifacts to build server.           | `IssuesBuildTasks.PublishIssuesArtifactsTask`    | `Cake.Frosting.Issues.Recipe.PublishIssuesArtifactsTask`    |
| `Report-IssuesToBuildServer` | Report issues to build server.               | `IssuesBuildTasks.ReportIssuesToBuildServerTask` | `Cake.Frosting.Issues.Recipe.ReportIssuesToBuildServerTask` |
| `Create-SummaryIssuesReport` | Creates a summary issue report.              | `IssuesBuildTasks.CreateSummaryIssuesReportTask` | `Cake.Frosting.Issues.Recipe.CreateSummaryIssuesReportTask` |
| `Report-IssuesToPullRequest` | Report issues to pull request.               | `IssuesBuildTasks.ReportIssuesToPullRequestTask` | `Cake.Frosting.Issues.Recipe.ReportIssuesToPullRequestTask` |
| `Set-PullRequestIssuesState` | Set pull request status.                     | `IssuesBuildTasks.SetPullRequestIssuesStateTask` | `Cake.Frosting.Issues.Recipe.SetPullRequestIssuesStateTask` |