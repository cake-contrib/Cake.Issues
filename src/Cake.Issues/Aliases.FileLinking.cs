namespace Cake.Issues
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to linking to files.
    /// </content>
    public static partial class Aliases
    {
        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForGitHub(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return context.IssueFileLinkSettingsForGitHub(repositoryUrl, branch, null);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub in a sub-folder.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForGitHub(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return FileLinkSettings.GitHub(repositoryUrl, branch, rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <returns>Settings for linking files hosted on Azure DevOps or Azure DevOps Server.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForAzureDevOps(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return context.IssueFileLinkSettingsForAzureDevOps(repositoryUrl, branch, null);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps in a sub-folder.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking files hosted on Azure DevOps.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForAzureDevOps(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return FileLinkSettings.AzureDevOps(repositoryUrl, branch, rootPath);
        }
    }
}
