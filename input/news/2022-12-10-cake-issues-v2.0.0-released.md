---
title: Cake Issues v2.0.0 Released
category: Release Notes
---

After several months with beta releases Cake Issues version 2.0.0 has been released.
This is a major release, containing breaking changes beside bringing new features and bug fixes across all addins.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen)
* [KirillOsenkov](https://github.com/KirillOsenkov)
* [pascalberger](https://github.com/pascalberger)
* [twenzel](https://github.com/twenzel)
* [yansklyarenko](https://github.com/yansklyarenko)

## Support for Cake 2.0

All addins have been updated to support Cake 2.x.

Target framework have been updated to .NET Core 3.1, .NET 5 and .NET 6 to be in line with Cake.
See [Sunsetting of .NET Framework and .NET Core runners in Cake 2.0] for details.

## Support for MsBuild binary logs version 9

Support for binary logs in version 9 has been added to `Cake.Issues.MsBuild`.

## Updating from previous versions

Cake.Issues 2.0.0 is a breaking release, which means that it probably requires changes to your build script.
This section documents the most common changes which might be required:

* Cake.Issues
  * Serialization format has been updated to version `5`.
    Older version are still supported for deserialization.
  * `ProviderIssueIssueLimits` has been renamed to `ProviderIssueLimits`
  * `IIssue.Rule` has been renamed to `IIssue.RuleId`
  * `IIssue` was extended with an additional `RuleName` property.
* Cake.Issues.MsBuild
  * MsBuild 15 or newer required for binary logs

For details see release notes of the individual addins:

* [Cake.Issues 2.0.0](https://github.com/cake-contrib/Cake.Issues/releases/tag/2.0.0)
* [Cake.Issues.DocFx 2.0.0](https://github.com/cake-contrib/Cake.Issues.DocFx/releases/tag/2.0.0)
* [Cake.Issues.DupFinder 2.0.0](https://github.com/cake-contrib/Cake.Issues.DupFinder/releases/tag/2.0.0)
* [Cake.Issues.EsLint 2.0.0](https://github.com/cake-contrib/Cake.Issues.EsLint/releases/tag/2.0.0)
* [Cake.Issues.GitRepository 2.0.0](https://github.com/cake-contrib/Cake.Issues.GitRepository/releases/tag/2.0.0)
* [Cake.Issues.InspectCode 2.0.0](https://github.com/cake-contrib/Cake.Issues.InspectCode/releases/tag/2.0.0)
* [Cake.Issues.Markdownlint 2.0.0](https://github.com/cake-contrib/Cake.Issues.Markdownlint/releases/tag/2.0.0)
* [Cake.Issues.MsBuild 2.0.0](https://github.com/cake-contrib/Cake.Issues.MsBuild/releases/tag/2.0.0)
* [Cake.Issues.Terraform 2.0.0](https://github.com/cake-contrib/Cake.Issues.Terraform/releases/tag/2.0.0)
* [Cake.Issues.Reporting.Console 2.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Console/releases/tag/2.0.0)
* [Cake.Issues.Reporting.Generic 2.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/releases/tag/2.0.0)
* [Cake.Issues.Reporting.Sarif 2.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif/releases/tag/2.0.0)
* [Cake.Issues.PullRequests.AppVeyor 2.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AppVeyor/releases/tag/2.0.0)
* [Cake.Issues.PullRequests.AzureDevOps 2.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AzureDevOps/releases/tag/2.0.0)
* [Cake.Issues.PullRequests.GitHubActions 2.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.GitHubActions/releases/tag/2.0.0)

[Sunsetting of .NET Framework and .NET Core runners in Cake 2.0]: https://cakebuild.net/blog/2021/10/sunsetting-runners
