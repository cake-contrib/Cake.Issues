---
title: Features
description: Features of the Cake.Issues.InspectCode addin.
icon: material/creation-outline
---

The [Cake.Issues.InspectCode addin]{target="_blank"} provides the following features.

??? tip "Tip: Running InspectCode"
    [JetBrains InspectCode]{target="_blank"} can be run using the [InspectCode alias]{target="_blank"}.

## Basic features

- [x]  Reads warnings from [JetBrains InspectCode]{target="_blank"} XML log files.
- [x]  Provides URLs for issues containing a Wiki URL.

!!! note
    Starting from version 2024.1, the default output format of [JetBrains InspectCode] is Static Analysis Results Interchange Format (SARIF).
    The XML format, which was the default in previous versions, will soon be deprecated.
    Results in the XML format are still available with the `-f="xml"` parameter.

    This issue provider is only for the deprecated XML format.
    For the new default SARIF format [Cake.Issues.Sarif] can be used.

## Supported IIssue properties

<div class="annotate" markdown>

- [x] `IIssue.ProviderType`
- [x] `IIssue.ProviderName`
- [ ] `IIssue.Run` (1)
- [x] `IIssue.Identifier` (2)
- [x] `IIssue.ProjectName`
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

[JetBrains InspectCode]: https://www.jetbrains.com/help/resharper/InspectCode.html
[Cake.Issues.InspectCode addin]: https://cakebuild.net/extensions/cake-issues-inspectcode/
[InspectCode alias]: https://cakebuild.net/dsl/resharper/#InspectCode
[Cake.Issues.Sarif]: ../sarif/index.md
