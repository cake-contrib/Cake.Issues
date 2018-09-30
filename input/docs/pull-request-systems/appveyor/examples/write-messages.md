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

:::{.alert .alert-warning}
Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the addins.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

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