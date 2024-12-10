---
title: Cake Issues Markdownlint v1.1.0 Released
category: Release Notes
---

Version 1.1.0 of Markdownlint support for Cake.Issues has been released.
This is a minor release adding features and improvements.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Support for markdownlint-cli JSON format

Since version 0.28.0 markdownlint-cli supports a `--json` option to output result in JSON format.
This version adds support for this format through the [MarkdownlintCliJsonLogFileFormat] alias.

## Provide column information

This release of Cake.Issues.Markdownlint enhances the [MarkdownlintCliLogFileFormat] to provide column information
if reported by markdownlint.

## Recipe packages

[Cake Issues recipes] have been released in version 1.3.0 shipping with Cake.Issues.Markdownlint 1.1.0 and
adding support for markdownlint-cli JSON files.

## Updating from previous versions

Cake.Issues.Markdownlint 1.1.0 is compatible with version 1.0.0 without any breaking changes.
To update to the new version bump the version of the addin.

[MarkdownlintCliJsonLogFileFormat]: /api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F
[MarkdownlintCliLogFileFormat]: /api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/B518F49E
[Cake Issues recipes]: /docs/recipe/overview