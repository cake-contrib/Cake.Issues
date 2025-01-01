---
title: Examples
description: Examples for using the Cake.Issues.DocFx addin.
icon: material/test-tube
---

To call [DocFx](https://dotnet.github.io/docfx/){target="_blank"} from a Cake script
the [Cake.DocFx](https://cakebuild.net/extensions/cake-docfx/){target="_blank"} addin can be used.
To read issues from DocFx log files the DocFx issue provider needs to be imported:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin "Cake.DocFx" // (1)!
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.DocFx&version={{ cake_issues_version }}
    ```

    --8<-- "snippets/pinning.md"

    !!! note
        In addition to the DocFx issue provider the `Cake.Issues` core addin needs to be added.

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
        <PackageReference Include="Cake.DocFx" Version="1.0.0" /> // (1)!
        <PackageReference Include="Cake.Frosting" Version="{{ cake_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.DocFx" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

    --8<-- "snippets/version-placeholder.md"

The following example contains a task which will build the [DocFx](https://dotnet.github.io/docfx/){target="_blank"}
project and write a log file and a task to read issues from the log file and write the number of warnings to the console:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    var logPath = @"c:\build\docfx.log";
    var repoRootFolder = MakeAbsolute(Directory("./"));
    var docRootPath = @"docs";

    Task("Build-Documentation").Does(() =>
    {
        // Run DocFx.
        DocFxBuild(new DocFxBuildSettings()
        {
            LogPath = logPath
        });
    });

    Task("Read-Issues")
        .IsDependentOn("Build-Documentation")
        .Does(() =>
        {
            // Read issues.
            var issues =
                ReadIssues(
                    DocFxIssuesFromFilePath(logPath, docRootPath),
                    repoRootPath);    

            Information("{0} issues are found.", issues.Count());
        });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
    using Cake.Core.IO;
    using Cake.Core;
    using Cake.DocFx;
    using Cake.DocFx.Build;
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
        public FilePath LogPath { get; } = @"c:\build\docfx.log";
        public DirectoryPath RepoRootPath { get; } =
            context.MakeAbsolute(context.Directory("./"));
        public string DocRootPath { get; } = "docs";
    }

    [TaskName("Build-Documentation")]
    public sealed class BuildDocumentationTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Run DocFx.
            context.DocFxBuild(new DocFxBuildSettings()
            {
                LogPath = context.LogPath
            });
        }
    }

    [TaskName("Read-Issues")]
    [IsDependentOn(typeof(BuildDocumentationTask))]
    public sealed class ReadIssuesTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Read issues.
            var issues =
                context.ReadIssues(
                    context.DocFxIssuesFromFilePath(
                        context.LogPath,
                        context.DocRootPath),
                    context.RepoRootPath);

            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```
