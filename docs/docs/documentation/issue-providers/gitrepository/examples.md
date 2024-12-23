---
title: Examples
description: Examples for using the Cake.Issues.GitRepository addin.
---

The following example prints the number of binary files which are not tracked by
[Git Large File Storage](https://git-lfs.github.com/){target="_blank"} in a repository.

!!! warning
    Checking binary files requires Git and [Git Large File Storage](https://git-lfs.github.com/){target="_blank"}
    available on the local machine.

To analyze Git repositories you need to import the core addin and the Git repository support:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.GitRepository"
```

!!! warning
    Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
    won't break due to updates to one of the addins.

    See [pinning addin versions](https://cakebuild.net/docs/writing-builds/reproducible-builds/){target="_blank"} for details.

We need some global variables:

```csharp
var repoRootPath = @"c:\repo";
```

The following task will analyze the repository:

```csharp
Task("Analyze-Repo")
.Does(() =>
{
    // Read Issues.
    var settings =
        new GitRepositoryIssuesSettings
        {
            CheckBinaryFilesTrackedByLfs = true
        };

    var issues =
        ReadIssues(
            GitRepositoryIssues(settings),
            repoRootPath);

    Information("{0} issues are found.", issues.Count());
});
```
