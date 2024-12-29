---
title: Creating reports
description: Usage instructions how to create reports.
---

To create report for issues you need to import the following core addins:

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
```

Also you need to import at least one issue provider and report format.
In the following example the issue provider for reading warnings from MsBuild log files
and generic report format is imported:

```csharp
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting.Generic&version={{ cake_issues_version }}
```

Finally you can define a task where you call the reporting addin with the desired issue provider and report format:

```csharp
Task("Create-Report").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");
    CreateIssueReport(
        MsBuildIssuesFromFilePath(
            @"C:\build\msbuild.log",
            MsBuildXmlFileLoggerFormat),
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        repoRootFolder,
        @"c:\report.html");
});
```
