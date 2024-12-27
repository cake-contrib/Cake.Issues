---
title: Breaking builds
description: Usage instructions how to break builds.
---

The `Cake.Issues` addin can be used to break builds if specific issues were reported.

To break builds you need to import the following core addin:

```csharp
#addin "Cake.Issues" // (1)!
```

--8<-- "snippets/pinning.md"

The following task will fail the build if any issues were added to the `issues` global variable:

```csharp
// Global issues list into which issues need to be added.
IEnumerable<IIssue> issues = null;

Task("BreakBuildOnIssues")
    .Description("Breaks build if any issues in the code are found.")
    .Does(() =>
{
    BreakBuildOnIssues(issues);
});
```
