---
title: Examples
description: Examples for using the Cake.Issues.InspectCode addin.
icon: material/test-tube
---

The following example will call [JetBrains InspectCode] and output the number of warnings.

To call [JetBrains InspectCode] from a Cake script you need to add the `JetBrains.ReSharper.CommandLineTools`:

```csharp
#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools" // (1)!
```

--8<-- "snippets/pinning.md"

To read issues from InspectCode log files you need to import the core addin and the InspectCode support:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
```

We need some global variables:

```csharp
var logPath = @"c:\build\inspectcode.xml";
var repoRootPath = @"c:\repo";
```

The following task will run [JetBrains InspectCode] and write a log file:

```csharp
Task("Analyze-Project").Does(() =>
{
    // Run InspectCode.
    var settings = new InspectCodeSettings() {
        OutputFile = logPath
    };

    InspectCode(repoRootPath.CombineWithFilePath("MySolution.sln"), settings);
});
```

Finally you can define a task where you call the core addin with the desired issue provider.

```csharp
Task("Read-Issues")
    .IsDependentOn("Analyze-Project")
    .Does(() =>
    {
        // Read Issues.
        var issues =
            ReadIssues(
                InspectCodeIssuesFromFilePath(logPath),
                repoRootPath);

        Information("{0} issues are found.", issues.Count());
    });
```

[JetBrains InspectCode]: https://www.jetbrains.com/help/resharper/InspectCode.html
