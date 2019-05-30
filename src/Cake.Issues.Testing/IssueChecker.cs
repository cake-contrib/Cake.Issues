namespace Cake.Issues.Testing
{
    using System;
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
        public static void Check(
            IIssue issueToCheck,
            IssueBuilder expectedIssue)
        {
            issueToCheck.NotNull(nameof(issueToCheck));
            expectedIssue.NotNull(nameof(expectedIssue));

            Check(
                issueToCheck,
                expectedIssue.Create());
        }

        /// <summary>
        /// Checks values of an issue.
        /// </summary>
        /// <param name="issueToCheck">Issue which should be checked.</param>
        /// <param name="expectedIssue">Description of the expected issue.</param>
        public static void Check(
            IIssue issueToCheck,
            IIssue expectedIssue)
        {
            issueToCheck.NotNull(nameof(issueToCheck));
            expectedIssue.NotNull(nameof(expectedIssue));

            Check(
                issueToCheck,
                expectedIssue.ProviderType,
                expectedIssue.ProviderName,
                expectedIssue.ProjectFileRelativePath?.ToString(),
                expectedIssue.ProjectName,
                expectedIssue.AffectedFileRelativePath?.ToString(),
                expectedIssue.Line,
                expectedIssue.Message,
                expectedIssue.Priority,
                expectedIssue.PriorityName,
                expectedIssue.Rule,
                expectedIssue.RuleUrl);
        }

        /// <summary>
        /// Checks values of an issue.
        /// </summary>
        /// <param name="issue">Issue which should be checked.</param>
        /// <param name="providerType">Expected type of the issue provider.</param>
        /// <param name="providerName">Expected human friendly name of the issue provider.</param>
        /// <param name="projectFileRelativePath">Expected relative path of the project file.
        /// <c>null</c> if the issue is not expected to be related to a project.</param>
        /// <param name="projectName">Expected project name.
        /// <c>null</c> or <see cref="string.Empty"/> if the issue is not expected to be related to a project.</param>
        /// <param name="affectedFileRelativePath">Expected relative path of the affected file.
        /// <c>null</c> if the issue is not expected to be related to a change in a file.</param>
        /// <param name="line">Expected line number.
        /// <c>null</c> if the issue is not expected to be related to a file or specific line.</param>
        /// <param name="message">Expected message.</param>
        /// <param name="priority">Expected priority.
        /// <c>null</c> if no priority is expected.</param>
        /// <param name="priorityName">Expected priority name.
        /// <c>null</c> or <see cref="string.Empty"/> if no priority is expected.</param>
        /// <param name="rule">Expected rule identifier.
        /// <c>null</c> or <see cref="string.Empty"/> if no rule identifier is expected.</param>
        /// <param name="ruleUrl">Expected URL containing information about the failing rule.
        /// <c>null</c> if no rule Url is expected.</param>
        public static void Check(
            IIssue issue,
            string providerType,
            string providerName,
            string projectFileRelativePath,
            string projectName,
            string affectedFileRelativePath,
            int? line,
            string message,
            int? priority,
            string priorityName,
            string rule,
            Uri ruleUrl)
        {
            issue.NotNull(nameof(issue));

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
                        $"Expected issue.ProjectFileRelativePath to be '{projectFileRelativePath}' but was '{issue.ProjectFileRelativePath.ToString()}'.");
                }

                if (!issue.ProjectFileRelativePath.IsRelative)
                {
                    throw new Exception(
                        $"Expected issue.ProjectFileRelativePath to be a relative path");
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
                        $"Expected issue.AffectedFileRelativePath to be '{affectedFileRelativePath}' but was '{issue.AffectedFileRelativePath.ToString()}'.");
                }

                if (!issue.AffectedFileRelativePath.IsRelative)
                {
                    throw new Exception(
                        $"Expected issue.AffectedFileRelativePath to be a relative path");
                }
            }

            if (issue.Line != line)
            {
                throw new Exception(
                    $"Expected issue.Line to be '{line}' but was '{issue.Line}'.");
            }

            if (issue.Message != message)
            {
                throw new Exception(
                    $"Expected issue.Message to be '{message}' but was '{issue.Message}'.");
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

            if (issue.Rule != rule)
            {
                throw new Exception(
                    $"Expected issue.Rule to be '{rule}' but was '{issue.Rule}'.");
            }

            if (issue.RuleUrl?.ToString() != ruleUrl?.ToString())
            {
                throw new Exception(
                    $"Expected issue.RuleUrl to be '{ruleUrl?.ToString()}' but was '{issue.RuleUrl?.ToString()}'.");
            }
        }
    }
}
