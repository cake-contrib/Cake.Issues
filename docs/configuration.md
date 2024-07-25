---
Order: 50
Title: Configuration
Description: Available parameters to configure Cake.Issues recipes.
---

This page lists configuration properties which can be used to define the functionality
and behavior of Cake.Issues recipes.

# Git repository information

Cake.Issues recipes require some information about current Git repository.

To define the Git provider in `Cake.Issues.Recipe` set the global variable `RepositoryInfoProvider`.
To define the Git provider in `Cake.Frosting.Issues.Recipe` pass the value to the constructor of `IssueContext`.

The following providers are supported:

| Provider                             | Description                                                                                                 |
|--------------------------------------|-------------------------------------------------------------------------------------------------------------|
| `RepositoryInfoProviderType.CakeGit` | Read repository information using [Cake.Git addin]. Requires system to be compatible with [Cake.Git addin]. |
| `RepositoryInfoProviderType.Cli`     | Read repository information using Git CLI. Requires Git CLI to be available in path.                        |

By default [Cake.Git addin] will be used.

# General

| Cake.Issues.Recipe Property                                       | Cake.Frosting.Issues.Recipe Property                                      | Default Value    | Description                                                                                                                                              |
|-------------------------------------------------------------------|---------------------------------------------------------------------------|------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| `IssuesParameters.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`OutputDirectory` | `IssuesContext.Parameters.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`OutputDirectory` | `BuildArtifacts` | Path to the output directory. A relative path will be relative to the current working directory.                                                         |
| `IssuesParameters.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`BuildIdentifier` | `IssuesContext.Parameters.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`BuildIdentifier` | `string.Empty`   | Identifier for the build run. If set this identifier will be used to identify to artifacts provided by the build if building on multiple configurations. |

# Input files

