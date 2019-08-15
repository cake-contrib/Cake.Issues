---
Order: 20
Title: Features
Description: Features of the Cake.Issues.EsLint addin.
---
The [Cake.Issues.EsLint addin] provides the following features.

# Basic features

* Reads issues reported by ESLint.
* Provides URLs for all issues.
* Support for custom URL resolving using the [EsLintAddRuleUrlResolver] alias.

# Supported log file formats

* [EsLintJsonFormat] alias for reading issues from log files created by [ESLint json formatter].

# Supported comment formats

|                                                                    | Comment format                 | Remarks                        |
|--------------------------------------------------------------------|--------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IssueCommentFormat.PlainText` |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IssueCommentFormat.Markdown`  |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IssueCommentFormat.Html`      |                                |

# Supported IIssue properties

|                                                                    | Property                          | Remarks                        |
|--------------------------------------------------------------------|-----------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectName`              |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Message`                  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  | Support for custom rules can be added through a custom [EsLintAddRuleUrlResolver] |

[Cake.Issues.EsLint addin]: https://www.nuget.org/packages/Cake.Issues.EsLint
[ESLint json formatter]: http://eslint.org/docs/user-guide/formatters/#json
[EsLintAddRuleUrlResolver]: ../../../api/Cake.Issues.EsLint/EsLintIssuesAliases/D64301E6
[EsLintJsonFormat]: ../../../api/Cake.Issues.EsLint/EsLintIssuesAliases/230C6E27