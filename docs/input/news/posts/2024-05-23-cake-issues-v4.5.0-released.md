---
title: Cake Issues v4.5.0 Released
date: 2024-05-23
categories:
  - Release Notes
links:
  - documentation/usage/breaking-builds/breaking-builds.md
  - documentation/report-formats/generic/templates/htmldxdatagrid.md
---

Cake Issues version 4.5.0 has been released adding support for [build breaking].

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}
* [Speeedy01](https://github.com/Speeedy01){target="_blank"}

## Support for breaking builds

New [BreakBuildOnIssues aliases]{target="_blank"} have been introduced to fail builds when issues are found.

See [build breaking] for an example.

## Possibility to define  license for HTML DevExtreme Data Grid

Starting with version `23.2` DevExtreme no longer comes with a free community license.
[HTML DevExtreme Data Grid template] will stay on version `23.1` for this reason.

If you have a DevExtreme license you can pass it to the new `DevExtremeLicenseKey` option
and update to a newer version using the `DevExtremeVersion` option.

## Updating from previous versions

Cake.Issues 4.5.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.5.0){target="_blank"}

[build breaking]: ../../documentation/usage/breaking-builds/breaking-builds.md
[BreakBuildOnIssues aliases]: https://cakebuild.net/extensions/cake-issues/#Build-Breaking
[HTML DevExtreme Data Grid template]: ../../documentation/report-formats/generic/templates/htmldxdatagrid.md
