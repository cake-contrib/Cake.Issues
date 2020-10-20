---
title: Cake Issues MsBuild v0.9.1 Released
category: Release Notes
---

Version 0.9.1 of MsBuild support for Cake.Issues has been released.
This is a minor release containing improvements.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [cho-trackman](https://github.com/cho-trackman)
* [eoehen](https://github.com/eoehen)
* [pascalberger](https://github.com/pascalberger)
* [x-jokay](https://github.com/x-jokay)

## Support for reading of errors

Until now MsBuild support did read warnings from MsBuild log files.
Starting with version 0.9.1 it will also return errors.
Reading of errors has been implemented for `MsBuildBinaryLogFileFormat` and `MsBuildXmlFileLoggerFormat`.
For errors `IIssue.Priority` will be set to `IssuePriority.Error`.

:::{.alert .alert-info}
To keep previous behavior result after reading the issues can be filtered for `IIssue.Priority == IIssuePriority.Warning`.
:::

## Updating from previous versions

Cake.Issues.MsBuild 0.9.1 is compatible with version 0.9.0 without any breaking changes.
To update to the new version bump the version of the addin.
