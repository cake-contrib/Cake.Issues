---
title: Cake Issues v7.0.0 Released
date: 2026-11-15
categories:
  - Release Notes
links:
  - documentation/issue-providers/sarif/index.md
  - documentation/report-formats/sarif/index.md
  - documentation/report-formats/console/index.md
---

Cake Issues version 7.0.0 has been released.
This major release brings Cake 7 support, .NET 11 targeting, SARIF improvements, and bug fixes
across multiple addins.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and
contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

People reporting issues:

* [christianbumann](https://github.com/christianbumann)
* [hotchkj](https://github.com/hotchkj)

## Support for Cake 7 and updated target frameworks

All addins have been updated for Cake 7.x.
Support for .NET 11 has been added, while support for .NET 8 and .NET 9 has been removed.
The supported target frameworks now are .NET 10 and .NET 11.

## SARIF improvements

`Cake.Issues.Sarif` now reads code snippets and source language from SARIF files and makes them
available through the `IIssue.Snippet` and `IIssue.SourceLanguage` properties.

`Cake.Issues.Reporting.Sarif` now writes the rule name to SARIF reports when available.

## Support for markdownlint-cli 0.47+

`Cake.Issues.Markdownlint` now supports the severity keyword introduced in markdownlint-cli 0.47.

## Richer diagnostic reports

The diagnostic template of `Cake.Issues.Reporting.Generic` now also shows the `IIssue.Snippet` and
`IIssue.SourceLanguage` properties.

## Bug fixes

`Cake.Issues.GitRepository` no longer incorrectly reports files which are not checked out in
sparse-checkout repositories as binary files not tracked by Git LFS.

`Cake.Issues.DocFx` now handles log entries without location information correctly, which
previously could result in SARIF reports which failed to upload to GitHub code scanning.

## Improved documentation

The documentation for the Console report format has been improved with screenshots showing the
different output options.

## Updating from previous versions

Cake.Issues 7.0.0 is a breaking release.
To update, bump all Cake.Issues addins to version 7.0.0 and update your Cake version to 7.x.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/7.0.0)
