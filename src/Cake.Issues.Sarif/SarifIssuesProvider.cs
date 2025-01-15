namespace Cake.Issues.Sarif;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
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
            var issueProviderName =
                this.IssueProviderSettings.UseToolNameAsIssueProviderName ? run.Tool.Driver.Name : this.ProviderName;

            foreach (var sarifResult in run.Results)
            {
                if (sarifResult.Suppressions != null &&
                    sarifResult.Suppressions.Any(x =>
                        x != null &&
                        (
                            x.Status == SuppressionStatus.None ||
                            x.Status == SuppressionStatus.Accepted)) &&
                    this.IssueProviderSettings.IgnoreSuppressedIssues)
                {
                    continue;
                }

                var (text, markdown) = GetMessage(sarifResult, run);
                var (ruleId, ruleUrl) = GetRule(sarifResult, run);
                var (filePath, startLine, endLine, startColumn, endColumn) = GetLocation(sarifResult, this.Settings);

                // Build issue.
                var issueBuilder =
                    IssueBuilder
                        .NewIssue(
                            text,
                            typeof(SarifIssuesProvider).FullName,
                            issueProviderName)
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
                            .InFile(filePath, startLine, endLine, startColumn, endColumn);
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
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>File and line of the result.</returns>
    private static (string FilePath, int? StartLine, int? EndLine, int? StartColumn, int? EndColumn) GetLocation(
        Result result, IRepositorySettings repositorySettings)
    {
        result.NotNull();

        // Only consider the first location.
        var location = result.Locations.FirstOrDefault();
        if (location is { PhysicalLocation: not null })
        {
            // Depending on tool Uri is written differently:
            // - Absolute path on Windows RFC 3986 conform: file://C:/path/to/file
            // - Absolute path on Windows not RFC 3986 conform: C:/path/to/file
            // - Absolute path on Linux not RFC 3986 conform: /path/to/file
            // - Absolute path on Linux RFC 3986 conform: file://path/to/file
            // - Relative path on Windows: path/to/file
            string filePath;
            bool isAbsolutePath;
            try
            {
                if (location.PhysicalLocation.ArtifactLocation.Uri.IsFile)
                {
                    // Handle RFC 3986 conform URIs.
                    if (!string.IsNullOrEmpty(location.PhysicalLocation.ArtifactLocation.Uri.Host))
                    {
                        // The case for Linux style URIs: file://path/to/file
                        filePath = $"/{location.PhysicalLocation.ArtifactLocation.Uri.Host}{location.PhysicalLocation.ArtifactLocation.Uri.AbsolutePath}";
                    }
                    else
                    {
                        // The case for Windows style URIs: file://C:/path/to/file
                        filePath = location.PhysicalLocation.ArtifactLocation.Uri.AbsolutePath;
                    }
                }
                else
                {
                    // Handle URIs without file scheme.
                    filePath = location.PhysicalLocation.ArtifactLocation.Uri.AbsolutePath;
                }

                isAbsolutePath = location.PhysicalLocation.ArtifactLocation.Uri.IsAbsoluteUri;
            }
            catch (InvalidOperationException)
            {
                // Handle relative paths.
                filePath = location.PhysicalLocation.ArtifactLocation.Uri.ToString();
                isAbsolutePath = !new FilePath(filePath).IsRelative;
            }

            // Validate file path and make relative to repository root if it is an absolute path.
            (var pathValidationResult, filePath) = ValidateFilePath(filePath, isAbsolutePath, repositorySettings);

            if (!pathValidationResult)
            {
                return (null, null, null, null, null);
            }

            int? startLine = null;
            int? endLine = null;
            int? startColumn = null;
            int? endColumn = null;
            if (location.PhysicalLocation.Region != null)
            {
                startLine = location.PhysicalLocation.Region.StartLine > 0 ? location.PhysicalLocation.Region.StartLine : null;
                endLine = location.PhysicalLocation.Region.EndLine > 0 ? location.PhysicalLocation.Region.EndLine : null;
                startColumn = location.PhysicalLocation.Region.StartColumn > 0 ? location.PhysicalLocation.Region.StartColumn : null;
                endColumn = location.PhysicalLocation.Region.EndColumn > 0 ? location.PhysicalLocation.Region.EndColumn : null;
            }

            return (filePath, startLine, endLine, startColumn, endColumn);
        }

        return (null, null, null, null, null);
    }

    /// <summary>
    /// Validates a file path.
    /// </summary>
    /// <param name="filePath">Full file path.</param>
    /// <param name="isAbsolutePath">Indicates if the file path is an absolute path.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
    private static (bool Valid, string FilePath) ValidateFilePath(
        string filePath,
        bool isAbsolutePath,
        IRepositorySettings repositorySettings)
    {
        filePath.NotNullOrWhiteSpace();
        repositorySettings.NotNull();

        if (isAbsolutePath)
        {
            // Ignore files from outside the repository.
            if (!filePath.IsInRepository(repositorySettings))
            {
                return (false, string.Empty);
            }

            // Make path relative to repository root.
            filePath = filePath.NormalizePath().MakeFilePathRelativeToRepositoryRoot(repositorySettings);
        }

        return (true, filePath);
    }
}