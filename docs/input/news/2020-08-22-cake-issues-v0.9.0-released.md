---
title: Cake Issues v0.9.0 Released
category: Release Notes
---

Cake Issues version 0.9.0 has been released. This is a major release bringing a lot of new features across all addins.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [AdmiringWorm](https://github.com/AdmiringWorm)
* [christianbumann](https://github.com/christianbumann)
* [eoehen](https://github.com/eoehen)
* [gep13](https://github.com/gep13)
* [janniksam](https://github.com/janniksam)
* [mholo65](https://github.com/mholo65)
* [pascalberger](https://github.com/pascalberger)
* [Speeedy01](https://github.com/Speeedy01)
* [jokay](https://github.com/jokay)

## Full cross-platform support

While in previous versions most parts of Cake Issues was already targeting .NET Standard and with this could be executed on
.NET Framework, .NET Core and Mono, `Cake.Issues.Reporting.Generic` could only run on .NET Framework and Mono, but not on
.NET Core.
With this release `Cake.Issues.Reporting.Generic` was ported to also run on .NET Core.

The migration was done by [gep13](https://github.com/gep13) on his [Twitch stream](https://www.twitch.tv/gep13) and you
can watch work done in [Stream 90 - Working on Cake.Issues.Recipe](https://www.youtube.com/watch?v=7roa5Q6KcrQ),
[Stream 91 - Working on Cake.Issues.Reporting.Generic and Gazorator](https://www.youtube.com/watch?v=ocacOz3CxME) and
[Stream 92 - Working on Cake.Issues.Reporting.Generic and Gazorator - Part 2](https://www.youtube.com/watch?v=P0IpkL9gUAE).

## Enhanced issue information

The description for issues has been extended by additional information for column and ranges:

* `IIssue.EndLine`
* `IIssue.Column`
* `IIssue.EndColumn`

Existing issue providers have been updated to provide the additional information where available.
See feature description for individual issue providers for which information a specific issue provider supports.

## File linking

In previous versions `Cake.Issues.Reporting.Generic` supported entries linking to the file on the source code provider
(GitHub, Azure Repos, ...).
With this release file linking infrastructure has been moved to the `Cake.Issues` addin and can be used by any addin.

File link settings can  be defined while reading issues and are passed through the new `IIssue.FileLink` property
to reporting formats, pull request systems and build server implementations:

```csharp
var settings =
    new ReadIssuesSettings(@"c:\repo")
    {
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubCommit(
                "https://github.com/cake-contrib/Cake.Issues.Reporting.Generic",
                "76a7cacef7ad4295a6766646d45c9b56")
    };

    var issues =
        ReadIssues(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            settings));
```

`Cake.Issues` comes with out-of-the-box support for linking to files hosted on GitHub and Azure Repos, either for a
specific branch or commit. Additionally there are aliases which can be used to define any custom pattern.

## Support for passing additional run information

If a build script needed to parse multiple log files from the same tool, e.g. because multiple MsBuild solutions
were built, this was currently possible by calling the issue provider multiple times.
If the results were read into the same list and shown on the same report, individual issues could not be
assigned to any of the calls, since issue provider type and name were identical.
Starting with Cake.Issues 0.9.0 it is now possible to pass additional run information while reading issues, which
then will be stored with each issues in the `IIssue.Run` property:

```csharp
var issues = new List<IIssue>();

// Parse issues from build of solutions 1
issues.AddRange(
    ReadIssues(
        MsBuildIssuesFromFilePath(
            @"C:\build\solution1-msbuild.log",
            MsBuildXmlFileLoggerFormat),
        new ReadIssuesSettings(@"c:\repo")
        {
            Run = "Solution 1"
        }
    )
);

// Parse issues from build of solutions 2
issues.AddRange(
    ReadIssues(
        MsBuildIssuesFromFilePath(
            @"C:\build\solution2-msbuild.log",
            MsBuildXmlFileLoggerFormat),
        new ReadIssuesSettings(@"c:\repo")
        {
            Run = "Solution 2"
        }
    )
);
```

## Improved pull request integration

In previous versions the text message of an issue was used to detect issues already reported in a previous run.
This didn't work well for issues which contain information in the message which likely changes between runs,
like e.g. line information.
In this version a specific identifier, `IIssue.Identifier`, is used which can be set by the issue provider.

When working with legacy code bases which contain a lot of existing issues, using something like Cake.Issues
can be hard, since it will notify about every existing issue if something is changed in a file.
To work around this issue it is possible to limit issues which will be posted to pull request systems.
In previous versions it was already possible to limit the total number of issues, the number of issues for
every issue provider and the total number of issues across multiple run.
With 0.9.0 it will additionally be possible to limit the number of issues for specific issue providers for either
a single or across multiple runs.
This allows advanced scenarios like posting a maximum of 10 MsBuild issues every run, but not more than 20 in total
across all runs.

## Get everything together

[Cake.Issues.Recipe], the Cake Recipe script which you can integrate into your build script for easy integration of
full feature issue management, has been updated to version 0.4.0, bringing all the new features of Cake.Issues 0.9.0.

[Cake.Issues.Recipe]: ../docs/recipe/overview

## Updating from previous versions

Cake.Issues 0.9.0 is a breaking release, which means that it probably requires changes to your build script.
This section documents the most common changes which might be required:

* Cake.Issues
  * Serialization format has been updated to version 3.
    Older version are still supported for deserialization.
* Cake.Issues.Markdownlint
  * `MarkdownlintLogFileFormat` alias has been renamed to `MarkdownlintV1LogFileFormat`
    ([#116](https://github.com/cake-contrib/Cake.Issues.Markdownlint/issues/116)).
* Cake.Issues.PullRequest
  * `ReportIssuesToPullRequest` alias which accepts an issue provider, or a list of issue providers, and settings requires now settings of type
    `IReportIssuesToPullRequestFromIssueProviderSettings` instead of `ReportIssuesToPullRequestSettings` to
    provider additional functionality, like support for [File linking] and [Support for passing additional run information].
* Cake.Issues.PullRequests.AzureDevOps
  * Cake.Issues.PullRequests.AzureDevOps requires at least Cake.AzureDevOps 0.5.0
* Cake.Issues.Reporting
  * `CreateIssueReport` alias which accepts an issue provider, or a list of issue providers, and settings requires now settings of type
    `ICreateIssueReportFromIssueProviderSettings` instead of `CreateIssueReportSettings` to
    provider additional functionality, like support for [File linking] and [Support for passing additional run information].
* Cake.Issues.Reporting.Generic
  * Cake.Issues.Reporting.Generic requires at least Cake 0.38.0
  * `HtmlDxDataGridOption.FileLinkSettings` has been removed.
    File link settings can now be defined while reading the issues.
    For details see [File linking].
    ([#265](https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/issues/265)).
  * `HtmlDxDataGridOption.JSZipLocation` has been split into `HtmlDxDataGridOption.JsZipLocation` and
    `HtmlDxDataGridOption.JsZipVersion`
    ([#320](https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/issues/320)).
* Cake.Issues.Recipe
  * Cake.Issues.Recipe requires at least Cake 0.38.0

For details see release notes of the individual addins:

* [Cake.Issues 0.9.0](https://github.com/cake-contrib/Cake.Issues/releases/tag/0.9.0)
* [Cake.Issues.MsBuild 0.9.0](https://github.com/cake-contrib/Cake.Issues.MsBuild/releases/tag/0.9.0)
* [Cake.Issues.InspectCode 0.9.0](https://github.com/cake-contrib/Cake.Issues.InspectCode/releases/tag/0.9.0)
* [Cake.Issues.DupFinder 0.9.0](https://github.com/cake-contrib/Cake.Issues.DupFinder/releases/tag/0.9.0)
* [Cake.Issues.GitRepository 0.9.0](https://github.com/cake-contrib/Cake.Issues.GitRepository/releases/tag/0.9.0)
* [Cake.Issues.Markdownlint 0.9.0](https://github.com/cake-contrib/Cake.Issues.Markdownlint/releases/tag/0.9.0)
* [Cake.Issues.EsLint 0.9.0](https://github.com/cake-contrib/Cake.Issues.EsLint/releases/tag/0.9.0)
* [Cake.Issues.DocFx 0.9.0](https://github.com/cake-contrib/Cake.Issues.DocFx/releases/tag/0.9.0)
* [Cake.Issues.PullRequests 0.9.0](https://github.com/cake-contrib/Cake.Issues.PullRequests/releases/tag/0.9.0)
* [Cake.Issues.PullRequests.AzureDevOps 0.9.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AzureDevOps/releases/tag/0.9.0)
* [Cake.Issues.PullRequests.AppVeyor 0.9.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AppVeyor/releases/tag/0.9.0)
* [Cake.Issues.Reporting 0.9.0](https://github.com/cake-contrib/Cake.Issues.Reporting/releases/tag/0.9.0)
* [Cake.Issues.Reporting.Generic 0.9.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/releases/tag/0.9.0)
* [Cake.Issues.Reporting.Sarif 0.9.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif/releases/tag/0.9.0)
* [Cake.Issues.Recipe 0.4.0](https://github.com/cake-contrib/Cake.Issues.Recipe/releases/tag/0.4.0)

[File linking]: #file-linking
[Support for passing additional run information]: #support-for-passing-additional-run-information
