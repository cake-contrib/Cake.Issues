---
title: Features
description: Features of the Cake.Issues.BuildServer.AppVeyor addin.
icon: material/creation-outline
---

The [Cake.Issues.BuildServer.AppVeyor addin] reports issues as messages to AppVeyor builds.

![AppVeyor messages](appveyor-messages.png "AppVeyor messages")

!!! info
    There's a [demo repository] available which you can fork and to which you can create pull requests
    to test the integration functionality.

## Basic features

* [x] Reports issues as messages to AppVeyor builds.
* [x] Messages can be [written as comment to GitHub pull requests].

## Supported capabilities

The [Cake.Issues.BuildServer.AppVeyor addin] doesn't support any additional capabilities.

* [ ] [Checking commit ID](../../how-cake-issues-works/pull-request-integration.md#check-commit-id)
* [ ] [Discussion threads](../../how-cake-issues-works/pull-request-integration.md#handle-existing-discussion-threads)
* [ ] [Filtering by modified files](../../how-cake-issues-works/pull-request-integration.md#filter-issues-by-path)

[demo repository]: https://github.com/pascalberger/Cake.Issues-Demo
[Cake.Issues.BuildServer.AppVeyor addin]: https://cakebuild.net/extensions/cake-issues-buildserver-appveyor/
[written as comment to GitHub pull requests]: examples/github-pullrequest-integration.md
