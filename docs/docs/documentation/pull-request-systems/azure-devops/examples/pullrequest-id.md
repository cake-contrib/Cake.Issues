---
title: Using with pull request id
description: Example how to use the Cake.Issues.PullRequests.AzureDevOps addin with pull request id.
---

This example shows how to write issues as comments to an Azure DevOps pull request while using pull request id.

To determine the remote repository URL you need the [Cake.Git](https://cakebuild.net/extensions/cake-git/){target="_blank"} addin:

```csharp
#addin "Cake.Git"
```

To write issues as comments to Azure DevOps pull requests you need to import the core addin,
the core pull request addin, the Azure DevOps support including the Cake.AzureDevOps addin, and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin "Cake.Issues" // (1)!
#addin "Cake.Issues.InspectCode"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.AzureDevOps"
#addin "Cake.AzureDevOps"
```

--8<-- "snippets/pinning.md"

In the following task we'll first determine the remote repository URL and
with this information call the [AzureDevOpsPullRequests](https://cakebuild.net/api/Cake.Issues.PullRequests.AzureDevOps/AzureDevOpsPullRequestSystemAliases/){target="_blank"}
alias, which will authenticate through NTLM to an on-premise Azure DevOps Server instance:

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = MakeAbsolute(Directory("./"));
    var repoRemoteUrl = new Uri(currentBranch.Remotes.Single(x => x.Name == "origin").Url);
    var pullRequestId = 123;

    ReportIssuesToPullRequest(
        InspectCodeIssuesFromFilePath(
            @"C:\build\inspectcode.log"),
        AzureDevOpsPullRequests(
            repoRemoteUrl,
            pullRequestId,
            AzureDevOpsAuthenticationNtlm()),
        repoRootFolder);
});
```
