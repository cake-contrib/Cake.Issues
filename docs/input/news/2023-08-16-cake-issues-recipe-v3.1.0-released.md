---
title: Cake Issues Recipes v3.1.0 released
category: Release Notes
---

Version 3.1.0 of Cake Issues recipes have been released adding support for creating of reports in SARIF format.

<!--excerpt-->

This post shows the highlights included in this release.
For details see [full release notes].
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Reports in SARIF format

This version of Cake Issues recipes adds a new configuration parameter which allows to create reports in
[SARIF format] allowing further processing in a lot of different tools also supporting the SARIF standard.
See [Report parameters] for details.

If running on Azure Pipelines the generated SARIF file is uploaded so that it will be
shown in the [SARIF SAST Scans Tab extension].

## Updating from previous versions

Cake Issues recipes 3.1.0 are compatible with version 3.x without any breaking changes.
To update to the new version bump the version in your build.

[full release notes]: https://github.com/cake-contrib/Cake.Issues.Recipe/releases/tag/3.1.0
[Report parameters]: /docs/recipe/configuration#report-creation
[SARIF format]: https://sarifweb.azurewebsites.net/
[SARIF SAST Scans Tab extension]: https://marketplace.visualstudio.com/items?itemName=sariftools.scans
