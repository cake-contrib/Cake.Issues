---
title: Cake Issues v4.7.1 Released
date: 2024-07-17
categories:
  - Release Notes
links:
  - documentation/issue-providers/sarif/index.md
---

Cake Issues version 4.7.1 has been released with bugfixes for SARIF reports

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann)
* [eoehen](https://github.com/eoehen)
* [pascalberger](https://github.com/pascalberger)

## Bugfixes for SARIF reports

This release fixes two bugs in `Cake.Issues.Reporting.Sarif`:

* If two or more different rules containing a rule URL are reported by the same issue provider,
  the first occurrence of the second rule will have `0` as `ruleIndex` instead of `1`
* `originalUriBaseIds` should end with a slash as defined in [SARIF Specification 2.1.0 §3.14.14].

## Updating from previous versions

Cake.Issues 4.7.1 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.7.1)

[SARIF Specification 2.1.0 §3.14.14]: https://docs.oasis-open.org/sarif/sarif/v2.1.0/os/sarif-v2.1.0-os.html#_Toc34317431
