---
title: Cake Issues v5.0.0 Released
date: 2024-12-02
categories:
  - Release Notes
search:
  boost: 0.5
---

Cake Issues version 5.0.0 has been released.
This is a major release, containing breaking changes beside bringing new features and bug fixes across all addins.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Support for Cake 5.0

All addins have been updated to support Cake 5.x.

Target framework have been updated to .NET 8 and .NET 9 to be in line with Cake.

## Improvements for Cake Frosting

`Cake.Frosting.Issues.PullRequests.AzureDevOps` now references `Cake.Frosting.AzureDevOps`,
a version of `Cake.AzureDevOps`, optimized for Cake Frosting.

Instead of shipping client assemblies to access Azure DevOps as part of `Cake.AzureDevOps`,
`Cake.Frosting.AzureDevOps` references the corresponding NuGet packages, which for example
allows to control version of the libraries used.

## Security improvements

Transitive dependencies which contain known security vulnerabilities have been updated to
newer versions where the vulnerabilities are fixed.

## Updating from previous versions

Cake.Issues 5.0.0 is a breaking release, which means that there is a small change that changes to your
build script are required.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.0.0){target="_blank"}
