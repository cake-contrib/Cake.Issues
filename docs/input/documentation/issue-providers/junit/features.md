---
title: Features
description: Features of the Cake.Issues.JUnit addin.
icon: material/creation-outline
---

The [Cake.Issues.JUnit addin](https://cakebuild.net/extensions/cake-issues-junit/){target="_blank"} provides the following features.

??? tip "Supported Tools"
    The JUnit issue provider can read JUnit XML output from any tool that generates standard JUnit XML format, including:
    
    - **cpplint**: C++ linter
    - **commitlint-format-junit**: Commit message linter  
    - **kubeconform**: Kubernetes manifest validator
    - **htmlhint**: HTML linter with JUnit format support
    - Any other tool that outputs JUnit XML format

## Basic features

- [x] Reads issues from JUnit XML files.
- [x] Supports both single `testsuite` and `testsuites` root elements.
- [x] Extracts file paths from `classname` attributes or embedded within failure messages.
- [x] Supports multiple file path patterns commonly used by linters.
- [x] Maps JUnit test failures and errors to Cake.Issues format with appropriate priorities.
- [x] Extracts rule names from failure types or test names.
- [x] Robust error handling for malformed XML.

## Supported file path patterns

The provider can extract file information from various formats commonly used in linter output:

- [x] `file:line:column` (e.g., `src/example.cpp:15:5`)
- [x] `file(line,column)` (e.g., `index.html(12,5)`)
- [x] `file line number` (e.g., `about.html line 8`)
- [x] `file:line` (e.g., `/path/to/file.txt:123`)

## Supported log file formats

- [x] [JUnitIssuesFromFilePath](https://cakebuild.net/api/Cake.Issues.JUnit/JUnitIssuesAliases/){target="_blank"}
    alias for reading issues from JUnit XML files.
- [x] [JUnitIssuesFromContent](https://cakebuild.net/api/Cake.Issues.JUnit/JUnitIssuesAliases/){target="_blank"}
    alias for reading issues from JUnit XML content.

## Supported IIssue properties

<div class="annotate" markdown>

- [x] `IIssue.ProviderType`
- [x] `IIssue.ProviderName`
- [ ] `IIssue.Run` (1)
- [ ] `IIssue.Identifier`
- [ ] `IIssue.ProjectName`
- [ ] `IIssue.ProjectFileRelativePath`
- [x] `IIssue.AffectedFileRelativePath` (2)
- [x] `IIssue.Line` (3)
- [ ] `IIssue.EndLine`
- [x] `IIssue.Column` (3)
- [ ] `IIssue.EndColumn`
- [ ] `IIssue.FileLink` (4)
- [x] `IIssue.MessageText`
- [ ] `IIssue.MessageHtml`
- [ ] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId` (5)
- [ ] `IIssue.RuleUrl`

</div>

1.  Can be set while reading issues
2.  Extracted from `classname` attribute or parsed from failure message content
3.  Extracted from failure message content when available
4.  Can be set while reading issues
5.  Set to the failure `type` attribute when available