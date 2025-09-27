namespace Cake.Issues.Build;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Issues.Shared;
using Spectre.Console;

/// <summary>
/// Class for writing issues to build servers.
/// </summary>
internal class BuildServerOrchestrator
{
    private readonly ICakeLog log;
    private readonly IAnsiConsole console;
    private readonly IBuildServerSystem buildServerSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildServerOrchestrator"/> class.
    /// </summary>
    /// <param name="log">Cake log instance.</param>
    /// <param name="console">Console instance.</param>
    /// <param name="buildServerSystem">Object for accessing build server system.</param>
    public BuildServerOrchestrator(
        ICakeLog log,
        IAnsiConsole console,
        IBuildServerSystem buildServerSystem)
    {
        log.NotNull();
        console.NotNull();
        buildServerSystem.NotNull();

        this.log = log;
        this.console = console;
        this.buildServerSystem = buildServerSystem;
    }

    /// <summary>
    /// Runs the orchestrator.
    /// Posts issues to the build server after applying filters.
    /// </summary>
    /// <param name="issueProviders">List of issue providers to use.</param>
    /// <param name="settings">Settings.</param>
    /// <returns>Information about the reported and written issues.</returns>
    public BuildServerIssueResult Run(
        IEnumerable<IIssueProvider> issueProviders,
        IReportIssuesToBuildServerFromIssueProviderSettings settings)
    {
        issueProviders.NotNullOrEmptyOrEmptyElement();
        settings.NotNull();

        var issuesReader =
            new IssuesReader(this.log, issueProviders, settings);

        return this.Run(issuesReader.ReadIssues(), settings);
    }

    /// <summary>
    /// Runs the orchestrator.
    /// Posts issues to the build server after applying filters.
    /// </summary>
    /// <param name="issues">Issues which should be reported.</param>
    /// <param name="settings">Settings.</param>
    /// <returns>Information about the reported and written issues.</returns>
    public BuildServerIssueResult Run(
        IEnumerable<IIssue> issues,
        IReportIssuesToBuildServerSettings settings)
    {
        issues.NotNullOrEmptyElement();
        settings.NotNull();

        this.log.Verbose("Running build server orchestrator with the following settings:");
        if (this.log.Verbosity >= Verbosity.Diagnostic)
        {
            var providerIssueLimitsTable = new Table()
                .AddColumn("ProviderType")
                .AddColumn("MaxIssuesToPost");
            foreach (var providerIssueLimit in settings.ProviderIssueLimits)
            {
#pragma warning disable IDE0058 // Expression value is never used
                providerIssueLimitsTable.AddRow(
                    providerIssueLimit.Key,
                    providerIssueLimit.Value.ToString());
#pragma warning restore IDE0058 // Expression value is never used
            }

            var table = new Table()
                .AddColumn("Property")
                .AddColumn("Value")
                .AddRow("MaxIssuesToPost", settings.MaxIssuesToPost.ToStringWithNullMarkup())
                .AddRow("MaxIssuesToPostForEachIssueProvider", settings.MaxIssuesToPostForEachIssueProvider.ToStringWithNullMarkup())
                .AddRow(new Text("ProviderIssueLimits"), providerIssueLimitsTable)
                .AddRow("Number of registered IssueFilters", settings.IssueFilters.Count.ToString());
            this.console.Write(table);
        }

        // Don't process issues if build server system could not be initialized.
        if (!this.InitializeBuildServerSystem(settings))
        {
            return new BuildServerIssueResult(issues, []);
        }

        var issuesList = issues.ToList();
        this.log.Information("Processing {0} new issues", issuesList.Count);
        var postedIssues = this.PostIssues(settings, issuesList);

        return new BuildServerIssueResult(issuesList, postedIssues);
    }

    /// <summary>
    /// Initializes the build server system.
    /// </summary>
    /// <param name="settings">Settings for posting issues.</param>
    /// <returns><c>True</c> if build server system could be initialized.</returns>
    private bool InitializeBuildServerSystem(IReportIssuesToBuildServerSettings settings)
    {
        // Initialize build server system.
        this.log.Verbose("Initialize build server system...");
        var result = this.buildServerSystem.Initialize(settings);
        if (!result)
        {
            this.log.Warning("Error initializing the build server system.");
        }

        return result;
    }

    /// <summary>
    /// Posts issues to the build server after applying filters.
    /// </summary>
    /// <param name="reportIssuesToBuildServerSettings">Settings for posting the issues.</param>
    /// <param name="issues">Issues to post.</param>
    /// <returns>Issues reported to the build server.</returns>
    private List<IIssue> PostIssues(
        IReportIssuesToBuildServerSettings reportIssuesToBuildServerSettings,
        IList<IIssue> issues)
    {
        reportIssuesToBuildServerSettings.NotNull();
        issues.NotNull();

        if (!issues.Any())
        {
            this.log.Information("No new issues were posted");
            return [];
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var filterer = new BasicIssueFilterer(this.log, reportIssuesToBuildServerSettings);

        var issuesToPost = filterer.FilterIssues(issues).ToList();

        this.log.Information(
            "Posting {0} issue(s):",
            issuesToPost.Count);

        this.buildServerSystem.PostIssues(issuesToPost);

        this.log.Verbose(
            "Posting {0} issues took {1} ms",
            issuesToPost.Count,
            stopwatch.ElapsedMilliseconds);

        return issuesToPost;
    }
}