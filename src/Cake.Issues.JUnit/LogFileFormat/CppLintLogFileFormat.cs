namespace Cake.Issues.JUnit.LogFileFormat;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// CppLint log file format for parsing JUnit XML files specifically from cpplint.
/// Optimized for cpplint's specific JUnit format where test case names represent file names.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal class CppLintLogFileFormat(ICakeLog log)
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
        var lines = content.Split(['\r', '\n'], StringSplitOptions.None);
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
                var issue = this.ProcessCppLintFailure(failure, testName, IssuePriority.Error, repositorySettings);
                if (issue != null)
                {
                    result.Add(issue);
                }
            }

            // Process errors
            foreach (var error in testCase.Elements("error"))
            {
                var issue = this.ProcessCppLintFailure(error, testName, IssuePriority.Error, repositorySettings);
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
    /// Processes a cpplint test failure or error element and creates an issue.
    /// </summary>
    /// <param name="failureElement">The failure or error XML element.</param>
    /// <param name="testName">The test name.</param>
    /// <param name="priority">The issue priority.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>The created issue or null if the failure should be ignored.</returns>
    private IIssue ProcessCppLintFailure(XElement failureElement, string testName, IssuePriority priority, IRepositorySettings repositorySettings)
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

        if (!string.IsNullOrEmpty(type))
        {
            issueBuilder = issueBuilder.OfRule(type);
        }
        else if (!string.IsNullOrEmpty(testName))
        {
            issueBuilder = issueBuilder.OfRule(testName);
        }

        // For cpplint-style output, if the test name looks like a file name,
        // use the test name as the file name and try to extract line info from the message
        if (!string.IsNullOrEmpty(testName) && testName != "errors")
        {
            // Check if the message contains line info in cpplint format like "5: FailMsg [category/subcategory] [3]"
            // This is a strong indicator that it's a cpplint-style failure where the test name is the file name
            var lineMatch = Regex.Match(
                fullMessage,
                @"^(\d+):\s*.*\[.*\].*\[.*\]",
                RegexOptions.Multiline);
            if (lineMatch.Success && int.TryParse(lineMatch.Groups[1].Value, out var lineNum))
            {
                var (valid, filePath) = ValidateFilePath(testName, repositorySettings);
                if (valid)
                {
                    issueBuilder = issueBuilder.InFile(filePath, lineNum, null);
                }
            }

            // Also check for simple line number pattern without the category/subcategory format
            else
            {
                var simpleLineMatch = Regex.Match(
                    fullMessage,
                    @"^(\d+):",
                    RegexOptions.Multiline);
                if (simpleLineMatch.Success &&
                    int.TryParse(simpleLineMatch.Groups[1].Value, out var simpleLineNum))
                {
                    // Only treat as file if the test name doesn't contain hyphens (which are common in rule names)
                    if (!testName.Contains('-') && !testName.Contains('_'))
                    {
                        var (valid, filePath) = ValidateFilePath(testName, repositorySettings);
                        if (valid)
                        {
                            issueBuilder = issueBuilder.InFile(filePath, simpleLineNum, null);
                        }
                    }
                }
            }
        }

        return issueBuilder.Create();
    }
}