---
title: Cake Issues v4.2.0 Released
date: 2024-04-14
categories:
  - Release Notes
links:
  - documentation/issue-providers/sarif/index.md
---

Cake Issues version 4.2.0 has been released introducing a new issue provider for SARIF compatible files.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen)
* [pascalberger](https://github.com/pascalberger)

## New issue provider for SARIF files

A new [Cake.Issues.Sarif addin] has been released which adds support for reading issues in [SARIF] format.

See [New addin for reading SARIF files](2024-04-14-sarif-issue-provider.md) for details.

## Improvement for Cake Frosting

Optimized versions for Cake Frosting have been released for the following addins:

* [Cake.Frosting.Issues.DocFx]
* [Cake.Frosting.Issues.EsLint]
* [Cake.Frosting.Issues.GitRepository]
* [Cake.Frosting.Issues.InspectCode]
* [Cake.Frosting.Issues.Markdownlint]
* [Cake.Frosting.Issues.Terraform]
* [Cake.Frosting.Issues.PullRequests.AppVeyor]
* [Cake.Frosting.Issues.PullRequests.AzureDevOps]
* [Cake.Frosting.Issues.PullRequests.GitHubActions]

These addins come with a dependency to the core addins, allowing the core addins to be consumed as transitive dependencies.

## Alignment of release lifecycle

As announced in [Alignment of addin lifecycles](2024-01-14-align-addin-lifecycle.md) work as started to move
addins into the main Cake Issues repository.

Starting with this release the following addins will be released together with the core addins:

* Cake.Issues.GitRepository
* Cake.Issues.Terraform
* Cake.Issues.PullRequests.AppVeyor
* Cake.Issues.PullRequests.AzureDevOps
* Cake.Issues.PullRequsts.GitHubActions
* Cake.Issues.Reporting.Console
* Cake.Issues.Reporting.Generic
* Cake.Issues.Reporting.Sarif

## Updating from previous versions

Cake.Issues 4.2.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.2.0)

[Cake.Issues.Sarif addin]: ../../documentation/issue-providers/sarif/index.md
[SARIF]: https://sarifweb.azurewebsites.net/
[Cake.Frosting.Issues.DocFx]: https://www.nuget.org/packages/Cake.Frosting.Issues.DocFx
[Cake.Frosting.Issues.EsLint]: https://www.nuget.org/packages/Cake.Frosting.Issues.EsLint
[Cake.Frosting.Issues.GitRepository]: https://www.nuget.org/packages/Cake.Frosting.Issues.GitRepository
[Cake.Frosting.Issues.InspectCode]: https://www.nuget.org/packages/Cake.Frosting.Issues.InspectCode
[Cake.Frosting.Issues.Markdownlint]: https://www.nuget.org/packages/Cake.Frosting.Issues.Markdownlint
[Cake.Frosting.Issues.Terraform]: https://www.nuget.org/packages/Cake.Frosting.Issues.Terraform
[Cake.Frosting.Issues.PullRequests.AppVeyor]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AppVeyor
[Cake.Frosting.Issues.PullRequests.AzureDevOps]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AzureDevOps
[Cake.Frosting.Issues.PullRequests.GitHubActions]: https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.GitHubActions
