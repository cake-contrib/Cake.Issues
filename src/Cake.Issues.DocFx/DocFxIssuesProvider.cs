namespace Cake.Issues.DocFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Diagnostics;
    using Core.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provider for warnings reported by DocFx.
    /// </summary>
    internal class DocFxIssuesProvider : IssueProvider
    {
        private readonly DocFxIssuesSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public DocFxIssuesProvider(ICakeLog log, DocFxIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        public override string ProviderName => "DocFX";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            // Determine path of the doc root.
            var docRootPath = this.settings.DocRootPath;
            if (docRootPath.IsRelative)
            {
                docRootPath = docRootPath.MakeAbsolute(this.Settings.RepositoryRoot);
            }

            return
                from logEntry in this.settings.LogFileContent.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries).Select(x => "{" + x + "}")
                let logEntryObject = JsonConvert.DeserializeObject<JToken>(logEntry)
                let severity = (string)logEntryObject.SelectToken("message_severity")
                let file = this.TryGetFile(logEntryObject, docRootPath)
                let line = (int?)logEntryObject.SelectToken("line")
                let message = (string)logEntryObject.SelectToken("message")
                let source = (string)logEntryObject.SelectToken("source") ?? "DocFx"
                where
                    severity == "warning" &&
                    !string.IsNullOrWhiteSpace(message)
                select
                    new Issue<DocFxIssuesProvider>(
                        this,
                        file,
                        line,
                        message,
                        0,
                        "Warning",
                        source);
        }

        /// <summary>
        /// Reads the affected file path from a issue logged in a DocFx log file.
        /// </summary>
        /// <param name="token">JSON Token object for the current log entry.</param>
        /// <param name="docRootPath">Absolute path to the root of the DocFx project.</param>
        /// <returns>The full path to the affected file.</returns>
        private string TryGetFile(
            JToken token,
            DirectoryPath docRootPath)
        {
            var fileName = (string)token.SelectToken("file");

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
    }
}