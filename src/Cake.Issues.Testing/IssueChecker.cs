namespace Cake.Issues.Testing;

using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core.IO;

/// <summary>
/// Class for checking issues.
/// </summary>
public static class IssueChecker
{
    /// <summary>
    /// Checks values of an issue.
    /// </summary>
    /// <param name="issueToCheck">Issue which should be checked.</param>
    /// <param name="expectedIssue">Description of the expected issue.</param>
    [AssertionMethod]
    public static void Check(
        IIssue issueToCheck,
        IssueBuilder expectedIssue)
    {
        issueToCheck.NotNull();
        expectedIssue.NotNull();

        Check(
            issueToCheck,
            expectedIssue.Create());
    }

    /// <summary>
    /// Checks values of an issue.
    /// </summary>
    /// <param name="issueToCheck">Issue which should be checked.</param>
    /// <param name="expectedIssue">Description of the expected issue.</param>
    [AssertionMethod]
    public static void Check(
        IIssue issueToCheck,
        IIssue expectedIssue)
    {
        issueToCheck.NotNull();
        expectedIssue.NotNull();

        Check(
            issueToCheck,
            expectedIssue.ProviderType,
            expectedIssue.ProviderName,
            expectedIssue.Run,
            expectedIssue.Identifier,
            expectedIssue.ProjectFileRelativePath?.ToString(),
            expectedIssue.ProjectName,
            expectedIssue.AffectedFileRelativePath?.ToString(),
            expectedIssue.Line,
            expectedIssue.EndLine,
            expectedIssue.Column,
            expectedIssue.EndColumn,
            expectedIssue.FileLink,
            expectedIssue.MessageText,
            expectedIssue.MessageHtml,
            expectedIssue.MessageMarkdown,
            expectedIssue.Priority,
            expectedIssue.PriorityName,
            expectedIssue.RuleId,
            expectedIssue.RuleName,
            expectedIssue.RuleUrl,
            expectedIssue.AdditionalInformation);
    }

