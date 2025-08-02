---
title: Features
description: Features of the Cake.Issues.CodeClimate addin.
icon: material/creation-outline
---

The [Cake.Issues.CodeClimate addin](https://cakebuild.net/extensions/cake-issues-codeclimate/){target="_blank"} provides the following features.

??? tip "Tip: CodeClimate compatible tools"
    The CodeClimate format is supported by many linting and analysis tools including 
    [ESLint](https://eslint.org/docs/user-guide/formatters/#json-with-metadata){target="_blank"},
    [RuboCop](https://docs.rubocop.org/rubocop/formatters.html#codeclimate-formatter){target="_blank"},
    [editorconfig-checker](https://github.com/editorconfig-checker/editorconfig-checker){target="_blank"},
    and many others.

## Basic features

- [x] Reads issues from CodeClimate compatible JSON format reports.
- [x] Supports both line-based and position-based location formats.
- [x] Maps CodeClimate severity levels to appropriate Cake.Issues priorities.
- [x] Filters to only process entries with `"type": "issue"`, ignoring measurements and other entry types.
- [x] Handles all required and optional CodeClimate fields including `check_name`, `description`, `categories`, `location`, `content`, `severity`, and `fingerprint`.

## Supported location formats

- [x] **Line-based locations**: `{"lines": {"begin": 5, "end": 10}}`
- [x] **Position-based locations**: `{"positions": {"begin": {"line": 5, "column": 10}, "end": {"line": 6, "column": 15}}}`

## Severity mapping

The CodeClimate issue provider maps CodeClimate severity levels to Cake.Issues priorities as follows:

| CodeClimate Severity | Cake.Issues Priority |
|---------------------|---------------------|
| `blocker`           | Error               |
| `critical`          | Error               |
| `major`             | Warning             |
| `minor`             | Warning             |
| `info`              | Suggestion          |

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
- [x] `IIssue.EndLine` (3)
- [x] `IIssue.Column` (4)
- [x] `IIssue.EndColumn` (4)
- [ ] `IIssue.FileLink` (5)
- [x] `IIssue.MessageText`
- [ ] `IIssue.MessageHtml`
- [ ] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId` (6)
- [ ] `IIssue.RuleUrl`

</div>

1. Can be set while reading issues
2. Set to CodeClimate `fingerprint` if available, otherwise generated from other fields
3. Supported for both line-based and position-based locations
4. Only supported for position-based locations
5. Can be set while reading issues
6. Set to CodeClimate `check_name`