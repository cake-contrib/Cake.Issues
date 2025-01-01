---
title: Custom template
description: Example how to create a report using a custom template
---

!!! info
    If you create a universally usable custom template we're happy to package it with the addin.
    To have it included in the addin please [create a pull request] with your contribution.

The following example will create a HTML report for issues logged as warnings by MsBuild using a custom template.

```csharp
#addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
#addin nuget:?package=Cake.Issues.Reporting.Generic&version={{ cake_issues_version }}

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

    // Create HTML report using Diagnostic template.
    CreateIssueReport(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildBinaryLogFileFormat)
        },
        GenericIssueReportFormatFromFilePath(@"c:\ReportTemplate.cshtml"),
        repoRootFolder,
        @"c:\report.html");
});
```

`ReportTemplate` looks like this:

```csharp
@model IEnumerable<Cake.Issues.IIssue>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <table>
        <thead>
            <tr>
                <th scope="col">AffectedFileRelativePath</th>
                <th scope="col">Line</th>
                <th scope="col">Message</th>
                <th scope="col">Priority</th>
                <th scope="col">Rule</th>
                <th scope="col">RuleUrl</th>
                <th scope="col">ProviderType</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var issue in Model)
            {
                <tr>
                    <td>@issue.AffectedFileRelativePath</td>
                    <td>@issue.Line</td>
                    <td>@issue.MessageText</td>
                    <td>@issue.Priority</td>
                    <td>@issue.RuleId</td>
                    <td>@issue.RuleUrl</td>
                    <td>@issue.ProviderType</td>
                </tr>
            }
        </tbody>
    </table>
</body>
</html>
```

The template retrieves an `IEnumerable<Cake.Issues.IIssue>` as model.

!!! info
    In custom templates functionality from the following assemblies are available:

    * System.dll
    * System.Core.dll
    * netstandard.dll
    * Cake.Core.dll
    * Cake.Issues.dll
    * Cake.Issues.Reporting.Generic.dll

[create a pull request]: ../../../contributing/how-to-contribute.md
