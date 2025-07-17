---
title: Report issues to pull requests
description: Usage instructions how to report issues to pull requests.
---

The `Cake.Issues.PullRequests` addin can be used to report issues to build servers and pull request systems.

??? Tip "List of all aliases for reporting issues"
    See all available [Aliases for reporting issues](https://cakebuild.net/extensions/cake-issues-pullrequests/#Reporting-Issues-To-Pull-Requests){target="_blank"}

To use report issues to pull requests you need to import the corresponding pull request system addin.
In the following example the issue provider for reading warnings from MsBuild log files
and support for Azure DevOps pull requests is imported:

??? tip "Example for other build server and pull request systems"
    For examples for other build server and pull request systems see [Pull Request System Examples](../../pull-request-systems/index.md).

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests.AzureDevOps&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the pull request system the `Cake.Issues` and `Cake.Issues.PullRequests` core addins need to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.MsBuild" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.AzureDevOps" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.AzureDevOps" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

Afterwards you can define a task where you call the core addin with the desired issue provider and pull request system:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
        Task("ReportIssuesToPullRequest").Does(() =>
        {
            var repoRootFolder = new DirectoryPath(@"C:\repo");
            ReportIssuesToPullRequest(
                MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    MsBuildBinaryLogFileFormat),
                AzureDevOpsPullRequests(),
                repoRootFolder);
        });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
        Task("ReportIssuesToPullRequest").Does(() =>
        {
            var repoRootFolder = new DirectoryPath(@"C:\repo");
            ReportIssuesToPullRequest(
                MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    MsBuildBinaryLogFileFormat),
                AzureDevOpsPullRequests(),
                repoRootFolder);
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

    [TaskName("ReportIssuesToPullRequest")]
    public sealed class ReportIssuesToPullRequestTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var repoRootFolder = new DirectoryPath(@"C:\repo");
            context.ReportIssuesToPullRequest(
                context.MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    context.MsBuildBinaryLogFileFormat()),
                context.AzureDevOpsPullRequests(),
                repoRootFolder);
        }
    }
    ```

