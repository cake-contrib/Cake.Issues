namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core.Diagnostics;
    using Core.IO;
    using RazorEngine;
    using RazorEngine.Configuration;
    using RazorEngine.Templating;

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

            this.ConfigureRazorEngine();
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            this.Log.Information("Creating report '{0}'", this.Settings.OutputFilePath.FullPath);

            try
            {
                var result =
                    Engine.Razor.RunCompile(
                        this.genericIssueReportFormatSettings.Template,
                        Guid.NewGuid().ToString(),
                        typeof(IEnumerable<IIssue>),
                        issues,
                        new DynamicViewBag(this.genericIssueReportFormatSettings.Options));

                File.WriteAllText(this.Settings.OutputFilePath.FullPath, result);

                return this.Settings.OutputFilePath;
            }
            catch (Exception e)
            {
                this.Log.Error(e.Message);

                throw;
            }
        }

        private void ConfigureRazorEngine()
        {
            var config = new TemplateServiceConfiguration
            {
                // Disable temp file locking, since we don't expect much templates, they don't change at runtime and we trust them.
                // This allows RazorEngine to delete the files, without requiring us to run it in a separate AppDomain.
                // See https://antaris.github.io/RazorEngine/index.html#Temporary-files.
                DisableTempFileLocking = true,

                // Disable warnings that temp files cannot be cleaned up.
                CachingProvider = new DefaultCachingProvider(t => { }),

                // Use custom reference resolver to make it work with assemblies embedded through Costura.Fody.
                ReferenceResolver = new RazorEngineReferenceResolver()
            };

            var service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }
    }
}
