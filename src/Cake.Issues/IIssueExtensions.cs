namespace Cake.Issues
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions for <see cref="IIssue"/>.
    /// </summary>
    public static class IIssueExtensions
    {
        /// <summary>
        /// Returns the line and column range in the format <c>{Line}:{Column}-{EndLine}:{EndColumn}</c>.
        /// </summary>
        /// <param name="issue">Issue for which the line and column range should be returned.</param>
        /// <returns>Line and column range.</returns>
        public static string LineRange(this IIssue issue)
        {
            issue.NotNull();

            return issue.LineRange(true);
        }

        /// <summary>
        /// Returns the line range in the format <c>{Line}:{Column}-{EndLine}:{EndColumn}</c>.
        /// </summary>
        /// <param name="issue">Issue for which the line range should be returned.</param>
        /// <param name="addColumn">Flag if column information should also be returned.</param>
        /// <returns>Line and column range.</returns>
        public static string LineRange(this IIssue issue, bool addColumn)
        {
            issue.NotNull();

            var result = string.Empty;

            if (issue.Line.HasValue)
            {
                result += issue.Line.ToString();

                if (addColumn && issue.Column.HasValue)
                {
                    result += $":{issue.Column.Value}";
                }

                if (issue.EndLine.HasValue)
                {
                    result += $"-{issue.EndLine.Value}";

                    if (addColumn && issue.EndColumn.HasValue)
                    {
                        result += $":{issue.EndColumn.Value}";
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the message of the issue in a specific format.
        /// If the message is not available in the specific format, the message in
        /// text format will be returned.
        /// </summary>
        /// <param name="issue">Issue for which the message should be returned.</param>
        /// <param name="format">Format in which the message should be returned.</param>
        /// <returns>Message in the format specified by <paramref name="format"/> or message in text
        /// format if it is not available in the desired format.</returns>
        public static string Message(this IIssue issue, IssueCommentFormat format)
        {
            issue.NotNull();

            return format switch
            {
                IssueCommentFormat.PlainText => issue.MessageText,
                IssueCommentFormat.Html => !string.IsNullOrEmpty(issue.MessageHtml) ? issue.MessageHtml : issue.MessageText,
                IssueCommentFormat.Markdown => !string.IsNullOrEmpty(issue.MessageMarkdown) ? issue.MessageMarkdown : issue.MessageText,
                _ => throw new ArgumentOutOfRangeException(nameof(format)),
            };
        }

        /// <summary>
        /// Returns the full path of <see cref="IIssue.ProjectFileRelativePath"/> or <c>null</c>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Full path to the project to which the file affected by the issue belongs.</returns>
        public static string ProjectPath(this IIssue issue)
        {
            issue.NotNull();

            return issue.ProjectFileRelativePath?.FullPath;
        }

        /// <summary>
        /// Returns the directory of the <see cref="IIssue.ProjectFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the project directory should be returned.</param>
        /// <returns>Directory of the project to which the file affected by the issue belongs.</returns>
        public static string ProjectDirectory(this IIssue issue)
        {
            issue.NotNull();

            return issue.ProjectFileRelativePath?.GetDirectory().FullPath;
        }

        /// <summary>
        /// Returns the full path of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Full path of the file affected by the issue.</returns>
        public static string FilePath(this IIssue issue)
        {
            issue.NotNull();

            return issue.AffectedFileRelativePath?.FullPath;
        }

        /// <summary>
        /// Returns the directory of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the directory should be returned.</param>
        /// <returns>Directory of the file affected by the issue.</returns>
        public static string FileDirectory(this IIssue issue)
        {
            issue.NotNull();

            return issue.AffectedFileRelativePath?.GetDirectory().FullPath;
        }

        /// <summary>
        /// Returns the name of the file of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the file name should be returned.</param>
        /// <returns>Name of the file affected by the issue.</returns>
        public static string FileName(this IIssue issue)
        {
            issue.NotNull();

            return issue.AffectedFileRelativePath?.GetFilename().ToString();
        }

        /// <summary>
        /// Returns the name or id of the rule.
        /// </summary>
        /// <param name="issue">Issue for which the rule should be returned.</param>
        /// <returns>Returns <see cref="IIssue.RuleName"/> if it is set, otherwise <see cref="IIssue.RuleId"/>.</returns>
        public static string Rule(this IIssue issue)
        {
            issue.NotNull();

            return !string.IsNullOrWhiteSpace(issue.RuleName) ? issue.RuleName : issue.RuleId;
        }

        /// <summary>
        /// Returns a string with all patterns replaced by the values of <paramref name="issue"/>.
        /// </summary>
        /// <param name="pattern">Pattern whose values should be replaced.
        /// The following tokens are supported:
        /// <list type="table">
        ///     <listheader>
        ///         <term>Token</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>{ProviderType}</term>
        ///         <description>The value of <see cref="IIssue.ProviderType"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProviderName}</term>
        ///         <description>The value of <see cref="IIssue.ProviderName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Identifier}</term>
        ///         <description>The value of <see cref="IIssue.Identifier"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Priority}</term>
        ///         <description>The value of <see cref="IIssue.Priority"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{PriorityName}</term>
        ///         <description>The value of <see cref="IIssue.PriorityName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProjectPath}</term>
        ///         <description>The value of <see cref="ProjectPath(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProjectDirectory}</term>
        ///         <description>The value of <see cref="ProjectDirectory(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProjectName}</term>
        ///         <description>The value of <see cref="IIssue.ProjectName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FilePath}</term>
        ///         <description>The value of <see cref="FilePath(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FileDirectory}</term>
        ///         <description>The value of <see cref="FileDirectory(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FileName}</term>
        ///         <description>The value of <see cref="FileName(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Line}</term>
        ///         <description>The value of <see cref="IIssue.Line"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{EndLine}</term>
        ///         <description>The value of <see cref="IIssue.EndLine"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Column}</term>
        ///         <description>The value of <see cref="IIssue.Column"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{EndColumn}</term>
        ///         <description>The value of <see cref="IIssue.EndColumn"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FileLink}</term>
        ///         <description>The value of <see cref="IIssue.FileLink"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{RuleId}</term>
        ///         <description>The value of <see cref="IIssue.RuleId"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{RuleName}</term>
        ///         <description>The value of <see cref="IIssue.RuleName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{RuleUrl}</term>
        ///         <description>The value of <see cref="IIssue.RuleUrl"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Run}</term>
        ///         <description>The value of <see cref="IIssue.Run"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{MessageText}</term>
        ///         <description>The value of <see cref="IIssue.MessageText"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{MessageHtml}</term>
        ///         <description>The value of <see cref="IIssue.MessageHtml"/> or <see cref="IIssue.MessageText"/>
        ///         if message in HTML format is not available.</description>
        ///     </item>
        ///     <item>
        ///         <term>{MessageMarkdown}</term>
        ///         <description>The value of <see cref="IIssue.MessageMarkdown"/> or <see cref="IIssue.MessageText"/>
        ///         if message in Markdown format is not available.</description>
        ///     </item>
        ///     <item>
        ///         <term>{AdditionalInformation:Key}</term>
        ///         <description>
        ///         The value of the key <c>Key</c> in <see cref="IIssue.AdditionalInformation"/>.
        ///         If the key <c>Key</c> does not exist in <see cref="IIssue.AdditionalInformation"/>, nothing will be replaced.
        ///         </description>
        ///     </item>
        /// </list>
        /// </param>
        /// <param name="issue">Issue whose values should be used to replace the patterns.</param>
        /// <returns>Value with all patterns replaced.</returns>
        public static string ReplaceIssuePattern(this string pattern, IIssue issue)
        {
            pattern.NotNull();
            issue.NotNull();

            return
                pattern
                    .Replace("{ProviderType}", issue.ProviderType)
                    .Replace("{ProviderName}", issue.ProviderName)
                    .Replace("{Identifier}", issue.Identifier)
                    .Replace("{Priority}", issue.Priority?.ToString())
                    .Replace("{PriorityName}", issue.PriorityName)
                    .Replace("{ProjectPath}", issue.ProjectPath())
                    .Replace("{ProjectDirectory}", issue.ProjectDirectory())
                    .Replace("{ProjectName}", issue.ProjectName)
                    .Replace("{FilePath}", issue.FilePath())
                    .Replace("{FileDirectory}", issue.FileDirectory())
                    .Replace("{FileName}", issue.FileName())
                    .Replace("{Line}", issue.Line?.ToString())
                    .Replace("{EndLine}", issue.EndLine?.ToString())
                    .Replace("{Column}", issue.Column?.ToString())
                    .Replace("{EndColumn}", issue.EndColumn?.ToString())
                    .Replace("{FileLink}", issue.FileLink?.ToString())
                    .Replace("{RuleId}", issue.RuleId)
                    .Replace("{RuleName}", issue.RuleName)
                    .Replace("{RuleUrl}", issue.RuleUrl?.ToString())
                    .Replace("{Run}", issue.Run)
                    .Replace("{MessageText}", issue.Message(IssueCommentFormat.PlainText))
                    .Replace("{MessageHtml}", issue.Message(IssueCommentFormat.Html))
                    .Replace("{MessageMarkdown}", issue.Message(IssueCommentFormat.Markdown))
                    .ReplaceAdditionalInformation(issue.AdditionalInformation);
        }

        private static string ReplaceAdditionalInformation(this string pattern, IReadOnlyDictionary<string, string> additionalInformation)
        {
            if (additionalInformation == null)
            {
                return pattern;
            }

            foreach (var keyValuePair in additionalInformation)
            {
                pattern = pattern.Replace($"{{AdditionalInformation:{keyValuePair.Key}}}", keyValuePair.Value);
            }

            return pattern;
        }
    }
}