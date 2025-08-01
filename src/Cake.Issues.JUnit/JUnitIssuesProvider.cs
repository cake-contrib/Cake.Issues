namespace Cake.Issues.JUnit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// Provider for issues in JUnit XML format.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="issueProviderSettings">Settings for the issue provider.</param>
public class JUnitIssuesProvider(ICakeLog log, JUnitIssuesSettings issueProviderSettings)
    : BaseConfigurableIssueProvider<JUnitIssuesSettings>(log, issueProviderSettings)
{
    /// <summary>
    /// Gets the name of the JUnit issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    public static string ProviderTypeName => typeof(JUnitIssuesProvider).FullName;

    /// <inheritdoc />
    public override string ProviderName => "JUnit";

    /// <inheritdoc />
    protected override IEnumerable<IIssue> InternalReadIssues()
    {
        var result = new List<IIssue>();

        var logContent = this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding();

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

                var suiteName = testSuite.Attribute("name")?.Value ?? string.Empty;

                foreach (var testCase in testSuite.Elements("testcase"))
                {
                    var className = testCase.Attribute("classname")?.Value ?? string.Empty;
                    var testName = testCase.Attribute("name")?.Value ?? string.Empty;
                    var time = testCase.Attribute("time")?.Value;

                    // Process failures
                    foreach (var failure in testCase.Elements("failure"))
                    {
                        var issue = this.ProcessTestFailure(failure, className, testName, suiteName, IssuePriority.Error);
                        if (issue != null)
                        {
                            result.Add(issue);
                        }
                    }

                    // Process errors
                    foreach (var error in testCase.Elements("error"))
                    {
                        var issue = this.ProcessTestFailure(error, className, testName, suiteName, IssuePriority.Error);
                        if (issue != null)
                        {
                            result.Add(issue);
                        }
                    }

                    //// Process system-out for additional context
                    //var systemOut = testCase.Element("system-out")?.Value;
                    //if (!string.IsNullOrEmpty(systemOut) && (testCase.Elements("failure").Any() || testCase.Elements("error").Any()))
                    //{
                    //    // Try to extract file path and line number from system-out
                    //    var fileInfo = ExtractFileInfoFromOutput(systemOut);
                    //    if (fileInfo.HasValue)
                    //    {
                    //        // Update the last added issue with file information if it doesn't have it
                    //        var lastIssue = result.LastOrDefault();
                    //        if (lastIssue != null && lastIssue.AffectedFileRelativePath != null)
                    //        {
                    //            result[^1] = UpdateIssueWithFileInfo(lastIssue, fileInfo.Value);
                    //        }
                    //    }
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to parse JUnit XML: {ex.Message}", ex);
        }

        return result;
    }

    /// <summary>
    /// Tries to extract file path and line information from output text.
    /// </summary>
    /// <param name="output">The output text to parse.</param>
    /// <returns>File information if found, null otherwise.</returns>
    private static (string FilePath, int? Line, int? Column)? ExtractFileInfoFromOutput(string output)
    {
        if (string.IsNullOrEmpty(output))
        {
            return null;
        }

        // Common patterns for file paths and line numbers in linter output:
        // file.txt:123:45: message
        // file.txt(123,45): message  
        // file.txt line 123: message
        // /path/to/file.txt:123: message
        // file.txt:123 message

        var patterns = new[]
        {
            @"([^\s:]+):(\d+):(\d+)",        // file:line:column
            @"([^\s:]+):(\d+)",              // file:line
            @"([^\s\(\)]+)\((\d+),(\d+)\)",  // file(line,column)
            @"([^\s\(\)]+)\((\d+)\)",        // file(line)
            @"([^\s]+)\s+line\s+(\d+)",      // file line 123
            @"File:\s*([^\s]+)",             // File: path
        };

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(output, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success)
            {
                var filePath = match.Groups[1].Value.Trim();

                // Skip if it looks like a URL or doesn't look like a file path
                if (filePath.StartsWith("http") || filePath.StartsWith("www."))
                {
                    continue;
                }

                int? line = null;
                int? column = null;

                if (match.Groups.Count > 2 && int.TryParse(match.Groups[2].Value, out var lineNum))
                {
                    line = lineNum;
                }

                if (match.Groups.Count > 3 && int.TryParse(match.Groups[3].Value, out var colNum))
                {
                    column = colNum;
                }

                return (filePath, line, column);
            }
        }

        return null;
    }

    /// <summary>
    /// Tries to extract file path from a class name.
    /// </summary>
    /// <param name="className">The class name to parse.</param>
    /// <returns>File information if the class name looks like a file path, null otherwise.</returns>
    private static (string FilePath, int? Line, int? Column)? ExtractFileInfoFromClassName(string className)
    {
        if (string.IsNullOrEmpty(className))
        {
            return null;
        }

        // Some tools use file paths as class names
        if (className.Contains('/') || className.Contains('\\'))
        {
            return (className, null, null);
        }

        // Convert class names to potential file paths
        if (className.Contains('.'))
        {
            // Java-style package.Class -> package/Class.java (but only if it looks like a real package)
            var parts = className.Split('.');
            if (parts.Length > 1 && parts.All(p => !string.IsNullOrEmpty(p)))
            {
                var potentialPath = string.Join("/", parts) + ".java";
                return (potentialPath, null, null);
            }
        }

        return null;
    }

    ///// <summary>
    ///// Updates an existing issue with file information.
    ///// </summary>
    ///// <param name="issue">The original issue.</param>
    ///// <param name="fileInfo">The file information to add.</param>
    ///// <returns>A new issue with the file information.</returns>
    //private static IIssue UpdateIssueWithFileInfo(IIssue issue, (string FilePath, int? Line, int? Column) fileInfo)
    //{
    //    var (filePath, line, column) = fileInfo;

    //    return IssueBuilder
    //        .NewIssue(issue.MessageText, issue.ProviderType, issue.ProviderName)
    //        .WithPriority(issue.Priority)
    //        .OfRule(issue.Rule, issue.RuleUrl)
    //        .InFile(filePath, line, line, column, column)
    //        .Create();
    //}

    /// <summary>
    /// Processes a test failure or error element and creates an issue.
    /// </summary>
    /// <param name="failureElement">The failure or error XML element.</param>
    /// <param name="className">The test class name.</param>
    /// <param name="testName">The test name.</param>
    /// <param name="suiteName">The test suite name.</param>
    /// <param name="priority">The issue priority.</param>
    /// <returns>The created issue or null if the failure should be ignored.</returns>
    private IIssue ProcessTestFailure(XElement failureElement, string className, string testName, string suiteName, IssuePriority priority)
    {
        var message = failureElement.Attribute("message")?.Value ?? string.Empty;
        var type = failureElement.Attribute("type")?.Value ?? string.Empty;
        var content = failureElement.Value ?? string.Empty;

        // Combine message and content for full description
        var fullMessage = string.IsNullOrEmpty(message) ? content :
                         string.IsNullOrEmpty(content) ? message :
                         $"{message}\n{content}";

        if (string.IsNullOrEmpty(fullMessage))
        {
            return null;
        }

        var issueBuilder = IssueBuilder
            .NewIssue(fullMessage, ProviderTypeName, this.ProviderName)
            .WithPriority(priority);

        if (!string.IsNullOrEmpty(type))
        {
            issueBuilder = issueBuilder.OfRule(type);
        }
        else if (!string.IsNullOrEmpty(testName))
        {
            issueBuilder = issueBuilder.OfRule(testName);
        }

        // Try to extract file information from the message or content
        var fileInfo = ExtractFileInfoFromOutput(fullMessage) ?? ExtractFileInfoFromClassName(className);

        if (fileInfo.HasValue)
        {
            var (filePath, line, column) = fileInfo.Value;
            issueBuilder = issueBuilder.InFile(filePath, line, line, column, column);
        }

        return issueBuilder.Create();
    }
}