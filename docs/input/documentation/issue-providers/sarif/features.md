---
title: Features
description: Features of the Cake.Issues.Sarif addin.
icon: material/creation-outline
---

The [Cake.Issues.Sarif addin](https://cakebuild.net/extensions/cake-issues-sarif/){target="_blank"} provides the following features.

## Basic features

- [x] Reads issues from files in [SARIF](https://sarifweb.azurewebsites.net/){target="_blank"} format.
- [x] Support for reading issues reported as suppressed by the linter

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
- [x] `IIssue.EndLine`
- [x] `IIssue.Column`
- [x] `IIssue.EndColumn`
- [ ] `IIssue.FileLink` (3)
- [x] `IIssue.MessageText`
- [ ] `IIssue.MessageHtml`
- [x] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId`
- [x] `IIssue.RuleUrl`

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Can be set while reading issues
