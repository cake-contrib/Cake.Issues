---
title: Cake Issues v4.1.0 Released
date: 2024-02-21
categories:
  - Release Notes
links:
  - documentation/issue-providers/msbuild/index.md
---

Cake Issues version 4.1.0 has been released with improvements for Cake Frosting and support for latest MsBuild binary log format.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann)
* [pascalberger](https://github.com/pascalberger)

## Improvement for Cake Frosting

[Cake.Frosting.Issues.Reporting] and [Cake.Frosting.Issues.PullRequests] have been released
as optimized version of the `Cake.Issues.Reporting` and `Cake.Issues.PullRequests` for Cake Frosting.

These addins come with a dependency to the core `Cake.Issues` addin, allowing it to be consumed as transitive dependency.

## Support for MsBuild binary logs version 18

Support for binary logs in version 18 has been added to `Cake.Issues.MsBuild`.

## Alignment of release lifecycle

As announced in [Alignment of addin lifecycles](2024-01-14-align-addin-lifecycle.md) work as started to move
addins into the main Cake Issues repository.

Starting with this release the following addins will be released together with the core addins:

* Cake.Issues.DocFx
* Cake.Issues.EsLint
* Cake.Issues.MsBuild
* Cake.Issues.InspectCode
* Cake.Issues.Markdownlint

## Updating from previous versions

Cake.Issues 4.1.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.1.0)

[Cake.Frosting.Issues.Reporting]: https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting
[Cake.Frosting.Issues.PullRequests]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests
