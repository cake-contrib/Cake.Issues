namespace Cake.Issues.InspectCode
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by JetBrains Inspect Code.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="issueProviderSettings">Settings for the issue provider.</param>
    internal class InspectCodeIssuesProvider(ICakeLog log, InspectCodeIssuesSettings issueProviderSettings)
        : BaseConfigurableIssueProvider<InspectCodeIssuesSettings>(log, issueProviderSettings)
    {
        /// <inheritdoc />
        public override string ProviderName => "InspectCode";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues()
        {
            var result = new List<IIssue>();

            var logDocument = XDocument.Parse(this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding());

            var solutionPath = Path.GetDirectoryName(logDocument.Descendants("Solution").Single().Value);

            // Read all issue types.
            var issueTypes =
                logDocument.Descendants("IssueType").ToDictionary(
                    x => x.Attribute("Id")?.Value,
                    x => new IssueType
                    {
                        Description = x.Attribute("Description")?.Value,
                        Severity = x.Attribute("Severity")?.Value,
                        WikiUrl = x.Attribute("WikiUrl")?.Value.ToUri(),
                    });

            // Loop through all issue tags.
            foreach (var issue in logDocument.Descendants("Issue"))
            {
                // Read affected project from the issue.
                if (!TryGetProject(issue, out var projectName))
                {
                    continue;
                }

                // Read affected file from the issue.
                if (!TryGetFile(issue, solutionPath, out var fileName))
                {
                    continue;
                }

                // Read affected line from the issue.
                if (!TryGetLine(issue, out var line))
                {
                    continue;
                }

                // Read rule code from the issue.
                if (!TryGetRuleId(issue, out var ruleId))
                {
                    continue;
                }

                // Read message from the issue.
                if (!TryGetMessage(issue, out var message))
                {
                    continue;
                }

                // Determine issue type properties.
                var issueType = issueTypes[ruleId];
                var severity = issueType.Severity.ToLowerInvariant();
                var ruleDescription = issueType.Description;
                var ruleUrl = issueType.WikiUrl;

                // Build issue.
                result.Add(
                    IssueBuilder
                        .NewIssue(message, this)
                        .InProjectOfName(projectName)
                        .InFile(fileName, line)
                        .WithPriority(GetPriority(severity))
                        .OfRule(ruleId, ruleDescription, ruleUrl)
                        .Create());
            }

            return result;
        }

        /// <summary>
        /// Determines the project for an issue logged in an Inspect Code log.
        /// </summary>
        /// <param name="issue">Issue element from Inspect Code log.</param>
        /// <param name="project">Returns project.</param>
        /// <returns>True if the project could be parsed.</returns>
        private static bool TryGetProject(
            XElement issue,
            out string project)
        {
            project = string.Empty;

            var projectNode = issue.Ancestors("Project").FirstOrDefault();
            if (projectNode == null)
            {
                return false;
            }

            var projectAttr = projectNode.Attribute("Name");
            if (projectAttr == null)
            {
                return false;
            }

            project = projectAttr.Value;
            if (string.IsNullOrWhiteSpace(project))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads the affected file path from an issue logged in an Inspect Code log.
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
        /// Reads the affected line from an issue logged in an Inspect Code log.
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
                var offsetAttr = issue.Attribute("Offset");

                var offsetValue = offsetAttr?.Value;
                if (string.IsNullOrWhiteSpace(offsetValue))
                {
                    return false;
                }

                // There are cases where InspectCode reports an offset, but no line.
                // In this case we will assume line 1.
                line = 1;
                return true;
            }

            line = int.Parse(lineValue, CultureInfo.InvariantCulture);

            return true;
        }

        /// <summary>
        /// Reads the rule code from an issue logged in an Inspect Code log.
        /// </summary>
        /// <param name="issue">Issue element from Inspect Code log.</param>
        /// <param name="ruleId">Returns the code of the rule.</param>
        /// <returns>True if the rule code could be parsed.</returns>
        private static bool TryGetRuleId(XElement issue, out string ruleId)
        {
            ruleId = string.Empty;

            var codeAttr = issue.Attribute("TypeId");
            if (codeAttr == null)
            {
                return false;
            }

            ruleId = codeAttr.Value;
            if (string.IsNullOrWhiteSpace(ruleId))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads the message from an issue logged in an Inspect Code log.
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
        /// Converts the severity level to a priority.
        /// </summary>
        /// <param name="severity">Severity level as reported by InspectCode.</param>
        /// <returns>Priority.</returns>
        private static IssuePriority GetPriority(string severity) =>
            severity.ToLowerInvariant() switch
            {
                "hint" => IssuePriority.Hint,
                "suggestion" => IssuePriority.Suggestion,
                "warning" => IssuePriority.Warning,
                "error" => IssuePriority.Error,
                _ => IssuePriority.Undefined,
            };

        /// <summary>
        /// Description of an issue type.
        /// </summary>
        private class IssueType
        {
            /// <summary>
            /// Gets the description of the issue.
            /// </summary>
            public string Description { get; init; }

            /// <summary>
            /// Gets the severity of this issue type.
            /// </summary>
            public string Severity { get; init; }

            /// <summary>
            /// Gets the URL to the page containing documentation about this issue type.
            /// </summary>
            public Uri WikiUrl { get; init; }
        }
    }
}
