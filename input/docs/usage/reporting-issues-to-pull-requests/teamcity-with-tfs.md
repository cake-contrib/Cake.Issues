---
Order: 20
Title: Using with TeamCity and TFS
Description: Tutorial how to use Cake.Issues with TeamCity and Team Foundation Server.
---
This page describes how to use the Cake Issues addin for builds run on TeamCity and 
with Team Foundation Server (TFS) as pull request system.

Team Foundation Server support is implemented in the [Cake.Issues.PullRequests.Tfs] addin.

In your main Cake build script run on TeamCity you need to determine the remote repository URL and
source branch of the pull request and create the [TfsPullRequests] object with this information.

You can retrieve the required information using the [Cake.Git] addin:

```csharp
#addin "Cake.Git"
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.PullRequests.Tfs"

Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = MakeAbsolute(Directory("./"));
    var currentBranch = GitBranchCurrent(repoRootFolder);
    var repoRemoteUrl = new Uri(currentBranch.Remotes.Single(x => x.Name == "origin").Url);
    var sourceBranchName = currentBranch.CanonicalName;

    ReportIssuesToPullRequest(
        MsBuildIssuesFromFilePath(
            @"C:\build\msbuild.log",
            MsBuildXmlFileLoggerFormat),
        TfsPullRequests(
            repoRemoteUrl,
            sourceBranchName,
            TfsAuthenticationNtlm()),
        repoRootFolder);
});
```

[Cake.Issues.PullRequests.Tfs]: ../../pull-request-system/tfs
[TfsPullRequests]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/
[Cake.Git]: https://www.nuget.org/packages/Cake.Git/
