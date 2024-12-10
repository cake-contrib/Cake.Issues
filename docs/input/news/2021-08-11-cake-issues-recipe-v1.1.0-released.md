---
title: Cake Issues Recipes v1.1.0 released
category: Release Notes
---

Version 1.1.0 of Cake Issues recipes have been released adding support to customize report generation.

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Report customization

This version of Cake Issues recipes adds a new `FullIssuesReportSettings` configuration parameter which allows
to customize the generated issue report.
See [Report creation parameters] for details.

The following example enables exporting of the report when using Cake.Issues.Recipe:

```csharp
IssuesParameters.Reporting.FullIssuesReportSettings
    .WithOption(HtmlDxDataGridOption.EnableExporting, true)
```

The following example enables exporting of the report when using Cake.Frosting.Issues.Recipe:

```csharp
context.Parameters.Reporting.FullIssuesReportSettings
    .WithOption(HtmlDxDataGridOption.EnableExporting, true)
```

## Updated addins

`Cake.Git` has been updated to version 1.1.0 which comes with an updated version of LibGit2Sharp which adds support for Ubuntu 20.

See [Cake.Git 1.1.0 release notes] for details.

## Updating from previous versions

Cake Issues recipes 1.1.0 are compatible with version 1.x without any breaking changes.
To update to the new version bump the version in your build.

[Report creation parameters]: https://cakeissues.net/docs/recipe/configuration#report-creation
[Cake.Git 1.1.0 release notes]: https://github.com/cake-contrib/Cake_Git/releases/tag/v1.1.0