---
title: Tasks
description: Tasks provided by Cake.Issues recipes.
---

Cake.Issues recipes provide the following tasks to your build script:

=== "Cake.Issues.Recipe"

    | Task                         | Description                                  | IssuesBuildTasks property       |
    |------------------------------|----------------------------------------------|---------------------------------|
    | `Issues`                     | Main tasks for issue management integration. | `IssuesTask`                    |
    | `Read-Issues`                | Reads issues from the provided log files.    | `ReadIssuesTask`                |
    | `Create-FullIssuesReport`    | Creates issue report.                        | `CreateFullIssuesReportTask`    |
    | `Publish-IssuesArtifacts`    | Publish artifacts to build server.           | `PublishIssuesArtifactsTask`    |
    | `Report-IssuesToBuildServer` | Report issues to build server.               | `ReportIssuesToBuildServerTask` |
    | `Create-SummaryIssuesReport` | Creates a summary issue report.              | `CreateSummaryIssuesReportTask` |
    | `Report-IssuesToPullRequest` | Report issues to pull request.               | `ReportIssuesToPullRequestTask` |
    | `Set-PullRequestIssuesState` | Set pull request status.                     | `SetPullRequestIssuesStateTask` |
    | `Report-IssuesToConsole`     | Report issues to console.                    | `ReportIssuesToConsoleTask`     |

=== "Cake.Frosting.Issues.Recipe"

    | Task                         | Description                                  | Task type                                                                                  |
    |------------------------------|----------------------------------------------|--------------------------------------------------------------------------------------------|
    | `Issues`                     | Main tasks for issue management integration. | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`IssuesTask`                    |
    | `Read-Issues`                | Reads issues from the provided log files.    | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReadIssuesTask`                |
    | `Create-FullIssuesReport`    | Creates issue report.                        | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`CreateFullIssuesReportTask`    |
    | `Publish-IssuesArtifacts`    | Publish artifacts to build server.           | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`PublishIssuesArtifactsTask`    |
    | `Report-IssuesToBuildServer` | Report issues to build server.               | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToBuildServerTask` |
    | `Create-SummaryIssuesReport` | Creates a summary issue report.              | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`CreateSummaryIssuesReportTask` |
    | `Report-IssuesToPullRequest` | Report issues to pull request.               | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToPullRequestTask` |
    | `Set-PullRequestIssuesState` | Set pull request status.                     | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`SetPullRequestIssuesStateTask` |
    | `Report-IssuesToConsole`     | Report issues to console.                    | `Cake.Frosting.Issues.Recipe.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportIssuesToConsoleTask`     |
