---
Order: 60
Title: Tasks
Description: Tasks provided by Cake.Issues recipes.
---

Cake.Issues recipes provide the following tasks to your build script:

| Task                         | Description                                  | Cake.Issues.Recipe task instance                                                | Cake.Frosting.Issues.Recipe task type                                                      |
|------------------------------|----------------------------------------------|---------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| `Issues`                     | Main tasks for issue management integration. | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`IssuesTask`                    | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`IssuesTask`                    |
| `Read-Issues`                | Reads issues from the provided log files.    | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReadIssuesTask`                | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReadIssuesTask`                |
| `Create-FullIssuesReport`    | Creates issue report.                        | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`CreateFullIssuesReportTask`    | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`CreateFullIssuesReportTask`    |
| `Publish-IssuesArtifacts`    | Publish artifacts to build server.           | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`PublishIssuesArtifactsTask`    | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`PublishIssuesArtifactsTask`    |
| `Report-IssuesToBuildServer` | Report issues to build server.               | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToBuildServerTask` | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToBuildServerTask` |
| `Create-SummaryIssuesReport` | Creates a summary issue report.              | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`CreateSummaryIssuesReportTask` | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`CreateSummaryIssuesReportTask` |
| `Report-IssuesToPullRequest` | Report issues to pull request.               | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToPullRequestTask` | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToPullRequestTask` |
| `Set-PullRequestIssuesState` | Set pull request status.                     | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`SetPullRequestIssuesStateTask` | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`SetPullRequestIssuesStateTask` |
| `Report-IssuesToConsole`     | Report issues to console.                    | `IssuesBuildTasks.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToConsoleTask`     | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToConsoleTask`     |