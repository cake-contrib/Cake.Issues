---
Order: 30
Title: Examples
Description: Examples for using the Cake.Issues.DocFx addin.
---
The following example will call [DocFx] to generate the documentation and outputs the number of warnings.

To call [DocFx] from a Cake script you can use the [Cake.DocFx] addin.

```csharp
#addin "Cake.DocFx"
```

To read issues from DocFx log files you need to import the core addin and the DocFx support:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.DocFx"
```

:::{.alert .alert-warning}
Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the addins.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

We need some global variables:

```csharp
var logPath = @"c:\build\docfx.log";
var repoRootPath = @"c:\repo";
var docRootPath = @"docs";
```

The following task will build the [DocFx] project and write a log file:

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
Task("Analyze-Log")
.IsDependentOn("Build-Documentation")
.Does(() =>
{
    // Read Issues.
    var issues = ReadIssues(
        DocFxIssuesFromFilePath(logPath, docRootPath),
        repoRootPath);

    Information("{0} issues are found.", issues.Count());
});
```

[DocFx]: http://dotnet.github.io/docfx/
[Cake.DocFx]: https://www.nuget.org/packages/Cake.DocFx/