    /// <summary>
    /// Checks values of an issue.
    /// </summary>
    /// <param name="issue">Issue which should be checked.</param>
    /// <param name="providerType">Expected type of the issue provider.</param>
    /// <param name="providerName">Expected human friendly name of the issue provider.</param>
    /// <param name="run">Expected name of the run which reported the issue.</param>
    /// <param name="identifier">Expected identifier of the issue.</param>
    /// <param name="projectFileRelativePath">Expected relative path of the project file.
    /// <c>null</c> if the issue is not expected to be related to a project.</param>
    /// <param name="projectName">Expected project name.
    /// <c>null</c> or <see cref="string.Empty"/> if the issue is not expected to be related to a project.</param>
    /// <param name="affectedFileRelativePath">Expected relative path of the affected file.
    /// <c>null</c> if the issue is not expected to be related to a change in a file.</param>
    /// <param name="line">Expected line number.
    /// <c>null</c> if the issue is not expected to be related to a file or specific line.</param>
    /// <param name="endLine">Expected end of line range.
    /// <c>null</c> if the issue is not expected to be related to a file, specific line or range of lines.</param>
    /// <param name="column">Expected column.
    /// <c>null</c> if the issue is not expected to be related to a file or specific column.</param>
    /// <param name="endColumn">Expected end of column range.
    /// <c>null</c> if the issue is not expected to be related to a file, specific column or range of columns.</param>
    /// <param name="fileLink">Expected file link.
    /// <c>null</c> if the issue is not expected to have a file link.</param>
    /// <param name="messageText">Expected message in plain text format.</param>
    /// <param name="messageHtml">Expected message in HTML format.</param>
    /// <param name="messageMarkdown">Expected message in Markdown format.</param>
    /// <param name="priority">Expected priority.
    /// <c>null</c> if no priority is expected.</param>
    /// <param name="priorityName">Expected priority name.
    /// <c>null</c> or <see cref="string.Empty"/> if no priority is expected.</param>
    /// <param name="ruleId">Expected rule identifier.
    /// <c>null</c> or <see cref="string.Empty"/> if no rule identifier is expected.</param>
    /// <param name="ruleName">Expected name of the rule.
    /// <c>null</c> or <see cref="string.Empty"/> if no rule is expected.</param>
    /// <param name="ruleUrl">Expected URL containing information about the failing rule.
    /// <c>null</c> if no rule Url is expected.</param>
    /// <param name="additionalInformation">Custom information regarding the issue.</param>
    [AssertionMethod]
    public static void Check(
        IIssue issue,
        string providerType,
        string providerName,
        string run,
        string identifier,
        string projectFileRelativePath,
        string projectName,
        string affectedFileRelativePath,
        int? line,
        int? endLine,
        int? column,
        int? endColumn,
        Uri fileLink,
        string messageText,
        string messageHtml,
        string messageMarkdown,
        int? priority,
        string priorityName,
        string ruleId,
        string ruleName,
        Uri ruleUrl,
        IReadOnlyDictionary<string, string> additionalInformation)
    {
        issue.NotNull();

        if (issue.ProviderType != providerType)
        {
            throw new Exception(
                $"Expected issue.ProviderType to be '{providerType}' but was '{issue.ProviderType}'.");
        }

        if (issue.ProviderName != providerName)
        {
            throw new Exception(
                $"Expected issue.ProviderName to be '{providerName}' but was '{issue.ProviderName}'.");
        }

        if (issue.Run != run)
        {
            throw new Exception(
                $"Expected issue.Run to be '{run}' but was '{issue.Run}'.");
        }

        if (issue.Identifier != identifier)
        {
            throw new Exception(
                $"Expected issue.Identifier to be '{identifier}' but was '{issue.Identifier}'.");
        }

        if (issue.ProjectFileRelativePath == null)
        {
            if (projectFileRelativePath != null)
            {
                throw new Exception(
                    $"Expected issue.ProjectFileRelativePath to be '{projectFileRelativePath}' but was 'null'.");
            }
        }
        else
        {
            if (issue.ProjectFileRelativePath.ToString() != new FilePath(projectFileRelativePath).ToString())
            {
                throw new Exception(
                    $"Expected issue.ProjectFileRelativePath to be '{projectFileRelativePath}' but was '{issue.ProjectFileRelativePath}'.");
            }

            if (!issue.ProjectFileRelativePath.IsRelative)
            {
                throw new Exception(
                    "Expected issue.ProjectFileRelativePath to be a relative path");
            }
        }

        if (issue.ProjectName != projectName)
        {
            throw new Exception(
                $"Expected issue.ProjectName to be '{projectName}' but was '{issue.ProjectName}'.");
        }

        if (issue.AffectedFileRelativePath == null)
        {
            if (affectedFileRelativePath != null)
            {
                throw new Exception(
                    $"Expected issue.AffectedFileRelativePath to be '{affectedFileRelativePath}' but was 'null'.");
            }
        }
        else
        {
            if (issue.AffectedFileRelativePath.ToString() != new FilePath(affectedFileRelativePath).ToString())
            {
                throw new Exception(
                    $"Expected issue.AffectedFileRelativePath to be '{affectedFileRelativePath}' but was '{issue.AffectedFileRelativePath}'.");
            }

            if (!issue.AffectedFileRelativePath.IsRelative)
            {
                throw new Exception(
                    "Expected issue.AffectedFileRelativePath to be a relative path");
            }
        }

        if (issue.Line != line)
        {
            throw new Exception(
                $"Expected issue.Line to be '{line}' but was '{issue.Line}'.");
        }

        if (issue.EndLine != endLine)
        {
            throw new Exception(
                $"Expected issue.EndLine to be '{endLine}' but was '{issue.EndLine}'.");
        }

        if (issue.Column != column)
        {
            throw new Exception(
                $"Expected issue.Column to be '{column}' but was '{issue.Column}'.");
        }

        if (issue.EndColumn != endColumn)
        {
            throw new Exception(
                $"Expected issue.EndColumn to be '{endColumn}' but was '{issue.EndColumn}'.");
        }

        if (issue.FileLink?.ToString() != fileLink?.ToString())
        {
            throw new Exception(
                $"Expected issue.FileLink to be '{fileLink}' but was '{issue.FileLink}'.");
        }

        if (issue.MessageText != messageText)
        {
            throw new Exception(
                $"Expected issue.MessageText to be '{messageText}' but was '{issue.MessageText}'.");
        }

        if (issue.MessageHtml != messageHtml)
        {
            throw new Exception(
                $"Expected issue.MessageHtml to be '{messageHtml}' but was '{issue.MessageHtml}'.");
        }

        if (issue.MessageMarkdown != messageMarkdown)
        {
            throw new Exception(
                $"Expected issue.MessageMarkdown to be '{messageMarkdown}' but was '{issue.MessageMarkdown}'.");
        }

        if (issue.Priority != priority)
        {
            throw new Exception(
                $"Expected issue.Priority to be '{priority}' but was '{issue.Priority}'.");
        }

        if (issue.PriorityName != priorityName)
        {
            throw new Exception(
                $"Expected issue.PriorityName to be '{priorityName}' but was '{issue.PriorityName}'.");
        }

        if (issue.RuleId != ruleId)
        {
            throw new Exception(
                $"Expected issue.RuleId to be '{ruleId}' but was '{issue.RuleId}'.");
        }

        if (issue.RuleName != ruleName)
        {
            throw new Exception(
                $"Expected issue.RuleName to be '{ruleName}' but was '{issue.RuleName}'.");
        }

        if (issue.RuleUrl?.ToString() != ruleUrl?.ToString())
        {
            throw new Exception(
                $"Expected issue.RuleUrl to be '{ruleUrl}' but was '{issue.RuleUrl}'.");
        }

        CheckAdditionalInformation(additionalInformation, issue.AdditionalInformation);
    }

    /// <summary>
    /// Checks additional information passed to an issue.
    /// </summary>
    /// <param name="expected">Expected additional information.</param>
    /// <param name="actual">Actual additional information.</param>
    private static void CheckAdditionalInformation(
        IReadOnlyDictionary<string, string> expected,
        IReadOnlyDictionary<string, string> actual)
    {
        if (expected == null && actual == null)
        {
            return;
        }

        if (expected == null)
        {
            throw new Exception(
                "Expected issue.AdditionalInformation to be null but was not null.");
        }

        if (actual == null)
        {
            throw new Exception(
                "Expected issue.AdditionalInformation to be not null but was null.");
        }

        var expectedItemsNotFound = expected.Except(actual).ToArray();
        if (expectedItemsNotFound.Length > 0)
        {
            throw new Exception(
                $"Expected issue.AdditionalInformation to have an item with the key '{expectedItemsNotFound.First()}' and value '{expectedItemsNotFound.First()}'");
        }

        var actualItemsContainsInvalidItem = actual.Except(expected).ToArray();
        if (actualItemsContainsInvalidItem.Length > 0)
        {
            throw new Exception(
                $"issue.AdditionalInformation contains an item with the key '{actualItemsContainsInvalidItem.First()}' and value '{actualItemsContainsInvalidItem.First()}' that was not expected");
        }
    }
}
