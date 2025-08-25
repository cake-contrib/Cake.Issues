---
title: Writing message to AppVeyor
description: Example how to write issues as messages to an AppVeyor build.
---

To report issues as messages to an AppVeyor build, the AppVeyor addin needs to be imported.
For this example the JetBrains InspectCode issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests.AppVeyor&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the AppVeyor pull request system the `Cake.Issues` and `Cake.Issues.PullRequests` core addins need to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.AppVeyor" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.AppVeyor" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

This example shows how to report issues as messages to an AppVeyor build using the
[AppVeyorBuilds](https://cakebuild.net/api/Cake.Issues.PullRequests.AppVeyor/AppVeyorBuildsAliases/){target="_blank"} alias:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Report-IssuesToAppVeyor").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));
    
        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            AppVeyorBuilds(),
            repoRootPath);
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Report-IssuesToAppVeyor").Does(() =>
    {
        var repoRootPath = MakeAbsolute(Directory("./"));
    
        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            AppVeyorBuilds(),
            repoRootPath);
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
                context.AppVeyorBuilds(),
                repoRootPath);
        }
    }
    ```

The output will look similar to this:

![AppVeyor messages](../appveyor-messages.png "AppVeyor messages")
