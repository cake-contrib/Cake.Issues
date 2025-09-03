---
title: Cake Issues v4.4.0 Released
date: 2024-05-18
categories:
  - Release Notes
links:
  - documentation/report-formats/generic/templates/htmldxdatagrid.md
---

Cake Issues version 4.4.0 has been released with improvements for Cake Frosting.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen)
* [pascalberger](https://github.com/pascalberger)

## Cake Frosting improvements

Support for implicit usings has been added to the Cake Frosting addins.

If `<ImplicitUsings>enable</ImplicitUsings>` is set in a Cake Frosting build, namespaces for Cake Issues addins are
implicitly added, resulting in a similar experience as when using Cake .NET Tool, where aliases can be used directly
without the requirements to first add `using` statements.

## Filtering improvements in HTML DevExtreme Data Grid template

The [HTML DevExtreme Data Grid template] now supports search boxes in filter dropdowns.

See [Header Filter Customization in DataGrid](https://js.devexpress.com/jQuery/New/23_1/#Data-Filtering-UI-Customization)
for details.

## Updating from previous versions

Cake.Issues 4.4.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.4.0)

[HTML DevExtreme Data Grid template]: ../../documentation/report-formats/generic/templates/htmldxdatagrid.md
