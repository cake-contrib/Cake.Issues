---
Order: 10
Title: Basic usage
Description: Usage instructions how to create reports.
---
To create report for issues you need to import the following core addins:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.Reporting"
```

Also you need to import at least one issue provider and report format.
In the following example the issue provider for reading warnings from MsBuild log files
and generic report format is imported:

```csharp
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.Reporting.Generic"
```

Finally you can define a task where you call the reporting addin with the desired issue provider and report format:

```csharp
Task("Create-Report").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"C:\repo");
    ReportIssuesToPullRequest(
        MsBuildIssuesFromFilePath(
            @"C:\build\msbuild.log",
            MsBuildXmlFileLoggerFormat),
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        repoRootFolder,
        @"c:\report.html");
});
```