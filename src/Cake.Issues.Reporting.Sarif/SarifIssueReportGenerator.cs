namespace Cake.Issues.Reporting.Sarif
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Microsoft.CodeAnalysis.Sarif;
    using Microsoft.CodeAnalysis.Sarif.Readers;
    using Newtonsoft.Json;

    /// <summary>
    /// Generator for creating SARIF compatible reports.
    /// </summary>
    internal class SarifIssueReportGenerator : IssueReportFormat
    {
        private readonly SarifIssueReportFormatSettings sarifIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SarifIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public SarifIssueReportGenerator(ICakeLog log, SarifIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

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
                foreach (var issueGroup in from issue in issues group issue by issue.ProviderType)
                {
                    log.Runs.Add(
                        new Run
                        {
                            Tool =
                                new Tool
                                {
                                    Driver =
                                        new ToolComponent
                                        {
                                            Name = issueGroup.Key,
                                        },
                                },
                            Results =
                                (from issue in issueGroup
                                 select this.GetResult(issue)).ToList(),
                        });
                }
            }

            var sarifText = JsonConvert.SerializeObject(log, settings);
            File.WriteAllText(this.Settings.OutputFilePath.FullPath, sarifText);

            return this.Settings.OutputFilePath;
        }

        private Result GetResult(IIssue issue)
        {
            issue.NotNull(nameof(issue));

            var result =
                new Result
                {
                    RuleId = issue.Rule,
                    Message =
                        new Message
                        {
                            Text = issue.MessageText,
                        },
                    Kind = issue.Kind(),
                    Level = issue.Level(),
                    Locations =
                        new List<Location>
                        {
                            issue.Location(this.Settings.RepositoryRoot),
                        },
                };

            if (issue.RuleUrl != null)
            {
                result.SetProperty("RuleUrl", issue.RuleUrl);
            }

            return result;
        }
    }
}