---
title: Cake Issues v5.2.0 Released
date: 2025-01-09
categories:
  - Release Notes
search:
  boost: 0.5
links:
  - documentation/usage/breaking-builds/breaking-builds.md
  - documentation/issue-providers/sarif/index.md
---

Cake Issues version 5.2.0 has been released bringing improvements to build breaking and SARIF issue provider.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Support for suppressed issues in SARIF files.

SARIF supports [suppressions]{target="_blank"} for issues which are suppressed, either in the source code or through some external tool.

Until now this property was ignored.
Starting with Cake Issues 5.2.0 issues which are marked as suppressed in a SARIF file will no longer be imported by default.
There is a new setting `IgnoreSuppressedIssues` which can be disabled to continue reading suppressed issues.

## Additional alias for build breaking

There is a new alias for fails build if any issues are found with settings to limit to priority and issue provider types to complement
the already existing [aliases for failing builds]{target="_blank"}.

The following example fails build if issues with severity warning or higher from MsBuild are found:

=== "Cake .NET Tool"

    ```csharp
    BreakBuildOnIssues(
        issues,
        new BuildBreakingSettings
        {
            MinimumPriority = IssuePriority.Warning,
            IssueProvidersToConsider = [MsBuildIssuesProviderTypeName]
        });
    ```

=== "Cake Frosting"

    ```csharp
    context.BreakBuildOnIssues(
        issues,
        new BuildBreakingSettings
        {
            MinimumPriority = IssuePriority.Warning,
            IssueProvidersToConsider = [context.MsBuildIssuesProviderTypeName()]
        });
    ```

The following example fails build if issues with severity warning or higher are found, ignoring issues reported by MsBuild:

=== "Cake .NET Tool"

    ```csharp
    BreakBuildOnIssues(
        issues,
        new BuildBreakingSettings
        {
            MinimumPriority = IssuePriority.Warning,
            IssueProvidersToIgnore = [MsBuildIssuesProviderTypeName]
        });
    ```

=== "Cake Frosting"

    ```csharp
    context.BreakBuildOnIssues(
        issues,
        new BuildBreakingSettings
        {
            MinimumPriority = IssuePriority.Warning,
            IssueProvidersToIgnore = [context.MsBuildIssuesProviderTypeName()]
        });
    ```

## Support for failing builds in Cake Issues Recipe

Cake Issues Recipe has new configuration options to support failing of builds if any issues are found:

- `ShouldFailBuildOnIssues`: Indicates whether build should fail if any issues are found
- `MinimumPriority`: Minimum priority of issues considered to fail the build
- `IssueProvidersToConsider`: List of issue provider types to consider
- `IssueProvidersToIgnore`: List of issue provider types to ignore

## Updating from previous versions

Cake.Issues 5.2.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.2.0){target="_blank"}

[suppressions]: https://docs.oasis-open.org/sarif/sarif/v2.1.0/errata01/os/sarif-v2.1.0-errata01-os-complete.html#_Toc141790911
[aliases for failing builds]: https://cakebuild.net/extensions/cake-issues/#Build-Breaking
[Cake Issues Recipe]: ../../documentation/recipe/index.md
