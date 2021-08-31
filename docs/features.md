---
Order: 20
Title: Features
Description: Features of the Cake.Issues.Markdownlint addin.
---
The [Cake.Issues.Markdownlint addin] provides the following features.

# Basic features

* Reads warnings from [Markdownlint] logfiles.
* Provides URLs for all issues.
* Support for custom URL resolving using the [MarkdownlintAddRuleUrlResolver] alias.

# Supported log file formats

* [MarkdownlintLogFileFormat] alias for reading issues from [Markdownlint] output generated with `options.resultVersion` set to 1.
* [MarkdownlintCliLogFileFormat] alias for reading issues from [markdownlint-cli] log files.

  :::{.alert .alert-info}
  [markdownlint-cli] can be run with the [Cake.Markdownlint] addin.
  :::

# Supported IIssue properties

|                                                                    | Property                          | Remarks                                 |
|--------------------------------------------------------------------|-----------------------------------|-----------------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                         |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Run`                      | Can be set while reading issues         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Identifier`               | Set to `IIssue.MessageText`             |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectName`              |                                         |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                         |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndLine`                  |                                         |
| <span class="glyphicon glyphicon-ok" style="color:orange"></span>  | `IIssue.Column`                   | Only for [MarkdownlintCliLogFileFormat] |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndColumn`                |                                         |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.FileLink`                 | Can be set while reading issues         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageText`              |                                         |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageHtml`              |                                         |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageMarkdown`          |                                         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 | Always [IssuePriority.Warning]          |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             | Always `Warning`                        |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                         |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  | Support for custom rules can be added through a custom [MarkdownlintAddRuleUrlResolver] |

[Cake.Issues.Markdownlint addin]: https://www.nuget.org/packages/Cake.Issues.Markdownlint
[Markdownlint]: https://github.com/DavidAnson/markdownlint
[markdownlint-cli]: https://github.com/igorshubovych/markdownlint-cli
[Cake.Markdownlint]: https://www.nuget.org/packages/Cake.Markdownlint/
[MarkdownlintAddRuleUrlResolver]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55
[MarkdownlintLogFileFormat]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/EBFF674A
[MarkdownlintCliLogFileFormat]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/B518F49E
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F
