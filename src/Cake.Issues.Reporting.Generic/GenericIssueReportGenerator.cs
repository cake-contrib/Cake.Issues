namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Gazorator;

    /// <summary>
    /// Generator for creating text based issue reports.
    /// </summary>
    internal class GenericIssueReportGenerator : IssueReportFormat
    {
        private readonly GenericIssueReportFormatSettings genericIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public GenericIssueReportGenerator(ICakeLog log, GenericIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.genericIssueReportFormatSettings = settings;
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            this.Log.Information("Creating report '{0}'", this.Settings.OutputFilePath.FullPath);

            try
            {
                using (var streamWriter = new StreamWriter(this.Settings.OutputFilePath.FullPath))
                {
                    Gazorator.Default
                        .WithOutput(streamWriter)
                        .WithModel(issues)
                        .WithReferences(
                            typeof(Cake.Issues.IIssue).Assembly,
                            typeof(Cake.Issues.Reporting.IIssueReportFormat).Assembly,
                            typeof(Cake.Issues.Reporting.Generic.DevExtremeTheme).Assembly,
                            typeof(Cake.Core.IO.FilePath).Assembly)
                        .WithViewBag(this.genericIssueReportFormatSettings.Options)
                        .ProcessTemplateAsync(this.genericIssueReportFormatSettings.Template)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();
                }

                return this.Settings.OutputFilePath;
            }
            catch (Exception e)
            {
                this.Log.Error(e.Message);

                throw;
            }
        }
    }
}
