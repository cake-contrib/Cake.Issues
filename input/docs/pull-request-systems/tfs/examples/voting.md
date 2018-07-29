---
Order: 20
Title: Voting for pull requests
Description: Examples how to approve or vote for pull requests using the Cake.Issues.PullRequests.Tfs addin.
---
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

[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Cake.Issues addin]: https://www.nuget.org/packages/Cake.Issues