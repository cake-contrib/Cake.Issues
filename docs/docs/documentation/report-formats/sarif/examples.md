---
title: Examples
description: Examples for using the Cake.Issues.Reporting.Sarif addin.
icon: material/test-tube
---

The following example will create a SARIF report for issues logged as warnings by MsBuild.

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack" // (1)!
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting.Sarif&version={{ cake_issues_version }}

Task("Create-IssueReport").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"c:\repo");

    // Build MySolution.sln solution in the repository root folder and log issues
    // using XmlFileLogger from MSBuild Extension Pack.
    FilePath msBuildLogFile = @"c:\build\msbuild.log";
    var settings = new MsBuildSettings()
        .WithLogger(
            Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
            "XmlFileLogger",
            string.Format(
                "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
                msBuildLogFile)
        );
    MSBuild(repoRootFolder.CombineWithFilePath("MySolution.sln"), settings);

    // Create SARIF report.
    CreateIssueReport(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildXmlFileLoggerFormat)
        },
        SarifIssueReportFormat(),
        repoRootFolder,
        @"c:\report.sarif");
});
```

--8<-- "snippets/pinning.md"
