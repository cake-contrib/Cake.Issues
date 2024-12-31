---
title: Report issues to pull requests
description: Usage instructions how to report issues to pull requests.
---

To use report issues to pull requests you need to import the following core addins:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
```

Also you need to import at least one issue provider and pull request system.
In the following example the issue provider for reading warnings from MsBuild log files
and support for Azure DevOps pull requests is imported:

```csharp
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests.AzureDevOps&version={{ cake_issues_version }}
```

Finally you can define a task where you call the core addin with the desired issue provider and pull request system:

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");
    ReportIssuesToPullRequest(
        MsBuildIssuesFromFilePath(
            @"C:\build\msbuild.log",
            MsBuildBinaryLogFileFormat),
        AzureDevOpsPullRequests(),
        repoRootFolder);
});
```
