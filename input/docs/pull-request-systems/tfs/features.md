---
Order: 20
Title: Features
Description: Features of the Cake.Issues.PullRequests.Tfs addin.
---
The [Cake.Issues.PullRequests.Tfs addin] provides the following features:

* Writes issues as comments to Team Foundation Server (TFS) or Visual Studio Team Services (VSTS) pull requests.
* Full support for all [Core features].
* Supported authentication methods:
  * NTLM using the [TfsAuthenticationNtlm] alias.
    Can only be used for on-premise Team Foundation Server.
  * Basic authentication using the [TfsAuthenticationBasic] alias.
    Can only be used for on-premise Team Foundation Server [configured for basic authentication].
  * Personal Access Token using the [TfsAuthenticationPersonalAccessToken] alias.
    Can be used for Team Foundation Server or Visual Studio Team Services.
  * OAuth using the [TfsAuthenticationOAuth] alias.
    Can only be used with Visual Studio Team Services.
  * Azure Active Directory using the [TfsAuthenticationAzureActiveDirectory] alias.
* Identification of pull requests through source branch or pull request ID.
* Comments written by the addin will be rendered with a specific icon corresponding to the state of the issue.
* Adds rule number and, if provided by the issue provider, link to the rule description to the comment.
* Support for issues messages formatted in Markdown format.
* Alias for approving or voting pull requests.

[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Core features]: ../overview/features#supported-core-functionality
[TfsAuthenticationNtlm]: ../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/7DFCE6F3
[TfsAuthenticationBasic]: ../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/3A473143
[TfsAuthenticationPersonalAccessToken]: ../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/B24D89BD
[TfsAuthenticationOAuth]: ../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/BEDAF9BF
[TfsAuthenticationAzureActiveDirectory]: ../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/DF54F8F0
[configured for basic authentication]: https://www.visualstudio.com/en-us/docs/integrate/get-started/auth/tfs-basic-auth
