---
title: Using with repository remote url and source branch name
description: Example how to use the Cake.Issues.PullRequests.AzureDevOps addin with repository remote url and source branch name.
---

This example shows how to write issues as comments to an Azure DevOps pull request while using repository information.

To determine the remote repository URL and source branch of the pull request you need the [Cake.Git](https://cakebuild.net/extensions/cake-git/){target="_blank"}
addin:

```csharp
#addin "Cake.Git" // (1)!
```

--8<-- "snippets/pinning.md"

To write issues as comments to Azure DevOps pull requests you need to import the core addin,
the core pull request addin, the Azure DevOps support including the Cake.AzureDevOps addin, and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests.AzureDevOps&version={{ cake_issues_version }}
#addin "Cake.AzureDevOps" // (1)!
```

--8<-- "snippets/pinning.md"

In the following task we'll first determine the remote repository URL and
source branch of the pull request and with this information call the [AzureDevOpsPullRequests](https://cakebuild.net/api/Cake.Issues.PullRequests.AzureDevOps/AzureDevOpsPullRequestSystemAliases/){target="_blank"}
alias, which will authenticate through NTLM to an on-premise Azure DevOps Server instance:

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = MakeAbsolute(Directory("./"));
    var currentBranch = GitBranchCurrent(repoRootFolder);
    var repoRemoteUrl = new Uri(currentBranch.Remotes.Single(x => x.Name == "origin").Url);
    var sourceBranchName = currentBranch.CanonicalName;

    ReportIssuesToPullRequest(
        InspectCodeIssuesFromFilePath(
            @"C:\build\inspectcode.log"),
        AzureDevOpsPullRequests(
            repoRemoteUrl,
            sourceBranchName,
            AzureDevOpsAuthenticationNtlm()),
        repoRootFolder);
});
```
