---
title: Features
description: Features of the Cake.Issues.DocFx addin.
---

The [Cake.Issues.DocFx addin] provides the following features.

## Basic features

* Reads warnings from [DocFx] log files.

!!! info
    [DocFx] can be run with [Cake.DocFx] addin.

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
|                  | `IIssue.Column`                   |                                 |
|                  | `IIssue.EndColumn`                |                                 |
|                  | `IIssue.FileLink`                 | Can be set while reading issues |
| :material-check: | `IIssue.MessageText`              |                                 |
|                  | `IIssue.MessageHtml`              |                                 |
|                  | `IIssue.MessageMarkdown`          |                                 |
| :material-check: | `IIssue.Priority`                 |                                 |
| :material-check: | `IIssue.PriorityName`             |                                 |
| :material-check: | `IIssue.Rule`                     |                                 |
| :material-check: | `IIssue.RuleUrl`                  |                                 |

[Cake.Issues.DocFx addin]: https://www.nuget.org/packages/Cake.Issues.DocFx
[DocFx]: https://dotnet.github.io/docfx/
[Cake.DocFx]: https://www.nuget.org/packages/Cake.DocFx
