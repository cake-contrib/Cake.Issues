---
title: Supported tools
description: Tools supported by Cake.Issues recipes.
---

Cake.Issues recipes support reading issues from different tools and integrates with different build and pull request systems.

## Tools

Cake.Issues recipes support reading issues from output of the following tools:

| Tool                              | Format                                 | Cake.Issues.Recipe Methods                                                                     | Cake.Frosting.Issues.Recipe Method                                                                     |
|-----------------------------------|----------------------------------------|------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------|
| MsBuild                           | [MSBuild Extension Pack XmlFileLogger] | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildXmlFileLoggerLogFile*()` | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildXmlFileLoggerLogFile*()` |
| MsBuild                           | Binary Log File                        | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildBinaryLogFile*()`        | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMsBuildBinaryLogFile*()`        |
| JetBrains InspectCode (ReSharper) |                                        | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddInspectCodeLogFile*()`          | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddInspectCodeLogFile*()`          |
| markdownlint                      | [markdownlint-cli]                     | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliLogFile*()`      | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliLogFile*()`      |
| markdownlint                      | [markdownlint-cli] with `--json`       | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliJsonLogFile*()`  | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintCliJsonLogFile*()`  |
| markdownlint                      | [markdownlint] version 1               | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintV1LogFile*()`       | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddMarkdownlintV1LogFile*()`       |
| [ESLint]                          | [json formatter]                       | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddEsLintJsonLogFile*()`           | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddEsLintJsonLogFile*()`           |
| Any SARIF compatible tool         | [SARIF]                                | `IssuesParameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddSarifLogFile*()`                | `IssuesContext.Parameters.InputFiles.`<br/>&nbsp;&nbsp;&nbsp;&nbsp;`AddSarifLogFile*()`                |

[MSBuild Extension Pack XmlFileLogger]: http://www.msbuildextensionpack.com/help/4.0.5.0/html/242ab4fd-c2e2-f6aa-325b-7588725aed24.htm
[markdownlint-cli]: https://github.com/igorshubovych/markdownlint-cli
[markdownlint]: https://github.com/DavidAnson/markdownlint
[ESLint]: https://eslint.org/
[json formatter]: https://eslint.org/docs/user-guide/formatters/#json
[SARIF]: https://sarifweb.azurewebsites.net/

## Build systems

Cake.Issues recipes integrates with the following build systems:

| Build System                   | Write issues to build server     | Issues summary     | Full issues report |
|--------------------------------|----------------------------------|--------------------|--------------------|
| AppVeyor                       | :material-check:                 |                    | :material-check:   |
| Azure Pipelines (Azure DevOps) | :material-check: (Only first 10) | :material-check:   | :material-check:   |
| GitHub Actions                 | :material-check:                 |                    |                    |

## Pull request systems

Cake.Issues recipes integrates with the following pull request systems:

| Pull Request System        | Write issues to pull requests                     | Set pull request status |
|----------------------------|---------------------------------------------------|-------------------------|
| Azure Repos (Azure DevOps) | :material-check:                                  | :material-check:        |
| GitHub                     | :material-check: (When build from GitHub Actions) |                         |
