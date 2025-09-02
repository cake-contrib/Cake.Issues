---
title: Features
description: Features of the Cake.Issues.DocFx addin.
icon: material/creation-outline
---

The [Cake.Issues.DocFx addin](https://cakebuild.net/extensions/cake-issues-docfx/)
provides the following features.

??? tip "Tip: Running DocFx"
    [DocFx](https://dotnet.github.io/docfx/) can be run with
    [Cake.DocFx](https://cakebuild.net/extensions/cake-docfx/) addin.

## Basic features

- [x] Reads warnings from [DocFx](https://dotnet.github.io/docfx/) log files.

## Supported IIssue properties

<div class="annotate" markdown>

- [x] `IIssue.ProviderType`
- [x] `IIssue.ProviderName`
- [ ] `IIssue.Run` (1)
- [x] `IIssue.Identifier` (2)
- [ ] `IIssue.ProjectName`
- [ ] `IIssue.ProjectFileRelativePath`
- [x] `IIssue.AffectedFileRelativePath`
- [x] `IIssue.Line`
- [ ] `IIssue.EndLine`
- [ ] `IIssue.Column`
- [ ] `IIssue.EndColumn`
- [ ] `IIssue.FileLink` (3)
- [x] `IIssue.MessageText`
- [ ] `IIssue.MessageHtml`
- [ ] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId`
- [x] `IIssue.RuleUrl`

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Can be set while reading issues
