---
Order: 20
Title: Features
Description: Features of the Cake.Issues.PullRequests.Tfs addin.
---
The [Cake.Issues.PullRequests.Tfs addin] provides the following features.

# Basic features

* Writes issues as comments to Team Foundation Server (TFS) or Visual Studio Team Services (VSTS) pull requests.
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

| On-Premise Team Foundation Server                                  | Visual Studio Team Services                                        | Authentication method          | Alias                                   | Remarks                                         |
|--------------------------------------------------------------------|--------------------------------------------------------------------|--------------------------------|-----------------------------------------|-------------------------------------------------|
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | <span class="glyphicon glyphicon-remove" style="color:red"></span> | NTLM                           | [TfsAuthenticationNtlm] alias.          |                                                 |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | <span class="glyphicon glyphicon-remove" style="color:red"></span> | Basic authentication           | [TfsAuthenticationBasic] alias.         | See [Configure TFS to use Basic Authentication] |
| <span class="glyphicon glyphicon-ok" style="color:green"></span>   | <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Personal access token          | [TfsAuthenticationPersonalAccessToken]  |                                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span>   | OAuth                          | [TfsAuthenticationOAuth]                |                                                 |
| <span class="glyphicon glyphicon-remove" style="color:red"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span>   | Azure Active Directory         | [TfsAuthenticationAzureActiveDirectory] |                                                 |

![Cake.Issues.PullRequests.Tfs](cake.issues.pullrequests.tfs.png "Cake.Issues.PullRequests.Tfs")

[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Core features]: ../../overview/features#supported-core-functionality
[TfsAuthenticationNtlm]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/4E73CD70
[TfsAuthenticationBasic]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/3FA02408
[TfsAuthenticationPersonalAccessToken]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/B7AA9CF6
[TfsAuthenticationOAuth]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/44032AF4
[TfsAuthenticationAzureActiveDirectory]: ../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/6826C541
[Configure TFS to use Basic Authentication]: https://docs.microsoft.com/en-us/vsts/integrate/get-started/auth/tfs-basic-auth?view=vsts#configure-tfs-to-use-basic-authentication
