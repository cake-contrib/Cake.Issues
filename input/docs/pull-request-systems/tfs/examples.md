---
Order: 30
Title: Examples
Description: Examples for using the Cake.Issues.PullRequests.Tfs addin.
---

## Voting for pull requests

The [Cake.Issues.PullRequests.Tfs addin] also provides an alias for approving or voting for pull requests.

:::{.alert .alert-info}
The approve functionality can be used without using the [Cake.Issues addin].
:::

The following example will approve a pull request on a Team Foundation Server:

```csharp
#addin "Cake.Issues.PullRequests.Tfs"

Task("vote-pullrequest").Does(() =>
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

The following example will mark the pull request as waiting for author if any MsBuild warnings have
occurred and approves the pull request otherwise:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.Issues.MsBuild"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.Tfs"

Task("ReportIssuesToPullRequest").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"c:\repo");
    var pullRequestSettings =
        new TfsPullRequestSettings(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm());

    // Write issues as comments to pull request.
    var result =
        ReportIssuesToPullRequest(
            MsBuildIssuesFromFilePath(
                @"c:\build\msbuild.log",
                @"c:\repo\docs"),
            TfsPullRequests(pullRequestSettings),
            repoRootFolder);

    // Vote for pull request.
    var vote = result.PostedIssues.Any() ? TfsPullRequestVote.WaitingForAuthor : TfsPullRequestVote.Approved;
    TfsVotePullRequest(
        pullRequestSettings,
        vote);
});
```

[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Cake.Issues addin]: https://www.nuget.org/packages/Cake.Issues