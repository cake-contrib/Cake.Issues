---
title: Features
description: Features of the Cake.Issues.EsLint addin.
---

The [Cake.Issues.EsLint addin] provides the following features.

## Basic features

* Reads issues reported by ESLint.
* Provides URLs for all issues.
* Support for custom URL resolving using the [EsLintAddRuleUrlResolver] alias.

## Supported log file formats

* [EsLintJsonFormat] alias for reading issues from log files created by [ESLint json formatter].

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
| :material-check: | `IIssue.RuleUrl`                  | Support for custom rules can be added through a custom [EsLintAddRuleUrlResolver] |

[Cake.Issues.EsLint addin]: https://www.nuget.org/packages/Cake.Issues.EsLint
[ESLint json formatter]: https://eslint.org/docs/user-guide/formatters/#json
[EsLintAddRuleUrlResolver]: ../../../api/Cake.Issues.EsLint/EsLintIssuesAliases/D64301E6
[EsLintJsonFormat]: ../../../api/Cake.Issues.EsLint/EsLintIssuesAliases/230C6E27
