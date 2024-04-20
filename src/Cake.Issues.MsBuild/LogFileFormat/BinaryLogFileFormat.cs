namespace Cake.Issues.MsBuild.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Cake.Core.Diagnostics;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Logging.StructuredLogger;

    /// <summary>
    /// MsBuild binary log file format.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    internal class BinaryLogFileFormat(ICakeLog log) : BaseMsBuildLogFileFormat(log)
    {
        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            MsBuildIssuesProvider issueProvider,
            IRepositorySettings repositorySettings,
            MsBuildIssuesSettings issueProviderSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            issueProviderSettings.NotNull(nameof(issueProviderSettings));

            // Ensure that strings for MsBuild.StructuredLogger are initialized.
            // See https://github.com/KirillOsenkov/MSBuildStructuredLog/issues/736
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Strings.Initialize();
            }

            var result = new List<IIssue>();

            var binLogReader = new BinLogReader();
            foreach (var buildEventArgs in binLogReader.ReadRecords(issueProviderSettings.LogFileContent).Select(x => x.Args))
            {
                IIssue issue = null;
                if (buildEventArgs is BuildErrorEventArgs buildError)
                {
                    issue = this.GetIssue(buildError, issueProvider, repositorySettings);
                }
                else if (buildEventArgs is BuildWarningEventArgs buildWarning)
                {
                    issue = this.GetIssue(buildWarning, issueProvider, repositorySettings);
                }

                if (issue == null)
                {
                    continue;
                }

                result.Add(issue);
            }

            return result;
        }

        /// <summary>
        /// Returns the column based on the value from a MsBuild log.
        /// </summary>
        /// <param name="column">Raw value from MsBuild log.</param>
        /// <returns>Column number or null if warning or error is not related to a file.</returns>
        private static int? GetColumn(int column)
        {
            // Convert negative column numbers or column number 0 to null
            if (column <= 0)
            {
                return null;
            }

            return column;
        }

        /// <summary>
        /// Returns the line based on the value from a MsBuild log.
        /// </summary>
        /// <param name="line">Raw value from MsBuild log.</param>
        /// <returns>Line number or null if warning or error is not related to a file.</returns>
        private static int? GetLine(int line)
        {
            // Convert negative line numbers or line number 0 to null
            if (line <= 0)
            {
                return null;
            }

            return line;
        }

        /// <summary>
        /// Returns an issue for a build error.
        /// </summary>
        /// <param name="buildError">Error for which the issue should be returned.</param>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <returns>Issue instance or null, if the <paramref name="buildError"/> could not be parsed.</returns>
        private IIssue GetIssue(
            BuildErrorEventArgs buildError,
            MsBuildIssuesProvider issueProvider,
            IRepositorySettings repositorySettings)
        {
            this.Log.Verbose("Process error '{0}'...", buildError.Message);

            return
                this.GetIssue(
                    IssuePriority.Error,
                    buildError.Message,
                    buildError.ProjectFile,
                    buildError.File,
                    buildError.LineNumber,
                    buildError.EndLineNumber,
                    buildError.ColumnNumber,
                    buildError.EndColumnNumber,
                    buildError.Code,
                    issueProvider,
                    repositorySettings);
        }

        /// <summary>
        /// Returns an issue for a build warning.
        /// </summary>
        /// <param name="buildWarning">Warning for which the issue should be returned.</param>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <returns>Issue instance or null, if the <paramref name="buildWarning"/> could not be parsed.</returns>
        private IIssue GetIssue(
            BuildWarningEventArgs buildWarning,
            MsBuildIssuesProvider issueProvider,
            IRepositorySettings repositorySettings)
        {
            this.Log.Verbose("Process warning '{0}'...", buildWarning.Message);

            return
                this.GetIssue(
                    IssuePriority.Warning,
                    buildWarning.Message,
                    buildWarning.ProjectFile,
                    buildWarning.File,
                    buildWarning.LineNumber,
                    buildWarning.EndLineNumber,
                    buildWarning.ColumnNumber,
                    buildWarning.EndColumnNumber,
                    buildWarning.Code,
                    issueProvider,
                    repositorySettings);
        }

        /// <summary>
        /// Returns an issue for values from an MsBuild log.
        /// </summary>
        /// <param name="priority">Priority of the issue.</param>
        /// <param name="message">Raw value from the MsBuild log containing the message.</param>
        /// <param name="projectFile">Raw value from the MsBuild log containing the project file.</param>
        /// <param name="file">Raw value from the MsBuild log containing the file.</param>
        /// <param name="lineNumber">Raw value from the MsBuild log containing the line number.</param>
        /// <param name="endLineNumber">Raw value from the MsBuild log containing the end of the line range.</param>
        /// <param name="columnNumber">Raw value from the MsBuild log containing the column.</param>
        /// <param name="endColumnNumber">Raw value from the MsBuild log containing the end of the column range.</param>
        /// <param name="code">Raw value from the MsBuild log containing the rule.</param>
        /// <param name="issueProvider">Issue provider instance.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <returns>Issue instance or null, if the values could not be parsed.</returns>
        private IIssue GetIssue(
            IssuePriority priority,
            string message,
            string projectFile,
            string file,
            int lineNumber,
            int endLineNumber,
            int columnNumber,
            int endColumnNumber,
            string code,
            MsBuildIssuesProvider issueProvider,
            IRepositorySettings repositorySettings)
        {
            // Ignore warnings or errors without a message.
            if (string.IsNullOrWhiteSpace(message))
            {
                this.Log.Verbose("Skip element since it doesn't contain a message");
                return null;
            }

            var projectFileRelativePath = this.GetProject(projectFile, repositorySettings);

            // Read affected file from the warning or error.
            var (result, fileName) = this.TryGetFile(file, projectFile, repositorySettings);
            if (!result)
            {
                this.Log.Information("Skip element since file path could not be parsed");
                return null;
            }

            var line = GetLine(lineNumber);
            var endLine = GetLine(endLineNumber);
            var column = GetColumn(columnNumber);
            var endColumn = GetColumn(endColumnNumber);
            var rule = code;

            // Determine rule URL.
            Uri ruleUrl = null;
            if (!string.IsNullOrWhiteSpace(rule))
            {
                ruleUrl = MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(rule);
            }

            // Build issue.
            return
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .WithPriority(priority)
                    .InProject(projectFileRelativePath, System.IO.Path.GetFileNameWithoutExtension(projectFileRelativePath))
                    .InFile(fileName, line, endLine, column, endColumn)
                    .OfRule(rule, ruleUrl)
                    .Create();
        }

        /// <summary>
        /// Determines the project from a value in a MsBuild log.
        /// </summary>
        /// <param name="project">Raw value from the MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <returns>Relative path to the project.</returns>
        private string GetProject(
            string project,
            IRepositorySettings repositorySettings)
        {
            // Validate project path and make relative to repository root.
            return this.ValidateFilePath(project, repositorySettings).FilePath;
        }

        /// <summary>
        /// Reads the affected file path from a value in a MsBuild log.
        /// </summary>
        /// <param name="fileName">Raw value for file path from MsBuild log.</param>
        /// <param name="project">Raw value for project path from the MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <returns>True if the file path could be parsed and the full path to the affected file.</returns>
        private (bool Successful, string FileName) TryGetFile(
            string fileName,
            string project,
            IRepositorySettings repositorySettings)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return (true, fileName);
            }

            // If not absolute path, combine with file path from project file.
            if (!fileName.IsFullPath())
            {
                var projectDirectory = System.IO.Path.GetDirectoryName(project);
                fileName = System.IO.Path.Combine(projectDirectory, fileName);
            }

            // Validate file path and make relative to repository root.
            return this.ValidateFilePath(fileName, repositorySettings);
        }
    }
}
