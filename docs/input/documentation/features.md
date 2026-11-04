---
title: Features
description: Overview about core features.
---

## Ready To Use Distributions

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards" markdown>

*   :material-arrow-collapse-all:{ .lg .middle } **Cake Recipe Packages**

    ---

    Cake Issues recipes provide build scripts, delivered as a NuGet package,
    which can be used inside your projects Cake build to add issue management.

    It handles all the parsing, integration with build and pull request systems for you,
    using the individual Cake Issues addins.

    [:octicons-arrow-right-24: Recipes](recipe/index.md)

</div>
<!-- markdownlint-restore -->

## Reading & Creating Issues

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards" markdown>

*   :material-import:{ .lg .middle } **Read issues provided by tools**

    ---

    The `ReadIssues` aliases can be used for reading issues reported by a linter to tool using an
    [issue provider].

    There are overloads for reading using a single or multiple [issue provider].

    [:octicons-arrow-right-24: Reading issues](usage/reading-issues/reading-issues.md)

*   :material-creation-outline:{ .lg .middle } **Create issues in your build**

    ---

    The `NewIssue` aliases can be used for creating issues in the build script.

    [:octicons-arrow-right-24: Creating issues](usage/creating-issues/creating-issues.md)

*   :material-file-link:{ .lg .middle } **Support for file links**

    ---

    Support for creating links to file & location on source code hosting system
    (GitHub, Azure Repos, etc).

    [:octicons-arrow-right-24: Linking to file repositories](usage/reading-issues/file-linking.md)

*   :material-swap-vertical-bold:{ .lg .middle } **Issue serialization**

    ---

    Support for serializing and deserializing created issues and issues read from tools.

    [:octicons-arrow-right-24: Aliases](https://cakebuild.net/extensions/cake-issues/#Issue-Serialization)

*   :material-format-title:{ .lg .middle } **Support for multiple message formats**

    ---

    Support for reading issues in multiple formats (Plain text, Markdown, HTML) if supported by
    [issue provider].

*   :material-information:{ .lg .middle } **Support for run information**

    ---

    Support for passing additional run information to identify specific runs.

</div>
<!-- markdownlint-restore -->

## Breaking builds

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards" markdown>

*   :material-exclamation:{ .lg .middle } **Fail builds on reported issues**

    ---

    The `BreakBuildOnIssues` aliases can be used for failing builds if specific issues were reported.

    There are overloads for failing if issues of certain minimum priority or issue providers are found,
    or by passing any custom function.

    [:octicons-arrow-right-24: Failing builds](usage/breaking-builds/breaking-builds.md)

</div>
<!-- markdownlint-restore -->

## Reporting

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards" markdown>

*   :material-monitor-dashboard:{ .lg .middle } **Create reports**

    ---

    The `CreateIssueReport` aliases can be used for creating reports in a supported [reporting format].

    There are overloads for reading issues from a single or multiple [issue provider] or for passing
    an existing list of issues.

    [:octicons-arrow-right-24: Creating reports](usage/creating-reports/creating-reports.md)

</div>
<!-- markdownlint-restore -->

## Build & Pull Request System Integration

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards" markdown>

*   :material-comment-text:{ .lg .middle } **Add comments to pull requests**

    ---

    The `ReportIssuesToPullRequest` aliases can be used for writing issues as comments to [pull requests].

    There are overloads for reading issues from a single or multiple [issue provider] or for passing
    an existing list of issues.

    [:octicons-arrow-right-24: Reporting issues to pull request systems](usage/reporting-issues-to-pull-requests/report-issues-to-pull-requests.md)

*   :material-message-plus:{ .lg .middle } **Report issues to build runs**

    ---

    The `ReportIssuesToPullRequest` aliases can be used for reporting issues to [build runs].

    There are overloads for reading issues from a single or multiple [issue provider] or for passing
    an existing list of issues.

    [:octicons-arrow-right-24: Reporting issues to build servers](usage/reporting-issues-to-pull-requests/report-issues-to-pull-requests.md)

*   :material-filter:{ .lg .middle } **Issue filters**

    ---

    Support for passing custom issue filter routines.

    [:octicons-arrow-right-24: Using custom issue filter](usage/reporting-issues-to-pull-requests/custom-issue-filter.md)

*   :material-car-speed-limiter:{ .lg .middle } **Limit reported issues**

    ---

    Advanced support to limit number of maximum issues per run, across multiple runs or per issue
    provider through settings.

    [:octicons-arrow-right-24: Settings](https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/)

*   :material-comment-check:{ .lg .middle } **Automatic comment resolving**

    ---

    If supported by the [pull request system], comments for issues are automatic resolved if fixed
    in subsequent commits.

</div>
<!-- markdownlint-restore -->

[issue provider]: issue-providers/index.md
[reporting format]: report-formats/index.md
[pull requests]: pull-request-systems/index.md
[build runs]: pull-request-systems/index.md
[pull request system]: pull-request-systems/index.md
