---
Order: 30
Title: Examples
Description: Examples for using the CakE.Issues.DocFx addin.
---
The following example will call [DocFx] to generate the documentation and report any warnings from
the build as comments to the Team Foundation Server pull request.

To call [DocFx] from a Cake script you can use the [Cake.DocFx] addin.

```csharp
#addin "Cake.DocFx"
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.PullRequests"
#addin "Cake.PullRequests.Tfs"

Task("Build-Documentation").Does(() =>
{
    // Run DocFx.
    var logPath = @"c:\build\docfx.log";
    DocFxBuild(new DocFxBuildSettings()
    {
        LogPath = logPath
    });

    // Report issues to pull request.
    var repoRootFolder = new DirectoryPath(@"c:\repo");
    ReportIssuesToPullRequest(
        DocFxIssuesFromFilePath(
            logPath,
            @"c:\repo\docs"),
        TfsPullRequests(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm()),
        repoRootFolder);
});
```

[DocFx]: https://dotnet.github.io/docfx/
[Cake.DocFx]: https://www.nuget.org/packages/Cake.DocFx/