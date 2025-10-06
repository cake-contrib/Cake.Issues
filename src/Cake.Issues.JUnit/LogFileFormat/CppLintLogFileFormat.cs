namespace Cake.Issues.JUnit.LogFileFormat;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// CppLint log file format for parsing JUnit XML files specifically from cpplint.
/// Optimized for cpplint's specific JUnit format where test case names represent file names.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal partial class CppLintLogFileFormat(ICakeLog log)
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

                ProcessTestSuite(testSuite, result, repositorySettings, this.ProcessCppLintFailure, this.ProcessCppLintFailure);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to parse JUnit XML: {ex.Message}", ex);
        }

        return result;
    }

    [GeneratedRegex(@"^(\d+):\s*.*\[.*\].*\[.*\]", RegexOptions.Multiline)]
    private static partial Regex LineRegex();

    [GeneratedRegex(@"^(\d+):", RegexOptions.Multiline)]
    private static partial Regex SimpleLineRegex();

    /// <summary>
    /// Processes a cpplint test failure or error element and creates an issue.
    /// </summary>
    /// <param name="failureElement">The failure or error XML element.</param>
    /// <param name="className">The test class name.</param>
    /// <param name="testName">The test name.</param>
    /// <param name="priority">The issue priority.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>The created issue or null if the failure should be ignored.</returns>
    private IIssue ProcessCppLintFailure(XElement failureElement, string className, string testName, IssuePriority priority, IRepositorySettings repositorySettings)
    {
        var content = NormalizeXmlContent(failureElement.Value) ?? string.Empty;

        if (string.IsNullOrEmpty(content))
        {
            return null;
        }

        var issueBuilder = IssueBuilder
            .NewIssue(content, typeof(JUnitIssuesProvider).FullName, "JUnit")
            .WithPriority(priority);

        if (!string.IsNullOrEmpty(testName))
        {
            issueBuilder = issueBuilder.OfRule(testName);
        }

        // For cpplint-style output, if the test name looks like a file name,
        // use the test name as the file name and try to extract line info from the message
        if (!string.IsNullOrEmpty(testName))
        {
            // Check if the message contains line info in cpplint format like "5: FailMsg [category/subcategory] [3]"
            // This is a strong indicator that it's a cpplint-style failure where the test name is the file name
            var lineMatch = LineRegex().Match(content);
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
                var simpleLineMatch = SimpleLineRegex().Match(content);
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