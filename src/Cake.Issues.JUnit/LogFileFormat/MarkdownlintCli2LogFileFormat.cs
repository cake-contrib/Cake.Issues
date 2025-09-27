namespace Cake.Issues.JUnit.LogFileFormat;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// Markdownlint-cli2 log file format for parsing JUnit XML files specifically from markdownlint-cli2.
/// Optimized for markdownlint-cli2's specific JUnit format where file paths are in classname attributes.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal class MarkdownlintCli2LogFileFormat(ICakeLog log)
    : BaseJUnitLogFileFormat(log)
{
    /// <inheritdoc />
    public override IEnumerable<IIssue> ReadIssues(
        JUnitIssuesProvider issueProvider,
        IRepositorySettings repositorySettings,
        JUnitIssuesSettings junitIssuesSettings)
    {
        issueProvider.NotNull();
        repositorySettings.NotNull();
        junitIssuesSettings.NotNull();

        var result = new List<IIssue>();

        var logContent = junitIssuesSettings.LogFileContent.ToStringUsingEncoding();

        try
        {
            var testSuites = ParseJUnitXml(logContent);

            foreach (var testSuite in testSuites)
            {
                if (testSuite == null)
                {
                    continue;
                }

                ProcessTestSuite(testSuite, result, repositorySettings, this.ProcessMarkdownlintCli2Failure, this.ProcessMarkdownlintCli2Failure);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to parse JUnit XML: {ex.Message}", ex);
        }

        return result;
    }

    /// <summary>
    /// Tries to extract line and column information from markdownlint-cli2 format text.
    /// </summary>
    /// <param name="output">The output text to parse.</param>
    /// <returns>Line and column information if found, null otherwise.</returns>
    private static (int? Line, int? Column)? ExtractLineColumnFromMarkdownlintCli2(string output)
    {
        if (string.IsNullOrEmpty(output))
        {
            return null;
        }

        // Patterns for markdownlint-cli2 format:
        // Line 3, Column 10, Expected: 0 or 2; Actual: 1
        // Line 5, Expected: 1; Actual: 2
        // Line 6, Context: "# Description"
        string[] patterns =
        [
            @"Line\s+(\d+),\s+Column\s+(\d+)",  // Line 3, Column 10
            @"Line\s+(\d+)",                    // Line 5
        ];

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(output, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                int? line = null;
                int? column = null;

                if (int.TryParse(match.Groups[1].Value, out var lineNum))
                {
                    line = lineNum;
                }

                if (match.Groups.Count > 2 && int.TryParse(match.Groups[2].Value, out var colNum))
                {
                    column = colNum;
                }

                return (line, column);
            }
        }

        return null;
    }

    /// <summary>
    /// Processes a markdownlint-cli2 test failure or error element and creates an issue.
    /// </summary>
    /// <param name="failureElement">The failure or error XML element.</param>
    /// <param name="className">The test class name.</param>
    /// <param name="testName">The test name.</param>
    /// <param name="priority">The issue priority.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>The created issue or null if the failure should be ignored.</returns>
    private IIssue ProcessMarkdownlintCli2Failure(XElement failureElement, string className, string testName, IssuePriority priority, IRepositorySettings repositorySettings)
    {
        var message = failureElement.Attribute("message")?.Value ?? string.Empty;
        var content = NormalizeXmlContent(failureElement.Value) ?? string.Empty;

        // Combine message and content for full description
        var fullMessage = string.IsNullOrEmpty(message) ? content :
                         string.IsNullOrEmpty(content) ? message :
                         $"{message}\n{content}";

        if (string.IsNullOrEmpty(fullMessage))
        {
            return null;
        }

        var issueBuilder = IssueBuilder
            .NewIssue(fullMessage, typeof(JUnitIssuesProvider).FullName, "JUnit")
            .WithPriority(priority);

        // Use test name as rule (markdownlint-cli2 puts rule IDs like "MD009/no-trailing-spaces" in test names)
        if (!string.IsNullOrEmpty(testName))
        {
            issueBuilder = issueBuilder.OfRule(testName);
        }

        // For markdownlint-cli2 style output, check if the content contains the specific format
        // and use the class name as file path in that case
        if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(content))
        {
            var lineColumnInfo = ExtractLineColumnFromMarkdownlintCli2(content);
            if (lineColumnInfo.HasValue)
            {
                // This looks like markdownlint-cli2 format, use class name as file path
                var (line, column) = lineColumnInfo.Value;
                var (valid, filePath) = ValidateFilePath(className, repositorySettings);
                if (valid)
                {
                    issueBuilder = issueBuilder.InFile(filePath, line, column);
                }
            }
        }

        return issueBuilder.Create();
    }
}