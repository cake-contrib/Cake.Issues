---
title: Cake Issues v4.9.0 Released
date: 2024-07-25
categories:
  - Release Notes
links:
  - documentation/issue-providers/sarif/index.md
  - documentation/issue-providers/terraform/index.md
---

Cake Issues version 4.9.0 has been released with bugfixes for SARIF report format and Terraform issue provider.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann)
* [eoehen](https://github.com/eoehen)
* [pascalberger](https://github.com/pascalberger)

## Bugfixes for SARIF report format

This release fixes an issue where entries in a SARIF report were marked as updated if branch or commit in file link has changed.

To achieve this, a [new constructor] for the generic `IIssueComparer` has been introduced, which allows to define
which `IIssue` properties should be ignored for the comparison.

## Bugfixes for Terraform issue provider

A bug has been fixed where root directory was not correctly determined when running on Linux or macOS.

## Updating from previous versions

Cake.Issues 4.9.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.9.0)

[new constructor]: https://cakebuild.net/api/Cake.Issues/IIssueComparer/0089D7CF
