---
Order: 20
Title: Using with repository remote url and source branch name
Description: Example how to use the Cake.Issues.PullRequests.Tfs addin with repository remote url and source branch name.
---
This example shows how to write issues as comments to a Team Foundation Server (TFS) or
Azure DevOps pull request while using repository information.

To determine the remote repository URL and source branch of the pull request you need the [Cake.Git] addin:

```csharp
#addin "Cake.Git"
```

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

:::{.alert .alert-warning}
Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the addins.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

In the following task we'll first determine the remote repository URL and
source branch of the pull request and with this information call the [TfsPullRequests] alias:

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
        TfsPullRequests(
            repoRemoteUrl,
            sourceBranchName,
            TfsAuthenticationNtlm()),
        repoRootFolder);
});
```

[TfsPullRequests]: ../../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/8B150084
[Cake.Git]: https://www.nuget.org/packages/Cake.Git/
