---
title: Custom issue filter
description: Usage instructions how to apply custom filters to issues.
---

The [IssueFilters setting property]{target="_blank"} allows to define custom filters which are applied to issues
before they are posted as comments to pull requests.

??? tip "Tip: Filter to issues introduced with pull request"
    You can use a custom filter to only have issues introduced with the current code posted to the pull request.

    For this you need to store your log files as artifacts on your build system, then you can define a custom filter
    which retrieves the logs from the previous build, parses them using the appropriate issue provider and filters
    out any issues which were already existing in the previous build.

The following example will filter out all issues from the rule `CA1000` from being posted to the pull request.

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.MsBuild&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.PullRequests.AzureDevOps&version={{ cake_issues_version }}    

    Task("ReportIssuesToPullRequest").Does(() =>
    {
        var repoRootFolder = new DirectoryPath(@"C:\repo");    

        var settings =
            new ReportIssuesToPullRequestFromIssueProviderSettings(repoRootFolder);

        // Add custom filter.
        settings.IssueFilters.Add(x => x.Where(issue => issue.RuleId != "CA1000"));

        ReportIssuesToPullRequest(
            new List<IIssueProvider>
            {
                MsBuildIssuesFromFilePath(
                    @"C:\build\msbuild.log",
                    MsBuildBinaryLogFileFormat)
            },
            AzureDevOpsPullRequests(),
            settings);
    });
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

            var settings =
                new ReportIssuesToPullRequestFromIssueProviderSettings(
                    repoRootFolder);

            // Add custom filter.
            settings.IssueFilters.Add(x =>
                x.Where(issue => issue.RuleId != "CA1000"));

            context.ReportIssuesToPullRequest(
                new List<IIssueProvider>
                {
                    context.MsBuildIssuesFromFilePath(
                        @"C:\build\msbuild.log",
                        context.MsBuildBinaryLogFileFormat())
                },
                context.AzureDevOpsPullRequests(),
                settings);
        }
    }
    ```

[IssueFilters Setting Property]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/48CB35E4
