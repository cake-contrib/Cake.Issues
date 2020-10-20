---
title: Cake Issues PullRequests v0.9.1 Released
category: Release Notes
---

Version 0.9.1 of Cake.Issues.PullRequests has been released.
This is a minor releases containing bug fixes.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [janniksam](https://github.com/janniksam)
* [pascalberger](https://github.com/pascalberger)
* [phlorian](https://github.com/phlorian)

## Don't post issues if a provider limit is set to 0

This version fixes a bug where if `MaxIssuesToPost` or `MaxIssuesToPostAcrossRuns` was set to 0 all issues were posted to the pull request instead of none.

## Updating from previous versions

Cake.Issues.PullRequests 0.9.1 is compatible with version 0.9.0 without any breaking changes.
To update to the new version bump the version of the addin.
