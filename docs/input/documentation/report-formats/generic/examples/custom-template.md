---
title: Custom template
description: Example how to create a report using a custom template
---

!!! info
    If you create a universally usable custom template we're happy to package it with the addin.
    To have it included in the addin please [create a pull request] with your contribution.

To create custom HTML reports the Generic report format needs to be imported.
For this example the MsBuild issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting.Generic&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the Generic report format the `Cake.Issues` and `Cake.Issues.Reporting` core addins need to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.MsBuild" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.Reporting.Generic" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

=== "Cake Frosting"

    ```csharp title="Build.csproj"
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
        <ImplicitUsings>enable</ImplicitUsings>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting" Version="{{ cake_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.MsBuild" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.Reporting.Generic" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

The following example will create a HTML report for issues logged as warnings by MsBuild using a custom template.

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Create-IssueReport").Does(() =>
    {
        var repoRootFolder = new DirectoryPath(@"c:\repo");
    
        // Build MySolution.sln solution in the repository root folder
        // and write a binary log.
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
            GenericIssueReportFormatFromFilePath(
                @"c:\ReportTemplate.cshtml"),
            repoRootFolder,
            @"c:\report.html");
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Create-IssueReport").Does(() =>
    {
        var repoRootFolder = new DirectoryPath(@"c:\repo");
    
        // Build MySolution.sln solution in the repository root folder
        // and write a binary log.
        FilePath msBuildLogFile = @"c:\build\msbuild.log";
        var msBuildSettings =
            new MSBuildSettings().WithLogger(
                "BinaryLogger," + Context.Environment.ApplicationRoot.CombineWithFilePath("StructuredLogger.dll"),
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
            GenericIssueReportFormatFromFilePath(
                @"c:\ReportTemplate.cshtml"),
            repoRootFolder,
            @"c:\report.html");
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.IO;
    using Cake.Common.Tools.DotNet;
    using Cake.Common.Tools.DotNet.Build;
    using Cake.Common.Tools.DotNet.MSBuild;
    using Cake.Core.IO;
    using Cake.Frosting;

    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .Run(args);
        }
    }

    [TaskName("Create-IssueReport")]
    public sealed class CreateIssueReportTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var repoRootPath = context.MakeAbsolute(context.Directory("./"));

            // Build MySolution.sln solution in the repository root folder
            // and write a binary log.
            FilePath msBuildLogFile = @"c:\build\msbuild.log";
            var msBuildSettings =
                new DotNetMSBuildSettings().WithLogger(
                    "BinaryLogger," + context.Environment.ApplicationRoot.CombineWithFilePath("StructuredLogger.dll"),
                    "",
                    msBuildLogFile.FullPath);
            context.DotNetBuild(
                repoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
                new DotNetBuildSettings{MSBuildSettings = msBuildSettings});

            // Write issues to console.
            context.CreateIssueReport(
                context.MsBuildIssuesFromFilePath(
                    msBuildLogFile,
                    context.MsBuildBinaryLogFileFormat()),
                context.GenericIssueReportFormatFromFilePath(
                    @"c:\ReportTemplate.cshtml"),
                repoRootPath,
                @"c:\report.html");
        }
    }
    ```

The template looks like this:

```csharp title="ReportTemplate.cshtml"
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
