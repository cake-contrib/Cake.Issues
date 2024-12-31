---
title: Cake Issues v4.10.1 Released
date: 2024-08-20
categories:
  - Release Notes
search:
  boost: 0.5
---

Cake Issues version v4.10.1 has been released with bugfixes for Cake Frosting and file linking

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann){target="_blank"}
* [eoehen](https://github.com/eoehen){target="_blank"}
* [gep13](https://github.com/gep13){target="_blank"}
* [hotchkj](https://github.com/hotchkj){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Bugfixes for Cake Frosting

This release fixes dependencies of the following Cake Frosting optimized NuGet packages:

* [Cake.Frosting.Issues.PullRequests.AppVeyor]{target="_blank"}
* [Cake.Frosting.Issues.PullRequests.AzureDevOps]{target="_blank"}
* [Cake.Frosting.Issues.PullRequests.GitHubActions]{target="_blank"}
* [Cake.Frosting.Issues.Reporting.Console]{target="_blank"}
* [Cake.Frosting.Issues.Reporting.Generic]{target="_blank"}
* [Cake.Frosting.Issues.Reporting.Sarif]{target="_blank"}

## Bugfixes for file linking

This release fixes an issue that file links are created for issues which are not related to a file.

## Updating from previous versions

Cake.Issues v4.10.1 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/v4.10.1){target="_blank"}

[Cake.Frosting.Issues.PullRequests.AppVeyor]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AppVeyor/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.PullRequests.AzureDevOps]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AzureDevOps/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.PullRequests.GitHubActions]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.GitHubActions/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.Reporting.Console]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Console/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.Reporting.Generic]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Generic/4.10.1#dependencies-body-tab
[Cake.Frosting.Issues.Reporting.Sarif]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Sarif/4.10.1#dependencies-body-tab
