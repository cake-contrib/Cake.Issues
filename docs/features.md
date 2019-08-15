---
Order: 20
Title: Features
Description: Features of the Cake.Issues.PullRequests.Tfs addin.
---
The [Cake.Issues.PullRequests.Tfs addin] provides the following features.

:::{.alert .alert-info}
There's a [demo repository] available which you can fork and to which you can create pull requests to test the integration functionality.
:::

# Basic features

* Writes issues as comments to Team Foundation Server (TFS) or [Azure DevOps] pull requests.
* Identification of pull requests through source branch or pull request ID.
* Comments written by the addin will be rendered with a specific icon corresponding to the state of the issue.
* Adds rule number and, if provided by the issue provider, link to the rule description to the comment.
* Support for issues messages formatted in Markdown format.

# Supported capabilities

The [Cake.Issues.PullRequests.Tfs addin] supports all [Core features].

|                                                                    | Capability                     | Remarks                        |
|--------------------------------------------------------------------|--------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Checking commit ID             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Discussion threads             |                                |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Filtering by modified files    |                                |

# Supported authentication methods

| On-Premise Team Foundation Server                                  | Azure DevOps                                                       | Authentication method          |
|--------------------------------------------------------------------|--------------------------------------------------------------------|--------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | <span class="glyphicon glyphicon-remove" style="color:red"></span> | NTLM                           |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | <span class="glyphicon glyphicon-remove" style="color:red"></span> | Basic authentication           |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Personal access token          |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span>   | OAuth                          |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Azure Active Directory         |

For detailed instructions how to connect using the different methods see [Setup instructions].

![Cake.Issues.PullRequests.Tfs](cake.issues.pullrequests.tfs.png "Cake.Issues.PullRequests.Tfs")

[demo repository]: https://dev.azure.com/pberger/Cake.Issues-Demo
[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Azure DevOps]: https://azure.microsoft.com/en-us/services/devops/
[Core features]: ../../overview/features#supported-core-functionality
[Setup instructions]: setup