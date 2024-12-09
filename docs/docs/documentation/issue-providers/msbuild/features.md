---
Order: 20
Title: Features
Description: Features of the Cake.Issues.MsBuild addin.
---
The [Cake.Issues.MsBuild addin] provides the following features.

# Basic features

* Reads errors and warnings from MSBuild log files.
* Provides URLs for all code analysis (`CA*`) and StyleCop (`SA*`) warnings.
* Support for custom URL resolving using the [MsBuildAddRuleUrlResolver] alias.

# Supported log file formats

* [MsBuildBinaryLogFileFormat] alias for reading issues from binary log files.
* [MsBuildXmlFileLoggerFormat] alias for reading issues from log files created by [MSBuild Extension Pack XmlFileLogger].

# Supported IIssue properties

|                                                                    | Property                          | Remarks                               |
|--------------------------------------------------------------------|-----------------------------------|---------------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                       |
| :material-check: | `IIssue.ProviderName`             |                                       |
|                  | `IIssue.Run`                      | Can be set while reading issues       |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`           |
| :material-check: | `IIssue.ProjectName`              |                                       |
| :material-check: | `IIssue.ProjectFileRelativePath`  |                                       |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                       |
| :material-check: | `IIssue.Line`                     |                                       |
|                  | `IIssue.EndLine`                  |                                       |
| <span class="glyphicon glyphicon-ok" style="color:orange"></span>  | `IIssue.Column`                   | Only for [MsBuildXmlFileLoggerFormat] |
|                  | `IIssue.EndColumn`                |                                       |
|                  | `IIssue.FileLink`                 | Can be set while reading issues       |
| :material-check: | `IIssue.MessageText`              |                                       |
|                  | `IIssue.MessageHtml`              |                                       |
|                  | `IIssue.MessageMarkdown`          |                                       |
| :material-check: | `IIssue.Priority`                 |                                       |
| :material-check: | `IIssue.PriorityName`             |                                       |
| :material-check: | `IIssue.Rule`                     |                                       |
| :material-check: | `IIssue.RuleUrl`                  | For code analysis (`CA*`) and StyleCop (`SA*`) warnings. Support for additional rules can be added through a custom [MsBuildAddRuleUrlResolver] |

[Cake.Issues.MsBuild addin]: https://www.nuget.org/packages/Cake.Issues.MsBuild
[MSBuild Extension Pack XmlFileLogger]: https://github.com/mikefourie-zz/MSBuildExtensionPack/blob/master/Solutions/Main/Loggers/Framework/XmlFileLogger.cs
[MsBuildAddRuleUrlResolver]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487
[MsBuildBinaryLogFileFormat]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/AD50C7E1
[MsBuildXmlFileLoggerFormat]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F
