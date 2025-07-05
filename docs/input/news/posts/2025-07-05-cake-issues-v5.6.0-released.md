---
title: Cake Issues v5.6.0 Released
date: 2025-07-05
categories:
  - Release Notes
links:
  - documentation/issue-providers/msbuild/index.md
  - documentation/report-formats/generic/templates/htmldxdatagrid.md
---

Cake Issues version 5.6.0 has been released bringing updates to dependencies.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Improvements for MsBuild binary logs

[MsBuild.StructuredLogger]{target='_blank'} has been updated to version 2.3.17.

## Improvements for HTML reports

The PDF export functionallity in the [HtmlDxDataGrid template] has been updated to use [jsPDF 3.0.0]{target='_blank'} and [jsPDF-AutoTable 5.0.0]{target='_blank'} by default.

## Updating from previous versions

Cake.Issues 5.6.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.6.0){target="_blank"}

[MsBuild.StructuredLogger]: https://github.com/KirillOsenkov/MSBuildStructuredLog
[HtmlDxDataGrid template]: ../../documentation/report-formats/generic/templates/htmldxdatagrid.md
[jsPDF 3.0.0]: https://github.com/parallax/jsPDF/releases/tag/v3.0.0
[jsPDF-AutoTable 5.0.0]: https://github.com/simonbengtsson/jsPDF-AutoTable/releases/tag/v5.0.0
