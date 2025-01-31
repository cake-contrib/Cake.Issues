---
title: Cake Issues v1.0.0 Released
date: 2021-07-28
categories:
  - Release Notes
links:
  - Cake v1.0.0 released: https://cakebuild.net/blog/2021/02/cake-v1.0.0-released
---

More than 4 years after the [first commit for Cake.Prca](https://github.com/cake-contrib/Cake.Prca/commit/438b3a1a609e5b9cc9e6f8f489a73988f9ed1f4d){target="_blank"},
the predecessor of Cake Issues, we're happy to announce that Cake Issues version 1.0.0 has been released.
This is a major release bringing a lot of new features across all addins.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [janniksam](https://github.com/janniksam){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}
* [phlorian](https://github.com/phlorian){target="_blank"}
* [jokay](https://github.com/jokay){target="_blank"}

## Support for Cake 1.0

All addins have been updated to support Cake 1.x.

## Support for Cake Frosting

All addins can be used with [Cake Frosting]{target="_blank"}.

Cake Issues addins have always been self-contained, shipping with all required dependencies, to provide the best user experience.
While this approach makes sense for Cake script runners, it makes things more complex than required when running under [Cake Frosting].
This is especially true for the [Cake.Issues.Reporting.Generic addin], which uses Razor engine to generate the reports.

It was therefore decided to release separate versions of the Cake.Issues.Reporting.Generic addin for the different script runners:

* [Cake.Issues.Reporting.Generic]{target="_blank"}: The addin packaged in a self-contained NuGet package for use with Cake script runners
* [Cake.Frosting.Issues.Reporting.Generic]{target="_blank"}: The addin packaged in a NuGet package containing dependencies for use with [Cake Frosting]

[Cake.Frosting.Issues.Reporting.Generic]{target="_blank"} has the additional benefit for the user that it gives the user full control
of what exact version of dependencies should be used.
In the future more Cake Issues addins might be released in Frosting specific packages.

[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[Cake.Issues.Reporting.Generic addin]: ../../documentation/report-formats/generic/index.md
[Cake.Issues.Reporting.Generic]: https://cakebuild.net/extensions/cake-issues-reporting-generic/
[Cake.Frosting.Issues.Reporting.Generic]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Generic/

## Support for arbitrary values in an issue

Specific issue providers might have additional information for which no equivalent does exist on `IIssue`.
These kind of information can now be stored in the `IIssue.AdditionalInformation` property.

## New provider type property

While there are aliases to get provider type name (e.g. [MsBuildIssuesProviderTypeName]{target="_blank"}), this information was in previous versions
not available through the `IIssueProvider` interface.
There is a new `IIssueProvider.ProviderType` property which can be used to retrieve the provider type.

[MsBuildIssuesProviderTypeName]: https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/0A221402

## PDF export

A new PDF export has been added to the `HtmlDxDataGrid` template of the `Cake.Issues.Reporting.Generic` addin.

## Simplified release process

Starting with Cake Issues 1.0.0 the three core addins `Cake.Issues`, `Cake.Issues.PullRequests` and
`Cake.Issues.Reporting` will be always released together.
For that source code for the addin has been merged in the [Cake.Issues repository]{target="_blank"}.
Please open any issues related to any of the core addins in the [Cake.Issues issue tracker]{target="_blank"}.

[Cake.Issues repository]: https://github.com/cake-contrib/Cake.Issues
[Cake.Issues issue tracker]: https://github.com/cake-contrib/Cake.Issues/issues

## Updating from previous versions

Cake.Issues 1.0.0 is a breaking release, which means that it probably requires changes to your build script.
This section documents the most common changes which might be required:

* Cake.Issues
  * Serialization format has been updated to version 4.
    Older version are still supported for deserialization.
  * `IIssueProvider` was extended with an additional `ProviderType` property.
    For issue providers inheriting from `BaseIssueProvider` no action is required to keep the same behavior
    as with previous versions, with the type name used as provider type.

!!! info
    Cake.Recipe has not been updated to 1.0 yet.

For details see release notes of the individual addins:

* [Cake.Issues 1.0.0](https://github.com/cake-contrib/Cake.Issues/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.MsBuild 1.0.0](https://github.com/cake-contrib/Cake.Issues.MsBuild/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.InspectCode 1.0.0](https://github.com/cake-contrib/Cake.Issues.InspectCode/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.DupFinder 1.0.0](https://github.com/cake-contrib/Cake.Issues.DupFinder/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.GitRepository 1.0.0](https://github.com/cake-contrib/Cake.Issues.GitRepository/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.Markdownlint 1.0.0](https://github.com/cake-contrib/Cake.Issues.Markdownlint/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.EsLint 1.0.0](https://github.com/cake-contrib/Cake.Issues.EsLint/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.DocFx 1.0.0](https://github.com/cake-contrib/Cake.Issues.DocFx/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.Terraform 1.0.0](https://github.com/cake-contrib/Cake.Issues.Terraform/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.PullRequests 1.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.PullRequests.AzureDevOps 1.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AzureDevOps/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.PullRequests.AppVeyor 1.0.0](https://github.com/cake-contrib/Cake.Issues.PullRequests.AppVeyor/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.Reporting 1.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.Reporting.Generic 1.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/releases/tag/1.0.0){target="_blank"}
* [Cake.Issues.Reporting.Sarif 1.0.0](https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif/releases/tag/1.0.0){target="_blank"}
