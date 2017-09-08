namespace Cake.Issues.InspectCode
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by JetBrains Inspect Code.
    /// </summary>
    internal class InspectCodeIssuesProvider : IssueProvider
    {
        private readonly InspectCodeIssuesSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="InspectCodeIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public InspectCodeIssuesProvider(ICakeLog log, InspectCodeIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            var result = new List<IIssue>();

            var logDocument = XDocument.Parse(this.settings.LogFileContent);

            var solutionPath = Path.GetDirectoryName(logDocument.Descendants("Solution").Single().Value);

            // Read all issue types
            var issueTypes =
                logDocument.Descendants("IssueType").ToDictionary(
                    x => x.Attribute("Id")?.Value,
                    x => new IssueType
                    {
                        WikiUrl = x.Attribute("WikiUrl")?.Value.ToUri()
                    });

            // Loop through all issue tags.
            foreach (var issue in logDocument.Descendants("Issue"))
            {
                // Read affected file from the issue.
                if (!TryGetFile(issue, solutionPath, out string fileName))
                {
                    continue;
                }

                // Read affected line from the issue.
                if (!TryGetLine(issue, out int line))
                {
                    continue;
                }

                // Read rule code from the issue.
                if (!TryGetRule(issue, out string rule))
                {
                    continue;
                }

                // Read message from the issue.
                if (!TryGetMessage(issue, out string message))
                {
                    continue;
                }

                result.Add(new Issue<InspectCodeIssuesProvider>(
                    fileName,
                    line,
                    message,
                    0, // TODO Set based on severity of issueType
                    rule,
                    issueTypes[rule].WikiUrl));
            }

            return result;
        }

        /// <summary>
        /// Reads the affected file path from an issue logged in a Inspect Code log.
        /// </summary>
        /// <param name="issue">Issue element from Inspect Code log.</param>
        /// <param name="solutionPath">Path to the solution file.</param>
        /// <param name="fileName">Returns the full path to the affected file.</param>
        /// <returns>True if the file path could be parsed.</returns>
        private static bool TryGetFile(XElement issue, string solutionPath, out string fileName)
        {
            fileName = string.Empty;

            var fileAttr = issue.Attribute("File");
            if (fileAttr == null)
            {
                return false;
            }

            fileName = fileAttr.Value;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            // Combine with path to the solution file.
            fileName = Path.Combine(solutionPath, fileName);

            return true;
        }

        /// <summary>
        /// Reads the affected line from an issue logged in a Inspect Code log.
        /// </summary>
        /// <param name="issue">Issue element from Inspect Code log.</param>
        /// <param name="line">Returns line.</param>
        /// <returns>True if the line could be parsed.</returns>
        private static bool TryGetLine(XElement issue, out int line)
        {
            line = -1;

            var lineAttr = issue.Attribute("Line");

            var lineValue = lineAttr?.Value;
            if (string.IsNullOrWhiteSpace(lineValue))
            {
                return false;
            }

            line = int.Parse(lineValue, CultureInfo.InvariantCulture);

            return true;
        }

        /// <summary>
        /// Reads the rule code from an issue logged in a Inspect Code log.
        /// </summary>
        /// <param name="issue">Issue element from Inspect Code log.</param>
        /// <param name="rule">Returns the code of the rule.</param>
        /// <returns>True if the rule code could be parsed.</returns>
        private static bool TryGetRule(XElement issue, out string rule)
        {
            rule = string.Empty;

            var codeAttr = issue.Attribute("TypeId");
            if (codeAttr == null)
            {
                return false;
            }

            rule = codeAttr.Value;
            if (string.IsNullOrWhiteSpace(rule))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads the message from an issue logged in a Inspect Code log.
        /// </summary>
        /// <param name="issue">Issue element from Inspect Code log.</param>
        /// <param name="message">Returns the message of the issue.</param>
        /// <returns>True if the message could be parsed.</returns>
        private static bool TryGetMessage(XElement issue, out string message)
        {
            message = string.Empty;

            var messageAttr = issue.Attribute("Message");
            if (messageAttr == null)
            {
                return false;
            }

            message = messageAttr.Value;
            if (string.IsNullOrWhiteSpace(message))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Description of an issue type.
        /// </summary>
        private class IssueType
        {
            /// <summary>
            /// Gets or sets the URL to the page containing documentation about this issue type.
            /// </summary>
            public Uri WikiUrl { get; set; }
        }
    }
}
