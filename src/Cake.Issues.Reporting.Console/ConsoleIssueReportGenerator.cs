namespace Cake.Issues.Reporting.Console
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Errata;
    using Spectre.Console;

    /// <summary>
    /// Generator for reporting issues to console.
    /// </summary>
    internal class ConsoleIssueReportGenerator : IssueReportFormat
    {
        private readonly ConsoleIssueReportFormatSettings consoleIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reporting the issues.</param>
        public ConsoleIssueReportGenerator(ICakeLog log, ConsoleIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.consoleIssueReportFormatSettings = settings;
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            var enumeratedIssues = issues.ToList();
            if (this.consoleIssueReportFormatSettings.ShowProviderSummary)
            {
                // Filter to issues from existing files
                var diagnosticIssues =
                enumeratedIssues
                    .Where(x =>
                        x.AffectedFileRelativePath != null &&
                        File.Exists(this.Settings.RepositoryRoot.CombineWithFilePath(x.AffectedFileRelativePath).FullPath))
                    .ToList();

                // Filter to issues with line and column information
                // Errata currently doesn't support file or line level diagnostics.
                // https://github.com/cake-contrib/Cake.Issues.Reporting.Console/issues/4
                // https://github.com/cake-contrib/Cake.Issues.Reporting.Console/issues/5
                diagnosticIssues =
                    diagnosticIssues
                        .Where(x => x.Line.HasValue && x.Column.HasValue)
                        .ToList();

                var report = new Report(new FileSystemRepository(this.Settings));

                if (this.consoleIssueReportFormatSettings.GroupByRule)
                {
                    foreach (var issueGroup in diagnosticIssues.GroupBy(x => x.RuleId))
                    {
                        report.AddDiagnostic(new IssueDiagnostic(issueGroup));
                    }
                }
                else
                {
                    foreach (var issue in diagnosticIssues)
                    {
                        report.AddDiagnostic(new IssueDiagnostic(issue));
                    }
                }

                report.Render(
                    AnsiConsole.Console,
                    new ReportSettings
                    {
                        Compact = this.consoleIssueReportFormatSettings.Compact,
                    });
            }

            if (this.consoleIssueReportFormatSettings.ShowProviderSummary || this.consoleIssueReportFormatSettings.ShowPrioritySummary)
            {
                this.PrintSummary(enumeratedIssues);
            }

            return null;
        }

        /// <summary>
        /// Prints the issues of issues.
        /// </summary>
        /// <param name="issues">List of issues.</param>
        private void PrintSummary(IList<IIssue> issues)
        {
            if (!issues.Any())
            {
                AnsiConsole.WriteLine("No issues");
                return;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();
            var rule = new Rule("Summary").Centered();
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();

            var providerChart = new BarChart();

            var priorityTable = new Table
            {
                Border = TableBorder.Rounded,
                Expand = true,
            };
            priorityTable.AddColumn(new TableColumn("Issue Provider / Run").Centered());
            priorityTable.AddColumn(new TableColumn("Number Of Issues").Centered());

            var i = 1;
            foreach (var providerGroup in issues.GroupBy(x => x.ProviderName))
            {
                var issueProvider = providerGroup.Key;

                providerChart.AddItem(issueProvider, providerGroup.Count(), Color.FromInt32(i));

                foreach (var runGroup in providerGroup.GroupBy(x => x.Run))
                {
                    if (!string.IsNullOrEmpty(runGroup.Key))
                    {
                        issueProvider += " / {runGroup.Key}";
                    }

                    var errorCount = runGroup.Count(x => x.Priority.HasValue && x.Priority.Value == (int)IssuePriority.Error);
                    var warningCount = runGroup.Count(x => x.Priority.HasValue && x.Priority.Value == (int)IssuePriority.Warning);
                    var hintCount = runGroup.Count(x => x.Priority.HasValue && x.Priority.Value == (int)IssuePriority.Hint);
                    var suggestionCount = runGroup.Count(x => x.Priority.HasValue && x.Priority.Value == (int)IssuePriority.Suggestion);
                    var unknownCount = runGroup.Count(x => !x.Priority.HasValue || x.Priority.Value == (int)IssuePriority.Undefined);

                    var chart =
                        new BarChart()
                            .AddItem("Errors", errorCount, Color.Red)
                            .AddItem("Warnings", warningCount, Color.Yellow)
                            .AddItem("Hint", hintCount, Color.LightSkyBlue1)
                            .AddItem("Suggestion", suggestionCount, Color.Green)
                            .AddItem("Unknown", unknownCount, Color.DarkSlateGray1);

                    priorityTable.AddRow(new Markup(issueProvider), chart);
                }

                priorityTable.AddEmptyRow();
                i++;
            }

            if (this.consoleIssueReportFormatSettings.ShowProviderSummary)
            {
                AnsiConsole.Write(new Markup("[bold]Issues per provider & run[/]").Centered());
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine();
                AnsiConsole.Write(providerChart);
                AnsiConsole.WriteLine();
            }

            if (this.consoleIssueReportFormatSettings.ShowPrioritySummary)
            {
                AnsiConsole.Write(new Markup("[bold]Issues per priority[/]").Centered());
                AnsiConsole.WriteLine();
                AnsiConsole.Write(priorityTable);
            }
        }
    }
}