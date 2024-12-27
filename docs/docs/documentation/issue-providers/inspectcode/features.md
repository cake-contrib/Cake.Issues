---
title: Features
description: Features of the Cake.Issues.InspectCode addin.
---

The [Cake.Issues.InspectCode addin]{target="_blank"} provides the following features.

??? tip "Tip: Running InspectCode"
    [JetBrains InsepectCode]{target="_blank"} can be run using the [InspectCode alias]{target="_blank"}.

## Basic features

- [x]  Reads warnings from [JetBrains InsepectCode]{target="_blank"} log files.
- [x]  Provides URLs for issues containing a Wiki URL.

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
- [x] `IIssue.Rule`
- [x] `IIssue.RuleUrl`

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Can be set while reading issues

[JetBrains InsepectCode]: https://www.jetbrains.com/help/resharper/InspectCode.html
[Cake.Issues.InspectCode addin]: https://cakebuild.net/extensions/cake-issues-inspectcode/
[InspectCode alias]: https://cakebuild.net/dsl/resharper/#InspectCode
