---
title: Cake Issues v5.7.0 Released
date: 2025-08-03
categories:
  - Release Notes
links:
  - documentation/issue-providers/gitrepository/examples/excluding-files.md
  - documentation/issue-providers/msbuild/index.md
  - documentation/recipe/index.md
---

Cake Issues version 5.7.0 has been released bringing enhanced integration with GitHub for Cake Issues Recipe
and support for ignoring files in Git Repository issue provider.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann){target="_blank"}
* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Possibility to ignore files in Git Repository issue provider

The Git Repository issue provider comes with new settings which allow to ignore specific files.

Files can be ignored for all or individual rules:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Analyze-Repo-With-Exclusions")
    .Does(() =>
    {
        // Read issues.
        var repoRootPath = MakeAbsolute(Directory("./"));
        var settings =
            new GitRepositoryIssuesSettings
            {
                CheckBinaryFilesTrackedByLfs = true,
                CheckFilesPathLength = true,
                MaxFilePathLength = 260
            };    

        // Exclude temporary files from all checks
        settings.FilesToExclude.Add("*.tmp");
        settings.FilesToExclude.Add("temp/**");
        
        // Exclude specific binary files from LFS tracking checks
        settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("*.dll");
        settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("bin/**");
        settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("obj/**");
        
        // Exclude generated code from path length checks
        settings.FilesToExcludeFromFilePathLengthCheck.Add("generated/**");
        settings.FilesToExcludeFromFilePathLengthCheck.Add("**/AssemblyInfo.cs");

        var issues =
            ReadIssues(
                GitRepositoryIssues(settings),
                repoRootPath);    

        Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Analyze-Repo-With-Exclusions")
    .Does(() =>
    {
        // Read issues.
        var repoRootPath = MakeAbsolute(Directory("./"));
        var settings =
            new GitRepositoryIssuesSettings
            {
                CheckBinaryFilesTrackedByLfs = true,
                CheckFilesPathLength = true,
                MaxFilePathLength = 260
            };    

        // Exclude temporary files from all checks
        settings.FilesToExclude.Add("*.tmp");
        settings.FilesToExclude.Add("temp/**");
        
        // Exclude specific binary files from LFS tracking checks
        settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("*.dll");
        settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("bin/**");
        settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("obj/**");
        
        // Exclude generated code from path length checks
        settings.FilesToExcludeFromFilePathLengthCheck.Add("generated/**");
        settings.FilesToExcludeFromFilePathLengthCheck.Add("**/AssemblyInfo.cs");

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

    [TaskName("Analyze-Repo-With-Exclusions")]
    public sealed class AnalyzeRepoWithExclusionsTask : FrostingTask<FrostingContext>
    {
        public override void Run(FrostingContext context)
        {
            // Read issues.
            var repoRootPath = context.MakeAbsolute(context.Directory("./"));
            var settings =
                new GitRepositoryIssuesSettings
                {
                    CheckBinaryFilesTrackedByLfs = true,
                    CheckFilesPathLength = true,
                    MaxFilePathLength = 260
                };    

            // Exclude temporary files from all checks
            settings.FilesToExclude.Add("*.tmp");
            settings.FilesToExclude.Add("temp/**");
            
            // Exclude specific binary files from LFS tracking checks
            settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("*.dll");
            settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("bin/**");
            settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("obj/**");
            
            // Exclude generated code from path length checks
            settings.FilesToExcludeFromFilePathLengthCheck.Add("generated/**");
            settings.FilesToExcludeFromFilePathLengthCheck.Add("**/AssemblyInfo.cs");
    
            var issues =
                context.ReadIssues(
                    context.GitRepositoryIssues(settings),
                    repoRootPath);    
    
            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```

See [Excluding files with patterns] for details.

## Fixes for MsBuild binary logs

Fixed an issue where an exception was thrown when a binary log contained a warning not related to any project.

## GitHub status checks in Cake Issues Recipe

Starting with version 5.7.0 Cake Issues Recipe will set GitHub status checks for all issue providers
if `ShouldSetPullRequestStatus` is set to `true`.

## SARIF report upload when running Cake Issues Recipe in GitHub Actions

Cake Issues Recipe will now report all issues to GitHub Actions code scanning if `ShouldPublishSarifReport`
is set to `true`.

With this enabled issues will become visible in the repository's `Security` → `Code scanning` tab.

## Updating from previous versions

Cake.Issues 5.7.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.7.0){target="_blank"}

[Excluding files with patterns]: ../../documentation/issue-providers/gitrepository/examples/excluding-files.md
