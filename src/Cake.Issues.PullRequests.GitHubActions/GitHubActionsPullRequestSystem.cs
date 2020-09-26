namespace Cake.Issues.PullRequests.GitHubActions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Class for posting issues to GitHub Actions.
    /// </summary>
    public class GitHubActionsPullRequestSystem : BasePullRequestSystem
    {
        private readonly GitHubActionsBuildSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubActionsPullRequestSystem"/> class.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="settings">Settings for writing the issues to GitHub Actions.</param>
        public GitHubActionsPullRequestSystem(ICakeContext context, GitHubActionsBuildSettings settings)
            : base(context?.Log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            issues.NotNull(nameof(issues));

            if (this.settings.GroupIssues)
            {
                this.WriteGroupedIssues(issues);
            }
            else
            {
                this.WriteIssues(issues);
            }
        }

        /// <summary>
        /// Formats the options for the warning service message.
        /// </summary>
        /// <param name="rootDirectoryPath">The root path of the file, relative to the repository root.</param>
        /// <param name="filePath">The file path relative to the project root.</param>
        /// <param name="line">The line where the issue ocurred.</param>
        /// <param name="column">The column where the issue ocurred.</param>
        /// <returns>Formatted options string for the warning service message.</returns>
        private static string FormatWarningOptions(DirectoryPath rootDirectoryPath, FilePath filePath, int? line, int? column)
        {
            var result = new List<string>();

            if (filePath != null)
            {
                result.Add($"file={rootDirectoryPath.CombineWithFilePath(filePath)}");
            }

            if (line.HasValue)
            {
                result.Add($"line={line.Value}");
            }

            if (column.HasValue)
            {
                result.Add($"col={column}");
            }

            return string.Join(",", result);
        }

        /// <summary>
        /// Writes services messages to report issues to GitHub Actions grouped by provider and run.
        /// </summary>
        /// <param name="issues">Issues which should be reported.</param>
        private void WriteGroupedIssues(IEnumerable<IIssue> issues)
        {
            // Group annotations by provider type and run
            var groupedIssues =
                from issue in issues
                group issue by new { issue.ProviderType, issue.Run };

            foreach (var group in groupedIssues)
            {
                var groupName = group.First().ProviderName;

                if (!string.IsNullOrWhiteSpace(group.Key.Run))
                {
                    groupName += " - " + group.Key.Run;
                }

                this.Log.Information($"::group::{groupName}");

                this.WriteIssues(group);

                this.Log.Information($"::endgroup::{groupName}");
            }
        }

        /// <summary>
        /// Writes services message to report issues to GitHub Actions.
        /// </summary>
        /// <param name="issues">Issues which should be reported.</param>
        private void WriteIssues(IEnumerable<IIssue> issues)
        {
            foreach (var issue in issues.OrderByDescending(x => x.Priority))
            {
                // Commands don't support line breaks, therefore we only use the first line of the message.
                var message =
                    issue.MessageText
                        .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault()
                        ?.Trim();

                this.Log.Information(
                    $"::warning {FormatWarningOptions(this.Settings.RepositoryRoot, issue.AffectedFileRelativePath, issue.Line, issue.Column)}::{message}");
            }
        }
    }
}
