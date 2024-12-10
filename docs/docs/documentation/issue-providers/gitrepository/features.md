---
title: Features
description: Features of the Cake.Issues.GitRepository addin.
---

The [Cake.Issues.GitRepository addin] provides the following features.

## Basic features

* Checks path length of files. See [FilePathTooLong] for details.
* Checks if binary files are tracked by Git LFS. See [BinaryFileNotTrackedByLfs] for details.

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
|                  | `IIssue.Line`                     |                                 |
|                  | `IIssue.EndLine`                  |                                 |
|                  | `IIssue.Column`                   |                                 |
|                  | `IIssue.EndColumn`                |                                 |
|                  | `IIssue.FileLink`                 | Can be set while reading issues |
| :material-check: | `IIssue.MessageText`              |                                 |
| :material-check: | `IIssue.MessageHtml`              |                                 |
| :material-check: | `IIssue.MessageMarkdown`          |                                 |
| :material-check: | `IIssue.Priority`                 |                                 |
| :material-check: | `IIssue.PriorityName`             |                                 |
| :material-check: | `IIssue.Rule`                     |                                 |
| :material-check: | `IIssue.RuleUrl`                  |                                 |

[Cake.Issues.GitRepository addin]: https://www.nuget.org/packages/Cake.Issues.GitRepository
[FilePathTooLong]: rules/FilePathTooLong
[BinaryFileNotTrackedByLfs]: rules/BinaryFileNotTrackedByLfs
