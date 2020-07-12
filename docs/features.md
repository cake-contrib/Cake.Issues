---
Order: 20
Title: Features
Description: Features of the Cake.Issues.GitRepository addin.
---
The [Cake.Issues.GitRepository addin] provides the following features.

# Basic features

* Checks path length of files. See [FilePathTooLong] for details.
* Checks if binary files are tracked by Git LFS. See [BinaryFileNotTrackedByLfs] for details.

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
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Line`                     |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndLine`                  |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Column`                   |                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.EndColumn`                |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageText`              |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageHtml`              |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageMarkdown`          |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.RuleUrl`                  |                                 |

[Cake.Issues.GitRepository addin]: https://www.nuget.org/packages/Cake.Issues.GitRepository
[FilePathTooLong]: rules/FilePathTooLong
[BinaryFileNotTrackedByLfs]: rules/BinaryFileNotTrackedByLfs
