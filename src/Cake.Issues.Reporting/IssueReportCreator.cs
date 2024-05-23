namespace Cake.Issues.Reporting;

using System.Collections.Generic;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

/// <summary>
/// Class for creating issue reports.
/// </summary>
internal class IssueReportCreator
{
    private readonly ICakeLog log;

    /// <summary>
    /// Initializes a new instance of the <see cref="IssueReportCreator"/> class.
    /// </summary>
    /// <param name="log">Cake log instance.</param>
    public IssueReportCreator(ICakeLog log)
    {
        log.NotNull();

        this.log = log;
    }

    /// <summary>
    /// Creates a report from a list of issues.
    /// </summary>
    /// <param name="issueProviders">Issue providers which should be used to get the issues.</param>
    /// <param name="reportFormat">Report format to use.</param>
    /// <param name="settings">Settings to use.</param>
    /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
    public FilePath CreateReport(
        IEnumerable<IIssueProvider> issueProviders,
        IIssueReportFormat reportFormat,
        ICreateIssueReportFromIssueProviderSettings settings)
    {
        issueProviders.NotNullOrEmptyOrEmptyElement();
        reportFormat.NotNull();
        settings.NotNull();

        var issuesReader = new IssuesReader(this.log, issueProviders, settings);
        var issues = issuesReader.ReadIssues();

        return this.CreateReport(issues, reportFormat, settings);
    }

    /// <summary>
    /// Creates a report from a list of issues.
    /// </summary>
    /// <param name="issues">Issues for which the report should be created.</param>
    /// <param name="reportFormat">Report format to use.</param>
    /// <param name="settings">Settings to use.</param>
    /// <returns>Path to the created report or <c>null</c> if report could not be created.</returns>
    public FilePath CreateReport(
        IEnumerable<IIssue> issues,
        IIssueReportFormat reportFormat,
        ICreateIssueReportSettings settings)
    {
        issues.NotNullOrEmptyElement();
        reportFormat.NotNull();
        settings.NotNull();

        var reportFormatName = reportFormat.GetType().Name;
        this.log.Verbose("Initialize report format {0}...", reportFormatName);

        if (!reportFormat.Initialize(settings))
        {
            this.log.Warning("Error initializing report format {0}.", reportFormatName);
            return null;
        }

        this.log.Verbose("Creating report {0}...", reportFormatName);
        return reportFormat.CreateReport(issues);
    }
}
