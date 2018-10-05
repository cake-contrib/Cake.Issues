namespace Cake.Issues.DocFx
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Provider for warnings reported by DocFx.
    /// </summary>
    internal class DocFxIssuesProvider : BaseConfigurableIssueProvider<DocFxIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="issueProviderSettings">Settings for the issue provider.</param>
        public DocFxIssuesProvider(ICakeLog log, DocFxIssuesSettings issueProviderSettings)
            : base(log, issueProviderSettings)
        {
        }

        /// <inheritdoc />
        public override string ProviderName => "DocFX";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            // Determine path of the doc root.
            var docRootPath = this.IssueProviderSettings.DocRootPath;
            if (docRootPath.IsRelative)
            {
                docRootPath = docRootPath.MakeAbsolute(this.Settings.RepositoryRoot);
            }

            IEnumerable<LogEntryDataContract> logFileEntries = null;

            var logFileContent = this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding(true);

            logFileContent =
                "[" +
                    string.Join(",", logFileContent.SplitLines()) +
                "]";

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(logFileContent)))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(LogEntryDataContract[]));
                logFileEntries = jsonSerializer.ReadObject(ms) as LogEntryDataContract[];
            }

            return
                from logEntry in logFileEntries
                let file = this.TryGetFile(logEntry.file, docRootPath)
                let line = this.TryGetLine(logEntry.line)
                where
                    logEntry.message_severity == "warning" &&
                    !string.IsNullOrWhiteSpace(logEntry.message)
                select
                    IssueBuilder
                        .NewIssue(logEntry.message, this)
                        .InFile(file, line)
                        .OfRule(logEntry.source)
                        .WithPriority(IssuePriority.Warning)
                        .Create();
        }

        /// <summary>
        /// Reads the affected file path from a issue logged in a DocFx log file.
        /// </summary>
        /// <param name="fileName">The file name in the current log entry.</param>
        /// <param name="docRootPath">Absolute path to the root of the DocFx project.</param>
        /// <returns>The full path to the affected file.</returns>
        private string TryGetFile(
            string fileName,
            DirectoryPath docRootPath)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            // Add path to repository root
            fileName = docRootPath.CombineWithFilePath(fileName).FullPath;

            // Make path relative to repository root.
            fileName = fileName.Substring(this.Settings.RepositoryRoot.FullPath.Length);

            // Remove leading directory separator.
            if (fileName.StartsWith("/"))
            {
                fileName = fileName.Substring(1);
            }

            return fileName;
        }

        /// <summary>
        /// Reads the affected line from a issue logged in a DocFx log file.
        /// </summary>
        /// <param name="line">The line in the current log entry.</param>
        /// <returns>The line of the issue.</returns>
        private int? TryGetLine(
            int? line)
        {
            // Convert negative line numbers or line number 0 to null
            if (line.HasValue && line.Value <= 0)
            {
                return null;
            }

            return line;
        }
    }
}