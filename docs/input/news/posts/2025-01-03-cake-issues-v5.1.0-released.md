---
title: Cake Issues v5.1.0 Released
date: 2025-01-03
categories:
  - Release Notes
links:
  - documentation/issue-providers/tap/index.md
---

Cake Issues version 5.1.0 has been released introducing a new issue provider for Test Anything Protocol (TAP) compatible files.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## New issue provider for Test Anything Protocol (TAP) files

A new [Cake.Issues.Tap addin] has been released which adds support for reading issues in [Test Anything Protocol (TAP)] format.

See [New addin for reading TAP files](2025-01-03-tap-issue-provider.md) for details.

## Improvements for Cake Frosting in Cake Issues Recipe

[Cake Issues Recipe] for Cake Frosting has been updated to use `Cake.Frosting.AzureDevOps` instead of `Cake.AzureDevOps` allow
more control over dependencies in Cake Frosting builds.

## Documentation improvements for Cake Frosting

Examples for Cake Frosting have been added across whole documentation.

## Updating from previous versions

Cake.Issues 5.1.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.1.0)

[Cake.Issues.Tap addin]: ../../documentation/issue-providers/tap/index.md
[Test Anything Protocol (TAP)]: https://testanything.org/
[Cake Issues Recipe]: ../../documentation/recipe/index.md
