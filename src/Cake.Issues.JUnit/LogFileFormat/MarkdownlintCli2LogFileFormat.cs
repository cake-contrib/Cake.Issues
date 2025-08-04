namespace Cake.Issues.JUnit.LogFileFormat;

using System;
using System.Collections.Generic;
using System.Linq;
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
            var doc = XDocument.Parse(logContent);

            // Handle both single testsuite and testsuites root elements
            var testSuites = doc.Root?.Name.LocalName == "testsuites"
                ? doc.Root.Elements("testsuite")
                : new[] { doc.Root }.Where(x => x?.Name.LocalName == "testsuite");

            foreach (var testSuite in testSuites)
            {
                if (testSuite == null)
                {
                    continue;
                }

                this.ProcessTestSuite(testSuite, result, repositorySettings);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to parse JUnit XML: {ex.Message}", ex);
        }

        return result;
    }

    /// <summary>
    /// Recursively processes a testsuite element and its nested testsuites and testcases.
    /// </summary>
    /// <param name="testSuite">The testsuite element to process.</param>
    /// <param name="result">The list to add found issues to.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    private void ProcessTestSuite(XElement testSuite, List<IIssue> result, IRepositorySettings repositorySettings)
    {
        if (testSuite == null)
        {
            return;
        }

        // Process direct testcase children
        foreach (var testCase in testSuite.Elements("testcase"))
        {
            var className = testCase.Attribute("classname")?.Value ?? string.Empty;
            var testName = testCase.Attribute("name")?.Value ?? string.Empty;

            // Process failures
            foreach (var failure in testCase.Elements("failure"))
            {
                var issue = this.ProcessMarkdownlintCli2Failure(failure, className, testName, IssuePriority.Error, repositorySettings);
                if (issue != null)
                {
                    result.Add(issue);
                }
            }

            // Process errors
            foreach (var error in testCase.Elements("error"))
            {
                var issue = this.ProcessMarkdownlintCli2Failure(error, className, testName, IssuePriority.Error, repositorySettings);
                if (issue != null)
                {
                    result.Add(issue);
                }
            }
        }

        // Recursively process nested testsuite elements
        foreach (var nestedTestSuite in testSuite.Elements("testsuite"))
        {
            this.ProcessTestSuite(nestedTestSuite, result, repositorySettings);
        }
    }

    /// <summary>
    /// Normalizes XML content by removing XML formatting indentation while preserving intentional structure.
    /// </summary>
    /// <param name="content">The XML content to normalize.</param>
    /// <returns>The normalized content.</returns>
    private static string NormalizeXmlContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return string.Empty;
        }

        // Split by lines, trim each line to remove XML indentation, then rejoin
        var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
        var normalizedLines = new List<string>();

        foreach (var line in lines)
        {
            // Trim leading and trailing whitespace (including tabs) from each line
            var trimmedLine = line.Trim();
            normalizedLines.Add(trimmedLine);
        }

        // Join lines back together and clean up multiple consecutive empty lines
        var result = string.Join("\n", normalizedLines);
        
        // Remove leading and trailing empty lines
        result = result.Trim('\n');
        
        // Normalize multiple consecutive newlines to double newlines maximum
        result = Regex.Replace(result, @"\n{3,}", "\n\n");
        
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
        var patterns = new[]
        {
            @"Line\s+(\d+),\s+Column\s+(\d+)",  // Line 3, Column 10
            @"Line\s+(\d+)",                    // Line 5
        };

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
        var type = failureElement.Attribute("type")?.Value ?? string.Empty;
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
        else if (!string.IsNullOrEmpty(type))
        {
            issueBuilder = issueBuilder.OfRule(type);
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
                var pathValidation = ValidateFilePath(className, repositorySettings);
                if (pathValidation.Valid)
                {
                    issueBuilder = issueBuilder.InFile(pathValidation.FilePath, line, column);
                }
            }
        }

        return issueBuilder.Create();
    }
}