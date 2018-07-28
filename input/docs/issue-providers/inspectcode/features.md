---
Order: 20
Title: Features
Description: Features of the Cake.Issues.InspectCode addin.
---
The [Cake.Issues.InspectCode addin] provides the following features:

# Basic features

* Reads warnings from [JetBrains InsepectCode] log files.
* Provides URLs for issues containing a Wiki URL.

# Supported comment formats

|                                                                    | Comment format                 | Remarks                               |
|--------------------------------------------------------------------|--------------------------------|---------------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IssueCommentFormat.PlainText` |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IssueCommentFormat.Markdown`  |                                       |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IssueCommentFormat.Html`      |                                       |

# Supported IIssue properties

|                                                                    | Property                          | Remarks                          |
|--------------------------------------------------------------------|-----------------------------------|----------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProjectName`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Message`                  |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  | For issues containing a Wiki Url |

[JetBrains InsepectCode]: https://www.jetbrains.com/help/resharper/2017.1/InspectCode.html
[Cake.Issues.InspectCode addin]: https://www.nuget.org/packages/Cake.Issues.InspectCode