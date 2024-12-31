---
title: Examples
description: Examples for using the Cake.Issues.MsBuild addin.
icon: material/test-tube
---

To read issues from MsBuild log files you need to import the MsBuild issue provider needs to be imported:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the MsBuild issue provider the `Cake.Issues` core addin needs to be added.

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
      </ItemGroup>
    </Project>
    ```

The following example contains a task which will call MsBuild to build the solution and write a binary log file
and a task to read issues from the binary log file and write the number of warnings to the console:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    var logPath = @"c:\build\msbuild.xml";
    var repoRootPath = MakeAbsolute(Directory("./"));

    Task("Build-Solution").Does(() =>
    {
        // Build solution.
        var msBuildSettings =
            new DotNetMSBuildSettings().WithLogger(
                "BinaryLogger," + Context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                "",
                logPath.FullPath);
        DotNetBuild(
            repoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
            new DotNetBuildSettings{MSBuildSettings = msBuildSettings});
    });
    
    Task("Read-Issues")
        .IsDependentOn("Build-Solution")
        .Does(() =>
        {
            // Read issues.
            var issues =
                ReadIssues(
                    MsBuildIssuesFromFilePath(
                        logPath,
                        MsBuildBinaryLogFileFormat),
                    repoRootPath);

            Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
    using Cake.Common.IO;
    using Cake.Common.Tools.DotNet;
    using Cake.Common.Tools.DotNet.Build;
    using Cake.Core;
    using Cake.Core.IO;
    using Cake.Frosting;

    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .UseContext<BuildContext>()
                .Run(args);
        }
    }

    public class BuildContext(ICakeContext context) : FrostingContext(context)
    {
        public FilePath LogPath { get; } = @"c:\build\msbuild.xml";
        public DirectoryPath RepoRootPath { get; } =
            context.MakeAbsolute(context.Directory("./"));
    }

    [TaskName("Build-Solution")]
    public sealed class BuildSolutionTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Build solution.
            var msBuildSettings =
                new DotNetMSBuildSettings().WithLogger(
                    "BinaryLogger," + context.Tools.Resolve("Cake.Issues.MsBuild*/**/StructuredLogger.dll"),
                    "",
                    context.LogPath.FullPath);
            context.DotNetBuild(
                context.RepoRootPath.CombineWithFilePath("MySolution.sln").FullPath,
                new DotNetBuildSettings{MSBuildSettings = msBuildSettings});
        }
    }

    [TaskName("Read-Issues")]
    [IsDependentOn(typeof(BuildSolutionTask))]
    public sealed class ReadIssuesTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Read issues.
            var issues =
                context.ReadIssues(
                    context.MsBuildIssuesFromFilePath(
                        context.LogPath,
                        context.MsBuildBinaryLogFileFormat()),
                    context.RepoRootPath);

            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```

!!! Tip
    When using `MSBuildSettings.BinaryLogger` property to write a binary log, the version of the binary log format written
    depends on the version of the .NET SDK.

    To avoid the risk of breaking builds when the .NET SDK is updated and introduces a new binary log format, which is not supported
    in the used version of Cake.Issues.MsBuild, the binary logger instance shipped as part of Cake.Issues.MsBuild is
    used in the above example.
