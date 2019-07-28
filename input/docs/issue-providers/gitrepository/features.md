---
Order: 20
Title: Features
Description: Features of the Cake.Issues.GitRepository addin.
---
The [Cake.Issues.GitRepository addin] provides the following features.

# Basic features

* Checks if binary files are tracked by Git LFS.

# Supported comment formats

|                                                                    | Comment format                 | Remarks                        |
|--------------------------------------------------------------------|--------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IssueCommentFormat.PlainText` |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IssueCommentFormat.Markdown`  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IssueCommentFormat.Html`      |                                |

# Supported IIssue properties

|                                                                    | Property                          | Remarks                        |
|--------------------------------------------------------------------|-----------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectName`              |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Line`                     |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Message`                  |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.RuleUrl`                  |                                |

[Cake.Issues.GitRepository addin]: https://www.nuget.org/packages/Cake.Issues.GitRepository
