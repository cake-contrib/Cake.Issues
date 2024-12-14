---
title: Features
description: Features of the Cake.Issues.InspectCode addin.
---

The [Cake.Issues.InspectCode addin] provides the following features:

## Basic features

* Reads warnings from [JetBrains InsepectCode] log files.
* Provides URLs for issues containing a Wiki URL.

## Supported IIssue properties

|                  | Property                          | Remarks                          |
|------------------|-----------------------------------|----------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                  |
| :material-check: | `IIssue.ProviderName`             |                                  |
|                  | `IIssue.Run`                      | Can be set while reading issues  |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`      |
| :material-check: | `IIssue.ProjectName`              |                                  |
|                  | `IIssue.ProjectFileRelativePath`  |                                  |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                  |
| :material-check: | `IIssue.Line`                     |                                  |
|                  | `IIssue.EndLine`                  |                                  |
|                  | `IIssue.Column`                   |                                  |
|                  | `IIssue.EndColumn`                |                                  |
|                  | `IIssue.FileLink`                 | Can be set while reading issues  |
| :material-check: | `IIssue.MessageText`              |                                  |
|                  | `IIssue.MessageHtml`              |                                  |
|                  | `IIssue.MessageMarkdown`          |                                  |
| :material-check: | `IIssue.Priority`                 |                                  |
| :material-check: | `IIssue.PriorityName`             |                                  |
| :material-check: | `IIssue.Rule`                     |                                  |
| :material-check: | `IIssue.RuleUrl`                  | For issues containing a Wiki Url |

[JetBrains InsepectCode]: https://www.jetbrains.com/help/resharper/2017.1/InspectCode.html
[Cake.Issues.InspectCode addin]: https://www.nuget.org/packages/Cake.Issues.InspectCode
