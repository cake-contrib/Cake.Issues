---
title: Cake Issues v0.9.1 Released
category: Release Notes
---

Version 0.9.1 of Cake Issues and Cake.Issues.PullRequests.AzureDevOps have been released.
These are minor releases containing improvements and bug fixes.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann)
* [janniksam](https://github.com/janniksam)
* [pascalberger](https://github.com/pascalberger)
* [x-jokay](https://github.com/x-jokay)

## Easier file linking for manually created issues

With 0.8.0 file links was implemented in Cake.Issues.Reporting.Generic and worked for any issues passed to the report.
With 0.9.0 [file link infrastructure was moved to Cake.Issues].
File link settings can now be set while reading issues, and are passed through the `IIssue.FileLink` property to reports and pull request systems.

While this solution works for issues read by an issue provider, where file link settings can be passed to the `ReadIssues` alias,
it become much more complicated for issues created using the `NewIssue` alias, where an URL can be set, which needs to be resolved manually before.

Cake.Issues 0.9.1 adds an `WithFileLinkSettings` method to `IIssueBuilder` where a file link setting object can be passed which does resolve the URL.

[file link infrastructure was moved to Cake.Issues]: cake-issues-v0.9.0-released#file-linking

## Line range and column support in Azure DevOps pull request integration

Cake Issues 0.9.0 added support for line ranges and column information.
With 0.9.0 the Azure DevOps pull request integration didn't use this information while posting comments to pull requests.
Cake.Issues.PullRequests.AzureDevOps 0.9.1 fixes this and will post comments for line and column ranges if they are available on an issue.

## Updating from previous versions

Cake.Issues and Cake.Issues.PullRequests.AzureDevOps 0.9.1 are compatible with version 0.9.0 without any breaking changes.
To update to the new version bump the version of the specific addins.
