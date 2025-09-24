---
title: Cake Issues v5.9.0 Released
date: 2025-09-24
categories:
  - Release Notes
links:
  - documentation/recipe/index.md
  - documentation/report-formats/console/index.md
  - documentation/issue-providers/msbuild/index.md
---

Cake Issues version 5.9.0 has been released with enhanced GitHub Actions support in Cake Issues Recipe
and bug fixes to console reporting.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Improved GitHub Actions support in Cake Issues Recipe

Cake Issues Recipe will now upload the full issue report as build artifact when running in a GitHub action.
Additionally a [job summary] will be posted.

Both functions can be controlled through [parameters].

## Bug fixes in console reporting

Cake.Issues.Reporting.Console has received a bunch of bug fixes:

* Can handle reporting of issues without a priority
* Issues where the column is reported after the last character will be displayed as issues at the last character
* Logging fixes and improvements

## Updating from previous versions

Cake.Issues 5.9.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.9.0)

[job summary]: https://github.blog/news-insights/product-news/supercharging-github-actions-with-job-summaries/
[parameters]: ../../documentation/recipe/configuration.md
