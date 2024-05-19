namespace Cake.Issues.Reporting.Sarif
{
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

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };

            var log = new SarifLog();

            if (issues.Any())
            {
                log.Runs = new List<Run>();

                foreach (var issueGroup in from issue in issues group issue by new { issue.ProviderType, issue.Run })
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
                                (from issue in issueGroup
                                 select this.GetResult(issue)).ToList(),
                            OriginalUriBaseIds = new Dictionary<string, ArtifactLocation>
                            {
                                [RepoRootUriBaseId] =
                                    new()
                                    {
                                        Uri = new Uri(this.Settings.RepositoryRoot.FullPath, UriKind.Absolute),
                                    },
                            },
                        };

                    if (!string.IsNullOrEmpty(issueGroup.Key.Run))
                    {
                        run.AutomationDetails =
                            new RunAutomationDetails
                            {
                                Id = issueGroup.Key.Run,
                            };
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

        private Result GetResult(IIssue issue)
        {
            issue.NotNull();

            var result =
                new Result
                {
                    RuleId = issue.RuleId,
                    Message =
                        new Message
                        {
                            Text = issue.MessageText,
                            Markdown = issue.MessageMarkdown,
                        },
                    Kind = issue.Kind(),
                    Level = issue.Level(),
                    Locations =
                        new List<Location>
                        {
                            issue.Location(),
                        },
                };

            if (issue.RuleUrl != null)
            {
                if (!string.IsNullOrEmpty(issue.RuleId))
                {
                    if (!this.ruleIndices.TryGetValue(issue.RuleId, out var value))
                    {
                        this.ruleIndices.Add(issue.RuleId, this.rules.Count);
                        this.rules.Add(
                            new ReportingDescriptor
                            {
                                Id = issue.RuleId,
                                Name = issue.RuleName,
                                HelpUri = issue.RuleUrl,
                            });
                    }

                    result.RuleIndex = value;
                }
                else
                {
                    // In the unusual case where there is a rule URL but no rule name, we put the
                    // URL in the result's property bag, because there's no rule whose metadata
                    // can hold it.
                    result.SetProperty(RuleUrlPropertyName, issue.RuleUrl);
                }
            }

            return result;
        }
    }
}