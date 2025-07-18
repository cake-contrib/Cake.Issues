---
title: Breaking builds
description: Usage instructions how to break builds.
---

The `Cake.Issues` addin can be used to break builds if specific issues were reported.

??? Tip "List of all aliases for breaking builds"
    See all available [Aliases for breaking builds](https://cakebuild.net/extensions/cake-issues/#Build-Breaking){target="_blank"}

To break builds you need to import the following core addin:

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

The following task will fail the build if any issues were added to the `issues` global variable:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    // Global issues list into which issues need to be added.
    IEnumerable<IIssue> issues = null;
    
    Task("BreakBuildOnIssues")
        .Description("Breaks build if any issues in the code are found.")
        .Does(() =>
    {
        BreakBuildOnIssues(issues);
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    // Global issues list into which issues need to be added.
    IEnumerable<IIssue> issues = null;
    
    Task("BreakBuildOnIssues")
        .Description("Breaks build if any issues in the code are found.")
        .Does(() =>
    {
        BreakBuildOnIssues(issues);
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Core;
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
        private readonly List<IIssue> _issues = [];
    
        public IEnumerable<IIssue> Issues { get { return _issues;  } }
    
        public void AddIssues(IEnumerable<IIssue> issues)
        {
            _issues.AddRange(issues);
        }
    }

    [TaskName("BreakBuildOnIssues")]
    public sealed class BreakBuildOnIssuesTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.BreakBuildOnIssues(context.Issues);
        }
    }
    ```
