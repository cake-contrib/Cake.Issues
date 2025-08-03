---
title: Create annotations in GitHub Actions
description: Example how to write issues as annotations to a GitHub Actions build.
---

To report issues as annotations to a GitHub Actions build, the GitHub Actions addin needs to be imported.
For this example the JetBrains InspectCode issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests.GitHubActions&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the GitHub Actions pull request system the `Cake.Issues` and `Cake.Issues.PullRequests` core addins need to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.GitHubActions" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.GitHubActions" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

This example shows how to report issues as annotations to GitHubActions build using the
[GitHubActionsBuilds](https://cakebuild.net/api/Cake.Issues.PullRequests.GitHubActions/GitHubActionsBuildsAliases/){target="_blank"} alias:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("ReportIssuesToGitHubActions").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));

        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            GitHubActionsBuilds(),
            repoRootFolder);
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("ReportIssuesToGitHubActions").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));

        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            GitHubActionsBuilds(),
            repoRootFolder);
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.IO;
    using Cake.Frosting;

    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .Run(args);
        }
    }

    [TaskName("Report-IssuesToAppVeyor")]
    public sealed class ReportIssuesToAppVeyorTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var repoRootPath = context.MakeAbsolute(context.Directory("./"));

            context.ReportIssuesToPullRequest(
                context.InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log"),
                context.GitHubActionsBuilds(),
                repoRootPath);
        }
    }
    ```

The output will show up in the build log grouped by issue provider / run:

![Log output](../githubactions-log-output.png "Log output")

Additionally the issues show up as annotations:

![Annotations](../githubactions-annotations.png "Annotations")

Having issues available as annotations also means that they will be shown in pull requests on the related file / position:

![Pull request integration](../githubactions-pullrequest-integration.png "Pull request integration")
