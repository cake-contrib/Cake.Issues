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
        /// Gets an instance of the file link settings for linking files hosted on GitHub on a specific branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="branch">Name of the branch on which the file linking will be based on.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForGitHubBranch(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return context.IssueFileLinkSettingsForGitHubBranch(repositoryUrl, branch, null);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub
        /// in a sub-folder on a specific branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="branch">Name of the branch on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForGitHubBranch(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return FileLinkSettings.GitHubBranch(repositoryUrl, branch, rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub fo a specific commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="commitId">The commit id on which the file linking will be based on.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForGitHubCommit(
            this ICakeContext context,
            Uri repositoryUrl,
            string commitId)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            commitId.NotNullOrWhiteSpace(nameof(commitId));

            return context.IssueFileLinkSettingsForGitHubCommit(repositoryUrl, commitId, null);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub
        /// in a sub-folder for a specific commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="commitId">The commit id on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForGitHubCommit(
            this ICakeContext context,
            Uri repositoryUrl,
            string commitId,
            string rootPath)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            commitId.NotNullOrWhiteSpace(nameof(commitId));

            return FileLinkSettings.GitHubCommit(repositoryUrl, commitId, rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps
        /// on a specific branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="branch">Name of the branch on which the file linking will be based on.</param>
        /// <returns>Settings for linking files hosted on Azure DevOps or Azure DevOps Server.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForAzureDevOpsBranch(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return context.IssueFileLinkSettingsForAzureDevOpsBranch(repositoryUrl, branch, null);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps
        /// in a sub-folder on a specific branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="branch">Name of the branch on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking files hosted on Azure DevOps.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForAzureDevOpsBranch(
            this ICakeContext context,
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return FileLinkSettings.AzureDevOpsBranch(repositoryUrl, branch, rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps
        /// for a specific commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="commitId">The commit id on which the file linking will be based on.</param>
        /// <returns>Settings for linking files hosted on Azure DevOps or Azure DevOps Server.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForAzureDevOpsCommit(
            this ICakeContext context,
            Uri repositoryUrl,
            string commitId)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            commitId.NotNullOrWhiteSpace(nameof(commitId));

            return context.IssueFileLinkSettingsForAzureDevOpsCommit(repositoryUrl, commitId, null);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps
        /// in a sub-folder for a specific commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="commitId">The commit id on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking files hosted on Azure DevOps.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettingsForAzureDevOpsCommit(
            this ICakeContext context,
            Uri repositoryUrl,
            string commitId,
            string rootPath)
        {
            context.NotNull(nameof(context));
            repositoryUrl.NotNull(nameof(repositoryUrl));
            commitId.NotNullOrWhiteSpace(nameof(commitId));

            return FileLinkSettings.AzureDevOpsCommit(repositoryUrl, commitId, rootPath);
        }
    }
}
