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
        /// Gets an instance of the file link settings for linking to files
        /// based on a custom pattern.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="pattern">Pattern of the file link.
        /// See <see cref="IIssueExtensions.ReplaceIssuePattern(string, IIssue)"/>
        /// for a list of tokens supported in the pattern.</param>
        /// <returns>Settings for linking files.</returns>
        /// <example>
        /// <para>Creates file link settings to an internal source hosting site:</para>
        /// <code>
        /// <![CDATA[
        ///     var fileLinkSettings =
        ///         IssueFileLinkSettings("https://awesomesource/{FilePath}?line={Line}");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettings(
            this ICakeContext context,
            string pattern)
        {
            context.NotNull(nameof(context));
            pattern.NotNullOrWhiteSpace(nameof(pattern));

            return FileLinkSettings.ForPattern(pattern);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files
        /// based on a custom action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="builder">Callback called for building the file link.</param>
        /// <returns>File link settings.</returns>
        /// <returns>Settings for linking files.</returns>
        /// <example>
        /// <para>Creates file link settings to an internal source hosting site with
        /// parameters set dynamically, based on values of the issue:</para>
        /// <code>
        /// <![CDATA[
        ///     var fileLinkSettings =
        ///         IssueFileLinkSettings(issue =>
        ///         {
        ///             var result =
        ///                 new Uri("https://awesomesource/")
        ///                     .Append(issue.FilePath());
        ///
        ///             if (issue.Line.HasValue)
        ///             {
        ///                 result = result.Append("?line={issue.Line.Value}")
        ///             }
        ///
        ///             return result;
        ///         });
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.FileLinkingCakeAliasCategory)]
        public static FileLinkSettings IssueFileLinkSettings(
            this ICakeContext context,
            Func<IIssue, Uri> builder)
        {
            context.NotNull(nameof(context));
            builder.NotNull(nameof(builder));

            return FileLinkSettings.ForAction(builder);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub on a specific branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
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
        /// e.g. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="branch">Name of the branch on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> if files are in the root of the repository.</param>
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

            return
                FileLinkSettings
                    .ForGitHub(repositoryUrl)
                    .Branch(branch)
                    .WithRootPath(rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking files hosted on GitHub fo a specific commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
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
        /// e.g. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
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

            return
                FileLinkSettings
                    .ForGitHub(repositoryUrl)
                    .Commit(commitId)
                    .WithRootPath(rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps
        /// on a specific branch.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
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
        /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="branch">Name of the branch on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> if files are in the root of the repository.</param>
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

            return
                FileLinkSettings
                    .ForAzureDevOps(repositoryUrl)
                    .Branch(branch)
                    .WithRootPath(rootPath);
        }

        /// <summary>
        /// Gets an instance of the file link settings for linking to files hosted in Azure DevOps
        /// for a specific commit.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
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
        /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="commitId">The commit id on which the file linking will be based on.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> if files are in the root of the repository.</param>
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

            return
                FileLinkSettings
                    .ForAzureDevOps(repositoryUrl)
                    .Commit(commitId)
                    .WithRootPath(rootPath);
        }
    }
}
