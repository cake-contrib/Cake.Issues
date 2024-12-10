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
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Run`                      | Can be set while reading issues       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Identifier`               | Set to `IIssue.MessageText`           |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProjectName`              |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProjectFileRelativePath`  |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndLine`                  |                                       |
| <span class="glyphicon glyphicon-ok" style="color:orange"></span>  | `IIssue.Column`                   | Only for [MsBuildXmlFileLoggerFormat] |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndColumn`                |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.FileLink`                 | Can be set while reading issues       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageText`              |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageHtml`              |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageMarkdown`          |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                       |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  | For code analysis (`CA*`) and StyleCop (`SA*`) warnings. Support for additional rules can be added through a custom [MsBuildAddRuleUrlResolver] |

[Cake.Issues.MsBuild addin]: https://www.nuget.org/packages/Cake.Issues.MsBuild
[MSBuild Extension Pack XmlFileLogger]: https://github.com/mikefourie-zz/MSBuildExtensionPack/blob/master/Solutions/Main/Loggers/Framework/XmlFileLogger.cs
[MsBuildAddRuleUrlResolver]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487
[MsBuildBinaryLogFileFormat]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/AD50C7E1
[MsBuildXmlFileLoggerFormat]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F
