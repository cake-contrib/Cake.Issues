---
title: Alignment of addin lifecycles
date: 2024-01-14
categories:
  - Announcements
search:
  boost: 0.5
---

Cake Issues has a [modular architecture] consisting of multiple addins.
Historically every addin had its own independent release lifecycle.
Starting with the next release work will begin to have all addins share the same release lifecycle.

<!-- more -->

When Cake Issues started all three core addins (`Cake.Issues`, `Cake.Issues.Reporting` and `Cake.Issues.PullRequsts`)
and every issue provider, report format and pull request system addin were released on their own schedule,
allowing fast iterations of individual components.
With the [release of Cake Issues 1.0](2021-07-28-cake-issues-v1.0.0-released.md#simplified-release-process) the
release lifecycle of the three core addins have been aligned, resulting in a simplified release process.

Starting with the next release we'll begin to move also the remaining issue provider, report format and
pull request system addins into the main Cake Issues repository and have them released together with the core addins.

[modular architecture]: ../../documentation/how-cake-issues-works/index.md
