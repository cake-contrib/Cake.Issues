---
Order: 10
Title: Overview
Description: Overview about Cake.Issues recipes.
---
Cake.Issues recipes provide build scripts, delivered as a NuGet package, which can be used inside your projects Cake build to add issue management.

Integration of code analyzing and linting tools into a build pipeline often looks the similar, and differentiates mainly on the used linters, build and pull request systems.
Cake.Issues recipes contain code to do all the parsing, integration with build and pull request systems for you, using the individual Cake.Issues addins.
They support different linters based on the linting log files you pass it and integrate automatically with different build and pull request systems.

There are two flavors available:

* [Cake.Issues.Recipe]: For [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core]
* [Cake.Frosting.Issues.Recipe]: For [Cake Frosting]

# Supported tools

See [supported tools] for a list of supported linters, build servers and pull request systems.

# Bundled addins

Cake.Issues recipes will add the following addins to your build:

| Addin Cake.Issues.Recipe                                | Addin.Frosting.Issues.Recipe                            | Remarks |
|---------------------------------------------------------|---------------------------------------------------------|-|
| [Cake.Git] 4.0.0                                        | [Cake.Frosting.Git] 4.0.0                               | Only used if `RepositoryInfoProvider` type is set to `RepositoryInfoProviderType.CakeGit`. See [Git repository information configuration] for details. |
| [Cake.Issues] 4.1.0                                     | [Cake.Issues] 4.1.0                                     | |
| [Cake.Issues.MsBuild] 4.1.0                             | [Cake.Frosting.Issues.MsBuild] 4.1.0                    | |
| [Cake.Issues.InspectCode] 4.1.0                         | [Cake.Frosting.Issues.InspectCode] 4.1.0                | |
| [Cake.Issues.Markdownlint] 4.1.0                        | [Cake.Frosting.Issues.Markdownlint] 4.1.0               | |
| [Cake.Issues.EsLint] 4.1.0                              | [Cake.Frosting.Issues.EsLint] 4.1.0                     | |
| [Cake.Issues.Reporting] 4.1.0                           | [Cake.Frosting.Issues.Reporting] 4.1.0                  | |
| [Cake.Issues.Reporting.Generic] 4.0.0                   | [Cake.Frosting.Issues.Reporting.Generic] 4.0.0          | |
| [Cake.Issues.Reporting.Sarif] 4.0.0                     | [Cake.Frosting.Issues.Reporting.Sarif] 4.0.0            | |
| [Cake.Issues.PullRequests] 4.1.0                        | [Cake.Frosting.Issues.PullRequests] 4.1.0               | |
| [Cake.Issues.PullRequests.AppVeyor] 4.0.0               | [Cake.Issues.PullRequests.AppVeyor] 4.0.0               | |
| [Cake.Issues.PullRequests.AzureDevOps] 4.0.0            | [Cake.Issues.PullRequests.AzureDevOps] 4.0.0            | |
| [Cake.Issues.PullRequests.GitHubActions] 4.0.0          | [Cake.Issues.PullRequests.GitHubActions] 4.0.0          | |
| [Cake.AzureDevOps]                                      | [Cake.AzureDevOps]                                      | |

[Cake.Issues.Recipe]: https://www.nuget.org/packages/Cake.Issues.Recipe
[Cake.Frosting.Issues.Recipe]: https://www.nuget.org/packages/Cake.Frosting.Issues.Recipe
[Cake .NET Tool]: https://cakebuild.net/docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: https://cakebuild.net/docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: https://cakebuild.net/docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[supported tools]: supported-tools
[Git repository information configuration]: /docs/recipe/configuration#git-repository-information
[Cake.Git]: https://cakebuild.net/extensions/cake-git/
[Cake.Frosting.Git]: https://cakebuild.net/extensions/cake-git/
[Cake.Issues]: https://cakebuild.net/extensions/cake-issues/
[Cake.Issues.MsBuild]: https://cakebuild.net/extensions/cake-issues-msbuild/
[Cake.Frosting.Issues.MsBuild]: https://cakebuild.net/extensions/cake-issues-msbuild/
[Cake.Issues.InspectCode]: https://cakebuild.net/extensions/cake-issues-inspectcode/
[Cake.Issues.Markdownlint]: https://cakebuild.net/extensions/cake-issues-markdownlint/
[Cake.Issues.EsLint]: https://cakebuild.net/extensions/cake-issues-eslint/
[Cake.Issues.Reporting]: https://cakebuild.net/extensions/cake-issues-reporting/
[Cake.Frosting.Issues.Reporting]: https://cakebuild.net/extensions/cake-issues-reporting/
[Cake.Issues.Reporting.Generic]: https://cakebuild.net/extensions/cake-issues-reporting-generic/
[Cake.Frosting.Issues.Reporting.Generic]: https://cakebuild.net/extensions/cake-issues-reporting-generic/
[Cake.Issues.Reporting.Sarif]: https://cakebuild.net/extensions/cake-issues-reporting-sarif/
[Cake.Frosting.Issues.Reporting.Sarif]: https://cakebuild.net/extensions/cake-issues-reporting-sarif/
[Cake.Issues.PullRequests]: https://cakebuild.net/extensions/cake-issues-pullrequests/
[Cake.Frosting.Issues.PullRequests]: https://cakebuild.net/extensions/cake-issues-pullrequests/
[Cake.Issues.PullRequests.AppVeyor]: https://cakebuild.net/extensions/cake-issues-pullrequests-appveyor/
[Cake.Issues.PullRequests.AzureDevOps]: https://cakebuild.net/extensions/cake-issues-pullrequests-azuredevops/
[Cake.Issues.PullRequests.GitHubActions]: https://cakebuild.net/extensions/cake-issues-pullrequests-githubactions/
[Cake.AzureDevOps]: https://cakebuild.net/extensions/cake-azuredevops/
