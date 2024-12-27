---
title: Examples
description: Examples for using the Cake.Issues.MsBuild addin.
icon: material/test-tube
---

The following example will call MsBuild to build the solution and outputs the number of warnings.

To read issues from MsBuild log files you need to import the core addin and the MsBuild support:

```csharp
#addin "Cake.Issues" // (1)!
#addin "Cake.Issues.MsBuild"
```

--8<-- "snippets/pinning.md"

In this example the log file is written by the `XmlFileLogger` class from [MSBuild Extension Pack].
In order to use the above logger, the following line will download and install the tool from NuGet.org:

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack" // (1)!
```

--8<-- "snippets/pinning.md"

We need some global variables:

```csharp
var logPath = @"c:\build\msbuild.log";
var repoRootPath = @"c:\repo";
```

The following task will build the solution and write a log file:

```csharp
Task("Build-Solution").Does(() =>
{
    // Build solution.
    var settings =
        new MSBuildSettings()
            .WithLogger(
                Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
                "XmlFileLogger",
                $"logfile=\"{logPath}\";verbosity=Detailed;encoding=UTF-8"
    );

    MSBuild(repoRootPath.CombineWithFilePath("MySolution.sln"), settings);
});
```

Finally you can define a task where you call the core addin with the desired issue provider.
The following example reads issues reported as MsBuild warnings by the `XmlFileLogger`
class from [MSBuild Extension Pack]:

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
                    MsBuildXmlFileLoggerFormat),
                repoRootFolder);

        Information("{0} issues are found.", issues.Count());
    });
```

[MSBuild Extension Pack]: https://github.com/mikefourie-zz/MSBuildExtensionPack
