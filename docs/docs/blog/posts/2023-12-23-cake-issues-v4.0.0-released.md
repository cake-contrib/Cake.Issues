---
title: Cake Issues v4.0.0 Released
date: 2023-12-23
categories:
  - Release Notes
---

Cake Issues version 4.0.0 has been released.
This is a major release, containing breaking changes beside bringing new features and bug fixes across all addins.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Support for Cake 4.0

All addins have been updated to support Cake 4.x.

Target framework have been updated to .NET 6, .NET 7 and .NET 8 to be in line with Cake.

## Switch to System.Text.Json for serialization

For serialization / deserialization of issues [LitJson](https://litjson.net/){target="_blank"} was used internally.
With Cake.Issues 4.0 internal code has been changed to use System.Text.Json classes.

The change should not have any impact for users.

## Updating from previous versions

While Cake.Issues 4.0.0 is a breaking release, there are no breaking changes beside the update to Cake 4.x and
the changes to target framework version.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.0.0)
