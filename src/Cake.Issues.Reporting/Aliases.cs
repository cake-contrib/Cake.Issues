namespace Cake.Issues.Reporting
{
    using System.Collections.Generic;
    using Core;
    using Core.Annotations;
    using Core.IO;
    using Issues.IssueProvider;
    using ReportFormat;

    /// <summary>
    /// Contains functionality related to creating issue reports.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    [CakeNamespaceImport("Cake.Issues.Reporting.ReportFormat")]
    public static class Aliases
    {
        /// <summary>
        /// Creates a report for a list of issues in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">Issue providers for whose issues the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Path to the report.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            IIssueReportFormat reportFormat,
            DirectoryPath repositoryRoot)
        {
            context.NotNull(nameof(context));
            reportFormat.NotNull(nameof(reportFormat));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            // ReSharper disable once PossibleMultipleEnumeration
            return
                context.CreateIssueReport(
                    issueProviders,
                    reportFormat,
                    new RepositorySettings(repositoryRoot));
        }

        /// <summary>
        /// Creates a report for a list of issues in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">Issue providers for whose issues the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Path to the report.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            IIssueReportFormat reportFormat,
            RepositorySettings settings)
        {
            context.NotNull(nameof(context));
            reportFormat.NotNull(nameof(reportFormat));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            var issueReportCreator = new IssueReportCreator(context.Log, settings);

            // ReSharper disable once PossibleMultipleEnumeration
            return issueReportCreator.CreateReport(issueProviders, reportFormat);
        }

        /// <summary>
        /// Creates a report for a list of issues in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issues">Issues for which the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Path to the report.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssue> issues,
            IIssueReportFormat reportFormat,
            DirectoryPath repositoryRoot)
        {
            context.NotNull(nameof(context));
            reportFormat.NotNull(nameof(reportFormat));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNullOrEmptyElement(nameof(issues));

            // ReSharper disable once PossibleMultipleEnumeration
            return
                context.CreateIssueReport(
                    issues,
                    reportFormat,
                    new RepositorySettings(repositoryRoot));
        }

        /// <summary>
        /// Creates a report for a list of issues in the specified format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issues">Issues for which the report should be generated.</param>
        /// <param name="reportFormat">Format in which the report should be generated.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Path to the report.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.CreateIssueReportCakeAliasCategory)]
        public static FilePath CreateIssueReport(
            this ICakeContext context,
            IEnumerable<IIssue> issues,
            IIssueReportFormat reportFormat,
            RepositorySettings settings)
        {
            context.NotNull(nameof(context));
            reportFormat.NotNull(nameof(reportFormat));

            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNullOrEmptyElement(nameof(issues));

            var issueReportCreator = new IssueReportCreator(context.Log, settings);

            // ReSharper disable once PossibleMultipleEnumeration
            return issueReportCreator.CreateReport(issues, reportFormat);
        }
    }
}
