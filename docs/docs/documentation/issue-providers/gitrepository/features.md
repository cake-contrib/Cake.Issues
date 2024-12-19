---
title: Features
description: Features of the Cake.Issues.GitRepository addin.
---

The [Cake.Issues.GitRepository addin](https://cakebuild.net/extensions/cake-issues-gitrepository/){target="_blank"}
provides the following features.

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

[FilePathTooLong]: rules/FilePathTooLong.md
[BinaryFileNotTrackedByLfs]: rules/BinaryFileNotTrackedByLfs.md
