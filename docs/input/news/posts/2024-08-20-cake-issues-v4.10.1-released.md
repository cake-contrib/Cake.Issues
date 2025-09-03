---
title: Cake Issues v4.10.1 Released
date: 2024-08-20
categories:
  - Release Notes
links:
  - documentation/usage/reading-issues/file-linking.md
---

Cake Issues version v4.10.1 has been released with bugfixes for Cake Frosting and file linking

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann)
* [eoehen](https://github.com/eoehen)
* [gep13](https://github.com/gep13)
* [hotchkj](https://github.com/hotchkj)
* [pascalberger](https://github.com/pascalberger)

## Bugfixes for Cake Frosting

This release fixes dependencies of the following Cake Frosting optimized NuGet packages:

* [Cake.Frosting.Issues.PullRequests.AppVeyor]
* [Cake.Frosting.Issues.PullRequests.AzureDevOps]
* [Cake.Frosting.Issues.PullRequests.GitHubActions]
* [Cake.Frosting.Issues.Reporting.Console]
* [Cake.Frosting.Issues.Reporting.Generic]
* [Cake.Frosting.Issues.Reporting.Sarif]

## Bugfixes for file linking

This release fixes an issue that file links are created for issues which are not related to a file.

## Updating from previous versions

Cake.Issues v4.10.1 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/v4.10.1)

[Cake.Frosting.Issues.PullRequests.AppVeyor]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AppVeyor/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.PullRequests.AzureDevOps]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AzureDevOps/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.PullRequests.GitHubActions]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.GitHubActions/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.Reporting.Console]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Console/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.Reporting.Generic]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Generic/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.Reporting.Sarif]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Sarif/4.10.1#dependencies-body-tab
