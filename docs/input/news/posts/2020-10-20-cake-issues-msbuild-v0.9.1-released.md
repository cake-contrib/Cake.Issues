---
title: Cake Issues MsBuild v0.9.1 Released
date: 2020-10-20
categories:
  - Release Notes
links:
  - documentation/issue-providers/msbuild/index.md
---

Version 0.9.1 of MsBuild support for Cake.Issues has been released.
This is a minor release containing improvements.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [cho-trackman](https://github.com/cho-trackman){target="_blank"}
* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}
* [jokay](https://github.com/jokay){target="_blank"}

## Support for reading of errors

Until now MsBuild support did read warnings from MsBuild log files.
Starting with version 0.9.1 it will also return errors.
Reading of errors has been implemented for `MsBuildBinaryLogFileFormat` and `MsBuildXmlFileLoggerFormat`.
For errors `IIssue.Priority` will be set to `IssuePriority.Error`.

!!! info
    To keep previous behavior result after reading the issues can be filtered for `IIssue.Priority == IIssuePriority.Warning`.

## Updating from previous versions

Cake.Issues.MsBuild 0.9.1 is compatible with version 0.9.0 without any breaking changes.
To update to the new version bump the version of the addin.
