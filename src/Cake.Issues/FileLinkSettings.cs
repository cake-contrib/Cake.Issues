namespace Cake.Issues
{
    using System;
    using System.Collections.Generic;
    using Cake.Issues.FileLinking;

    /// <summary>
    /// Settings how issues should be linked to files.
    /// </summary>
    public class FileLinkSettings
    {
        private readonly Func<IIssue, IDictionary<string, string>, Uri> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLinkSettings"/> class.
        /// </summary>
        /// <param name="builder">Callback called for building the file link.</param>
        internal FileLinkSettings(Func<IIssue, IDictionary<string, string>, Uri> builder)
        {
            builder.NotNull(nameof(builder));

            this.builder = builder;
        }

        /// <summary>
        /// Returns the URL to the file on the source code hosting system
        /// for the issue <paramref name="issue"/>.
        /// </summary>
        /// <param name="issue">Issue for which the link should be returned.</param>
        /// <returns>URL to the file on the source code hosting system.</returns>
        public Uri GetFileLink(IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return this.builder(issue, new Dictionary<string, string>());
        }

        /// <summary>
        /// Returns builder class for settings for linking to files hosted in GitHub.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues</code>.</param>
        /// <returns>Builder class for the settings.</returns>
        internal static GitHubFileLinkSettingsBuilder ForGitHub(Uri repositoryUrl)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));

            return new GitHubFileLinkSettingsBuilder(repositoryUrl);
        }

        /// <summary>
        /// Returns builder class for settings for linking to files hosted in Azure DevOps.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
        /// <returns>Builder class for the settings.</returns>
        internal static AzureDevOpsFileLinkSettingsBuilder ForAzureDevOps(Uri repositoryUrl)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));

            return new AzureDevOpsFileLinkSettingsBuilder(repositoryUrl);
        }
    }
}
