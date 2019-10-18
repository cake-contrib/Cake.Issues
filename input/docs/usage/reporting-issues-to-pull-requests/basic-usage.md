---
Order: 10
Title: Basic usage
Description: Usage instructions how to report issues to pull requests.
---
To use report issues to pull requests you need to import the following core addins:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.PullRequests"
```

Also you need to import at least one issue provider and pull request system.
In the following example the issue provider for reading warnings from MsBuild log files
and support for Azure DevOps pull requests is imported:

```csharp
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.PullRequests.AzureDevOps"
```

:::{.alert .alert-warning}
Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the addins.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

Finally you can define a task where you call the core addin with the desired issue provider and pull request system:

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");
    ReportIssuesToPullRequest(
        MsBuildIssuesFromFilePath(
            @"C:\build\msbuild.log",
            MsBuildXmlFileLoggerFormat),
        AzureDevOpsPullRequests(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            AzureDevOpsAuthenticationNtlm()),
        repoRootFolder);
});
```