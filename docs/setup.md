---
Order: 30
Title: Setup
Description: Instructions how to setup the Cake.Issues.PullRequests.AzureDevOps addin.
---
This page describes the different ways how the [Cake.Issues.PullRequests.AzureDevOps addin] can be setup.

# NTLM authentication

:::{.alert .alert-info}
NTLM authentication is only available for on-premise Azure DevOps Server.
:::

To authenticate with NTLM you can use the [AzureDevOpsAuthenticationNtlm] alias from the [Cake.AzureDevOps addin].

The user needs to have `Contribute to pull requests` permission for the specific repository to
allow [Cake.Issues.PullRequests.AzureDevOps addin] to post issues as comments to pull requests.

# Basic authentication

:::{.alert .alert-info}
Basic authentication is only available for on-premise Azure DevOps Server.
:::

To authenticate with basic authentication you can use the [AzureDevOpsAuthenticationBasic] alias from the [Cake.AzureDevOps addin] and
need to [Configure AzureDevOps Server to use Basic Authentication].

The user needs to have `Contribute to pull requests` permission for the specific repository to
allow [Cake.Issues.PullRequests.AzureDevOps addin] to post issues as comments to pull requests.

# Personal access token

To authenticate with an personal access token you can use the [AzureDevOpsAuthenticationPersonalAccessToken] alias from the [Cake.AzureDevOps addin].

If you want to use the [Cake.Issues.PullRequests.AzureDevOps addin] with an personal access token see
[Authenticate access with personal access tokens for Azure DevOps] for instructions how to create
a personal access token.

The access token needs to have the scope `Code (read and write)` set and the user needs to have `Contribute to pull requests`
permission for the specific repository to allow [Cake.Issues.PullRequests.AzureDevOps addin] to post issues as comments to pull requests.

# OAuth authentication from Azure Pipelines

:::{.alert .alert-info}
OAuth authentication is only available for Azure DevOps Service.
:::

If you want to use the [Cake.Issues.PullRequests.AzureDevOps addin] from an Azure Pipelines you can authenticate using the
OAuth token provided to the build.
For this you need to enable the [Allow scripts to access the OAuth token] option on the build definition.

To authenticate you can use the [AzureDevOpsAuthenticationOAuth] alias from the [Cake.AzureDevOps addin].

The user under which the build runs, named `<projectName> Build Service (<organizationName>)` (e.g. `Cake.Issues-Demo Build Service (cake-contrib)`),
needs to have `Contribute to pull requests` permission for the specific repository to allow [Cake.Issues.PullRequests.AzureDevOps addin]
to post issues as comments to pull requests.

# Azure Active Directory

:::{.alert .alert-info}
OAuth authentication is only available for Azure DevOps Service.
:::

To authenticate with Azure Active Directory you can use the [AzureDevOpsAuthenticationAzureActiveDirectory] alias from the [Cake.AzureDevOps addin].

The user needs to have `Contribute to pull requests` permission for the specific repository to
allow [Cake.Issues.PullRequests.AzureDevOps addin] to post issues as comments to pull requests.

[Cake.Issues.PullRequests.AzureDevOps addin]: https://www.nuget.org/packages/Cake.Issues.PullRequests.AzureDevOps
[Cake.AzureDevOps addin]: https://www.nuget.org/packages/Cake.AzureDevOps
[Configure TFS to use Basic Authentication]: https://docs.microsoft.com/en-us/azure/devops/integrate/get-started/auth/tfs-basic-auth#configure-tfs-to-use-basic-authentication
[Authenticate access with personal access tokens for Azure DevOps]: https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate
[Allow scripts to access the OAuth token]: https://docs.microsoft.com/en-us/azure/devops/pipelines/build/options#allow-scripts-to-access-the-oauth-token
[AzureDevOpsAuthenticationNtlm]: https://cakebuild.net/api/Cake.AzureDevOps/AzureDevOpsAliases/F2A040B7
[AzureDevOpsAuthenticationBasic]: https://cakebuild.net/api/Cake.AzureDevOps/AzureDevOpsAliases/7CD679FF
[AzureDevOpsAuthenticationPersonalAccessToken]: https://cakebuild.net/api/Cake.AzureDevOps/AzureDevOpsAliases/F4DCC101
[AzureDevOpsAuthenticationOAuth]: https://cakebuild.net/api/Cake.AzureDevOps/AzureDevOpsAliases/988E9C28
[AzureDevOpsAuthenticationAzureActiveDirectory]: https://cakebuild.net/api/Cake.AzureDevOps/AzureDevOpsAliases/0B9F5DF6
