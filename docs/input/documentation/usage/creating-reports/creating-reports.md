---
title: Creating reports
description: Usage instructions how to create reports.
---

The `Cake.Issues.Reporting` addin can be used to create issue reports in different formats.

??? Tip "List of all aliases for creating reports"
    See all available [Aliases for creating reports](https://cakebuild.net/extensions/cake-issues-reporting/#Creating-Issue-Reports){target="_blank"}

To create report for issues you need to import the corresponding report format.
In the following example the issue provider for reading warnings from MsBuild log files
and generic report format is imported:

??? tip "Example for other report formats"
    For examples for other formats see [Report Format Examples](../../report-formats/index.md).

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting.Generic&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the report format the `Cake.Issues` and `Cake.Issues.Reporting` core addins need to be added.

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
Afterwards you can define a task where you call the reporting addin with the desired issue provider and report format:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Create-Report").Does(() =>
    {
        var repoRootFolder = new DirectoryPath(@"C:\repo");
        CreateIssueReport(
            MsBuildIssuesFromFilePath(
                @"C:\build\msbuild.log",
                MsBuildBinaryLogFileFormat),
            GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
            repoRootFolder,
            @"c:\report.html");
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
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

    [TaskName("Create-Report")]
    public sealed class CreateReportTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var repoRootFolder = new DirectoryPath(@"C:\repo");
            context.CreateIssueReport(
                context.MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    context.MsBuildBinaryLogFileFormat()),
                context.GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
                repoRootFolder,
                @"c:\report.html");
        }
    }
    ```
