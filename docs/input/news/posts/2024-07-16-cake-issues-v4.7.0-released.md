---
title: Cake Issues v4.7.0 Released
date: 2024-07-16
categories:
  - Release Notes
links:
  - documentation/issue-providers/msbuild/index.md
  - documentation/issue-providers/sarif/index.md
---

Cake Issues version 4.7.0 has been released with detailed line information for SARIF issue provider and 
support for MsBuild binary log format version 21.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Support for MsBuild binary logs version 21

Support for binary logs in version 21 has been added to `Cake.Issues.MsBuild`.

## Enhanced line and column information from SARIF reports

`Cake.Issues.Sarif` has been enhanced to also provide the following `IIssue` properties if available:

- [x] `IIssue.EndLine`
- [x] `IIssue.Column`
- [x] `IIssue.EndColumn`

## Updating from previous versions

Cake.Issues 4.7.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.7.0){target="_blank"}
