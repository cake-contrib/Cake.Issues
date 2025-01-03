---
title: Features
description: Features of the Cake.Issues.MsBuild addin.
icon: material/creation-outline
---

The [Cake.Issues.MsBuild addin](https://cakebuild.net/extensions/cake-issues-msbuild/){target="_blank"}
provides the following features.

??? tip "Tip: Running MSBuild"
    MSBuild can be run using the [DotNet aliases]{target="_blank"} or [MsBuild aliases]{target="_blank"}.

## Basic features

- [x] Reads errors and warnings from MSBuild log files.
- [x] Provides URLs for all code analysis (`CA*`) and StyleCop (`SA*`) warnings.
- [x] Support for custom URL resolving using the [MsBuildAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487){target="_blank"} alias.

## Supported log file formats

- [x] [MsBuildBinaryLogFileFormat](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/AD50C7E1){target="_blank"} alias for reading issues from binary log files.
- [x] [MsBuildXmlFileLoggerFormat](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E){target="_blank"} alias for reading issues from log files created by [MSBuild Extension Pack XmlFileLogger](https://github.com/mikefourie-zz/MSBuildExtensionPack/blob/master/Solutions/Main/Loggers/Framework/XmlFileLogger.cs){target="_blank"}.

## Supported IIssue properties

<div class="annotate" markdown>

- [x] `IIssue.ProviderType`
- [x] `IIssue.ProviderName`
- [ ] `IIssue.Run` (1)
- [x] `IIssue.Identifier` (2)
- [x] `IIssue.ProjectName`
- [x] `IIssue.ProjectFileRelativePath`
- [x] `IIssue.AffectedFileRelativePath`
- [x] `IIssue.Line`
- [ ] `IIssue.EndLine`
- [x] `IIssue.Column` (3)
- [ ] `IIssue.EndColumn`
- [ ] `IIssue.FileLink` (4)
- [x] `IIssue.MessageText`
- [ ] `IIssue.MessageHtml`
- [ ] `IIssue.MessageMarkdown`
- [x] `IIssue.Priority`
- [x] `IIssue.PriorityName`
- [x] `IIssue.RuleId`
- [x] `IIssue.RuleUrl` (5)

</div>

1.  Can be set while reading issues
2.  Set to `IIssue.MessageText`
3.  Only for [MsBuildXmlFileLoggerFormat](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E){target="_blank"}
4.  Can be set while reading issues
5.  For code analysis (`CA*`) and StyleCop (`SA*`) warnings. Support for additional rules can be added through a custom [MsBuildAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487){target="_blank"}

[DotNet aliases]: https://cakebuild.net/dsl/dotnet/#Built-In
[MsBuild aliases]: https://cakebuild.net/dsl/msbuild/#Built-In
