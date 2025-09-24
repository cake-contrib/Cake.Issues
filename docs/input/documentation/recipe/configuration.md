---
title: Configuration
description: Available parameters to configure Cake.Issues recipes.
---

This page lists configuration properties which can be used to define the functionality
and behavior of Cake.Issues recipes.

## Git repository information

Cake.Issues recipes require some information about current Git repository.

To define the Git provider in `Cake.Issues.Recipe` set the global variable `RepositoryInfoProvider`.
To define the Git provider in `Cake.Frosting.Issues.Recipe` pass the value to the constructor of `IssueContext`.

The following providers are supported:

| Provider                             | Description                                                                                                 |
|--------------------------------------|-------------------------------------------------------------------------------------------------------------|
| `RepositoryInfoProviderType.CakeGit` | Read repository information using [Cake.Git addin](https://cakebuild.net/extensions/cake-git/). Requires system to be compatible with [Cake.Git addin](https://cakebuild.net/extensions/cake-git/). |
| `RepositoryInfoProviderType.Cli`     | Read repository information using Git CLI. Requires Git CLI to be available in path.                        |

By default [Cake.Git addin](https://cakebuild.net/extensions/cake-git/) will be used.

## General

=== "Cake.Issues.Recipe"

    | IssuesParameters Property | Default Value    | Description                                                                                                                                              |
    |---------------------------|------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
    | `OutputDirectory`         | `BuildArtifacts` | Path to the output directory. A relative path will be relative to the current working directory.                                                         |
    | `BuildIdentifier`         | `string.Empty`   | Identifier for the build run. If set this identifier will be used to identify to artifacts provided by the build if building on multiple configurations. |

=== "Cake.Frosting.Issues.Recipe"

    | IssuesContext.Parameters Property | Default Value    | Description                                                                                                                                              |
    |-----------------------------------|------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
    | `OutputDirectory`                 | `BuildArtifacts` | Path to the output directory. A relative path will be relative to the current working directory.                                                         |
    | `BuildIdentifier`                 | `string.Empty`   | Identifier for the build run. If set this identifier will be used to identify to artifacts provided by the build if building on multiple configurations. |

## Input files

=== "Cake.Issues.Recipe"

    | IssuesParameters.InputFiles Methods       | Description                                                                 |
    |-------------------------------------------|-----------------------------------------------------------------------------|
    | `AddMsBuildXmlFileLoggerLogFilePath()`    | Adds a path to a MSBuild log file created by XmlFileLogger.                 |
    | `AddMsBuildXmlFileLoggerLogFileContent()` | Adds content of a MSBuild log file created by XmlFileLogger.                |
    | `AddMsBuildBinaryLogFilePath()`           | Adds a path to a MSBuild binary log file.                                   |
    | `AddMsBuildBinaryLogFileContent()`        | Adds content of a MSBuild binary log file.                                  |
    | `AddInspectCodeLogFilePath()`             | Adds a path to a JetBrains InspectCode log file.                            |
    | `AddInspectCodeLogFileContent()`          | Adds content of a JetBrains InspectCode log file.                           |
    | `AddMarkdownlintCliLogFilePath()`         | Adds a path to a markdownlint-cli log file.                                 |
    | `AddMarkdownlintCliLogFileContent()`      | Adds content of a markdownlint-cli log file.                                |
    | `AddMarkdownlintCliJsonLogFilePath()`     | Adds a path to a markdownlint-cli log file writting with `--json`.          |
    | `AddMarkdownlintCliJsonLogFileContent()`  | Adds content of a markdownlint-cli log file writting with `--json`.         |
    | `AddMarkdownlintV1LogFilePath()`          | Adds a path to a markdownlint log file in version 1.                        |
    | `AddMarkdownlintV1LogFileContent()`       | Adds content of a markdownlint log file in version 1.                       |
    | `AddEsLintJsonLogFilePath()`              | Adds a path to a ESLint log file generated by the [ESLint json formatter](https://eslint.org/docs/user-guide/formatters/#json).  |
    | `AddEsLintJsonLogFileContent()`           | Adds content of a ESLint log file generated by the [ESLint json formatter](https://eslint.org/docs/user-guide/formatters/#json). |
    | `AddSarifLogFilePath()`                   | Adds a path to a SARIF log file.                                            |
    | `AddSarifLogFileContent()`                | Adds content of a SARIF log file.                                           |

=== "Cake.Frosting.Issues.Recipe"

    | IssuesContext.Parameters.InputFiles Methods | Description                                                                 |
    |---------------------------------------------|-----------------------------------------------------------------------------|
    | `AddMsBuildXmlFileLoggerLogFilePath()`      | Adds a path to a MSBuild log file created by XmlFileLogger.                 |
    | `AddMsBuildXmlFileLoggerLogFileContent()`   | Adds content of a MSBuild log file created by XmlFileLogger.                |
    | `AddMsBuildBinaryLogFilePath()`             | Adds a path to a MSBuild binary log file.                                   |
    | `AddMsBuildBinaryLogFileContent()`          | Adds content of a MSBuild binary log file.                                  |
    | `AddInspectCodeLogFilePath()`               | Adds a path to a JetBrains InspectCode log file.                            |
    | `AddInspectCodeLogFileContent()`            | Adds content of a JetBrains InspectCode log file.                           |
    | `AddMarkdownlintCliLogFilePath()`           | Adds a path to a markdownlint-cli log file.                                 |
    | `AddMarkdownlintCliLogFileContent()`        | Adds content of a markdownlint-cli log file.                                |
    | `AddMarkdownlintCliJsonLogFilePath()`       | Adds a path to a markdownlint-cli log file writting with `--json`.          |
    | `AddMarkdownlintCliJsonLogFileContent()`    | Adds content of a markdownlint-cli log file writting with `--json`.         |
    | `AddMarkdownlintV1LogFilePath()`            | Adds a path to a markdownlint log file in version 1.                        |
    | `AddMarkdownlintV1LogFileContent()`         | Adds content of a markdownlint log file in version 1.                       |
    | `AddEsLintJsonLogFilePath()`                | Adds a path to a ESLint log file generated by the [ESLint json formatter](https://eslint.org/docs/user-guide/formatters/#json).  |
    | `AddEsLintJsonLogFileContent()`             | Adds content of a ESLint log file generated by the [ESLint json formatter](https://eslint.org/docs/user-guide/formatters/#json). |
    | `AddSarifLogFilePath()`                     | Adds a path to a SARIF log file.                                            |
    | `AddSarifLogFileContent()`                  | Adds content of a SARIF log file.                                           |

## Report creation

=== "Cake.Issues.Recipe"

    | IssuesParameters.Reporting Property | Default Value                                                                                        | Description                                                                                |
    |-------------------------------------|------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
    | `ShouldCreateFullIssuesReport`      | `true`                                                                                               | Indicates whether full issues report should be created.                                    |
    | `FullIssuesReportSettings`          | `GenericIssueReportTemplate.HtmlDxDataGrid` template with `DevExtremeTheme.MaterialBlueLight` theme. | Settings for creating the full issues report. See [Template Gallery] for possible options. |
    | `ShouldCreateSarifReport`           | `true`                                                                                               | Indicates whether a report in SARIF format should be created.                              |
    | `ShouldReportIssuesToConsole`       | `false`                                                                                              | Indicates whether issues should be reported to the console.                                |
    | `ReportToConsoleSettings`           |                                                                                                      | Settings for reporting issues to the console.                                              |

=== "Cake.Frosting.Issues.Recipe"

    | IssuesContext.Parameters.Reporting Property | Default Value                                                                                        | Description                                                                                |
    |---------------------------------------------|------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
    | `ShouldCreateFullIssuesReport`              | `true`                                                                                               | Indicates whether full issues report should be created.                                    |
    | `FullIssuesReportSettings`                  | `GenericIssueReportTemplate.HtmlDxDataGrid` template with `DevExtremeTheme.MaterialBlueLight` theme. | Settings for creating the full issues report. See [Template Gallery] for possible options. |
    | `ShouldCreateSarifReport`                   | `true`                                                                                               | Indicates whether a report in SARIF format should be created.                              |
    | `ShouldReportIssuesToConsole`               | `false`                                                                                              | Indicates whether issues should be reported to the console.                                |
    | `ReportToConsoleSettings`                   |                                                                                                      | Settings for reporting issues to the console.                                              |

[Template Gallery]: ../report-formats/generic/templates/index.md

## Build server integration

=== "Cake.Issues.Recipe"

    | IssuesParameters.BuildServer Property | Default Value | Description                                                                                    |
    |---------------------------------------|---------------|------------------------------------------------------------------------------------------------|
    | `ShouldReportIssuesToBuildServer`     | `true`        | Indicates whether issues should be reported to the build server.                               |
    | `ShouldPublishFullIssuesReport`       | `true`        | Indicates whether full issues report should be published as artifact to the build system.      |
    | `ShouldPublishSarifReport`            | `true`        | Indicates whether report int SARIF format shoudl be published as artifact to the build system. |
    | `ShouldCreateSummaryIssuesReport`     | `true`        | Indicates whether summary issues report should be created.                                     |

=== "Cake.Frosting.Issues.Recipe"

    | IssuesContext.Parameters.BuildServer Property | Default Value | Description                                                                                    |
    |-----------------------------------------------|---------------|------------------------------------------------------------------------------------------------|
    | `ShouldReportIssuesToBuildServer`             | `true`        | Indicates whether issues should be reported to the build server.                               |
    | `ShouldPublishFullIssuesReport`               | `true`        | Indicates whether full issues report should be published as artifact to the build system.      |
    | `ShouldPublishSarifReport`                    | `true`        | Indicates whether report int SARIF format shoudl be published as artifact to the build system. |
    | `ShouldCreateSummaryIssuesReport`             | `true`        | Indicates whether summary issues report should be created.                                     |

## Pull request integration

=== "Cake.Issues.Recipe"

    | IssuesParameters.PullRequestSystem Property                    | Default Value | Description                                                                                                                                                                                                                              |
    |----------------------------------------------------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
    | `ShouldReportIssuesToPullRequest`                              | `true`        | Indicates whether issues should be reported to the pull request system.                                                                                                                                                                  |
    | `MaxIssuesToPost`                                              | `null`        | Global number of issues which should be posted at maximum over all issue provider. Issues are filtered by priority and issues with a file path are prioritized. `null` won't set a global limit.                                         |
    | `MaxIssuesToPostAcrossRuns`                                    | `null`        | Global number of issues which should be posted at maximum over all issue providers and across multiple runs. Issues are filtered by priority and issues with a file path are prioritized. `null` won't set a limit across multiple runs. |
    | `MaxIssuesToPostForEachIssueProvider`                          | `100`         | Number of issues which should be posted at maximum for each issue provider. Issues are filtered by priority and issues with a file path are prioritized. `null` won't limit issues per issue provider.                                   |
    | `ProviderIssueLimits`                                          | Empty         | Issue limits for individual issue provider. The key must be the `IIssue.ProviderType` of a specific provider to which the limits should be applied to.                                                                                   |
    | `IssueFilters`                                                 | Empty         | List of filter functions which should be applied before posting issues to pull requests.                                                                                                                                                 |
    | `ShouldSetPullRequestStatus`                                   | `true`        | Indicates whether a status on the pull request should be set if there are any issues found.                                                                                                                                              |
    | `ShouldSetSeparatePullRequestStatusForEachIssueProviderAndRun` | `true`        | Indicates whether a separate status should be set for issues of every issue provider and run.                                                                                                                                            |

=== "Cake.Frosting.Issues.Recipe"

    | IssuesContext.Parameters.PullRequestSystem Property            | Default Value | Description                                                                                                                                                                                                                              |
    |----------------------------------------------------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
    | `ShouldReportIssuesToPullRequest`                              | `true`        | Indicates whether issues should be reported to the pull request system.                                                                                                                                                                  |
    | `MaxIssuesToPost`                                              | `null`        | Global number of issues which should be posted at maximum over all issue provider. Issues are filtered by priority and issues with a file path are prioritized. `null` won't set a global limit.                                         |
    | `MaxIssuesToPostAcrossRuns`                                    | `null`        | Global number of issues which should be posted at maximum over all issue providers and across multiple runs. Issues are filtered by priority and issues with a file path are prioritized. `null` won't set a limit across multiple runs. |
    | `MaxIssuesToPostForEachIssueProvider`                          | `100`         | Number of issues which should be posted at maximum for each issue provider. Issues are filtered by priority and issues with a file path are prioritized. `null` won't limit issues per issue provider.                                   |
    | `ProviderIssueLimits`                                          | Empty         | Issue limits for individual issue provider. The key must be the `IIssue.ProviderType` of a specific provider to which the limits should be applied to.                                                                                   |
    | `IssueFilters`                                                 | Empty         | List of filter functions which should be applied before posting issues to pull requests.                                                                                                                                                 |
    | `ShouldSetPullRequestStatus`                                   | `true`        | Indicates whether a status on the pull request should be set if there are any issues found.                                                                                                                                              |
    | `ShouldSetSeparatePullRequestStatusForEachIssueProviderAndRun` | `true`        | Indicates whether a separate status should be set for issues of every issue provider and run.                                                                                                                                            |

## Build breaking

=== "Cake.Issues.Recipe"

    | IssuesParameters.BuildBreaking Property | Default Value             | Description                                                                                                                  |
    |-----------------------------------------|---------------------------|------------------------------------------------------------------------------------------------------------------------------|
    | `ShouldFailBuildOnIssues`               | `false`                   | Indicates whether build should fail if any issues are found.                                                                 |
    | `MinimumPriority`                       | `IssuePriority.Undefined` | The minimum priority of issues considered to fail the build. If set to `IssuePriority.Undefined`, all issues are considered. |
    | `IssueProvidersToConsider`              | `[]`                      | List of issue provider types to consider.                                                                                    |
    | `IssueProvidersToIgnore`                | `[]`                      | List of issue provider types to ignore.                                                                                      |

=== "Cake.Frosting.Issues.Recipe"

    | IssuesContext.Parameters.BuildBreaking Property | Default Value             | Description                                                                                                                  |
    |-------------------------------------------------|---------------------------|------------------------------------------------------------------------------------------------------------------------------|
    | `ShouldFailBuildOnIssues`                       | `false`                   | Indicates whether build should fail if any issues are found.                                                                 |
    | `MinimumPriority`                               | `IssuePriority.Undefined` | The minimum priority of issues considered to fail the build. If set to `IssuePriority.Undefined`, all issues are considered. |
    | `IssueProvidersToConsider`                      | `[]`                      | List of issue provider types to consider.                                                                                    |
    | `IssueProvidersToIgnore`                        | `[]`                      | List of issue provider types to ignore.                                                                                      |

