namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using System.Dynamic;

    /// <summary>
    /// Extension for <see cref="IIssue"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Follows name of the interface which is extended")]
    public static class IIssueExtensions
    {
        /// <summary>
        /// Returns an dynamic object containing the properties of an issue.
        /// </summary>
        /// <param name="issue">Issue for which the dynamic object should be returned.</param>
        /// <param name="addProviderType">Flag if value of <see cref="IIssue.ProviderType"/> should be added.</param>
        /// <param name="addProviderName">Flag if value of <see cref="IIssue.ProviderName"/> should be added.</param>
        /// <param name="addRun">Flag if value of <see cref="IIssue.Run"/> should be added.</param>
        /// <param name="addPriority">Flag if value of <see cref="IIssue.Priority"/> should be added.</param>
        /// <param name="addPriorityName">Flag if value of <see cref="IIssue.PriorityName"/> should be added.</param>
        /// <param name="addProjectPath">Flag if value of <see cref="Cake.Issues.IIssueExtensions.ProjectPath"/> should be added.</param>
        /// <param name="addProjectName">Flag if value of <see cref="IIssue.ProjectName"/> should be added.</param>
        /// <param name="addFilePath">Flag if value of <see cref="IIssue.AffectedFileRelativePath"/> should be added.</param>
        /// <param name="addFileDirectory">Flag if value of <see cref="Cake.Issues.IIssueExtensions.FileDirectory"/> should be added.</param>
        /// <param name="addFileName">Flag if value of <see cref="Cake.Issues.IIssueExtensions.FileName"/> should be added.</param>
        /// <param name="addFileLink">Flag if value of <see cref="IIssue.FileLink"/> should be added.</param>
        /// <param name="addLine">Flag if value of <see cref="IIssue.Line"/> should be added.</param>
        /// <param name="addRule">Flag if value of <see cref="IIssue.Rule"/> should be added.</param>
        /// <param name="addRuleUrl">Flag if value of <see cref="IIssue.RuleUrl"/> should be added.</param>
        /// <param name="addMessageText">Flag if value of <see cref="IIssue.MessageText"/> should be added.</param>
        /// <param name="addMessageHtml">Flag if value of <see cref="IIssue.MessageHtml"/> should be added.</param>
        /// <param name="fallbackToTextMessageIfHtmlMessageNotAvailable">Flag if value of <see cref="IIssue.MessageText"/> should be
        /// returned if <see cref="IIssue.MessageHtml"/> is not available.</param>
        /// <param name="addMessageMarkdown">Flag if value of <see cref="IIssue.MessageMarkdown"/> should be added.</param>
        /// <param name="fallbackToTextMessageIfMarkdownMessageNotAvailable">Flag if value of <see cref="IIssue.MessageText"/> should be
        /// returned if <see cref="IIssue.MessageMarkdown"/> is not available.</param>
        /// <param name="additionalValues">Additional values which should be added to the object.</param>
        /// <returns>Dynamic object containing the properties of the issue.</returns>
        public static ExpandoObject GetExpandoObject(
            this IIssue issue,
            bool addProviderType = true,
            bool addProviderName = true,
            bool addRun = true,
            bool addPriority = true,
            bool addPriorityName = true,
            bool addProjectPath = true,
            bool addProjectName = true,
            bool addFilePath = true,
            bool addFileDirectory = true,
            bool addFileName = true,
            bool addFileLink = true,
            bool addLine = true,
            bool addRule = true,
            bool addRuleUrl = true,
            bool addMessageText = true,
            bool addMessageHtml = false,
            bool fallbackToTextMessageIfHtmlMessageNotAvailable = true,
            bool addMessageMarkdown = false,
            bool fallbackToTextMessageIfMarkdownMessageNotAvailable = true,
            IDictionary<string, Func<IIssue, object>> additionalValues = null)
        {
            issue.NotNull(nameof(issue));

            dynamic result = new ExpandoObject();

            if (addProviderType)
            {
                result.ProviderType = issue.ProviderType;
            }

            if (addProviderName)
            {
                result.ProviderName = issue.ProviderName;
            }

            if (addRun)
            {
                result.Run = issue.Run;
            }

            if (addPriority)
            {
                result.Priority = issue.Priority;
            }

            if (addPriorityName)
            {
                result.PriorityName = issue.PriorityName;
            }

            if (addProjectPath)
            {
                result.ProjectPath = issue.ProjectPath();
            }

            if (addProjectName)
            {
                result.ProjectName = issue.ProjectName;
            }

            if (addFilePath)
            {
                result.FilePath = issue.FilePath();
            }

            if (addFileDirectory)
            {
                result.FileDirectory = issue.FileDirectory();
            }

            if (addFileName)
            {
                result.FileName = issue.FileName();
            }

            if (addFileLink)
            {
                result.FileLink = issue.FileLink?.ToString();
            }

            if (addLine)
            {
                result.Line = issue.Line;
            }

            if (addRule)
            {
                result.Rule = issue.Rule;
            }

            if (addRuleUrl)
            {
                result.RuleUrl = issue.RuleUrl?.ToString();
            }

            if (addMessageText)
            {
                result.MessageText = issue.MessageText;
            }

            if (addMessageHtml)
            {
                result.MessageHtml =
                    fallbackToTextMessageIfHtmlMessageNotAvailable ? issue.Message(IssueCommentFormat.Html) : issue.MessageHtml;
            }

            if (addMessageMarkdown)
            {
                result.MessageMarkdown =
                    fallbackToTextMessageIfMarkdownMessageNotAvailable ? issue.Message(IssueCommentFormat.Markdown) : issue.MessageMarkdown;
            }

            if (additionalValues != null)
            {
                var resultDictionary = (IDictionary<string, object>)result;
                foreach (var additionalValue in additionalValues)
                {
                    resultDictionary.Add(additionalValue.Key, additionalValue.Value(issue));
                }
            }

            return result;
        }
    }
}
