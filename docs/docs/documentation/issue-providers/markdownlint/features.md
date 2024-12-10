---
Order: 20
Title: Features
Description: Features of the Cake.Issues.Markdownlint addin.
---
The [Cake.Issues.Markdownlint addin] provides the following features.

# Basic features

* Reads warnings from [Markdownlint] logfiles.
* Provides URLs for all issues.
* Support for custom URL resolving using the [MarkdownlintAddRuleUrlResolver] alias (except for [MarkdownlintCliJsonLogFileFormat]).

# Supported log file formats

* [MarkdownlintLogFileFormat] alias for reading issues from [Markdownlint] output generated with `options.resultVersion` set to 1.
* [MarkdownlintCliLogFileFormat] alias for reading issues from [markdownlint-cli] log files.
* [MarkdownlintCliJsonLogFileFormat] alias for reading issues from [markdownlint-cli] log files created with the `--json` parameter.

  :::{.alert .alert-info}
  [markdownlint-cli] can be run with the [Cake.Markdownlint] addin.
  :::

# Supported IIssue properties

|                                                                    | Property                          | Remarks                                 |
|--------------------------------------------------------------------|-----------------------------------|-----------------------------------------|
| :material-check: | `IIssue.ProviderType`             |                                         |
| :material-check: | `IIssue.ProviderName`             |                                         |
|                  | `IIssue.Run`                      | Can be set while reading issues         |
| :material-check: | `IIssue.Identifier`               | Set to `IIssue.MessageText`             |
|                  | `IIssue.ProjectName`              |                                         |
|                  | `IIssue.ProjectFileRelativePath`  |                                         |
| :material-check: | `IIssue.AffectedFileRelativePath` |                                         |
| :material-check: | `IIssue.Line`                     |                                         |
|                  | `IIssue.EndLine`                  |                                         |
| <span class="glyphicon glyphicon-ok" style="color:orange"></span>  | `IIssue.Column`                   | Only for [MarkdownlintCliLogFileFormat] |
|                  | `IIssue.EndColumn`                |                                         |
|                  | `IIssue.FileLink`                 | Can be set while reading issues         |
| :material-check: | `IIssue.MessageText`              |                                         |
|                  | `IIssue.MessageHtml`              |                                         |
|                  | `IIssue.MessageMarkdown`          |                                         |
| :material-check: | `IIssue.Priority`                 | Always [IssuePriority.Warning]          |
| :material-check: | `IIssue.PriorityName`             | Always `Warning`                        |
| :material-check: | `IIssue.Rule`                     |                                         |
| :material-check: | `IIssue.RuleUrl`                  | Support for custom rules can be added through a custom [MarkdownlintAddRuleUrlResolver] except for [MarkdownlintCliJsonLogFileFormat] |

[Cake.Issues.Markdownlint addin]: https://www.nuget.org/packages/Cake.Issues.Markdownlint
[Markdownlint]: https://github.com/DavidAnson/markdownlint
[markdownlint-cli]: https://github.com/igorshubovych/markdownlint-cli
[Cake.Markdownlint]: https://www.nuget.org/packages/Cake.Markdownlint/
[MarkdownlintAddRuleUrlResolver]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/2EE35F55
[MarkdownlintLogFileFormat]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/EBFF674A
[MarkdownlintCliLogFileFormat]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/B518F49E
[MarkdownlintCliJsonLogFileFormat]: ../../../api/Cake.Issues.Markdownlint/MarkdownlintIssuesAliases/36DE6F5F
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F
