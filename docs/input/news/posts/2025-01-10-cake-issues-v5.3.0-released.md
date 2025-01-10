---
title: Cake Issues v5.3.0 Released
date: 2025-01-10
categories:
  - Release Notes
search:
  boost: 0.5
---

Cake Issues version 5.3.0 has been released bringing improvements to Test Anything Protocol issue provider.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Improvements for Test Anything Protocol issue provider

This version fixes an issue where TAP files created by [stylelint]{target="_blank"} containing
absolute paths could not be read.

There are also cases where [stylelint]{target="_blank"} would write invalid YAML content into the TAP file.
The parser now tries to handle the case where double quotes are not escaped in a YAML block,
which is the case if a message from a [stylelint]{target="_blank"} rule contains double quotes.

Starting with this release the [stylelint]{target="_blank"} log format for the TAP issue provider
will also skip issues with a path outside of the defined repository root path.

## Updating from previous versions

Cake.Issues 5.3.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.3.0){target="_blank"}

[stylelint]: https://stylelint.io/