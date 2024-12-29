---
title: Examples
description: Examples for using the Cake.Issues.DocFx addin.
icon: material/test-tube
---

The following example will call [DocFx](https://dotnet.github.io/docfx/){target="_blank"} to
generate the documentation and outputs the number of warnings.

To call [DocFx](https://dotnet.github.io/docfx/){target="_blank"} from a Cake script you can
use the [Cake.DocFx](https://cakebuild.net/extensions/cake-docfx/){target="_blank"} addin.

```csharp
#addin "Cake.DocFx" // (1)!
```

--8<-- "snippets/pinning.md"

To read issues from DocFx log files you need to import the core addin and the DocFx support:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.DocFx&version={{ cake_issues_version }}
```

We need some global variables:

```csharp
var logPath = @"c:\build\docfx.log";
var repoRootPath = @"c:\repo";
var docRootPath = @"docs";
```

The following task will build the [DocFx](https://dotnet.github.io/docfx/){target="_blank"} project and write a log file:

```csharp
Task("Build-Documentation").Does(() =>
{
    // Run DocFx.
    DocFxBuild(new DocFxBuildSettings()
    {
        LogPath = logPath
    });
});
```

Finally you can define a task where you call the core addin with the desired issue provider.

```csharp
Task("Read-Issues")
    .IsDependentOn("Build-Documentation")
    .Does(() =>
    {
        // Read Issues.
        var issues =
            ReadIssues(
                DocFxIssuesFromFilePath(logPath, docRootPath),
                repoRootPath);

        Information("{0} issues are found.", issues.Count());
    });
```
