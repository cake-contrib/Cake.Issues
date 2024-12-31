---
title: Examples
description: Examples for using the Cake.Issues.GitRepository addin.
icon: material/test-tube
---

To analyze Git repositories you need to import the Git repository issue provider:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.GitRepository&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the Git repository issue provider the `Cake.Issues` core addin needs to be added.

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
        <PackageReference Include="Cake.Frosting.Issues.GitRepository" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

The following example prints the number of binary files which are not tracked by
[Git Large File Storage](https://git-lfs.github.com/){target="_blank"} in a repository.

!!! warning
    Checking binary files requires Git and [Git Large File Storage](https://git-lfs.github.com/){target="_blank"}
    available on the local machine.

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Analyze-Repo")
    .Does(() =>
    {
        // Read issues.
        var repoRootPath = MakeAbsolute(Directory("./"));
        var settings =
            new GitRepositoryIssuesSettings
            {
                CheckBinaryFilesTrackedByLfs = true
            };    

        var issues =
            ReadIssues(
                GitRepositoryIssues(settings),
                repoRootPath);    

        Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
    using Cake.Frosting;

    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .Run(args);
        }
    }

    [TaskName("Analyze-Repo")]
    public sealed class AnalyzeRepoTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            // Read issues.
            var repoRootPath = context.MakeAbsolute(context.Directory("./"));
            var settings =
                new GitRepositoryIssuesSettings
                {
                    CheckBinaryFilesTrackedByLfs = true
                };    
    
            var issues =
                context.ReadIssues(
                    context.GitRepositoryIssues(settings),
                    repoRootPath);    
    
            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```