| Cake.Issues.Recipe Methods                                                                    | Cake.Frosting.Issues.Recipe Methods                                                                   | Description                                                                |
|-----------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------|
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildXmlFileLoggerLogFile()` | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildXmlFileLoggerLogFile()` | Adds a path to a MSBuild log file created by XmlFileLogger.                |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildBinaryLogFile()`        | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildBinaryLogFile()`        | Adds a path to a MSBuild binary log file.                                  |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddInspectCodeLogFile()`          | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddInspectCodeLogFile()`          | Adds a path to a JetBrains InspectCode log file.                           |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliLogFile()`      | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliLogFile()`      | Adds a path to a markdownlint-cli log file.                                |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliJsonLogFile()`  | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliJsonLogFile()`  | Adds a path to a markdownlint-cli log file writting with `--json`.         |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintV1LogFile()`       | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintV1LogFile()`       | Adds a path to a markdownlint log file in version 1.                       |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddEsLintJsonLogFile()`           | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddEsLintJsonLogFile()`           | Adds a path to a ESLint log file generated by the [ESLint json formatter]. |
| `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddSarifLogFile()`                | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddSarifLogFile()`                | Adds a path to a SARIF log file.                                           |

[ESLint json formatter]: https://eslint.org/docs/user-guide/formatters/#json

# Report creation

| Cake.Issues.Recipe Property                                                               | Cake.Frosting.Issues.Recipe Property                                                             | Default Value                                                                                        | Description                                                                                |
|-------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| `IssuesParameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldCreateFullIssuesReport`  | `IssuesContext.Parameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldCreateFullIssuesReport` | `true`                                                                                               | Indicates whether full issues report should be created.                                    |
| `IssuesParameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`FullIssuesReportSettings`      | `IssuesContext.Parameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`FullIssuesReportSettings`     | `GenericIssueReportTemplate.HtmlDxDataGrid` template with `DevExtremeTheme.MaterialBlueLight` theme. | Settings for creating the full issues report. See [Template Gallery] for possible options. |
| `IssuesParameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldCreateSarifReport`       | `IssuesContext.Parameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldCreateSarifReport`      | `true`                                                                                               | Indicates whether a report in SARIF format should be created.                              |
| `IssuesParameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldReportIssuesToConsole`   | `IssuesContext.Parameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldReportIssuesToConsole`  | `false`                                                                                              | Indicates whether issues should be reported to the console.                                |
| `IssuesParameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportToConsoleSettings`       | `IssuesContext.Parameters.Reporting.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ReportToConsoleSettings`      |                                                                                                      | Settings for reporting issues to the console.                                              |

[Template Gallery]: /docs/report-formats/generic/templates/

# Build server integration

| Cake.Issues.Recipe Property                                                                   | Cake.Frosting.Issues.Recipe Property                                                                  | Default Value | Description                                                                                    |
|-----------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------|---------------|------------------------------------------------------------------------------------------------|
| `IssuesParameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldReportIssuesToBuildServer` | `IssuesContext.Parameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldReportIssuesToBuildServer` | `true`        | Indicates whether issues should be reported to the build server.                               |
| `IssuesParameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldPublishFullIssuesReport`   | `IssuesContext.Parameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldPublishFullIssuesReport`   | `true`        | Indicates whether full issues report should be published as artifact to the build system.      |
| `IssuesParameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldPublishSarifReport`        | `IssuesContext.Parameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldPublishSarifReport`        | `true`        | Indicates whether report int SARIF format shoudl be published as artifact to the build system. |
| `IssuesParameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldCreateSummaryIssuesReport` | `IssuesContext.Parameters.BuildServer.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldCreateSummaryIssuesReport` | `true`        | Indicates whether summary issues report should be created.                                     |

# Pull request integration

| Cake.Issues.Recipe Property                                                                                                      | Cake.Frosting.Issues.Recipe Property                                                                                                     | Default Value | Description                                                                                                                                                                                                                              |
|----------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldReportIssuesToPullRequest`                              | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldReportIssuesToPullRequest`                              | `true`        | Indicates whether issues should be reported to the pull request system.                                                                                                                                                                  |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`MaxIssuesToPost`                                              | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`MaxIssuesToPost`                                              | `null`        | Global number of issues which should be posted at maximum over all issue provider. Issues are filtered by priority and issues with a file path are prioritized. `null` won't set a global limit.                                         |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`MaxIssuesToPostAcrossRuns`                                    | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`MaxIssuesToPostAcrossRuns`                                    | `null`        | Global number of issues which should be posted at maximum over all issue providers and across multiple runs. Issues are filtered by priority and issues with a file path are prioritized. `null` won't set a limit across multiple runs. |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`MaxIssuesToPostForEachIssueProvider`                          | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`MaxIssuesToPostForEachIssueProvider`                          | `100`         | Number of issues which should be posted at maximum for each issue provider. Issues are filtered by priority and issues with a file path are prioritized. `null` won't limit issues per issue provider.                                   |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ProviderIssueLimits`                                          | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ProviderIssueLimits`                                          | Empty         | Issue limits for individual issue provider. The key must be the `IIssue.ProviderType` of a specific provider to which the limits should be applied to.                                                                                   |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`IssueFilters`                                                 | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`IssueFilters`                                                 | Empty         | List of filter functions which should be applied before posting issues to pull requests.                                                                                                                                                 |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldSetPullRequestStatus`                                   | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldSetPullRequestStatus`                                   | `true`        | Indicates whether a status on the pull request should be set if there are any issues found.                                                                                                                                              |
| `IssuesParameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldSetSeparatePullRequestStatusForEachIssueProviderAndRun` | `IssuesContext.Parameters.PullRequestSystem.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`ShouldSetSeparatePullRequestStatusForEachIssueProviderAndRun` | `true`        | Indicates whether a separate status should be set for issues of every issue provider and run.                                                                                                                                            |

[Cake.Git addin]: https://cakebuild.net/extensions/cake-git/
