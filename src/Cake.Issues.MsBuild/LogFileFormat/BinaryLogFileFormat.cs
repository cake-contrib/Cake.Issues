namespace Cake.Issues.MsBuild.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Logging.StructuredLogger;

    /// <summary>
    /// MsBuild binary log file format.
    /// </summary>
    internal class BinaryLogFileFormat : BaseMsBuildLogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public BinaryLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            MsBuildIssuesProvider issueProvider,
            RepositorySettings repositorySettings,
            MsBuildIssuesSettings issueProviderSettings)
        {
#pragma warning disable SA1123 // Do not place regions within elements
            #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            issueProviderSettings.NotNull(nameof(issueProviderSettings));

            #endregion

            var result = new List<IIssue>();

            var binLogReader = new BinLogReader();
            foreach (var record in binLogReader.ReadRecords(issueProviderSettings.LogFileContent))
            {
                var buildEventArgs = record.Args;

                if (buildEventArgs is BuildWarningEventArgs buildWarning)
                {
                    // Ignore warnings without a message.
                    if (string.IsNullOrWhiteSpace(buildWarning.Message))
                    {
                        continue;
                    }

                    var projectFileRelativePath = this.GetProject(buildWarning, repositorySettings);

                    // Read affected file from the warning.
                    if (!this.TryGetFile(buildWarning, repositorySettings, out string fileName))
                    {
                        continue;
                    }

                    var line = GetLine(buildWarning);
                    var rule = buildWarning.Code;

                    // Determine rule URL.
                    Uri ruleUrl = null;
                    if (!string.IsNullOrWhiteSpace(rule))
                    {
                        ruleUrl = MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(rule);
                    }

                    // Build issue.
                    result.Add(
                        IssueBuilder
                            .NewIssue(buildWarning.Message, issueProvider)
                            .WithPriority(IssuePriority.Warning)
                            .InProject(projectFileRelativePath, System.IO.Path.GetFileNameWithoutExtension(projectFileRelativePath))
                            .InFile(fileName, line)
                            .OfRule(rule, ruleUrl)
                            .Create());
                }
            }

            return result;
        }

        /// <summary>
        /// Reads the affected line from a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from MsBuild log.</param>
        /// <returns>Line number or null if warning is not related to a file.</returns>
        private static int? GetLine(BuildWarningEventArgs warning)
        {
            var line = warning.LineNumber;

            // Convert negative line numbers or line number 0 to null
            if (line <= 0)
            {
                return null;
            }

            return line;
        }

        /// <summary>
        /// Determines the project for a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from the MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <returns>Relative path to the project.</returns>
        private string GetProject(
            BuildWarningEventArgs warning,
            RepositorySettings repositorySettings)
        {
            var project = warning.ProjectFile;

            // Validate project path and make relative to repository root.
            return this.ValidateFilePath(project, repositorySettings).FilePath;
        }

        /// <summary>
        /// Reads the affected file path from a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="fileName">Returns the full path to the affected file.</param>
        /// <returns>True if the file path could be parsed.</returns>
        private bool TryGetFile(
            BuildWarningEventArgs warning,
            RepositorySettings repositorySettings,
            out string fileName)
        {
            fileName = warning.File;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return true;
            }

            // If not absolute path, combine with file path from project file.
            if (!fileName.IsFullPath())
            {
                var projectDirectory = System.IO.Path.GetDirectoryName(warning.ProjectFile);
                fileName = System.IO.Path.Combine(projectDirectory, fileName);
            }

            // Validate file path and make relative to repository root.
            bool result;
            (result, fileName) = this.ValidateFilePath(fileName, repositorySettings);
            return result;
        }
    }
}
