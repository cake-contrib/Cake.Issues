---
title: Features
description: Features of the Cake.Issues.GitRepository addin.
icon: material/creation-outline
---

The [Cake.Issues.GitRepository addin](https://cakebuild.net/extensions/cake-issues-gitrepository/){target="_blank"}
provides the following features.

## Basic features

- [x] Checks path length of files. See [FilePathTooLong] for details.
- [x] Checks if binary files are tracked by Git LFS. See [BinaryFileNotTrackedByLfs] for details.

## Supported IIssue properties

<div class="annotate" markdown>

- [x] `IIssue.ProviderType`
- [x] `IIssue.ProviderName`
- [ ] `IIssue.Run` (1)
- [x] `IIssue.Identifier` (2)
- [ ] `IIssue.ProjectName`
- [ ] `IIssue.ProjectFileRelativePath`
- [x] `IIssue.AffectedFileRelativePath`
- [ ] `IIssue.Line`
- [ ] `IIssue.EndLine`
- [ ] `IIssue.Column`
- [ ] `IIssue.EndColumn`
- [ ] `IIssue.FileLink` (3)
- [x] `IIssue.MessageText`
- [x] `IIssue.MessageHtml`
- [x] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId`
- [x] `IIssue.RuleUrl`

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Can be set while reading issues

[FilePathTooLong]: rules/FilePathTooLong.md
[BinaryFileNotTrackedByLfs]: rules/BinaryFileNotTrackedByLfs.md
