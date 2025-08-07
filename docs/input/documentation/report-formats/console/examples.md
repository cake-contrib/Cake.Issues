---
title: Examples
description: Examples for using the Cake.Issues.Reporting.Console addin.
icon: material/test-tube
---

To report issues to the console the Console report format needs to be imported.
For this example the MsBuild issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting.Console&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the Console report format the `Cake.Issues` and `Cake.Issues.Reporting` core addins need to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.MsBuild" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.Reporting.Console" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.Reporting.Console" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

## Console Output Examples

The Console report format can be configured with various settings to customize the output. Below are examples showing different configuration options and their visual impact.

### Default Settings

By default, the Console report format shows individual diagnostics for each issue:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(),
        repoRootPath,
        string.Empty);
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    context.CreateIssueReport(
        issues,
        context.ConsoleIssueReportFormat(),
        repoRootPath,
        string.Empty);
    ```

![Default Console Output](../../assets/images/console-examples/console-default.png)

### Grouped by Rule

When `GroupByRule` is set to `true`, issues with the same rule ID are grouped together:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true
            }),
        repoRootPath,
        string.Empty);
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    context.CreateIssueReport(
        issues,
        context.ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true
            }),
        repoRootPath,
        string.Empty);
    ```

![Grouped by Rule Output](../../assets/images/console-examples/console-grouped.png)

### With Summary Tables

Enable summary tables to see issue counts by provider and priority:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        repoRootPath,
        string.Empty);
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    context.CreateIssueReport(
        issues,
        context.ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        repoRootPath,
        string.Empty);
    ```

![Console Output with Summaries](../../assets/images/console-examples/console-summaries.png)

### Combined Features

Combine grouping with summary tables for the most comprehensive output:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true,
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        repoRootPath,
        string.Empty);
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    context.CreateIssueReport(
        issues,
        context.ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                GroupByRule = true,
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        repoRootPath,
        string.Empty);
    ```

![Combined Features Output](../../assets/images/console-examples/console-combined.png)

## Complete Build Example

The following example shows how to build a solution and create a console report with MsBuild issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Create-IssueReport").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));

        // Build MySolution.sln solution in the repository root folder and write a binary log.
        FilePath msBuildLogFile = @"c:\build\msbuild.log";
        var msBuildSettings =
            new DotNetMSBuildSettings().WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                "",
                msBuildLogFile.FullPath);
        DotNetBuild(
            repoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
            new DotNetBuildSettings{MSBuildSettings = msBuildSettings});

        // Write issues to console.
        CreateIssueReport(
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildBinaryLogFileFormat),
            ConsoleIssueReportFormat(
                new ConsoleIssueReportFormatSettings
                {
                    GroupByRule = true,
                    ShowProviderSummary = true,
                    ShowPrioritySummary = true
                }),
            repoRootPath,
            string.Empty);
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Create-IssueReport").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));

        // Build MySolution.sln solution in the repository root folder and write a binary log.
        FilePath msBuildLogFile = @"c:\build\msbuild.log";
        var msBuildSettings =
            new DotNetMSBuildSettings().WithLogger(
                "BinaryLogger," + Context.Environment.ApplicationRoot.CombineWithFilePath("StructuredLogger.dll"),
                "",
                msBuildLogFile.FullPath);
        DotNetBuild(
            repoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
            new DotNetBuildSettings{MSBuildSettings = msBuildSettings});

        // Write issues to console.
        CreateIssueReport(
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildBinaryLogFileFormat),
            ConsoleIssueReportFormat(
                new ConsoleIssueReportFormatSettings
                {
                    GroupByRule = true,
                    ShowProviderSummary = true,
                    ShowPrioritySummary = true
                }),
            repoRootPath,
            string.Empty);
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

            // Build MySolution.sln solution in the repository root folder and write a binary log.
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
                context.ConsoleIssueReportFormat(
                    new ConsoleIssueReportFormatSettings
                    {
                        GroupByRule = true,
                        ShowProviderSummary = true,
                        ShowPrioritySummary = true
                    }),
                repoRootPath,
                string.Empty);
        }
    }
    ```
