---
Order: 30
Title: Examples
Description: Examples for using the Cake.Issues.PullRequests.Tfs addin.
---

# Using with repository remote url and source branch name

This example shows how to write issues as comments to a Team Foundation Server (TFS) or
Visual Studio Team Services (VSTS) pull request while using repository information.

For determing the remote repository URL and source branch of the pull request you need the [Cake.Git] addin:

```csharp
#addin "Cake.Git"
```

To write issues as comments to TFS or VSTS pull requests you need to import the core addin,
the core pull request addin, the TFS/VSTS support and one or more issue provider, in this example
for JetBrains InspectCode:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.InspectCode"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.Tfs"
```

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

# Voting for pull requests

The [Cake.Issues.PullRequests.Tfs addin] also provides an alias for approving or voting for pull requests.

:::{.alert .alert-info}
The approve functionality can be used without using the [Cake.Issues addin].
:::

The following example will approve a pull request on a Team Foundation Server:

```csharp
#addin "Cake.Issues.PullRequests.Tfs"

Task("Vote-PullRequest").Does(() =>
{
    var pullRequestSettings =
        new TfsPullRequestSettings(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm());

    TfsVotePullRequest(
        pullRequestSettings,
        TfsPullRequestVote.Approved);
});
```

You can also vote based on the issues provided to the [Cake.Issues addin].

The following example will mark the pull request as waiting for author if any JetBrains InspectCode
warnings have occurred and approves the pull request otherwise:

```csharp
#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"
#addin "Cake.Issues"
#addin "Cake.Issues.Issues.InspectCode"
#addin "Cake.Issues.PullRequests.Tfs"

var logPath = @"c:\build\inspectcode.xml";
var repoRootPath = @"c:\repo";

Task("Analyze-Project").Does(() =>
{
    // Run InspectCode.
    var settings = new InspectCodeSettings() {
        OutputFile = logPath
    };

    InspectCode(repoRootPath.CombineWithFilePath("MySolution.sln"), settings);
});

Task("Vote-Pullrequest")
.IsDependentOn("Analyze-Project")
.Does(() =>
{
    // Read Issues.
    var issues = ReadIssues(
        InspectCodeIssuesFromFilePath(logPath),
        repoRootPath);

    // Vote for pull request.
    var vote = issues.Any() ? TfsPullRequestVote.WaitingForAuthor : TfsPullRequestVote.Approved;
    TfsVotePullRequest(
        pullRequestSettings,
        vote);
});
```

[TfsPullRequests]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/
[Cake.Git]: https://www.nuget.org/packages/Cake.Git/
[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Cake.Issues addin]: https://www.nuget.org/packages/Cake.Issues