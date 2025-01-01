---
title: Using with pull request id
description: Example how to use the Cake.Issues.PullRequests.AzureDevOps addin with pull request id.
---

To write issues as comments to Azure DevOps pull requests, the Azure DevOps addin needs to be imported.
To determine the remote repository URL the [Cake.Git](https://cakebuild.net/extensions/cake-git/){target="_blank"} addin can be used.
For this example the JetBrains InspectCode issue provider is additionally used for reading issues:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin "Cake.Git" // (1)!
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.InspectCode&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests.AzureDevOps&version={{ cake_issues_version }}
    #addin "Cake.AzureDevOps" // (1)!
    ```

    --8<-- "snippets/pinning.md"

    !!! note
        In addition to the Azure DevOps pull request system the `Cake.Issues` and `Cake.Issues.PullRequests` core addins
        and the Cake.AzureDevOps addin need to be added.

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
        <PackageReference Include="Cake.Frosting.Git" Version="1.0.0" /> // (1)!
        <PackageReference Include="Cake.Frosting.Issues.InspectCode" Version="{{ cake_issues_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.PullRequests.AzureDevOps" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

    --8<-- "snippets/version-placeholder.md"

The following example shows a task which will first determine the remote repository URL and
with this information call the [AzureDevOpsPullRequests](https://cakebuild.net/api/Cake.Issues.PullRequests.AzureDevOps/AzureDevOpsPullRequestSystemAliases/){target="_blank"}
alias, which will authenticate through NTLM to an on-premise Azure DevOps Server instance:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Report-IssuesToPullRequest").Does(() =>
    {
        var repoRootPath =
            MakeAbsolute(Directory("./"));
        var currentBranch =
            GitBranchCurrent(repoRootPath);
        var repoRemoteUrl =
            new Uri(currentBranch.Remotes.Single(x => x.Name == "origin").Url);
        var pullRequestId = 123;

        ReportIssuesToPullRequest(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            AzureDevOpsPullRequests(
                repoRemoteUrl,
                pullRequestId,
                AzureDevOpsAuthenticationNtlm()),
            repoRootPath);
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.IO;
    using Cake.Frosting;
    using Cake.Git;

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
            var currentBranch =
                context.GitBranchCurrent(repoRootPath);
            var repoRemoteUrl = 
                new Uri(currentBranch.Remotes.Single(x => x.Name == "origin").Url);
            var pullRequestId = 123;

            context.ReportIssuesToPullRequest(
                context.InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log"),
                context.AzureDevOpsPullRequests(
                    repoRemoteUrl,
                    pullRequestId,
                    context.AzureDevOpsAuthenticationNtlm()),
                repoRootPath);
        }
    }
    ```
