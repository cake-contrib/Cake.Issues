namespace Cake.Issues.DocFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

            return
                from logEntry in this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding(true).Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries).Select(x => "{" + x + "}")
                let logEntryObject = JsonConvert.DeserializeObject<JToken>(logEntry)
                let severity = (string)logEntryObject.SelectToken("message_severity")
                let file = this.TryGetFile(logEntryObject, docRootPath)
                let line = this.TryGetLine(logEntryObject)
                let message = (string)logEntryObject.SelectToken("message")
                let source = (string)logEntryObject.SelectToken("source") ?? "DocFx"
                where
                    severity == "warning" &&
                    !string.IsNullOrWhiteSpace(message)
                select
                    IssueBuilder
                        .NewIssue(message, this)
                        .InFile(file, line)
                        .OfRule(source)
                        .WithPriority(IssuePriority.Warning)
                        .Create();
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

        /// <summary>
        /// Reads the affected line from a issue logged in a DocFx log file.
        /// </summary>
        /// <param name="token">JSON Token object for the current log entry.</param>
        /// <returns>The line of the issue.</returns>
        private int? TryGetLine(
            JToken token)
        {
            var line = (int?)token.SelectToken("line");

            // Convert negative line numbers or line number 0 to null
            if (line.HasValue && line.Value <= 0)
            {
                return null;
            }

            return line;
        }
    }
}