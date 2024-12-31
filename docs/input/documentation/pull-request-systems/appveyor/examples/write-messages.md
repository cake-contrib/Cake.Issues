---
title: Writing message to AppVeyor
description: Example how to write issues as messages to an AppVeyor build.
---

This example shows how to report issues as messages to an AppVeyor build.

To report issues as messages to an AppVeyor build you need to import the core addin,
the core pull request addin, the AppVeyor support and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.PullRequests.AppVeyor&version={{ cake_issues_version }}
```

In the following task we'll first determine the remote repository URL and
source branch of the pull request and with this information call the
[AppVeyorBuilds](https://cakebuild.net/api/Cake.Issues.PullRequests.AppVeyor/AppVeyorBuildsAliases/){target="_blank"} alias:

```csharp
Task("ReportIssuesToAppVeyor").Does(() =>
{
    var repoRootFolder = MakeAbsolute(Directory("./"));

    ReportIssuesToPullRequest(
        InspectCodeIssuesFromFilePath(
            @"C:\build\inspectcode.log"),
        AppVeyorBuilds(),
        repoRootFolder);
});
```

The output will look similar to this:

![AppVeyor messages](../appveyor-messages.png "AppVeyor messages")
