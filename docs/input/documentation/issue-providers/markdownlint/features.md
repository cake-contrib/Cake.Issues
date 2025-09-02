---
title: Features
description: Features of the Cake.Issues.Markdownlint addin.
icon: material/creation-outline
---

The [Cake.Issues.Markdownlint addin](https://cakebuild.net/extensions/cake-issues-markdownlint/)
provides the following features.

??? tip "Tip: Running markdownlint"
    [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli)
    can be run with the [Cake.Markdownlint](https://cakebuild.net/extensions/cake-markdownlint/)
    addin.

## Basic features

- [x] Reads warnings from [Markdownlint](https://github.com/DavidAnson/markdownlint) logfiles.
- [x] Provides URLs for all issues.
- [x] Support for custom URL resolving using the [MarkdownlintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55)
  alias (except for [MarkdownlintCliJsonLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F)).

## Supported log file formats

- [x] [MarkdownlintV1LogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/65609BEB)
  alias for reading issues from [Markdownlint](https://github.com/DavidAnson/markdownlint)
  output generated with `options.resultVersion` set to 1.
- [x] [MarkdownlintCliLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/B518F49E)
  alias for reading issues from [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli) log files.
- [x] [MarkdownlintCliJsonLogFileFormat](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F)
  alias for reading issues from [markdownlint-cli](https://github.com/igorshubovych/markdownlint-cli)
  log files created with the `--json` parameter.

## Supported IIssue properties

=== "MarkdownlintV1LogFileFormat"

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
    - [x] `IIssue.Priority` (4)
    - [x] `IIssue.PriorityName` (5)
    - [x] `IIssue.RuleId`
    - [x] `IIssue.RuleUrl` (6)
    
    </div>
    
    1.  Can be set while reading issues
    2.  Set to `IIssue.MessageText`
    3.  Can be set while reading issues
    4.  Always [IssuePriority.Warning](https://cakebuild.net/api/Cake.Issues/IssuePriority/7A0CE07F)
    5.  Always `Warning`
    6.  Support for custom rules can be added through a custom [MarkdownlintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55)

=== "MarkdownlintCliLogFileFormat"

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
    - [x] `IIssue.Priority` (4)
    - [x] `IIssue.PriorityName` (5)
    - [x] `IIssue.RuleId`
    - [x] `IIssue.RuleUrl` (6)
    
    </div>
    
    1.  Can be set while reading issues
    2.  Set to `IIssue.MessageText`
    3.  Can be set while reading issues
    4.  Always [IssuePriority.Warning](https://cakebuild.net/api/Cake.Issues/IssuePriority/7A0CE07F)
    5.  Always `Warning`
    6.  Support for custom rules can be added through a custom [MarkdownlintAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55)

=== "MarkdownlintCliJsonLogFileFormat"

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
    - [x] `IIssue.Priority` (4)
    - [x] `IIssue.PriorityName` (5)
    - [x] `IIssue.RuleId`
    - [x] `IIssue.RuleUrl`
    
    </div>
    
    1.  Can be set while reading issues
    2.  Set to `IIssue.MessageText`
    3.  Can be set while reading issues
    4.  Always [IssuePriority.Warning](https://cakebuild.net/api/Cake.Issues/IssuePriority/7A0CE07F)
    5.  Always `Warning`
