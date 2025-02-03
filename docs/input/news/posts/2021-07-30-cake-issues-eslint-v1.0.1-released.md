---
title: Cake Issues ESLint v1.0.1 Released
date: 2021-07-30
categories:
  - Release Notes
search:
  boost: 0.5
links:
  - documentation/issue-providers/eslint/index.md
---

Version 1.0.1 of ESLint support for Cake.Issues has been released.
This is a minor release containing bug fixes.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}
* [Speeedy01](https://github.com/Speeedy01){target="_blank"}

## Bug fix for issues with line, column or rule

When an issue reported by ESLint didn't contain line or column information, or rule was set to `null`
an exception ocurred while parsing the file.
This release fixes this and can now also correctly parse issues without position or rule information.

## Updating from previous versions

Cake.Issues.EsLint 1.0.1 is compatible with version 1.0.0 without any breaking changes.
To update to the new version bump the version of the addin.
