---
title: Examples
description: Examples for using the Cake.Issues.GitRepository addin.
icon: material/test-tube
---

The following example prints the number of binary files which are not tracked by
[Git Large File Storage](https://git-lfs.github.com/){target="_blank"} in a repository.

!!! warning
    Checking binary files requires Git and [Git Large File Storage](https://git-lfs.github.com/){target="_blank"}
    available on the local machine.

To analyze Git repositories you need to import the core addin and the Git repository support:

```csharp
#addin "Cake.Issues" // (1)!
#addin "Cake.Issues.GitRepository"
```

--8<-- "snippets/pinning.md"

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
