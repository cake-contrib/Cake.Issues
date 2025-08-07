namespace Cake.Issues.JUnit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

/// <summary>
/// Base class for all log file formats supported by the JUnit issue provider.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract partial class BaseJUnitLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<JUnitIssuesProvider, JUnitIssuesSettings>(log)
{
    /// <summary>
    /// Validates a file path.
    /// </summary>
    /// <param name="filePath">Full file path.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
    protected static (bool Valid, string FilePath) ValidateFilePath(string filePath, IRepositorySettings repositorySettings)
    {
        filePath.NotNullOrWhiteSpace();
        repositorySettings.NotNull();

        if (!new FilePath(filePath).IsRelative)
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

    /// <summary>
    /// Normalizes XML content by removing XML formatting indentation while preserving intentional structure.
    /// </summary>
    /// <param name="content">The XML content to normalize.</param>
    /// <returns>The normalized content.</returns>
    protected static string NormalizeXmlContent(string content)
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
        result = DoubleNewlinesRegex().Replace(result, "\n\n");

        return result;
    }

    /// <summary>
    /// Parses the JUnit XML document and extracts test suites.
    /// </summary>
    /// <param name="logContent">The XML log content to parse.</param>
    /// <returns>Collection of test suite elements.</returns>
    protected static IEnumerable<XElement> ParseJUnitXml(string logContent)
    {
        var doc = XDocument.Parse(logContent);

        // Handle both single testsuite and testsuites root elements
        return doc.Root?.Name.LocalName == "testsuites"
            ? doc.Root.Elements("testsuite")
            : new[] { doc.Root }.Where(x => x?.Name.LocalName == "testsuite");
    }

    /// <summary>
    /// Recursively processes a testsuite element and its nested testsuites and testcases.
    /// </summary>
    /// <param name="testSuite">The testsuite element to process.</param>
    /// <param name="result">The list to add found issues to.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <param name="failureProcessor">Function to process failure elements.</param>
    /// <param name="errorProcessor">Function to process error elements.</param>
    protected static void ProcessTestSuite(
        XElement testSuite,
        List<IIssue> result,
        IRepositorySettings repositorySettings,
        Func<XElement, string, string, IssuePriority, IRepositorySettings, IIssue> failureProcessor,
        Func<XElement, string, string, IssuePriority, IRepositorySettings, IIssue> errorProcessor)
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
                var issue = failureProcessor(failure, className, testName, IssuePriority.Error, repositorySettings);
                if (issue != null)
                {
                    result.Add(issue);
                }
            }

            // Process errors
            foreach (var error in testCase.Elements("error"))
            {
                var issue = errorProcessor(error, className, testName, IssuePriority.Error, repositorySettings);
                if (issue != null)
                {
                    result.Add(issue);
                }
            }
        }

        // Recursively process nested testsuite elements
        foreach (var nestedTestSuite in testSuite.Elements("testsuite"))
        {
            ProcessTestSuite(nestedTestSuite, result, repositorySettings, failureProcessor, errorProcessor);
        }
    }

    [GeneratedRegex(@"\n{3,}")]
    private static partial Regex DoubleNewlinesRegex();
}