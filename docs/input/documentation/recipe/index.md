---
title: Overview
description: Overview about Cake.Issues recipes.
---

Cake.Issues recipes provide build scripts, delivered as a NuGet package, which can be used inside your projects Cake build to add issue management.

Integration of code analyzing and linting tools into a build pipeline often looks the similar, and differentiates mainly on the used linters, build and pull request systems.
Cake.Issues recipes contain code to do all the parsing, integration with build and pull request systems for you, using the individual Cake.Issues addins.
They support different linters based on the linting log files you pass it and integrate automatically with different build and pull request systems.

There are two flavors available:

<div class="grid cards" markdown>

-   :material-receipt-text: [Cake.Issues.Recipe]

    ---

    For [Cake .NET Tool]

-   :material-receipt-text: [Cake.Frosting.Issues.Recipe]

    ---

    For [Cake Frosting]

</div>

## Supported tools

See [supported tools] for a list of supported linters, build servers and pull request systems.

## Bundled addins

Cake.Issues recipes will add the following addins to your build:

=== "Cake.Issues.Recipe"

    | Addin                                                     | Version                   |  Remarks |
    |-----------------------------------------------------------|---------------------------|-|
    | [Cake.Git]                               | 5.0.1                     | Only used if `RepositoryInfoProvider` type is set to `RepositoryInfoProviderType.CakeGit`. See [Git repository information configuration] for details. |
    | [Cake.Issues]                            | {{ cake_issues_version }} | |
    | [Cake.Issues.MsBuild]                    | {{ cake_issues_version }} | |
    | [Cake.Issues.InspectCode]                | {{ cake_issues_version }} | |
    | [Cake.Issues.Markdownlint]               | {{ cake_issues_version }} | |
    | [Cake.Issues.EsLint]                     | {{ cake_issues_version }} | |
    | [Cake.Issues.Sarif]                      | {{ cake_issues_version }} | |
    | [Cake.Issues.Reporting]                  | {{ cake_issues_version }} | |
    | [Cake.Issues.Reporting.Generic]          | {{ cake_issues_version }} | |
    | [Cake.Issues.Reporting.Sarif]            | {{ cake_issues_version }} | |
    | [Cake.Issues.PullRequests]               | {{ cake_issues_version }} | |
    | [Cake.Issues.PullRequests.AppVeyor]      | {{ cake_issues_version }} | |
    | [Cake.Issues.PullRequests.AzureDevOps]   | {{ cake_issues_version }} | |
    | [Cake.Issues.PullRequests.GitHubActions] | {{ cake_issues_version }} | |
    | [Cake.AzureDevOps]                       | 5.0.1                     | |

=== "Cake.Frosting.Issues.Recipe"

    | Addin                                                              | Version                   | Remarks |
    |--------------------------------------------------------------------|---------------------------|-|
    | [Cake.Frosting.Git]                               | 5.0.1                     | Only used if `RepositoryInfoProvider` type is set to `RepositoryInfoProviderType.CakeGit`. See [Git repository information configuration] for details. |
    | [Cake.Issues]                                     | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.MsBuild]                    | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.InspectCode]                | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.Markdownlint]               | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.EsLint]                     | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.Sarif]                      | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.Reporting]                  | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.Reporting.Generic]          | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.Reporting.Sarif]            | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.PullRequests]               | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.PullRequests.AppVeyor]      | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.PullRequests.AzureDevOps]   | {{ cake_issues_version }} | |
    | [Cake.Frosting.Issues.PullRequests.GitHubActions] | {{ cake_issues_version }} | |
    | [Cake.Frosting.AzureDevOps]                       | 5.0.1                     | |

[Cake.Issues.Recipe]: https://www.nuget.org/packages/Cake.Issues.Recipe
[Cake.Frosting.Issues.Recipe]: https://www.nuget.org/packages/Cake.Frosting.Issues.Recipe
[Cake .NET Tool]: https://cakebuild.net/docs/running-builds/runners/dotnet-tool
[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[supported tools]: supported-tools.md
[Git repository information configuration]: configuration.md#git-repository-information
[Cake.Git]: https://cakebuild.net/extensions/cake-git/
[Cake.Frosting.Git]: https://cakebuild.net/extensions/cake-git/
[Cake.Issues]: https://cakebuild.net/extensions/cake-issues/
[Cake.Issues.MsBuild]: https://cakebuild.net/extensions/cake-issues-msbuild/
[Cake.Frosting.Issues.MsBuild]: https://cakebuild.net/extensions/cake-issues-msbuild/
[Cake.Issues.InspectCode]: https://cakebuild.net/extensions/cake-issues-inspectcode/
[Cake.Frosting.Issues.InspectCode]: https://cakebuild.net/extensions/cake-issues-inspectcode/
[Cake.Issues.Markdownlint]: https://cakebuild.net/extensions/cake-issues-markdownlint/
[Cake.Frosting.Issues.Markdownlint]: https://cakebuild.net/extensions/cake-issues-markdownlint/
[Cake.Issues.EsLint]: https://cakebuild.net/extensions/cake-issues-eslint/
[Cake.Frosting.Issues.EsLint]: https://cakebuild.net/extensions/cake-issues-eslint/
[Cake.Issues.Sarif]: https://cakebuild.net/extensions/cake-issues-sarif/
[Cake.Frosting.Issues.Sarif]: https://cakebuild.net/extensions/cake-issues-sarif/
[Cake.Issues.Reporting]: https://cakebuild.net/extensions/cake-issues-reporting/
[Cake.Frosting.Issues.Reporting]: https://cakebuild.net/extensions/cake-issues-reporting/
[Cake.Issues.Reporting.Generic]: https://cakebuild.net/extensions/cake-issues-reporting-generic/
[Cake.Frosting.Issues.Reporting.Generic]: https://cakebuild.net/extensions/cake-issues-reporting-generic/
[Cake.Issues.Reporting.Sarif]: https://cakebuild.net/extensions/cake-issues-reporting-sarif/
[Cake.Frosting.Issues.Reporting.Sarif]: https://cakebuild.net/extensions/cake-issues-reporting-sarif/
[Cake.Issues.PullRequests]: https://cakebuild.net/extensions/cake-issues-pullrequests/
[Cake.Frosting.Issues.PullRequests]: https://cakebuild.net/extensions/cake-issues-pullrequests/
[Cake.Issues.PullRequests.AppVeyor]: https://cakebuild.net/extensions/cake-issues-pullrequests-appveyor/
[Cake.Frosting.Issues.PullRequests.AppVeyor]: https://cakebuild.net/extensions/cake-issues-pullrequests-appveyor/
[Cake.Issues.PullRequests.AzureDevOps]: https://cakebuild.net/extensions/cake-issues-pullrequests-azuredevops/
[Cake.Frosting.Issues.PullRequests.AzureDevOps]: https://cakebuild.net/extensions/cake-issues-pullrequests-azuredevops/
[Cake.Issues.PullRequests.GitHubActions]: https://cakebuild.net/extensions/cake-issues-pullrequests-githubactions/
[Cake.Frosting.Issues.PullRequests.GitHubActions]: https://cakebuild.net/extensions/cake-issues-pullrequests-githubactions/
[Cake.AzureDevOps]: https://cakebuild.net/extensions/cake-azuredevops/
[Cake.Frosting.AzureDevOps]: https://cakebuild.net/extensions/cake-azuredevops/
