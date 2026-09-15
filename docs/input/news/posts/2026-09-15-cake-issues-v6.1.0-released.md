---
title: Cake Issues v6.1.0 Released
date: 2026-09-15
categories:
  - Release Notes
links:
  - documentation/issue-providers/docfx/index.md
  - documentation/report-formats/sarif/index.md
---

Cake Issues version 6.1.0 has been released with improved compatibility for newer Cake versions,
updated DocFx support, and dependency updates.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and
contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Compatibility fixes for newer Cake releases

Cake Issues has been updated to use Spectre.Console 0.55.2 and Errata 0.16.0.
This fixes reporting issues when using Cake 6.2.0 and newer.

This release includes an exceptional compatibility-breaking requirement change:
while the 6.1.0 update restores compatibility with newer Cake versions, it now requires at least
Cake 6.2.0 because of the Spectre.Console update and is no longer compatible with Cake 6.0 or 6.1.

## Improved support for DocFx

Cake.Issues.DocFx now supports the current DocFx log field mapping used by DocFx 2.78.5 and newer.
This allows issues from newer DocFx logs to be detected and classified correctly.

## Dependency and documentation updates

Cake.Issues.Reporting.Sarif has been updated to use Sarif.Sdk 4.6.5.

NuGet package READMEs have also been updated to clarify Cake SDK support alongside Cake Frosting.

## Updating from previous versions

Cake.Issues 6.1.0 introduces an exceptional compatibility-breaking minimum Cake requirement.
To use this version, upgrade to Cake 6.2.0 or newer because of the Spectre.Console update, then bump the version of the specific addins.
If you are not yet on Cake 6.2.0 or newer, stay on the latest Cake.Issues release supported by your current Cake version.
For Cake 6.0 or 6.1 this means remaining on Cake.Issues 6.0.0.

For details see [GitHub releases](https://github.com/cake-contrib/Cake.Issues/releases).
