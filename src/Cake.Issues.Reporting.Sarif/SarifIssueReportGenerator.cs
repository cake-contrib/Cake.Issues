namespace Cake.Issues.Reporting.Sarif;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Microsoft.CodeAnalysis.Sarif;
using Newtonsoft.Json;

/// <summary>
/// Generator for creating SARIF compatible reports.
/// </summary>
internal class SarifIssueReportGenerator : IssueReportFormat
{
    /// <summary>
    /// The symbolic name for the repository root location, used in UriBaseIds.
    /// </summary>
    internal const string RepoRootUriBaseId = "REPOROOT";

    /// <summary>
    /// The name of the result object property bag property that holds the rule URL in the
    /// unusual case where the ruleId is absent but the URL is present.
    /// </summary>
    internal const string RuleUrlPropertyName = "RuleUrl";

    private readonly SarifIssueReportFormatSettings sarifIssueReportFormatSettings;
    private List<ReportingDescriptor> rules;
    private Dictionary<string, int> ruleIndices;

    /// <summary>
    /// Initializes a new instance of the <see cref="SarifIssueReportGenerator"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="settings">Settings for reading the log file.</param>
    public SarifIssueReportGenerator(ICakeLog log, SarifIssueReportFormatSettings settings)
        : base(log)
    {
        settings.NotNull();

        this.sarifIssueReportFormatSettings = settings;
    }

    /// <inheritdoc />
    protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
    {
        this.Log.Information("Creating report '{0}'", this.Settings.OutputFilePath.FullPath);

        List<SarifIssue> sarifIssues = [];

        if (this.sarifIssueReportFormatSettings.BaselineGuid != Guid.Empty &&
            this.sarifIssueReportFormatSettings.ExistingIssues.Count > 0)
        {
            var issueComparerOnlyPersistentProperties = new IIssueComparer(true);
            var issueComparerAllProperties = new IIssueComparer(false);

            // compare with all properties to identify same issues
            var unchangedIssuesNew = issues
                .Intersect(this.sarifIssueReportFormatSettings.ExistingIssues, issueComparerAllProperties);

            // compare with all properties to identify same issues
            var unchangedIssuesOld = this.sarifIssueReportFormatSettings.ExistingIssues
                .Intersect(issues, issueComparerAllProperties);

            // exclude issues by object reference
            var remainingIssuesNew = issues
                .Except(unchangedIssuesNew);

            // exclude issues by object reference
            var remainingIssuesOld = this.sarifIssueReportFormatSettings.ExistingIssues
                .Except(unchangedIssuesOld);

            // compare with persistend properties to identify potentially updated issues
            // intersect will not work at is takes the first item and produce a distinct set of item(s).
            var updatedIssuesExcepted = remainingIssuesNew
                .Except(remainingIssuesOld, issueComparerOnlyPersistentProperties);

            // exclude issues by object reference
            var updatedIssuesNew = remainingIssuesNew
                .Except(updatedIssuesExcepted);

            // compare with persistend properties to identify potentially updated issues
            // intersect will not work at is takes the first item and produce a distinct set of item(s).
            var updatedIssuesOldExpected = remainingIssuesOld
                .Except(remainingIssuesNew, issueComparerOnlyPersistentProperties);

            // exclude issues by object reference
            var updatedIssuesOld = remainingIssuesOld
                .Except(updatedIssuesOldExpected);

            // exclude issues by object reference
            var newIssues = remainingIssuesNew
                .Except(updatedIssuesNew);

            // exclude issues by object reference
            var absentIssues = remainingIssuesOld
                .Except(updatedIssuesOld);

            sarifIssues.AddRange(newIssues.Select(issue => new SarifIssue { BaselineState = BaselineState.New, Issue = issue }));
            sarifIssues.AddRange(updatedIssuesNew.Select(issue => new SarifIssue { BaselineState = BaselineState.Updated, Issue = issue }));
            sarifIssues.AddRange(unchangedIssuesNew.Select(issue => new SarifIssue { BaselineState = BaselineState.Unchanged, Issue = issue }));
            sarifIssues.AddRange(absentIssues.Select(issue => new SarifIssue { BaselineState = BaselineState.Absent, Issue = issue }));
        }
        else
        {
            sarifIssues.AddRange(issues.Select(issue => new SarifIssue() { Issue = issue }));
        }

        var settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        var log = new SarifLog();

        if (sarifIssues.Count > 0)
        {
            log.Runs = [];

            foreach (var issueGroup in from sarifIssue in sarifIssues group sarifIssue by new { sarifIssue.Issue.ProviderType, sarifIssue.Issue.Run })
            {
                this.rules = [];
                this.ruleIndices = [];

                Run run = new()
                {
                    Tool =
                        new Tool
                        {
                            Driver =
                                new ToolComponent
                                {
                                    Name = issueGroup.Key.ProviderType,
                                },
                        },
                    Results =
                        (from sarifIssue in issueGroup
                         select this.GetResult(sarifIssue)).ToList(),
                    OriginalUriBaseIds = new Dictionary<string, ArtifactLocation>
                    {
                        [RepoRootUriBaseId] =
                            new()
                            {
                                Uri = new Uri(this.Settings.RepositoryRoot.FullPath, UriKind.Absolute),
                            },
                    },
                };

                if (!string.IsNullOrEmpty(issueGroup.Key.Run) ||
                    (this.sarifIssueReportFormatSettings.CorrelationGuid != Guid.Empty) ||
                    (this.sarifIssueReportFormatSettings.Guid != Guid.Empty))
                {
                    run.AutomationDetails = new RunAutomationDetails();

                    if (!string.IsNullOrEmpty(issueGroup.Key.Run))
                    {
                        run.AutomationDetails.Id = issueGroup.Key.Run;
                    }

                    if (this.sarifIssueReportFormatSettings.CorrelationGuid != Guid.Empty)
                    {
                        run.AutomationDetails.CorrelationGuid = this.sarifIssueReportFormatSettings.CorrelationGuid;
                    }

                    if (this.sarifIssueReportFormatSettings.Guid != Guid.Empty)
                    {
                        run.AutomationDetails.Guid = this.sarifIssueReportFormatSettings.Guid;
                    }
                }

                if (this.sarifIssueReportFormatSettings.BaselineGuid != Guid.Empty)
                {
                    run.BaselineGuid = this.sarifIssueReportFormatSettings.BaselineGuid;
                }

                if (this.rules.Count != 0)
                {
                    run.Tool.Driver.Rules = this.rules;
                }

                log.Runs.Add(run);
            }
        }

        var sarifText = JsonConvert.SerializeObject(log, settings);
        File.WriteAllText(this.Settings.OutputFilePath.FullPath, sarifText);

        return this.Settings.OutputFilePath;
    }

