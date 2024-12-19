---
title: Features
description: Features of the Cake.Issues.DocFx addin.
---

The [Cake.Issues.DocFx addin](https://cakebuild.net/extensions/cake-issues-docfx/){target="_blank"}
provides the following features.

## Basic features

* Reads warnings from [DocFx](https://dotnet.github.io/docfx/){target="_blank"} log files.

!!! info
    [DocFx](https://dotnet.github.io/docfx/){target="_blank"} can be run with
    [Cake.DocFx](https://cakebuild.net/extensions/cake-docfx/){target="_blank"} addin.

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
