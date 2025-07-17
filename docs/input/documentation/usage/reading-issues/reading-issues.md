---
title: Reading issues
description: Usage instructions how to read issues.
---

The `Cake.Issues` addin can be used to aggregate issues from different sources.
This can for example be useful to break builds based on the reported issues.

??? Tip "List of all aliases for reading issues"
    See all available [Aliases for reading issues](https://cakebuild.net/extensions/cake-issues/#Reading-Issues){target="_blank"}

To read issues you need to import at least one issue provider.
In the following example the issue providers for reading warnings from MsBuild log files
and from JetBrains InspectCode are imported:

??? tip "Example for other tools"
    For examples for issue providers for reading issues form other tools see [Issue Provider Examples](../../issue-providers/index.md).

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the issue providers the `Cake.Issues` core addin needs to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.MsBuild" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.MsBuild" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

Afterwards you can define a task where you call the core addin with the desired issue providers.
The following example reads warnings and errors reported by MsBuild from a binary log
and issues reported by JetBrains InspectCode:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Read-Issues").Does(() =>
    {
        var repoRootFolder = new DirectoryPath(@"C:\repo");
        var issues = ReadIssues(
            new List<IIssueProvider>
            {
                MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    MsBuildBinaryLogFileFormat),
                InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log")
            },
            repoRootFolder);
    
        Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Read-Issues").Does(() =>
    {
        var repoRootFolder = new DirectoryPath(@"C:\repo");
        var issues = ReadIssues(
            new List<IIssueProvider>
            {
                MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    MsBuildBinaryLogFileFormat),
                InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log")
            },
            repoRootFolder);
    
        Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
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

    [TaskName("Read-Issues")]
    public sealed class ReadIssuesTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var repoRootFolder = new DirectoryPath(@"C:\repo");
            var issues = context.ReadIssues(
                new List<IIssueProvider>
                {
                    context.MsBuildIssuesFromFilePath(
                        @"C:\build\msbuild.log",
                        context.MsBuildBinaryLogFileFormat()),
                    context.InspectCodeIssuesFromFilePath(
                        @"C:\build\inspectcode.log")
                },
                repoRootFolder);

            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```
