---
title: Examples
description: Examples for using the Cake.Issues.Reporting.Sarif addin.
icon: material/test-tube
---

The following example will create a SARIF report for issues logged as warnings by MsBuild.

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting.Sarif&version={{ cake_issues_version }}

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

    // Create SARIF report.
    CreateIssueReport(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildBinaryLogFileFormat)
        },
        SarifIssueReportFormat(),
        repoRootFolder,
        @"c:\report.sarif");
});
```
