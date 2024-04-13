---
Order: 20
Title: Features
Description: Features of the Cake.Issues.Terraform addin.
---
The [Cake.Issues.Terraform addin] provides the following features:

# Basic features

* Reads warnings from [Terraform validate command].

# Supported IIssue properties

|                                                                    | Property                          | Remarks                          |
|--------------------------------------------------------------------|-----------------------------------|----------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderType`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.ProviderName`             |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.Run`                      | Can be set while reading issues  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Identifier`               | Set to `IIssue.MessageText`      |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectName`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.ProjectFileRelativePath`  |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.AffectedFileRelativePath` |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Line`                     |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.EndLine`                  |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Column`                   |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.EndColumn`                |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.FileLink`                 | Can be set while reading issues  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.MessageText`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageHtml`              |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.MessageMarkdown`          |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Priority`                 |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.PriorityName`             |                                  |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | `IIssue.Rule`                     |                                  |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | `IIssue.RuleUrl`                  |                                  |

[Terraform validate command]: https://www.terraform.io/docs/cli/commands/validate.html
[Cake.Issues.Terraform addin]: https://cakebuild.net/extensions/cake-issues-terraform/
