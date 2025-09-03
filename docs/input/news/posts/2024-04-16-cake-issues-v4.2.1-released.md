---
title: Cake Issues v4.2.1 Released
date: 2024-04-16
categories:
  - Release Notes
---

Cake Issues version 4.2.1 has been released with compatibility fixes.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Compatibility improvements

As a side-effect of moving all addins to the central Cake Issues repository, `AssemblyVersion` of every
has been set to the release version.

Addins are backwards compatible though to latest major version, which was no longer possible with this change.

This release fixes this by setting the `AssemblyVersion` to the major version (currently `4.0.0`).

## Updating from previous versions

Cake.Issues 4.2.1 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.2.1)
