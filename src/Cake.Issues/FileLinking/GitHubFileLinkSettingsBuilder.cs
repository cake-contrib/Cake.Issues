namespace Cake.Issues.FileLinking
{
    using System;

    /// <summary>
    /// Class for building settings for file links of files hosted on GitHub.
    /// </summary>
    public class GitHubFileLinkSettingsBuilder
    {
        private readonly Uri repositoryUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubFileLinkSettingsBuilder"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// e.g. <code>https://github.com/cake-contrib/Cake.Issues</code>.</param>
        internal GitHubFileLinkSettingsBuilder(Uri repositoryUrl)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));

            this.repositoryUrl = repositoryUrl;
        }

        /// <summary>
        /// Returns settings to files on GitHub on a specific branch.
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
                            this.repositoryUrl.Append(
                                "blob",
                                branchName,
                                values.GetValueOrDefault("rootPath", null),
                                GetFilePathPattern(issue)).ToString();

                        return new Uri(fileLinkPattern.ReplaceIssuePattern(issue));
                    });
        }

        /// <summary>
        /// Returns settings to files on GitHub on a specific commit.
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
                            this.repositoryUrl.Append(
                                "blob",
                                commitId,
                                values.GetValueOrDefault("rootPath", null),
                                GetFilePathPattern(issue)).ToString();

                        return new Uri(fileLinkPattern.ReplaceIssuePattern(issue));
                    });
        }

        /// <summary>
        /// Returns the pattern for the file path and line information.
        /// </summary>
        /// <param name="issue">Issue for which the pattern should be returned.</param>
        /// <returns>Pattern.</returns>
        private static string GetFilePathPattern(IIssue issue)
        {
            issue.NotNull(nameof(issue));

            var filePathPattern = "{FilePath}";

            if (issue.Line.HasValue)
            {
                filePathPattern += "#L{Line}";
            }

            if (issue.EndLine.HasValue)
            {
                filePathPattern += "-L{EndLine}";
            }

            return filePathPattern;
        }
    }
}
