---
Order: 30
Title: Examples
Description: Examples for using the Cake.Issues.MsBuild addin.
---
The following example will call MsBuild to build the solution and outputs the number of warnings.

To read issues from MsBuild log files you need to import the core addin and the MsBuild support:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
```

In this example the log file is written by the `XmlFileLogger` class from [MSBuild Extension Pack].
In order to use the above logger, the following line will download and install the tool from NuGet.org:

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack"
```

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
                string.Format(
                    "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
                logPath)
    );

    MSBuild(repoRootPath.CombineWithFilePath("MySolution.sln"), settings);
});
```

Finally you can define a task where you call the core addin with the desired issue provider.
The following example reads issues reported as MsBuild warnings by the `XmlFileLogger`
class from [MSBuild Extension Pack]:

```csharp
Task("Analyze-Log")
.IsDependentOn("Build-Solution")
.Does(() =>
{
    // Read Issues.
    var issues = ReadIssues(
        MsBuildIssuesFromFilePath(
            logPath,
            MsBuildXmlFileLoggerFormat),
        repoRootFolder);

    Information("{0} issues are found.", issues.Count());
});
```

[MSBuild Extension Pack]: http://www.msbuildextensionpack.com/