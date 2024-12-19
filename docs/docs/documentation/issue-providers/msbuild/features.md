---
title: Features
description: Features of the Cake.Issues.MsBuild addin.
---

The [Cake.Issues.MsBuild addin](https://cakebuild.net/extensions/cake-issues-msbuild/){target="_blank"}
provides the following features.

## Basic features

* Reads errors and warnings from MSBuild log files.
* Provides URLs for all code analysis (`CA*`) and StyleCop (`SA*`) warnings.
* Support for custom URL resolving using the [MsBuildAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487){target="_blank"} alias.

## Supported log file formats

* [MsBuildBinaryLogFileFormat](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/AD50C7E1){target="_blank"} alias for reading issues from binary log files.
* [MsBuildXmlFileLoggerFormat](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E){target="_blank"} alias for reading issues from log files created by [MSBuild Extension Pack XmlFileLogger](https://github.com/mikefourie-zz/MSBuildExtensionPack/blob/master/Solutions/Main/Loggers/Framework/XmlFileLogger.cs){target="_blank"}.

## Supported IIssue properties

|                  | Property                          | Remarks                               |
|------------------|-----------------------------------|---------------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                       |
| :material-check: | `IIssue.ProviderName`             |                                       |
|                  | `IIssue.Run`                      | Can be set while reading issues       |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`           |
| :material-check: | `IIssue.ProjectName`              |                                       |
| :material-check: | `IIssue.ProjectFileRelativePath`  |                                       |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                       |
| :material-check: | `IIssue.Line`                     |                                       |
|                  | `IIssue.EndLine`                  |                                       |
| :material-check: | `IIssue.Column`                   | Only for [MsBuildXmlFileLoggerFormat](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E){target="_blank"} |
|                  | `IIssue.EndColumn`                |                                       |
|                  | `IIssue.FileLink`                 | Can be set while reading issues       |
| :material-check: | `IIssue.MessageText`              |                                       |
|                  | `IIssue.MessageHtml`              |                                       |
|                  | `IIssue.MessageMarkdown`          |                                       |
| :material-check: | `IIssue.Priority`                 |                                       |
| :material-check: | `IIssue.PriorityName`             |                                       |
| :material-check: | `IIssue.Rule`                     |                                       |
| :material-check: | `IIssue.RuleUrl`                  | For code analysis (`CA*`) and StyleCop (`SA*`) warnings. Support for additional rules can be added through a custom [MsBuildAddRuleUrlResolver](https://cakebuild.net/api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487){target="_blank"} |
