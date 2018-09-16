---
Order: 10
Title: Using with pull request id
Description: Example how to use the Cake.Issues.PullRequests.Tfs addin with pull request id.
---
This example shows how to write issues as comments to a Team Foundation Server (TFS) or
Azure DevOps pull request while using pull request id.

To write issues as comments to TFS or Azure DevOps pull requests you need to import the core addin,
the core pull request addin, the TFS/Azure DevOps support including the Cake TFS addin, and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.InspectCode"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.Tfs"
#addin "Cake.Tfs"
```

In the following task we'll first determine the remote repository URL and
source branch of the pull request and with this information call the [TfsPullRequests] alias:

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = MakeAbsolute(Directory("./"));

    ReportIssuesToPullRequest(
        InspectCodeIssuesFromFilePath(
            @"C:\build\inspectcode.log"),
        TfsPullRequests(
            repoRemoteUrl,
            pullRequestId,
            TfsAuthenticationNtlm()),
        repoRootFolder);
});
```

[TfsPullRequests]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/