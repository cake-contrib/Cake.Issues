---
title: Creating issues
description: Usage instructions how to create issues.
---

The `Cake.Issues` addin can be used to create issues directly in the build script.
This issues can for example be used to create reports.

??? Tip "List of all aliases for creating issues"
    See all available [Aliases for creating issues](https://cakebuild.net/extensions/cake-issues/#Creating-Issues)

To create issues you need to import the following core addin:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    ```

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Issues" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Issues" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

In the following task a new warning for the myfile.txt file on line 42 is created:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Create-Issue").Does(() =>
    {
        var issue =
            NewIssue(
                "Something went wrong",
                "MyCakeScript",
                "My Cake Script")
                .WithMessageInHtmlFormat("Something went <b>wrong</b>")
                .WithMessageInMarkdownFormat("Something went **wrong**")
                .InFile("myfile.txt", 42)
                .WithPriority(IssuePriority.Warning)
                .Create();
    
        Information(
            "Issue created with message: {0}",
            issue.MessageText);
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Create-Issue").Does(() =>
    {
        var issue =
            NewIssue(
                "Something went wrong",
                "MyCakeScript",
                "My Cake Script")
                .WithMessageInHtmlFormat("Something went <b>wrong</b>")
                .WithMessageInMarkdownFormat("Something went **wrong**")
                .InFile("myfile.txt", 42)
                .WithPriority(IssuePriority.Warning)
                .Create();
    
        Information(
            "Issue created with message: {0}",
            issue.MessageText);
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

    [TaskName("Create-Issue")]
    public sealed class CreateIssueTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var issue =
                context.NewIssue(
                    "Something went wrong",
                    "MyCakeScript",
                    "My Cake Script")
                    .WithMessageInHtmlFormat("Something went <b>wrong</b>")
                    .WithMessageInMarkdownFormat("Something went **wrong**")
                    .InFile("myfile.txt", 42)
                    .WithPriority(IssuePriority.Warning)
                    .Create();
        
            context.Information(
                "Issue created with message: {0}",
                issue.MessageText);
        }
    }
    ```
