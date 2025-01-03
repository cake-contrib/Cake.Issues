---
title: Cake Issues v4.8.0 Released
date: 2024-07-19
categories:
  - Release Notes
search:
  boost: 0.5
---

Cake Issues version 4.8.0 has been released with improvements for SARIF issue provider.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Improvements for SARIF issue provider

In previous releases the SARIF issue provider used the tool name defined in the SARIF file for a specific run as issue provider.
This has the advantage that if multiple SARIF files from different sources are read,
every issue has a different issue provider name, which reflects the original tool.

But it can also have side-effect, for example that different states are reported to pull request depending if run is successful or not
(see [cake-contrib/Cake.Issues.Recipe#477]{target="_blank"}), because the actual issue provider name is only known once there are issues reported.

This release introduces a new option [SarifIssuesSettings.UseToolNameAsIssueProviderName]{target="_blank"} to define whether the tool name reported
in the SARIF log or a fixed value should be used as issue provider name.

## Updating from previous versions

Cake.Issues 4.8.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.8.0){target="_blank"}

[cake-contrib/Cake.Issues.Recipe#477]: https://github.com/cake-contrib/Cake.Issues.Recipe/issues/477
[SarifIssuesSettings.UseToolNameAsIssueProviderName]: https://cakebuild.net/api/Cake.Issues.Sarif/SarifIssuesSettings/16594493
