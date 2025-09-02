---
title: Use custom URL resolver for MsBuild issues
description: Examples for using custom URL resolver for MsBuild issues.
icon: material/test-tube
---

!!! note

    This example builds on top of the [Read binary log file](read-binary-log.md) example.

The following example shows how URL schema for custom Roslyn analyzers can be defined
using the [MsBuildAddRuleUrlResolver] alias.

Before reading the issues a custom rule URL resolver can be registered to have all issues starting with `CUS` linking to an internal URL:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    Task("Read-Issues")
        .IsDependentOn("Build-Solution")
        .Does(() =>
        {
            // Define custom URL resolver adding URL for all rules starting with FOO.
            MsBuildAddRuleUrlResolver(x =>
                x.Category.ToUpperInvariant() == "CUS" ?
                new Uri("https://myIntranet/rules/" + x.Rule) :
                null);

            // Read issues.
            var issues =
                ReadIssues(
                    MsBuildIssuesFromFilePath(
                        logPath,
                        MsBuildBinaryLogFileFormat),
                    repoRootPath);

            Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    Task("Read-Issues")
        .IsDependentOn("Build-Solution")
        .Does(() =>
        {
            // Define custom URL resolver adding URL for all rules starting with FOO.
            MsBuildAddRuleUrlResolver(x =>
                x.Category.ToUpperInvariant() == "CUS" ?
                new Uri("https://myIntranet/rules/" + x.Rule) :
                null);

            // Read issues.
            var issues =
                ReadIssues(
                    MsBuildIssuesFromFilePath(
                        logPath,
                        MsBuildBinaryLogFileFormat),
                    repoRootPath);

            Information("{0} issues are found.", issues.Count());
    });
    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
    using Cake.Frosting;

    [TaskName("Read-Issues")]
    [IsDependentOn(typeof(BuildSolutionTask))]
    public sealed class ReadIssuesTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Define custom URL resolver adding URL for all rules starting with FOO.
            context.MsBuildAddRuleUrlResolver(x =>
                x.Category.ToUpperInvariant() == "CUS" ?
                new Uri("https://myIntranet/rules/" + x.Rule) :
                null);

            // Read issues.
            var issues =
                context.ReadIssues(
                    context.MsBuildIssuesFromFilePath(
                        context.LogPath,
                        context.MsBuildBinaryLogFileFormat()),
                    context.RepoRootPath);

            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```

[MsBuildAddRuleUrlResolver]: https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487
