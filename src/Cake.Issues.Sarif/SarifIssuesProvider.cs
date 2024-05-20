namespace Cake.Issues.Sarif
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Issues;
    using Microsoft.CodeAnalysis.Sarif;
    using Newtonsoft.Json;

    /// <summary>
    /// Provider for issues in SARIF compatible format.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="issueProviderSettings">Settings for the issue provider.</param>
    internal class SarifIssuesProvider(ICakeLog log, SarifIssuesSettings issueProviderSettings) : BaseConfigurableIssueProvider<SarifIssuesSettings>(log, issueProviderSettings)
    {
        /// <inheritdoc />
        public override string ProviderName => "SARIF";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues()
        {
            var result = new List<IIssue>();

            var logContent =
                JsonConvert.DeserializeObject<SarifLog>(this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding());

            foreach (var run in logContent.Runs)
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
        private static (string Text, string Markdown) GetMessage(
            Result result,
            Run run)
        {
            // Once https://github.com/microsoft/sarif-sdk/issues/430 is implemented this code should become mostly obsolete
            result.NotNull();
            run.NotNull();

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
                if (rule.MessageStrings.TryGetValue(result.Message.Id, out var ruleMessage))
                {
                    var arguments = result.Message.Arguments;
                    return GetFormattedMessage(ruleMessage, arguments);
                }

                // Check if a global message was referenced.
                if (run.Tool.Driver.GlobalMessageStrings.TryGetValue(result.Message.Id, out var globalMessage))
                {
                    var arguments = result.Message.Arguments;
                    return GetFormattedMessage(globalMessage, arguments);
                }
            }

            return (null, null);

            static (string Text, string Markdown) GetFormattedMessage(
                MultiformatMessageString message,
                IList<string> arguments)
            {
                if (arguments.Any())
                {
                    string messageText = null;
                    string messageMarkdown = null;

                    if (!string.IsNullOrEmpty(message.Text))
                    {
                        messageText =
                            string.Format(
                                CultureInfo.InvariantCulture,
                                message.Text,
                                [.. arguments]);
                    }

                    if (!string.IsNullOrEmpty(message.Markdown))
                    {
                        messageMarkdown =
                            string.Format(
                                CultureInfo.InvariantCulture,
                                message.Markdown,
                                [.. arguments]);
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
        private static (string RuleId, Uri RuleUrl) GetRule(
            Result result,
            Run run)
        {
            result.NotNull();
            run.NotNull();

            var rule = result.GetRule(run);

            return (rule.Id, rule.HelpUri);
        }

        /// <summary>
        /// Determines the location for a SARIF result.
        /// </summary>
        /// <param name="result">Result to read the location from.</param>
        /// <returns>File and line of the result.</returns>
        private static (string FilePath, int? Line) GetLocation(
            Result result)
        {
            result.NotNull();

            // Only consider the first location.
            var location = result.Locations.FirstOrDefault();
            if (location is { PhysicalLocation: not null })
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