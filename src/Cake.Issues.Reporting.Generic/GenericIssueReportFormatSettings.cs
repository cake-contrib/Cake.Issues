namespace Cake.Issues.Reporting.Generic
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="GenericIssueReportFormatAliases"/>.
    /// </summary>
    public class GenericIssueReportFormatSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericIssueReportFormatSettings"/> class.
        /// </summary>
        /// <param name="template">Template to use for generating the report.</param>
        protected GenericIssueReportFormatSettings(GenericIssueReportTemplate template)
        {
            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Reporting.Generic.Templates." + template.GetTemplateResourceName()))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Template = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericIssueReportFormatSettings"/> class.
        /// </summary>
        /// <param name="templatePath">Path to the template to use for generating the report.</param>
        protected GenericIssueReportFormatSettings(FilePath templatePath)
        {
            templatePath.NotNull(nameof(templatePath));

            using (var stream = new FileStream(templatePath.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Template = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericIssueReportFormatSettings"/> class.
        /// </summary>
        /// <param name="templateContent">Content of the template to use for generating the report.</param>
        protected GenericIssueReportFormatSettings(string templateContent)
        {
            templateContent.NotNullOrWhiteSpace(nameof(templateContent));

            this.Template = templateContent;
        }

        /// <summary>
        /// Gets the template to use for generating the report.
        /// </summary>
        public string Template { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="GenericIssueReportFormatSettings"/> class from a template file on disk.
        /// </summary>
        /// <param name="template">Template to use for generating the report.</param>
        /// <returns>Instance of the <see cref="GenericIssueReportFormatSettings"/> class.</returns>
        public static GenericIssueReportFormatSettings FromEmbeddedTemplate(GenericIssueReportTemplate template)
        {
            return new GenericIssueReportFormatSettings(template);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="GenericIssueReportFormatSettings"/> class from a template file on disk.
        /// </summary>
        /// <param name="templatePath">Path to the template to use for generating the report.</param>
        /// <returns>Instance of the <see cref="GenericIssueReportFormatSettings"/> class.</returns>
        public static GenericIssueReportFormatSettings FromFilePath(FilePath templatePath)
        {
            return new GenericIssueReportFormatSettings(templatePath);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="GenericIssueReportFormatSettings"/> class from the content
        /// of a template file.
        /// </summary>
        /// <param name="templateContent">Content of the template to use for generating the report.</param>
        /// <returns>Instance of the <see cref="GenericIssueReportFormatSettings"/> class.</returns>
        public static GenericIssueReportFormatSettings FromContent(string templateContent)
        {
            return new GenericIssueReportFormatSettings(templateContent);
        }
    }
}
