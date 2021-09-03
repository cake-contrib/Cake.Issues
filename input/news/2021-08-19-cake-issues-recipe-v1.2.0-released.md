---
title: Cake Issues Recipes v1.2.0 released
category: Release Notes
---

Version 1.2.0 of Cake Issues recipes have been released adding support to customize issue reporting to pull requests.

<!--excerpt-->

This post shows the highlights included in this release.
For details see [full release notes].
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Report customization

This version of Cake Issues recipes adds several new configuration parameter which allows
to customize how issues are reported to pull requests.
See [Pull request integration parameters] for details.

The following example limits the number of issues posted to pull requests to `20` when using Cake.Issues.Recipe:

```csharp
IssuesParameters.PullRequestSystem.MaxIssuesToPost = 20;
```

The following example limits the number of issues posted to pull requests to `20` when using Cake.Frosting.Issues.Recipe:

```csharp
context.Parameters.PullRequestSystem.MaxIssuesToPost = 20;
```

## Updating from previous versions

Cake Issues recipes 1.2.0 are compatible with version 1.x without any breaking changes.
To update to the new version bump the version in your build.

[Pull request integration parameters]: /docs/recipe/configuration#pull-request-integration
[full release notes]: https://github.com/cake-contrib/Cake.Issues.Recipe/releases/tag/1.2.0
