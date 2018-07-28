---
Order: 20
Title: Features
Description: Features of the Cake.Issues.DocFx addin.
---
The [Cake.Issues.DocFx addin] provides the following features.

# Basic features

* Reads warnings from [DocFx] log files.

  :::{.alert .alert-info}
  [DocFx] can be run with [Cake.DocFx] addin.
  :::

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
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 | Always [IssuePriority.Warning] |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             | Always [IssuePriority.Warning] |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  |                                |

[Cake.Issues.DocFx addin]: https://www.nuget.org/packages/Cake.Issues.DocFx
[DocFx]: https://dotnet.github.io/docfx/
[Cake.DocFx]: https://www.nuget.org/packages/Cake.DocFx
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F