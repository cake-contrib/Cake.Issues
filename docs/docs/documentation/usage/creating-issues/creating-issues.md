---
title: Creating issues
description: Usage instructions how to create issues.
---

The `Cake.Issues` addin can be used to create issues directly in the build script.
This issues can for example be used to create reports.

To create issues you need to import the following core addin:

```csharp
#addin "Cake.Issues" // (1)!
```

--8<-- "snippets/pinning.md"

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
