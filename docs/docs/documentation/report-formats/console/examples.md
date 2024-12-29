---
title: Examples
description: Examples for using the Cake.Issues.Reporting.Console addin.
icon: material/test-tube
---

The following example will print issues logged as warnings by MsBuild to the console.

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting.Console&version={{ cake_issues_version }}

Task("Create-IssueReport").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"c:\repo");

    // Build MySolution.sln solution in the repository root folder and write a binary log.
    FilePath msBuildLogFile = @"c:\build\msbuild.log";
    var msBuildSettings =
        new MSBuildSettings().WithLogger(
            "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
            "",
            msBuildLogFile)
    DotNetBuild(
        repoRootPath.CombineWithFilePath("MySolution.sln"),
        new DotNetBuildSettings{MSBuildSettings = msBuildSettings});

    // Write issues to console.
    CreateIssueReport(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildBinaryLogFileFormat)
        },
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true,
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        repoRootFolder,
        string.Empty);
});
```
