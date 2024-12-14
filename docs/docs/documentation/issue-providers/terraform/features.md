---
title: Features
description: Features of the Cake.Issues.Terraform addin.
---

The [Cake.Issues.Terraform addin] provides the following features:

## Basic features

* Reads warnings from [Terraform validate command].

## Supported IIssue properties

|                  | Property                          | Remarks                          |
|------------------|-----------------------------------|----------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                  |
| :material-check: | `IIssue.ProviderName`             |                                  |
|                  | `IIssue.Run`                      | Can be set while reading issues  |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`      |
|                  | `IIssue.ProjectName`              |                                  |
|                  | `IIssue.ProjectFileRelativePath`  |                                  |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                  |
| :material-check: | `IIssue.Line`                     |                                  |
| :material-check: | `IIssue.EndLine`                  |                                  |
| :material-check: | `IIssue.Column`                   |                                  |
| :material-check: | `IIssue.EndColumn`                |                                  |
|                  | `IIssue.FileLink`                 | Can be set while reading issues  |
| :material-check: | `IIssue.MessageText`              |                                  |
|                  | `IIssue.MessageHtml`              |                                  |
|                  | `IIssue.MessageMarkdown`          |                                  |
| :material-check: | `IIssue.Priority`                 |                                  |
| :material-check: | `IIssue.PriorityName`             |                                  |
| :material-check: | `IIssue.Rule`                     |                                  |
|                  | `IIssue.RuleUrl`                  |                                  |

[Terraform validate command]: https://www.terraform.io/docs/cli/commands/validate.html
[Cake.Issues.Terraform addin]: https://cakebuild.net/extensions/cake-issues-terraform/
