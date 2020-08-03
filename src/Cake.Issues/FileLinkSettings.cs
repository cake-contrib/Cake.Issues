﻿namespace Cake.Issues
{
    using System;

    /// <summary>
    /// Settings how issues should be linked to files.
    /// </summary>
    public class FileLinkSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileLinkSettings"/> class.
        /// </summary>
        /// <param name="fileLinkPattern">Pattern which should be used to link issues to files.</param>
        public FileLinkSettings(string fileLinkPattern)
        {
            fileLinkPattern.NotNullOrWhiteSpace(nameof(fileLinkPattern));

            this.FileLinkPattern = fileLinkPattern;
        }

        /// <summary>
        /// Gets the pattern which should be used to link issues to files.
        /// Fields in the form <c>{FieldName}</c> are replaced with the value of the issue.
        /// All fields of <see cref="IIssue"/> are supported.
        /// See <see cref="FileLinkSettingsExtensions.GetFileLink(FileLinkSettings, IIssue)"/>
        /// to receive the resolved URL to the file.
        /// </summary>
        public string FileLinkPattern { get; }

        /// <summary>
        /// Returns settings for linking to files hosted in GitHub.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        public static FileLinkSettings GitHub(
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return
                new FileLinkSettings(
                    repositoryUrl.Append("blob", branch, rootPath, "{FilePath}#L{Line}-L{EndLine}").ToString());
        }

        /// <summary>
        /// Returns settings for linking to files hosted in Azure DevOps.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in Azure DevOps.</returns>
        public static FileLinkSettings AzureDevOps(
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            if (!string.IsNullOrWhiteSpace(rootPath))
            {
                rootPath = rootPath.Trim('/') + "/";
            }

            return
                new FileLinkSettings(
                    repositoryUrl.ToString().TrimEnd('/') +
                    "?path=" + rootPath + "{FilePath}" +
                    "&version=GB" + branch +
                    "&line={Line}" +
                    "&lineEnd={EndLine}" +
                    "&lineStartColumn={Column}" +
                    "&lineEndColumn={EndColumn}");
        }
    }
}
