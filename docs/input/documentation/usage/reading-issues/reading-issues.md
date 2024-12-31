---
title: Reading issues
description: Usage instructions how to read issues.
---

The `Cake.Issues` addin can be used to aggregate issues from different sources.
This can for example be useful to break builds based on the reported issues.

To read issues you need to import the following core addin:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
```

Also you need to import at least one issue provider.
In the following example the issue providers for reading warnings from MsBuild log files
and from JetBrains InspectCode are imported:

```csharp
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
```

Finally you can define a task where you call the core addin with the desired issue providers.
The following example reads warnings and errors reported by MsBuild from a binary log
and issues reported by JetBrains InspectCode:

```csharp
Task("Read-Issues").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");
    var issues = ReadIssues(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                @"C:\build\msbuild.log",
                MsBuildBinaryLogFileFormat),
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log")
        },
        repoRootFolder);

    Information("{0} issues are found.", issues.Count());
});
```
