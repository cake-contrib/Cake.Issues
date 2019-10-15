---
Order: 50
Title: Configuration
Description: Available parameters to configure Cake.Issues.Recipe.
---

This page lists configuration properties which can be used to define the functionality
and behavior of Cake.Issues.Recipe.

# General

| Property                                           | Default Value    | Description                                                                                                                                              |
|----------------------------------------------------|------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| `IssuesParameters.OutputDirectory`                 | `BuildArtifacts` | Path to the output directory. A relative path will be relative to the current working directory.                                                         |
| `IssuesParameters.BuildIdentifier`                 | `string.Empty`   | Identifier for the build run. If set this identifier will be used to identify to artifacts provided by the build if building on multiple configurations. |

# Input files

| Property                                                      | Default Value | Description                                            |
|---------------------------------------------------------------|---------------|--------------------------------------------------------|
| `IssuesParameters.InputFiles.MsBuildXmlFileLoggerLogFilePath` | `null`        | Path to the MSBuild log file created by XmlFileLogger. |
| `IssuesParameters.InputFiles.MsBuildBinaryLogFilePath`        | `null`        | Path to the MSBuild binary log file.                   |
| `IssuesParameters.InputFiles.InspectCodeLogFilePath`          | `null`        | Path to the JetBrains InspectCoe log file.             |

# Report creation

| Property                                                   | Default Value | Description                                             |
|------------------------------------------------------------|---------------|---------------------------------------------------------|
| `IssuesParameters.Reporting.ShouldCreateFullIssuesReport`  | `true`        | Indicates whether full issues report should be created. |

# Build server integration

| Property                                                       | Default Value | Description                                                                               |
|----------------------------------------------------------------|---------------|-------------------------------------------------------------------------------------------|
| `IssuesParameters.PullRequest.ShouldReportIssuesToBuildServer` | `true`        | Indicates whether issues should be reported to the build server.                          |
| `IssuesParameters.BuildServer.ShouldPublishFullIssuesReport`   | `true`        | Indicates whether full issues report should be published as artifact to the build system. |
| `IssuesParameters.BuildServer.ShouldCreateSummaryIssuesReport` | `true`        | Indicates whether summary issues report should be created.                                |

# Pull request integration

| Property                                                       | Default Value | Description                                                             |
|----------------------------------------------------------------|---------------|-------------------------------------------------------------------------|
| `IssuesParameters.PullRequest.ShouldReportIssuesToPullRequest` | `true`        | Indicates whether issues should be reported to the pull request system. |
| `IssuesParameters.PullRequest.ShouldSetPullRequestStatus`      | `true`        | Indicates whether a status on the pull request should be set.           |
