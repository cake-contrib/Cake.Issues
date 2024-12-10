---
Order: 10
Title: Reading issues
Description: Usage instructions how to read issues.
---
The `Cake.Issues` addin can be used to aggregate issues from different sources.
This can for example be useful to break builds based on the reported issues.

To read issues you need to import the following core addin:

```csharp
#addin "Cake.Issues"
```

Also you need to import at least one issue provider.
In the following example the issue providers for reading warnings from MsBuild log files
and from JetBrains InspectCode are imported:

```csharp
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.InspectCode"
```

:::{.alert .alert-warning}
Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the addins.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

Finally you can define a task where you call the core addin with the desired issue providers.
The following example reads issues reported as MsBuild warnings by the `XmlFileLogger`
class from [MSBuild Extension Pack] and issues reported by JetBrains InspectCode:

```csharp
Task("Read-Issues").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");
    var issues = ReadIssues(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                @"C:\build\msbuild.log",
                MsBuildXmlFileLoggerFormat),
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log")
        },
        repoRootFolder);

    Information("{0} issues are found.", issues.Count());
});
```

[MSBuild Extension Pack]: http://www.msbuildextensionpack.com/