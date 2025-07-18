---
title: Using with Azure Pipelines
description: Example how to use the Cake.Issues.PullRequests.AzureDevOps addin from an Azure Pipelines build.
---

To write issues as comments to Azure DevOps pull requests, the Azure DevOps addin needs to be imported.
For this example the JetBrains InspectCode issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests.AzureDevOps&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.AzureDevOps&version={{ cake_azuredevops_version }}
    ```

    !!! note
        In addition to the Azure DevOps pull request system the `Cake.Issues` and `Cake.Issues.PullRequests` core addins
        and the Cake.AzureDevOps addin need to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.AzureDevOps" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

The following example shows a task which will call the [AzureDevOpsPullRequests](https://cakebuild.net/api/Cake.Issues.PullRequests.AzureDevOps/AzureDevOpsPullRequestSystemAliases/){target="_blank"}
alias to connect to the pull request using the environment variables provided by Azure Pipelines.:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Report-IssuesToPullRequest").Does(() =>
    {
        var repoRootFolder =
            MakeAbsolute(Directory("./"));

        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            AzureDevOpsPullRequests(),
            repoRootPath);
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Report-IssuesToPullRequest").Does(() =>
    {
        var repoRootFolder =
            MakeAbsolute(Directory("./"));

        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            AzureDevOpsPullRequests(),
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

    [TaskName("Report-IssuesToPullRequest")]
    public sealed class ReportIssuesToPullRequestTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            var repoRootPath =
                context.MakeAbsolute(context.Directory("./"));

            context.ReportIssuesToPullRequest(
                context.InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log"),
                context.AzureDevOpsPullRequests(),
                repoRootPath);
        }
    }
    ```

!!! info
    Please note that you'll need to setup your Azure Pipelines build to
    [Allow scripts to access the OAuth token](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/options#allow-scripts-to-access-the-oauth-token){target="_blank"}
    and need to setup proper permissions.

    See [OAuth authentication from Azure Pipelines] for details.

[OAuth authentication from Azure Pipelines]: ../setup.md#oauth-authentication-from-azure-pipelines
