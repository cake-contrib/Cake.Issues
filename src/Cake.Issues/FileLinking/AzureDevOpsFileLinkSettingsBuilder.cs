namespace Cake.Issues.FileLinking
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for building settings for file links of files hosted on Azure DevOps.
    /// </summary>
    public class AzureDevOpsFileLinkSettingsBuilder
    {
        private readonly Uri repositoryUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsFileLinkSettingsBuilder"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://github.com/cake-contrib/Cake.Issues</code>.</param>
        internal AzureDevOpsFileLinkSettingsBuilder(Uri repositoryUrl)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));

            this.repositoryUrl = repositoryUrl;
        }

        /// <summary>
        /// Returns settings to files on AzureDevOps on a specific branch.
        /// </summary>
        /// <param name="branchName">Name of the branch.</param>
        /// <returns>Class for setting additional settings.</returns>
        public FileLinkOptionalSettingsBuilder Branch(string branchName)
        {
            branchName.NotNullOrWhiteSpace(nameof(branchName));

            return
                new FileLinkOptionalSettingsBuilder(
                    (issue, values) =>
                    {
                        issue.NotNull(nameof(issue));
                        values.NotNull(nameof(values));

                        var fileLinkPattern =
                            this.GetFileLinkPattern(issue, values, "GB", branchName);
                        return new Uri(fileLinkPattern.ReplaceIssuePattern(issue));
                    });
        }

        /// <summary>
        /// Returns settings to files on AzureDevOps on a specific commit.
        /// </summary>
        /// <param name="commitId">The commit id on which the file linking will be based on.</param>
        /// <returns>Class for setting additional settings.</returns>
        public FileLinkOptionalSettingsBuilder Commit(string commitId)
        {
            commitId.NotNullOrWhiteSpace(nameof(commitId));

            return
                new FileLinkOptionalSettingsBuilder(
                    (issue, values) =>
                    {
                        issue.NotNull(nameof(issue));
                        values.NotNull(nameof(values));

                        var fileLinkPattern =
                            this.GetFileLinkPattern(issue, values, "GC", commitId);
                        return new Uri(fileLinkPattern.ReplaceIssuePattern(issue));
                    });
        }

        private string GetFileLinkPattern(
            IIssue issue,
            IDictionary<string, string> values,
            string versionType,
            string version)
        {
            issue.NotNull(nameof(issue));
            values.NotNull(nameof(values));
            versionType.NotNullOrWhiteSpace(nameof(versionType));
            version.NotNullOrWhiteSpace(nameof(version));

            var result =
                this.repositoryUrl.ToString().TrimEnd('/') +
                "?path=/" + values.GetValueOrDefault("rootPath", null)?.TrimStart('/').WithEnding("/") + "{FilePath}" +
                "&version=" + versionType + version;

            if (issue.Line.HasValue)
            {
                result +=
                    "&line={Line}" +
                    "&lineEnd=" + (issue.EndLine.HasValue ? "{EndLine}" : "{Line}") +
                    "&lineStartColumn=" + (issue.Column.HasValue ? "{Column}" : "1") +
                    "&lineEndColumn=" + (issue.EndColumn.HasValue ? "{EndColumn}" : int.MaxValue.ToString());
            }

            return result;
        }
    }
}
