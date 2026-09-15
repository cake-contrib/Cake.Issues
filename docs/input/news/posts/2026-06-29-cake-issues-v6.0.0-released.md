---
title: Cake Issues v6.0.0 Released
date: 2026-06-29
categories:
  - Release Notes
links:
  - documentation/build-servers/index.md
  - documentation/issue-providers/markdownlint/index.md
  - documentation/extending/issue-provider/overview.md
---

Cake Issues version 6.0.0 has been released.
This major release brings Cake 6 support, .NET 10 targeting, and new capabilities across multiple addins.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and
contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Support for Cake 6 and updated target frameworks

All addins have been updated for Cake 6.x.
The supported target frameworks now include .NET 8, .NET 9, and .NET 10.

## New Cake.Issues.Build addin

This release introduces the new `Cake.Issues.Build` addin, expanding options for integrating
Cake Issues into build workflows.
Build server integrations for AppVeyor and GitHub Actions have been aligned with the
`Cake.Issues.BuildServer` architecture.
This change clarifies the separation of responsibilities: `Cake.Issues.PullRequests` for pull
request integration and `Cake.Issues.BuildServer` for build server integration.
Previously, build servers were also implemented as pull request systems, which was more confusing
for users.

## Richer issue metadata

The core issue model now includes `IIssue.Snippet` and `IIssue.SourceLanguage`.
This enables issue providers and report formats to surface richer contextual information.

## Markdownlint 0.46.0 support

`Cake.Issues.Markdownlint` now supports Markdownlint 0.46.0 format.

## Additional improvements

This release also includes centralized file path validation for issue providers and dependency
updates across addins.

## Updating from previous versions

Cake.Issues 6.0.0 is a breaking release.
To update, bump all Cake.Issues addins to version 6.0.0 and update your Cake version to 6.x.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/6.0.0)
