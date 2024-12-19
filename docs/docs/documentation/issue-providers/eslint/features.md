---
title: Features
description: Features of the Cake.Issues.EsLint addin.
---

The [Cake.Issues.EsLint addin](https://cakebuild.net/extensions/cake-issues-eslint/){target="_blank"} provides the following features.

## Basic features

* Reads issues reported by ESLint.
* Provides URLs for all issues.
* Support for custom URL resolving using the [EsLintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.EsLint/EsLintIssuesAliases/0F6CCE21){target="_blank"}
  alias.

## Supported log file formats

* [EsLintJsonFormat](https://cakebuild.net/api/Cake.Issues.EsLint/EsLintIssuesAliases/230C6E27){target="_blank"}
  alias for reading issues from log files created by
  [ESLint json formatter](https://eslint.org/docs/user-guide/formatters/#json){target="_blank"}.

## Supported IIssue properties

|                  | Property                          | Remarks                         |
|------------------|-----------------------------------|---------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                 |
| :material-check: | `IIssue.ProviderName`             |                                 |
|                  | `IIssue.Run`                      | Can be set while reading issues |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`     |
|                  | `IIssue.ProjectName`              |                                 |
|                  | `IIssue.ProjectFileRelativePath`  |                                 |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                 |
| :material-check: | `IIssue.Line`                     |                                 |
|                  | `IIssue.EndLine`                  |                                 |
| :material-check: | `IIssue.Column`                   |                                 |
|                  | `IIssue.EndColumn`                |                                 |
| :material-check: | `IIssue.MessageText`              |                                 |
|                  | `IIssue.MessageHtml`              |                                 |
|                  | `IIssue.MessageMarkdown`          |                                 |
| :material-check: | `IIssue.Priority`                 |                                 |
| :material-check: | `IIssue.PriorityName`             |                                 |
| :material-check: | `IIssue.Rule`                     |                                 |
| :material-check: | `IIssue.RuleUrl`                  | Support for custom rules can be added through a custom [EsLintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.EsLint/EsLintIssuesAliases/0F6CCE21){target="_blank"} |
