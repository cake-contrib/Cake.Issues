---
title: Using with Azure Pipelines
description: Example how to use the Cake.Issues.PullRequests.AzureDevOps addin from an Azure Pipelines build.
---

This example shows how to write issues as comments to an Azure DevOps pull request from an Azure Pipelines build.

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

The following task will call the [AzureDevOpsPullRequests](https://cakebuild.net/api/Cake.Issues.PullRequests.AzureDevOps/AzureDevOpsPullRequestSystemAliases/){target="_blank"}
alias to connect to the pull request using the environment variables provided by Azure Pipelines.

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    ReportIssuesToPullRequest(
        InspectCodeIssuesFromFilePath(
            @"C:\build\inspectcode.log"),
        AzureDevOpsPullRequests(),
        repoRootFolder);
});
```

!!! info
    Please note that you'll need to setup your Azure Pipelines build to
    [Allow scripts to access the OAuth token](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/options#allow-scripts-to-access-the-oauth-token){target="_blank"}
    and need to setup proper permissions.

    See [OAuth authentication from Azure Pipelines] for details.

[OAuth authentication from Azure Pipelines]: ../setup.md#oauth-authentication-from-azure-pipelines
