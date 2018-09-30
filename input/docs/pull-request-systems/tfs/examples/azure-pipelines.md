---
Order: 30
Title: Using with Azure Pipelines
Description: Example how to use the Cake.Issues.PullRequests.Tfs addin from an Azure Pipelines build.
---
This example shows how to write issues as comments to a Team Foundation Server (TFS) or
Azure DevOps pull request from an Azure Pipelines build.

To write issues as comments to TFS or Azure DevOps pull requests you need to import the core addin,
the core pull request addin, the TFS/Azure DevOps support including the Cake TFS addin, and one or more issue providers,
in this example for JetBrains InspectCode:

```csharp
#addin "Cake.Issues"
#addin "Cake.Issues.InspectCode"
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.Tfs"
#addin "Cake.Tfs"
```

In the following task we'll first determine if the build is running on Azure DevOps and for a pull request,
then read the remote repository URL and pull request id from environment variables set by the Azure Pipelines build 
and finally call the [TfsPullRequests] alias using the OAuth token provided by the Azure Pipeline build.

:::{.alert .alert-info}
Please note that you'll need to setup your Azure Pipelines build to allow scripts to
access the OAuth token and need to setup proper permissions.

See [OAuth authentication from Azure Pipelines] for details.
:::

```csharp
Task("ReportIssuesToPullRequest").Does(() =>
{
    var isRunningOnAzureDevOps =
        !string.IsNullOrWhiteSpace(context.EnvironmentVariable("TF_BUILD")) &&
        !string.IsNullOrWhiteSpace(context.EnvironmentVariable("SYSTEM_COLLECTIONURI")) &&
        (
            new Uri(context.EnvironmentVariable("SYSTEM_COLLECTIONURI")).Host == "dev.azure.com" ||
            new Uri(context.EnvironmentVariable("SYSTEM_COLLECTIONURI")).Host.EndsWith("visualstudio.com")
        );

    var isPullRequestBuild =
        !string.IsNullOrWhiteSpace(context.EnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID"));

    if (isRunningOnAzureDevOps)
        {
            var repositoryUrl = new Uri(context.EnvironmentVariable("BUILD_REPOSITORY_URI"));

            if (isPullRequestBuild)
            {
                if (!Int32.TryParse(context.EnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID"), out var pullRequestId))
                {
                    throw new Exception(
                        string.Format(
                            "Invalid pull request ID: {0}",
                            context.EnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID")));
                }
                else
                {
                    var repoRootFolder = MakeAbsolute(Directory("./"));

                    ReportIssuesToPullRequest(
                        InspectCodeIssuesFromFilePath(
                            @"C:\build\inspectcode.log"),
                        TfsPullRequests(
                            repositoryUrl,
                            pullRequestId,
                            TfsAuthenticationOAuth(EnvironmentVariable("SYSTEM_ACCESSTOKEN"))),
                        repoRootFolder);
                }
            }
        }
});
```

[TfsPullRequests]: ../../../../api/Cake.Issues.PullRequests.Tfs/TfsPullRequestSystemAliases/BC3F9B2C
[Allow scripts to access the OAuth token]: https://docs.microsoft.com/en-us/azure/devops/pipelines/build/options?view=vsts&tabs=yaml#allow-scripts-to-access-the-oauth-token
[OAuth authentication from Azure Pipelines]: ../setup#oauth-authentication-from-azure-pipelines