---
title: Cake Issues v5.4.0 Released
date: 2025-01-16
categories:
  - Release Notes
search:
  boost: 0.5
---

Cake Issues version 5.4.0 has been released bringing improvements for build breaking and multiple issue providers.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger){target="_blank"}

## Improvements for build breaking

This version adds additional overloads to the [build breaking aliases]{target="_blank"} to support passing
in an action which is called before the build is failed.

The following example fails the build if issues with severity warning or higher are found, ignoring MsBuild issues.
In case of the build failing the number of issues is printed:

=== "Cake .NET Tool"

    ```csharp
    BreakBuildOnIssues(
        issues,
        new BuildBreakingSettings
        {
            MinimumPriority = IssuePriority.Warning
            IssueProvidersToIgnore = [MsBuildIssuesProviderTypeName]
        },
        x => Error(
          "{0} issues with severity warning or higher are found",
          x.Count()));
    ```

=== "Cake Frosting"

    ```csharp
    context.BreakBuildOnIssues(
        issues,
        new BuildBreakingSettings
        {
            MinimumPriority = IssuePriority.Warning
            IssueProvidersToIgnore = [context.MsBuildIssuesProviderTypeName()]
        },
        x => context.Error(
          "{0} issues with severity warning or higher are found",
          x.Count()));
    ```

## Improvements for MsBuild issue provider

Improvements have been made for providing rule URLs for issues reported by the MsBuild issue provider:

* For NET SDK analyzers code style rules (`IDE*`) URLs will be provided.
* .NET SDK analyzers code quality rules (`CA*`) will link directly to the rule page instead of starting a Google search

There is also a new [example for using custom URL resolver].

## Improvements for SARIF issue provider

The SARIF issue provider has been improved to also support files which use absolute paths for results.

## Improvements for Test Anything Protocol issue provider

Rule URL resolving has been implemented in the Test Anything Provider for the following log formats:

* `StylelintLogFileFormat` for [rules shipped with stylelint]{target="_blank"}
* `TextlintLogFileFormat` for [rules shipped with Textlint]{target="_blank"}

Starting with this version there also new aliases for providing custom URL resolvers to support plugins or custom rules.

## Documentation updates

There is a new page describing the [process to report issues to build servers and pull requests],
including the available customization options.

## Updating from previous versions

Cake.Issues 5.4.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.4.0){target="_blank"}

[build breaking aliases]: https://cakebuild.net/extensions/cake-issues/#Build-Breaking
[process to report issues to build servers and pull requests]: ../../documentation/how-cake-issues-works/pull-request-integration.md
[example for using custom URL resolver]: ../../documentation/issue-providers/msbuild/examples/use-custom-url-resolver.md
[rules shipped with stylelint]: https://stylelint.io/user-guide/rules
[rules shipped with Textlint]: https://github.com/textlint/textlint/wiki/Collection-of-textlint-rule#rule-list
