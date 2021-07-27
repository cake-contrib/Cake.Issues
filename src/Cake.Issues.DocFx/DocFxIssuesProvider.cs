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

        /// <summary>
        /// Gets the name of the DocFx issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        public static string ProviderTypeName => typeof(DocFxIssuesProvider).FullName;

        /// <inheritdoc />
        public override string ProviderName => "DocFX";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues()
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
                let line = TryGetLine(logEntry.line)
                where
                    (logEntry.message_severity == "warning" || logEntry.message_severity == "suggestion") &&
                    !string.IsNullOrWhiteSpace(logEntry.message)
                select
                    IssueBuilder
                        .NewIssue(logEntry.message, this)
                        .InFile(file, line)
                        .OfRule(logEntry.source)
                        .WithPriority(GetPriority(logEntry.message_severity))
                        .Create();
        }

        /// <summary>
        /// Converts the severity to a priority.
        /// </summary>
        /// <param name="severity">Severity as reported by DocFX.</param>
        /// <returns>Priority.</returns>
        private static IssuePriority GetPriority(string severity)
        {
            switch (severity.ToLower())
            {
                case "warning":
                    return IssuePriority.Warning;

                case "suggestion":
                    return IssuePriority.Suggestion;

                default:
                    return IssuePriority.Undefined;
            }
        }

        /// <summary>
        /// Reads the affected line from a issue logged in a DocFx log file.
        /// </summary>
        /// <param name="line">The line in the current log entry.</param>
        /// <returns>The line of the issue.</returns>
        private static int? TryGetLine(
            int? line)
        {
            // Convert negative line numbers or line number 0 to null
            if (line.HasValue && line.Value <= 0)
            {
                return null;
            }

            return line;
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
            if (fileName.StartsWith("/", StringComparison.InvariantCulture))
            {
                fileName = fileName.Substring(1);
            }

            return fileName;
        }
    }
}