---
title: Creating issues
description: Usage instructions how to create issues.
---

The `Cake.Issues` addin can be used to create issues directly in the build script.
This issues can for example be used to create reports.

To create issues you need to import the following core addin:

```csharp
#addin "Cake.Issues"
```

!!! warning
    Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
    won't break due to updates to one of the addins.

    See [pinning addin versions](https://cakebuild.net/docs/writing-builds/reproducible-builds/){target="_blank"} for details.

In the following task a new warning for the myfile.txt file on line 42 is created:

```csharp
Task("Create-Issue").Does(() =>
{
    var issue =
        NewIssue(
            "Something went wrong",
            "MyCakeScript",
            "My Cake Script")
            .WithMessageInHtmlFormat("Something went <b>wrong</b>")
            .WithMessageInMarkdownFormat("Something went **wrong**")
            .InFile("myfile.txt", 42)
            .WithPriority(IssuePriority.Warning)
            .Create();

    Information("Issue created with message: {0}", issues.MessageText);
});
```
