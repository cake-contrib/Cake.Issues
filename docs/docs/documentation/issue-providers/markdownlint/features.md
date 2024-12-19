---
title: Features
description: Features of the Cake.Issues.Markdownlint addin.
---

The [Cake.Issues.Markdownlint addin](https://cakebuild.net/extensions/cake-issues-markdownlint/){target="_blank"}
provides the following features.

## Basic features

* Reads warnings from [Markdownlint](https://github.com/DavidAnson/markdownlint){target="_blank"} logfiles.
* Provides URLs for all issues.
* Support for custom URL resolving using the [MarkdownlintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55){target="_blank"}
  alias (except for [MarkdownlintCliJsonLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F){target="_blank"}).

## Supported log file formats

* [MarkdownlintV1LogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/65609BEB){target="_blank"}
  alias for reading issues from [Markdownlint](https://github.com/DavidAnson/markdownlint){target="_blank"}
  output generated with `options.resultVersion` set to 1.
* [MarkdownlintCliLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/B518F49E){target="_blank"}
  alias for reading issues from [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli){target="_blank"} log files.
* [MarkdownlintCliJsonLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F){target="_blank"}
  alias for reading issues from [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli){target="_blank"}
  log files created with the `--json` parameter.

!!! tip
    [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli){target="_blank"}
    can be run with the [Cake.Markdownlint](https://cakebuild.net/extensions/cake-markdownlint/){target="_blank"}
    addin.

## Supported IIssue properties

|                  | Property                          | Remarks                                 |
|------------------|-----------------------------------|-----------------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                         |
| :material-check: | `IIssue.ProviderName`             |                                         |
|                  | `IIssue.Run`                      | Can be set while reading issues         |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`             |
|                  | `IIssue.ProjectName`              |                                         |
|                  | `IIssue.ProjectFileRelativePath`  |                                         |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                         |
| :material-check: | `IIssue.Line`                     |                                         |
|                  | `IIssue.EndLine`                  |                                         |
| :material-check: | `IIssue.Column`                   | Only for [MarkdownlintCliLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/B518F49E){target="_blank"} |
|                  | `IIssue.EndColumn`                |                                         |
|                  | `IIssue.FileLink`                 | Can be set while reading issues         |
| :material-check: | `IIssue.MessageText`              |                                         |
|                  | `IIssue.MessageHtml`              |                                         |
|                  | `IIssue.MessageMarkdown`          |                                         |
| :material-check: | `IIssue.Priority`                 | Always [IssuePriority.Warning](https://cakebuild.net/api/Cake.Issues/IssuePriority/7A0CE07F){target="_blank"} |
| :material-check: | `IIssue.PriorityName`             | Always `Warning`                        |
| :material-check: | `IIssue.Rule`                     |                                         |
| :material-check: | `IIssue.RuleUrl`                  | Support for custom rules can be added through a custom [MarkdownlintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55){target="_blank"} except for [MarkdownlintCliJsonLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F){target="_blank"} |
