---
title: Cake Issues v5.8.0 Released
date: 2025-08-22
categories:
  - Release Notes
links:
  - documentation/report-formats/console/index.md
  - documentation/issue-providers/msbuild/index.md
  - documentation/recipe/index.md
---

Cake Issues version 5.8.0 has been released with enhancements to console reporting and bug fixes for Cake Issues Recipe.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Displaying of project when reporting to console

The console reporter has been extended so that beside the file it also shows the project, if available.

## Bug fixes in Cake Issues Recipe

Cake Issues Recipe has receive a bunch of bug fixes:

* Support build projects which are not in a direct sub directory of the project
* Don't try to upload to GitHub code scanning if it is not available
* Reported GitHub pull request status not always visible on pull request

## Updating from previous versions

Cake.Issues 5.8.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.8.0)
