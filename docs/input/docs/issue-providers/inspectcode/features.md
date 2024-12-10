---
Order: 20
Title: Features
Description: Features of the Cake.Issues.InspectCode addin.
---
The [Cake.Issues.InspectCode addin] provides the following features:

# Basic features

* Reads warnings from [JetBrains InsepectCode] log files.
* Provides URLs for issues containing a Wiki URL.

# Supported IIssue properties

|                                                                    | Property                          | Remarks                          |
|--------------------------------------------------------------------|-----------------------------------|----------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Run`                      | Can be set while reading issues  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Identifier`               | Set to `IIssue.MessageText`      |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProjectName`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndLine`                  |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Column`                   |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndColumn`                |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.FileLink`                 | Can be set while reading issues  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageText`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageHtml`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageMarkdown`          |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  | For issues containing a Wiki Url |

[JetBrains InsepectCode]: https://www.jetbrains.com/help/resharper/2017.1/InspectCode.html
[Cake.Issues.InspectCode addin]: https://www.nuget.org/packages/Cake.Issues.InspectCode
