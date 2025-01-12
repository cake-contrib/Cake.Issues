---
title: Examples
description: Examples for using the Cake.Issues.Reporting.Sarif addin.
icon: material/test-tube
---

To report issues to the console the SARIF report format needs to be imported.
For this example the MsBuild issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Reporting.Sarif&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the SARIF report format the `Cake.Issues` and `Cake.Issues.Reporting` core addins need to be added.

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
        <PackageReference Include="Cake.Frosting.Issues.Reporting.Sarif" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

The following example will create a SARIF report for issues logged as warnings by MsBuild.

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Create-IssueReport").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));

        // Build MySolution.sln solution in the repository root folder
        // and write a binary log.
        FilePath msBuildLogFile = @"c:\build\msbuild.log";
        var msBuildSettings =
            new DotNetMSBuildSettings().WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                "",
                msBuildLogFile.FullPath);
        DotNetBuild(
            repoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
            new DotNetBuildSettings{MSBuildSettings = msBuildSettings});
    
        // Create SARIF report.
        CreateIssueReport(
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildBinaryLogFileFormat,
            SarifIssueReportFormat(),
            repoRootPath,
            @"c:\report.sarif");
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
                    "BinaryLogger," + context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                    "",
                    msBuildLogFile.FullPath);
            context.DotNetBuild(
                repoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
                new DotNetBuildSettings{MSBuildSettings = msBuildSettings});

            // Create SARIF report.
            context.CreateIssueReport(
                context.MsBuildIssuesFromFilePath(
                    msBuildLogFile,
                    context.MsBuildBinaryLogFileFormat()),
                context.SarifIssueReportFormat(),
                repoRootPath,
                @"c:\report.sarif");
        }
    }
    ```
