---
title: Supported tools
description: Tools supported by Cake.Issues recipes.
---

Cake.Issues recipes support reading issues from different tools and integrates with different build and pull request systems.

## Tools

Cake.Issues recipes support reading issues from output of the following tools:

=== "Cake.Issues.Recipe"

    | Tool                              | Format                                                  | IssuesParameters.InputFiles Method  |
    |-----------------------------------|---------------------------------------------------------|-------------------------------------|
    | MsBuild                           | [MSBuild Extension Pack XmlFileLogger]{target="_blank"} | `AddMsBuildXmlFileLoggerLogFile*()` |
    | MsBuild                           | Binary Log File                                         | `AddMsBuildBinaryLogFile*()`        |
    | JetBrains InspectCode (ReSharper) | [xml]{target="_blank"}                                  | `AddInspectCodeLogFile*()`          |
    | markdownlint                      | [markdownlint-cli]{target="_blank"} default format      | `AddMarkdownlintCliLogFile*()`      |
    | markdownlint                      | [markdownlint-cli]{target="_blank"} with `--json`       | `AddMarkdownlintCliJsonLogFile*()`  |
    | markdownlint                      | [markdownlint]{target="_blank"} version 1               | `AddMarkdownlintV1LogFile*()`       |
    | [ESLint]                          | [json formatter]{target="_blank"}                       | `AddEsLintJsonLogFile*()`           |
    | Any SARIF compatible tool         | [SARIF]{target="_blank"}                                | `AddSarifLogFile*()`                |

=== "Cake.Frosting.Issues.Recipe"

    | Tool                              | Format                                                  | IssuesContext.Parameters.InputFiles Method |
    |-----------------------------------|---------------------------------------------------------|--------------------------------------------|
    | MsBuild                           | [MSBuild Extension Pack XmlFileLogger]{target="_blank"} | `AddMsBuildXmlFileLoggerLogFile*()`        |
    | MsBuild                           | Binary Log File                                         | `AddMsBuildBinaryLogFile*()`               |
    | JetBrains InspectCode (ReSharper) | [xml]{target="_blank"}                                  | `AddInspectCodeLogFile*()`                 |
    | markdownlint                      | [markdownlint-cli]{target="_blank"} default format      | `AddMarkdownlintCliLogFile*()`             |
    | markdownlint                      | [markdownlint-cli]{target="_blank"} with `--json`       | `AddMarkdownlintCliJsonLogFile*()`         |
    | markdownlint                      | [markdownlint]{target="_blank"} version 1               | `AddMarkdownlintV1LogFile*()`              |
    | [ESLint]                          | [json formatter]{target="_blank"}                       | `AddEsLintJsonLogFile*()`                  |
    | Any SARIF compatible tool         | [SARIF]{target="_blank"}                                | `AddSarifLogFile*()`                       |

!!! tip

    See [Supported tools](../supported-tools.md) for a list of tools supporting the SARIF format.

[MSBuild Extension Pack XmlFileLogger]: https://github.com/mikefourie-zz/MSBuildExtensionPack/blob/master/Solutions/Main/Loggers/Framework/XmlFileLogger.cs
[xml]: https://www.jetbrains.com/help/resharper/InspectCode.html#alternative-output-formats
[markdownlint-cli]: https://github.com/igorshubovych/markdownlint-cli
[markdownlint]: https://github.com/DavidAnson/markdownlint
[ESLint]: https://eslint.org/
[json formatter]: https://eslint.org/docs/user-guide/formatters/#json
[SARIF]: https://sarifweb.azurewebsites.net/

## Build systems

Cake.Issues recipes integrates with the following build systems:

=== "AppVeyor"

    <div class="annotate" markdown>

    - [x] Write issues to build server
    - [ ] Issues summary
    - [x] SARIF report
    - [x] Full issues report

    </div>

=== "Azure Pipelines"

    <div class="annotate" markdown>

    - [x] Write issues to build server (1)
    - [x] Issues summary
    - [x] SARIF report
    - [x] Full issues report

    </div>

    1.  Only first 10

=== "GitHub Actions"

    <div class="annotate" markdown>

    - [x] Write issues to build server
    - [x] Issues summary
    - [x] SARIF report (1)
    - [x] Full issues report

    </div>

    1.  Uploaded to [GitHub code scanning](https://docs.github.com/en/code-security/code-scanning/introduction-to-code-scanning/about-code-scanning){target="_blank"}

## Pull request systems

Cake.Issues recipes integrates with the following pull request systems:

=== "Azure Repos"

    <div class="annotate" markdown>

    - [x] Write issues to pull requests
    - [x] Set pull request status

    </div>

=== "GitHub"

    <div class="annotate" markdown>

    - [x] Write issues to pull requests (1)
    - [x] Set pull request status (2)

    </div>

    1.  When build from GitHub Actions
    2.  Requires `statuses: write` permission
