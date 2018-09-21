---
Order: 30
Title: Setup
Description: Instructions how to setup the Cake.Issues.PullRequests.Tfs addin.
---
This page describes the different ways how the [Cake.Issues.PullRequests.Tfs addin] can be setup.

# NTLM authentication

:::{.alert .alert-info}
NTLM authentication is only available for on-premise Team Foundation Server.
:::

To authenticate with NTLM you can use the [TfsAuthenticationNtlm] alias from the [Cake.Tfs addin].

The user needs to have `Contribute to pull requests` permission for the specific repository to
allow [Cake.Issues.PullRequests.Tfs addin] to post issues as comments to pull requests.

# Basic authentication

:::{.alert .alert-info}
Basic authentication is only available for on-premise Team Foundation Server.
:::

To authenticate with basic authentication you can use the [TfsAuthenticationBasic] alias from the [Cake.Tfs addin] and 
need to [Configure TFS to use Basic Authentication].

The user needs to have `Contribute to pull requests` permission for the specific repository to
allow [Cake.Issues.PullRequests.Tfs addin] to post issues as comments to pull requests.

# Personal access token

To authenticate with an personal access token you can use the [TfsAuthenticationPersonalAccessToken] alias from the [Cake.Tfs addin].

If you want to use the [Cake.Issues.PullRequests.Tfs addin] with an personal access token see
[Authenticate access with personal access tokens for Azure DevOps Services and TFS] for instructions how to create
a personal access token.

The access token needs to have the scope `Code (read and write)` set and the user needs to have `Contribute to pull requests`
permission for the specific repository to allow [Cake.Issues.PullRequests.Tfs addin] to post issues as comments to pull requests.

# OAuth authentication from Azure Pipelines

:::{.alert .alert-info}
OAuth authentication is only available for Azure DevOps.
:::

If you want to use the [Cake.Issues.PullRequests.Tfs addin] from an Azure Pipelines you can authenticate using the
OAuth token provided to the build.
For this you need to enable the [Allow scripts to access the OAuth token] option on the build definition.

To authenticate you can use the [TfsAuthenticationOAuth] alias from the [Cake.Tfs addin].

The user under which the build runs, named `<projectName> Build Service (<organizationName>)` (e.g. `Cake.Issues-Demo Build Service (cake-contrib)`),
needs to have `Contribute to pull requests` permission for the specific repository to allow [Cake.Issues.PullRequests.Tfs addin]
to post issues as comments to pull requests.

# Azure Active Directory

:::{.alert .alert-info}
OAuth authentication is only available for Azure DevOps.
:::

To authenticate with Azure Active Directory you can use the [TfsAuthenticationAzureActiveDirectory] alias from the [Cake.Tfs addin].

The user needs to have `Contribute to pull requests` permission for the specific repository to
allow [Cake.Issues.PullRequests.Tfs addin] to post issues as comments to pull requests.

[Cake.Issues.PullRequests.Tfs addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.Tfs
[Cake.Tfs addin]: https://www.nuget.org/packages/Cake.Tfs
[Configure TFS to use Basic Authentication]: https://docs.microsoft.com/en-us/azure/devops/integrate/get-started/auth/tfs-basic-auth?view=tfs-2018#configure-tfs-to-use-basic-authentication
[Authenticate access with personal access tokens for Azure DevOps Services and TFS]: https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=vsts
[Allow scripts to access the OAuth token]: https://docs.microsoft.com/en-us/azure/devops/pipelines/build/options?view=vsts&tabs=yaml#allow-scripts-to-access-the-oauth-token
[TfsAuthenticationNtlm]: https://cakebuild.net/api/Cake.Tfs/TfsAliases/6989592E
[TfsAuthenticationBasic]: https://cakebuild.net/api/Cake.Tfs/TfsAliases/86407B1D
[TfsAuthenticationPersonalAccessToken]: https://cakebuild.net/api/Cake.Tfs/TfsAliases/0E4AC3E3
[TfsAuthenticationOAuth]: https://cakebuild.net/api/Cake.Tfs/TfsAliases/B5D45B5D
[TfsAuthenticationAzureActiveDirectory]: https://cakebuild.net/api/Cake.Tfs/TfsAliases/0E787800