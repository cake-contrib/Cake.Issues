---
Order: 20
Title: Custom issue filter
Description: Usage instructions how to apply custom filters to issues.
---
You can define custom filters which are applied to issues before they are posted as comments to pull requests.

:::{.alert .alert-info}
You can use a custom filter to only have issues introduced with the current code posted to the pull request.

For this you need to store your log files as artifacts on your build system, then you can define a custom filter
which retrieves the logs from the previous build, parses them using the appropriate issue provider and filters
out any issues which were already existing in the previous build.
:::

The following example will filter out all issues from the rule `CA1000` from being posted to the pull request:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.Tfs"

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
        TfsPullRequests(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm()),
        settings));
});
```