---
title: How Cake Issues Works
description: Overview about the architecture of Cake Issues.
---

The Cake Issues addins are built in a modular architecture, allowing to easily
enhance it for supporting additional analyzers, linters, report formats and pull request systems.

![](./assets/overview.drawio)

## Read and create issues

The `Cake.Issues` addin provides aliases for creating issues or reading issues using one or more issue providers.

Support for different code analyzers and linters is provided through [issue provider] addins
which cover a wide range of [linters and tools].

The issues are read into [IIssue](https://cakebuild.net/api/Cake.Issues/IIssue/){target="_blank"} objects
which then can be passed to [Cake.Issues.Reporting addin](#create-issue-reports),
[Cake.Issues.PullRequests addin](#write-issues-to-pull-requests-and-build-servers) or further processed in the build script.

The use of [issue provider] addins, which contain the parsing logic for individual tool output formats,
and the use of [IIssue](https://cakebuild.net/api/Cake.Issues/IIssue/){target="_blank"} as common data structure,
allows to abstract the tooling output from other concerns like integration with
build systems, pull request workflow or the creation of reports.

## Create issue reports

The `Cake.Issues.Reporting` addin provides aliases for creating reports for issues
which are read or have been created using the [Cake.Issues addin](#read-and-create-issues).

Support for different report formats is provided through [report format] addins.

## Write issues to pull requests and build servers

The `Cake.Issues.PullRequests` addin provides aliases for reporting issues
which are read or have been created using the [Cake.Issues addin](#read-and-create-issues)
as comments to pull requests or builds.

Support for different pull request systems is provided through [pull request system] addins.

!!! tip
    See [Pull Request Integration] for details how integration with build servers and
    pull request systems works.

[issue provider]: ../issue-providers/index.md
[linters and tools]: ../supported-tools.md
[report format]: ../report-formats/index.md
[pull request system]: ../pull-request-systems/index.md
[Pull Request Integration]: pull-request-integration.md
