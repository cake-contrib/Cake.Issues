---
title: Features
description: Features of the Cake.Issues.Terraform addin.
icon: material/creation-outline
---

The [Cake.Issues.Terraform addin] provides the following features.

??? tip "Tip: Running Terraform"
    [Terraform](https://www.terraform.io) can be run with
    [Cake.Terraform](https://cakebuild.net/extensions/cake-terraform/) addin.

## Basic features

- [x] Reads warnings from [Terraform validate command].

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
- [ ] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId`
- [x] `IIssue.RuleUrl`

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Can be set while reading issues

[Terraform validate command]: https://www.terraform.io/docs/cli/commands/validate.html
[Cake.Issues.Terraform addin]: https://cakebuild.net/extensions/cake-issues-terraform/
