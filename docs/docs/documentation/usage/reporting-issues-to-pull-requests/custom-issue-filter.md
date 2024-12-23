---
title: Custom issue filter
description: Usage instructions how to apply custom filters to issues.
---

You can define custom filters which are applied to issues before they are posted as comments to pull requests.

!!! info
    You can use a custom filter to only have issues introduced with the current code posted to the pull request.

    For this you need to store your log files as artifacts on your build system, then you can define a custom filter
    which retrieves the logs from the previous build, parses them using the appropriate issue provider and filters
    out any issues which were already existing in the previous build.

The following example will filter out all issues from the rule `CA1000` from being posted to the pull request.

!!! warning
    Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
    won't break due to updates to one of the addins.

    See [pinning addin versions](https://cakebuild.net/docs/writing-builds/reproducible-builds/){target="_blank"} for details.

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.AzureDevOps"

Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");

    var settings = new ReportIssuesToPullRequestSettings(repoRootFolder);

    // Add custom filter.
    settings.IssueFilters.Add(x => x.Where(issue => issue.Rule != "CA1000"));

    ReportIssuesToPullRequest(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                @"C:\build\msbuild.log",
                MsBuildXmlFileLoggerFormat)
        },
        AzureDevOpsPullRequests(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            AzureDevOpsAuthenticationNtlm()),
        settings));
});
```
