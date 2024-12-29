---
title: Examples
description: Examples for using the Cake.Issues.MsBuild addin.
icon: material/test-tube
---

The following example will call MsBuild to build the solution and outputs the number of warnings.

To read issues from MsBuild log files you need to import the core addin and the MsBuild support:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
```

We need some global variables:

```csharp
var logPath = @"c:\build\msbuild.log";
var repoRootPath = @"c:\repo";
```

The following task will build the solution and write a binary log file:

```csharp
Task("Build-Solution").Does(() =>
{
    var msBuildSettings =
        new MSBuildSettings().WithLogger(
            "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
            "",
            logPath)
    DotNetBuild(
        repoRootPath.CombineWithFilePath("MySolution.sln"),
        new DotNetBuildSettings{MSBuildSettings = msBuildSettings});
});
```

!!! Tip
    When using `MSBuildSettings.BinaryLogger` property to write a binary log, the version of the binary log format written
    depends on the version of the .NET SDK.

    To avoid the risk of breaking builds when the .NET SDK is updated and introduces a new binary log format, which is not supported
    in the used version of Cake.Issues.MsBuild, the binary logger instance shipped as part of Cake.Issues.MsBuild is
    used in the above example.

Finally you can define a task where you call the core addin with the desired issue provider.
The following example reads warnings and errors reported as MsBuild in a binary log:

```csharp
Task("Read-Issues")
    .IsDependentOn("Build-Solution")
    .Does(() =>
    {
        // Read Issues.
        var issues =
            ReadIssues(
                MsBuildIssuesFromFilePath(
                    logPath,
                    MsBuildBinaryLogFileFormat),
                repoRootFolder);

        Information("{0} issues are found.", issues.Count());
    });
```
