---
title: Create annotations in GitHub Actions
description: Example how to write issues as annotations to a GitHub Actions build.
---

This example shows how to report issues as annotations to a GitHub Actions build.

To report issues as annotations to a GitHub Actions build you need to import the core addin,
the core pull request addin, the GitHub Actions support and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests.GitHubActions&version={{ cake_issues_version }}
```

In the following task we'll first determine the remote repository URL and
source branch of the pull request and with this information call the
[GitHubActionsBuilds](https://cakebuild.net/api/Cake.Issues.PullRequests.GitHubActions/GitHubActionsBuildsAliases/){target="_blank"} alias:

```csharp
Task("ReportIssuesToGitHubActions").Does(() =>
{
    var repoRootFolder = MakeAbsolute(Directory("./"));

    ReportIssuesToPullRequest(
        InspectCodeIssuesFromFilePath(
            @"C:\build\inspectcode.log"),
        GitHubActionsBuilds(),
        repoRootFolder);
});
```

The output will show up in the build log grouped by issue provider / run:

![Log output](../githubactions-log-output.png "Log output")

Additionally the issues show up as annotations:

![Annotations](../githubactions-annotations.png "Annotations")

Having issues available as annotations also means that they will be shown in pull requests on the related file / position:

![Pull request integration](../githubactions-pullrequest-integration.png "Pull request integration")
