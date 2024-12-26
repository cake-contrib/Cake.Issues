---
title: Writing message to AppVeyor
description: Example how to write issues as messages to an AppVeyor build.
---

This example shows how to report issues as messages to an AppVeyor build.

To report issues as messages to an AppVeyor build you need to import the core addin,
the core pull request addin, the AppVeyor support and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin "Cake.Issues" // (1)!
#addin "Cake.Issues.InspectCode"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.AppVeyor"
```

--8<-- "snippets/pinning.md"

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
