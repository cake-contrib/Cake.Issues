namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    /// <summary>
    /// Extension for <see cref="IIssue"/>.
    /// </summary>
    public static class IIssueExtension
    {
        /// <summary>
        /// Returns the full path of <see cref="IIssue.ProjectFileRelativePath"/> or <c>null</c>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Full path to the project to which the file affected by the issue belongs.</returns>
        public static string ProjectPath(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.ProjectFileRelativePath?.FullPath;
        }

        /// <summary>
        /// Returns the full path of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Full path of the file affected by the issue.</returns>
        public static string FilePath(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.AffectedFileRelativePath?.FullPath;
        }

        /// <summary>
        /// Returns the directory of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the directory should be returned.</param>
        /// <returns>Directory of the file affected by the issue.</returns>
        public static string FileDirectory(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.AffectedFileRelativePath?.GetDirectory().FullPath;
        }

        /// <summary>
        /// Returns the name of the file of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the file name should be returned.</param>
        /// <returns>Name of the file affected by the issue.</returns>
        public static string FileName(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.AffectedFileRelativePath?.GetFilename().ToString();
        }

        /// <summary>
        /// Returns an dynamic object containing the properties of an issue.
        /// </summary>
        /// <param name="issue">Issue for which the dynamic object should be returned.</param>
        /// <param name="addProviderType">Flag if value of <see cref="IIssue.ProviderType"/> should be added.</param>
        /// <param name="addProviderName">Flag if value of <see cref="IIssue.ProviderName"/> should be added.</param>
        /// <param name="addPriority">Flag if value of <see cref="IIssue.Priority"/> should be added.</param>
        /// <param name="addPriorityName">Flag if value of <see cref="IIssue.PriorityName"/> should be added.</param>
        /// <param name="addProjectPath">Flag if value of <see cref="ProjectPath"/> should be added.</param>
        /// <param name="addProjectName">Flag if value of <see cref="IIssue.ProjectName"/> should be added.</param>
        /// <param name="addFilePath">Flag if value of <see cref="IIssue.AffectedFileRelativePath"/> should be added.</param>
        /// <param name="addFileDirectory">Flag if value of <see cref="FileDirectory"/> should be added.</param>
        /// <param name="addFileName">Flag if value of <see cref="FileName"/> should be added.</param>
        /// <param name="addLine">Flag if value of <see cref="IIssue.Line"/> should be added.</param>
        /// <param name="addRule">Flag if value of <see cref="IIssue.Rule"/> should be added.</param>
        /// <param name="addRuleUrl">Flag if value of <see cref="IIssue.RuleUrl"/> should be added.</param>
        /// <param name="addMessage">Flag if value of <see cref="IIssue.Message"/> should be added.</param>
        /// <param name="fileLinkSettings">Settings for file linking.</param>
        /// <param name="additionalValues">Additional values which should be added to the object.</param>
        /// <returns>Dynamic object containing the properties of the issue.</returns>
        public static ExpandoObject GetExpandoObject(
            this IIssue issue,
            bool addProviderType = true,
            bool addProviderName = true,
            bool addPriority = true,
            bool addPriorityName = true,
            bool addProjectPath = true,
            bool addProjectName = true,
            bool addFilePath = true,
            bool addFileDirectory = true,
            bool addFileName = true,
            bool addLine = true,
            bool addRule = true,
            bool addRuleUrl = true,
            bool addMessage = true,
            FileLinkSettings fileLinkSettings = null,
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
                result.RuleUrl = issue.RuleUrl;
            }

            if (addMessage)
            {
                result.Message = issue.Message;
            }

            if (fileLinkSettings != null)
            {
                result.FileLink = fileLinkSettings.GetFileLink(issue);
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
