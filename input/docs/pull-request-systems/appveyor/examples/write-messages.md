---
Order: 10
Title: Writing message to AppVeyor
Description: Example how to write issues as messages to an AppVeyor build.
---
This example shows how to report issues as messages to an AppVeyor build.

To report issues as messages to an AppVeyor build you need to import the core addin,
the core pull request addin, the AppVeyor support and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.InspectCode"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.AppVeyor"
```

In the following task we'll first determine the remote repository URL and
source branch of the pull request and with this information call the [AppVeyorBuilds] alias:

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

![AppVeyor messages](appveyor-messages.png "AppVeyor messages")

[AppVeyorBuilds]: ../../../../api/Cake.Issues.PullRequests.AppVeyor/AppVeyorBuildsAliases/