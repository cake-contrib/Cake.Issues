---
title: Examples
description: Examples for using the Cake.Issues.Markdownlint addin.
icon: material/test-tube
---

To call [markdownlint-cli]{target="_blank"} from a Cake script the [Cake.Markdownlint]{target="_blank"} addin can be used.
To read issues from markdownlint-cli log files the markdownlint issue provider needs to be imported:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    #addin nuget:?package=Cake.Markdownlint&version={{ cake_markdownlint_version }}
    #addin nuget:?package=Cake.Issues&version={{ cake_issues_version }}
    #addin nuget:?package=Cake.Issues.Markdownlint&version={{ cake_issues_version }}
    ```

    !!! note
        In addition to the markdownlint issue provider the `Cake.Issues` core addin needs to be added.

=== "Cake SDK"

    ```csharp title="Build.csproj"
    <Project Sdk="Cake.Sdk">
      <PropertyGroup>
        <TargetFramework>{{ example_tfm }}</TargetFramework>
        <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
      </PropertyGroup>
      <ItemGroup>
        <PackageReference Include="Cake.Markdownlint" Version="{{ cake_markdownlint_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.Markdownlint" Version="{{ cake_issues_version }}" />
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
        <PackageReference Include="Cake.Markdownlint" Version="{{ cake_markdownlint_version }}" />
        <PackageReference Include="Cake.Frosting" Version="{{ cake_version }}" />
        <PackageReference Include="Cake.Frosting.Issues.Markdownlint" Version="{{ cake_issues_version }}" />
      </ItemGroup>
    </Project>
    ```

The following example contains a task which will run [markdownlint-cli]{target="_blank"} and write a log file
and a task to read issues from the log file and write the number of warnings to the console:

=== "Cake .NET Tool"

    ```csharp title="build.cake"
    var logPath = @"c:\build\markdownlint.log";
    var repoRootFolder = MakeAbsolute(Directory("./"));

    Task("Lint-Documentation").Does(() =>
    {
        // Run markdownlint-cli.
        var settings =
            MarkdownlintNodeJsRunnerSettings.ForDirectory(
                context.RepoRootPath.Combine("docs"));
        settings.OutputFile = logPath;
        settings.ThrowOnIssue = false;
        RunMarkdownlintNodeJs(settings);
    });

    Task("Read-Issues")
        .IsDependentOn("Lint-Documentation")
        .Does(() =>
        {
            // Read issues.
            var issues =
                ReadIssues(
                    MarkdownlintIssuesFromFilePath(
                        logPath,
                        MarkdownlintCliLogFileFormat),
                    repoRootPath);

            Information("{0} issues are found.", issues.Count());
    });

    ```

=== "Cake SDK"

    ```csharp title="build.cs"
    var logPath = @"c:\build\markdownlint.log";
    var repoRootFolder = MakeAbsolute(Directory("./"));

    Task("Lint-Documentation").Does(() =>
    {
        // Run markdownlint-cli.
        var settings =
            MarkdownlintNodeJsRunnerSettings.ForDirectory(
                context.RepoRootPath.Combine("docs"));
        settings.OutputFile = logPath;
        settings.ThrowOnIssue = false;
        RunMarkdownlintNodeJs(settings);
    });

    Task("Read-Issues")
        .IsDependentOn("Lint-Documentation")
        .Does(() =>
        {
            // Read issues.
            var issues =
                ReadIssues(
                    MarkdownlintIssuesFromFilePath(
                        logPath,
                        MarkdownlintCliLogFileFormat),
                    repoRootPath);

            Information("{0} issues are found.", issues.Count());
    });

    ```

=== "Cake Frosting"

    ```csharp title="Program.cs"
    using Cake.Common.Diagnostics;
    using Cake.Common.IO;
    using Cake.Core;
    using Cake.Core.IO;
    using Cake.Frosting;
    using Cake.Markdownlint;
    using Cake.Markdownlint.NodeJs;

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
        public FilePath LogPath { get; } = @"c:\build\markdownlint.log";
        public DirectoryPath RepoRootPath { get; } =
            context.MakeAbsolute(context.Directory("./"));
    }

    [TaskName("Lint-Documentation")]
    public sealed class LintDocumentationTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Run markdownlint-cli.
            var settings =
                MarkdownlintNodeJsRunnerSettings.ForDirectory(
                    context.RepoRootPath.Combine("docs"));
            settings.OutputFile = context.LogPath;
            settings.ThrowOnIssue = false;
            context.RunMarkdownlintNodeJs(settings);
        }
    }

    [TaskName("Read-Issues")]
    [IsDependentOn(typeof(LintDocumentationTask))]
    public sealed class ReadIssuesTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            // Read issues.
            var issues =
                context.ReadIssues(
                    context.MarkdownlintIssuesFromFilePath(
                        context.LogPath,
                        context.MarkdownlintCliLogFileFormat()),
                    context.RepoRootPath);

            context.Information("{0} issues are found.", issues.Count());
        }
    }
    ```

[markdownlint-cli]: https://github.com/igorshubovych/markdownlint-cli
[Cake.Markdownlint]: https://cakebuild.net/extensions/cake-markdownlint/
