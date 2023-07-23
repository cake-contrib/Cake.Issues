---
title: Cake Issues v3.0.0 Released
category: Release Notes
---

Cake Issues version 3.0.0 has been released.
This is a major release, containing breaking changes beside bringing new features and bug fixes across all addins.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [DiDoHH](https://github.com/DiDoHH)
* [eoehen](https://github.com/eoehen)
* [pascalberger](https://github.com/pascalberger)

## Support for Cake 3.0

All addins have been updated to support Cake 3.x.

Target framework have been updated to .NET 6 and .NET 7 to be in line with Cake.

## Support for MsBuild binary logs version 16

Support for binary logs in version 16 has been added to `Cake.Issues.MsBuild`.

## Out of the box support for more rule links

`Cake.Issues.MsBuild` now will automatically provide links for [Roslynator] and [SonarLint] rules.
Links for `CA` rules have updated to link to `learn.microsoft.com`.

## Updating from previous versions

Cake.Issues 3.0.0 is a breaking release, which means that it probably requires changes to your build script.
This section documents the most common changes which might be required:

* Cake.Issues
  * `StringPathExtensions.IsValideRepositoryFilePath` has been renamed to `StringPathExtensions.IsValidRepositoryFilePath`
  * `BaseRuleDescription.Rule` has been made immutable after initialization
* Cake.Issues.Recipe
  * Since [Dupfinder has been sunsetted] end of 2021, out of the box support for it has been removed from Cake Issues Recipe
    and `DupFinderLogFilePaths` is no longer available.
    To keep using DupFinder you need to manually add `Cake.Issues.DupFinder` and add issues using the `AddIssues` method.

For details see release notes of the individual addins:

* [Cake.Issues.Recipe 3.0.0](https://github.com/cake-contrib/Cake.Issues.Recipe/releases/tag/3.0.0)
* [Cake.Issues 3.0.0](https://github.com/cake-contrib/Cake.Issues/releases/tag/3.0.0)
* [Cake.Issues.DocFx 3.0.0](https://github.com/cake-contrib/Cake.Issues.DocFx/releases/tag/3.0.0)
* [Cake.Issues.DupFinder 3.0.0](https://github.com/cake-contrib/Cake.Issues.DupFinder/releases/tag/3.0.0)
* [Cake.Issues.EsLint 3.0.0](https://github.com/cake-contrib/Cake.Issues.EsLint/releases/tag/3.0.0)
* [Cake.Issues.GitRepository 3.0.0](https://github.com/cake-contrib/Cake.Issues.GitRepository/releases/tag/3.0.0)
* [Cake.Issues.InspectCode 3.0.0](https://github.com/cake-contrib/Cake.Issues.InspectCode/releases/tag/3.0.0)
* [Cake.Issues.Markdownlint 3.0.0](https://github.com/cake-contrib/Cake.Issues.Markdownlint/releases/tag/3.0.0)
* [Cake.Issues.MsBuild 3.0.0](https://github.com/cake-contrib/Cake.Issues.MsBuild/releases/tag/3.0.0)
* [Cake.Issues.Terraform 3.0.0](https://github.com/cake-contrib/Cake.Issues.Terraform/releases/tag/3.0.0)
* [Cake.Issues.Reporting.Console 3.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Console/releases/tag/3.0.0)
* [Cake.Issues.Reporting.Generic 3.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/releases/tag/3.0.0)
* [Cake.Issues.Reporting.Sarif 3.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif/releases/tag/3.0.0)
* [Cake.Issues.PullRequests.AppVeyor 3.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AppVeyor/releases/tag/3.0.0)
* [Cake.Issues.PullRequests.AzureDevOps 3.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AzureDevOps/releases/tag/3.0.0)
* [Cake.Issues.PullRequests.GitHubActions 3.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.GitHubActions/releases/tag/3.0.0)

[Roslynator]: https://josefpihrt.github.io/docs/roslynator/
[SonarLint]: https://www.sonarsource.com/products/sonarlint/
[Dupfinder has been sunsetted]: https://blog.jetbrains.com/dotnet/2021/08/12/sunsetting-dupfinder-command-line-tool/
