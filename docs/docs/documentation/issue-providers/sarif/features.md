---
title: Features
description: Features of the Cake.Issues.Sarif addin.
---

The [Cake.Issues.Sarif addin](https://cakebuild.net/extensions/cake-issues-terraform/){target="_blank"} provides the following features:

## Basic features

* Reads issues from files in [SARIF](https://sarifweb.azurewebsites.net/){target="_blank"} format.

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
| :material-check: | `IIssue.MessageMarkdown`          |                                  |
| :material-check: | `IIssue.Priority`                 |                                  |
| :material-check: | `IIssue.PriorityName`             |                                  |
| :material-check: | `IIssue.Rule`                     |                                  |
| :material-check: | `IIssue.RuleUrl`                  |                                  |
