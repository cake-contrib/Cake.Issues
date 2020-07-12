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

# Supported IIssue properties

|                                                                    | Property                          | Remarks                         |
|--------------------------------------------------------------------|-----------------------------------|---------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Run`                      | Can be set while reading issues |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Identifier`               | Set to `IIssue.MessageText`     |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectName`              |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndLine`                  |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Column`                   |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndColumn`                |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageText`              |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageHtml`              |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageMarkdown`          |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  |                                 |

[Cake.Issues.DocFx addin]: https://www.nuget.org/packages/Cake.Issues.DocFx
[DocFx]: https://dotnet.github.io/docfx/
[Cake.DocFx]: https://www.nuget.org/packages/Cake.DocFx
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F