    private Result GetResult(SarifIssue sarifIssue)
    {
        sarifIssue.NotNull();
        sarifIssue.Issue.NotNull();

        var result =
            new Result
            {
                RuleId = sarifIssue.Issue.RuleId,
                Message =
                    new Message
                    {
                        Text = sarifIssue.Issue.MessageText,
                        Markdown = sarifIssue.Issue.MessageMarkdown,
                    },
                Kind = sarifIssue.Issue.Kind(),
                Level = sarifIssue.Issue.Level(),
                Locations = [sarifIssue.Issue.Location()],
                BaselineState = sarifIssue.BaselineState,
            };

        if (sarifIssue.Issue.RuleUrl != null)
        {
            if (!string.IsNullOrEmpty(sarifIssue.Issue.RuleId))
            {
                if (!this.ruleIndices.TryGetValue(sarifIssue.Issue.RuleId, out var value))
                {
                    this.ruleIndices.Add(sarifIssue.Issue.RuleId, this.rules.Count);
                    this.rules.Add(
                        new ReportingDescriptor
                        {
                            Id = sarifIssue.Issue.RuleId,
                            Name = sarifIssue.Issue.RuleName,
                            HelpUri = sarifIssue.Issue.RuleUrl,
                        });
                }

                result.RuleIndex = value;
            }
            else
            {
                // In the unusual case where there is a rule URL but no rule name, we put the
                // URL in the result's property bag, because there's no rule whose metadata
                // can hold it.
                result.SetProperty(RuleUrlPropertyName, sarifIssue.Issue.RuleUrl);
            }
        }

        return result;
    }

    /// <summary>
    /// Contains the <see cref="BaselineState"/> and <see cref="IIssue"/>.
    /// </summary>
    private class SarifIssue
    {
        /// <summary>
        /// Gets the BaselineState for the <see cref="SarifIssue"/>.
        /// </summary>
        public BaselineState BaselineState { get; init; }

        /// <summary>
        /// Gets the <see cref="IIssue"/> for the <see cref="SarifIssue"/>.
        /// </summary>
        public IIssue Issue { get; init; }
    }
}