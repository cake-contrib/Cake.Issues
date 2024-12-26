---
title: Creating reports
description: Usage instructions how to create reports.
---

To create report for issues you need to import the following core addins:

```csharp
#addin "Cake.Issues" // (1)!
#addin "Cake.Issues.Reporting"
```

--8<-- "snippets/pinning.md"

Also you need to import at least one issue provider and report format.
In the following example the issue provider for reading warnings from MsBuild log files
and generic report format is imported:

```csharp
#addin "Cake.Issues.MsBuild" // (1)!
#addin "Cake.Issues.Reporting.Generic"
```

--8<-- "snippets/pinning.md"

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
