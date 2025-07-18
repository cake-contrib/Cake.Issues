---
title: Examples
description: Examples for using the Cake.Issues.InspectCode addin.
icon: material/test-tube
---

To read issues from InspectCode log files the InspectCode issue provider needs to be imported:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the InspectCode issue provider the `Cake.Issues` core addin needs to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

The following example contains a task which will run [JetBrains InspectCode]{target="_blank"}
and write a log file and a task to read issues from the log file and write the number of warnings to the console.
[JetBrains InspectCode] is installed using `JetBrains.ReSharper.CommandLineTools`:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #tool "nuget:?package=JetBrains.ReSharper.CommandLineTools&version={{ resharper_commandlinetool_version }}"

    var logPath = @"c:\build\inspectcode.xml";
    var repoRootFolder = MakeAbsolute(Directory("./"));

    Task("Analyze-Project").Does(() =>
    {
        // Run InspectCode and enforce XML output.
        var settings = new InspectCodeSettings() {
            OutputFile = logPath,
            ArgumentCustomization = x => x.Append("-f=xml")
        };
    
        InspectCode(repoRootPath.CombineWithFilePath("MySolution.sln"), settings);
    });
    
    Task("Read-Issues")
        .IsDependentOn("Analyze-Project")
        .Does(() =>
        {
            // Read issues.
            var issues =
                ReadIssues(
                    InspectCodeIssuesFromFilePath(logPath),
                    repoRootPath);
    
            Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    #tool "nuget:?package=JetBrains.ReSharper.CommandLineTools&version={{ resharper_commandlinetool_version }}"

    var logPath = @"c:\build\inspectcode.xml";
    var repoRootFolder = MakeAbsolute(Directory("./"));

    Task("Analyze-Project").Does(() =>
    {
        // Run InspectCode and enforce XML output.
        var settings = new InspectCodeSettings() {
            OutputFile = logPath,
            ArgumentCustomization = x => x.Append("-f=xml")
        };
    
        InspectCode(repoRootPath.CombineWithFilePath("MySolution.sln"), settings);
    });
    
    Task("Read-Issues")
        .IsDependentOn("Analyze-Project")
        .Does(() =>
        {
            // Read issues.
            var issues =
                ReadIssues(
                    InspectCodeIssuesFromFilePath(logPath),
                    repoRootPath);
    
            Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
    using Cake.Core.IO;
    using Cake.Core;
    using Cake.Frosting;
    using Cake.Common.Tools.InspectCode;

    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .UseContext<BuildContext>()
                .InstallTool(
                new Uri(
                    "nuget:?package=JetBrains.ReSharper.CommandLineTools&version={{ resharper_commandlinetool_version }}"))
                .Run(args);
        }
    }

    public class BuildContext(ICakeContext context) : FrostingContext(context)
    {
        public FilePath LogPath { get; } = @"c:\build\inspectcode.xml";
        public DirectoryPath RepoRootPath { get; } =
            context.MakeAbsolute(context.Directory("./"));
    }

    [TaskName("Analyze-Project")]
    public sealed class AnalyzeProjectTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Run InspectCode and enforce XML output.
            var settings = new InspectCodeSettings() {
                OutputFile = context.LogPath,
                ArgumentCustomization = x => x.Append("-f=xml")
            };
        
            context.InspectCode(
                context.RepoRootPath.CombineWithFilePath("MySolution.sln"),
                settings);
        }
    }

    [TaskName("Read-Issues")]
    [IsDependentOn(typeof(AnalyzeProjectTask))]
    public sealed class ReadIssuesTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Read issues.
            var issues =
                context.ReadIssues(
                    context.InspectCodeIssuesFromFilePath(context.LogPath),
                    context.RepoRootPath);
    
            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```

[JetBrains InspectCode]: https://www.jetbrains.com/help/resharper/InspectCode.html
