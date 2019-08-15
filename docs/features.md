---
Order: 20
Title: Features
Description: Features of the Cake.Issues.MsBuild addin.
---
The [Cake.Issues.MsBuild addin] provides the following features.

# Basic features

* Reads warnings from MSBuild log files.
* Provides URLs for all code analysis (`CA*`) and StyleCop (`SA*`) warnings.
* Support for custom URL resolving using the [MsBuildAddRuleUrlResolver] alias.

# Supported log file formats

* [MsBuildBinaryLogFileFormat] alias for reading issues from binary log files.
* [MsBuildXmlFileLoggerFormat] alias for reading issues from log files created by [MSBuild Extension Pack XmlFileLogger].

# Supported comment formats

|                                                                    | Comment format                 | Remarks                        |
|--------------------------------------------------------------------|--------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IssueCommentFormat.PlainText` |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IssueCommentFormat.Markdown`  |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IssueCommentFormat.Html`      |                                |

# Supported IIssue properties

|                                                                  | Property                          | Remarks                        |
|------------------------------------------------------------------|-----------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.ProviderType`             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.ProviderName`             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.ProjectName`              |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.ProjectFileRelativePath`  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.AffectedFileRelativePath` |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.Line`                     |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.Message`                  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.Priority`                 | Always [IssuePriority.Warning] |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.PriorityName`             | Always [IssuePriority.Warning] |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.Rule`                     |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span> | `IIssue.RuleUrl`                  | For code analysis (`CA*`) and StyleCop (`SA*`) warnings. Support for additional rules can be added through a custom [MsBuildAddRuleUrlResolver] |

[Cake.Issues.MsBuild addin]: https://www.nuget.org/packages/Cake.Issues.MsBuild
[MSBuild Extension Pack XmlFileLogger]: http://www.msbuildextensionpack.com/help/4.0.5.0/html/242ab4fd-c2e2-f6aa-325b-7588725aed24.htm
[MsBuildAddRuleUrlResolver]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487
[MsBuildBinaryLogFileFormat]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/AD50C7E1
[MsBuildXmlFileLoggerFormat]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/051D7B6E
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F
