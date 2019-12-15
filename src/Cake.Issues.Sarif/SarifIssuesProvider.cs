namespace Cake.Issues.Sarif
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Issues;
    using Microsoft.CodeAnalysis.Sarif;
    using Newtonsoft.Json;

    /// <summary>
    /// Provider for issues in SARIF compatible formt.
    /// </summary>
    internal class SarifIssuesProvider : BaseConfigurableIssueProvider<SarifIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SarifIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="issueProviderSettings">Settings for the issue provider.</param>
        public SarifIssuesProvider(ICakeLog log, SarifIssuesSettings issueProviderSettings)
            : base(log, issueProviderSettings)
        {
        }

        /// <inheritdoc />
        public override string ProviderName => "SARIF";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues()
        {
            var result = new List<IIssue>();

            var log =
                JsonConvert.DeserializeObject<SarifLog>(this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding());

            foreach (var run in log.Runs)
            {
                var toolName = run.Tool.Driver.Name;

                foreach (var sarifResult in run.Results)
                {
                    var (text, markdown) = GetMessage(sarifResult, run);
                    var (ruleId, ruleUrl) = GetRule(sarifResult, run);
                    var (filePath, line) = GetLocation(sarifResult);

                    // Build issue.
                    var issueBuilder =
                        IssueBuilder
                            .NewIssue(
                                text,
                                typeof(SarifIssuesProvider).FullName,
                                toolName)
                            .WithPriority(sarifResult.Level.ToPriority())
                            .OfRule(ruleId, ruleUrl);

                    if (!string.IsNullOrEmpty(markdown))
                    {
                        issueBuilder =
                            issueBuilder
                                .WithMessageInMarkdownFormat(markdown);
                    }

                    if (filePath != null)
                    {
                        issueBuilder =
                            issueBuilder
                                .InFile(filePath, line);
                    }

                    result.Add(issueBuilder.Create());
                }
            }

            return result;
        }

        /// <summary>
        /// Determines the message for a SARIF result.
        /// </summary>
        /// <param name="result">Result to read the message from.</param>
        /// <param name="run">SARIF run description.</param>
        /// <returns>Message of the result.</returns>
        private static (string text, string markdown) GetMessage(
            Result result,
            Run run)
        {
            // Once https://github.com/microsoft/sarif-sdk/issues/430 is implemented this code should become mostly obsolete
            result.NotNull(nameof(result));
            run.NotNull(nameof(run));

            // If result has message text assigned directly.
            if (!string.IsNullOrEmpty(result.Message.Text))
            {
                return (result.Message.Text, result.Message.Markdown);
            }

            // If result has global message or message defined on rule.
            if (!string.IsNullOrEmpty(result.Message.Id))
            {
                // Check if a message defined on the rule was referenced.
                var rule = result.GetRule(run);
                if (rule.MessageStrings.ContainsKey(result.Message.Id))
                {
                    var message = rule.MessageStrings[result.Message.Id];
                    var arguments = result.Message.Arguments;
                    return GetFormattedMessage(message, arguments);
                }

                // Check if a global message was referenced.
                if (run.Tool.Driver.GlobalMessageStrings.ContainsKey(result.Message.Id))
                {
                    var message = run.Tool.Driver.GlobalMessageStrings[result.Message.Id];
                    var arguments = result.Message.Arguments;
                    return GetFormattedMessage(message, arguments);
                }
            }

            return (null, null);

            (string text, string markdown) GetFormattedMessage(
                MultiformatMessageString message,
                IList<string> arguments)
            {
                if (arguments.Any())
                {
                    string messageText = null;
                    string messageMarkdown = null;

                    if (!string.IsNullOrEmpty(message.Text))
                    {
                        messageText = string.Format(message.Text, arguments.ToArray());
                    }

                    if (!string.IsNullOrEmpty(message.Markdown))
                    {
                        messageMarkdown = string.Format(message.Markdown, arguments.ToArray());
                    }

                    return (messageText, messageMarkdown);
                }

                return (message.Text, message.Markdown);
            }
        }

        /// <summary>
        /// Determines the rule for a SARIF result.
        /// </summary>
        /// <param name="result">Result to read the rule from.</param>
        /// <param name="run">SARIF run description.</param>
        /// <returns>File and line of the result.</returns>
        private static (string ruleId, Uri ruleUrl) GetRule(
            Result result,
            Run run)
        {
            result.NotNull(nameof(result));
            run.NotNull(nameof(run));

            var rule = result.GetRule(run);

            return (rule.Id, rule.HelpUri);
        }

        /// <summary>
        /// Determines the location for a SARIF result.
        /// </summary>
        /// <param name="result">Result to read the location from.</param>
        /// <returns>File and line of the result.</returns>
        private static (string filePath, int? line) GetLocation(
            Result result)
        {
            result.NotNull(nameof(result));

            // Only consider the first location.
            var location = result.Locations.FirstOrDefault();
            if (location != null && location.PhysicalLocation != null)
            {
                var filePath = location.PhysicalLocation.ArtifactLocation.Uri.ToString();

                int? line = null;
                if (location.PhysicalLocation.Region != null)
                {
                    line = location.PhysicalLocation.Region.StartLine;
                }

                return (filePath, line);
            }

            return (null, null);
        }
    }
}