---
title: Features
description: Features of the Cake.Issues.EsLint addin.
icon: material/creation-outline
---

The [Cake.Issues.EsLint addin](https://cakebuild.net/extensions/cake-issues-eslint/) provides the following features.

??? tip "Tip: Running ESLint"
    [ESLint](https://eslint.org) can be run with
    [Cake.ESLint](https://cakebuild.net/extensions/cake-eslint/) addin.

## Basic features

- [x] Reads issues reported by ESLint.
- [x] Provides URLs for all issues.
- [x] Support for custom URL resolving using the [EsLintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.EsLint/EsLintIssuesAliases/0F6CCE21)
  alias.

## Supported log file formats

- [x] [EsLintJsonFormat](https://cakebuild.net/api/Cake.Issues.EsLint/EsLintIssuesAliases/230C6E27)
    alias for reading issues from log files created by
    [ESLint json formatter](https://eslint.org/docs/user-guide/formatters/#json).

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
- [x] `IIssue.Column`
- [ ] `IIssue.EndColumn`
- [ ] `IIssue.FileLink` (3)
- [x] `IIssue.MessageText`
- [ ] `IIssue.MessageHtml`
- [ ] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId`
- [x] `IIssue.RuleUrl` (4)

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Can be set while reading issues
4.  Support for custom rules can be added through a custom [EsLintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.EsLint/EsLintIssuesAliases/0F6CCE21)
