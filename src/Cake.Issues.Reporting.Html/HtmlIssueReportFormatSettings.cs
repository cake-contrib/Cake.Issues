namespace Cake.Issues.Reporting.Html
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="HtmlIssueReportFormatAliases"/>.
    /// </summary>
    public class HtmlIssueReportFormatSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlIssueReportFormatSettings"/> class.
        /// </summary>
        /// <param name="template">Template to use for generating the HTML report.</param>
        protected HtmlIssueReportFormatSettings(HtmlIssueReportTemplate template)
        {
            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Reporting.Html.Templates." + template.GetTemplateResourceName()))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Template = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlIssueReportFormatSettings"/> class.
        /// </summary>
        /// <param name="templatePath">Path to the template to use for generating the HTML report.</param>
        protected HtmlIssueReportFormatSettings(FilePath templatePath)
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
        /// Initializes a new instance of the <see cref="HtmlIssueReportFormatSettings"/> class.
        /// </summary>
        /// <param name="templateContent">Content of the template to use for generating the HTML report.</param>
        protected HtmlIssueReportFormatSettings(string templateContent)
        {
            templateContent.NotNullOrWhiteSpace(nameof(templateContent));

            this.Template = templateContent;
        }

        /// <summary>
        /// Gets the template to use for generating the HTML report.
        /// </summary>
        public string Template { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="HtmlIssueReportFormatSettings"/> class from a template file on disk.
        /// </summary>
        /// <param name="template">Template to use for generating the HTML report.</param>
        /// <returns>Instance of the <see cref="HtmlIssueReportFormatSettings"/> class.</returns>
        public static HtmlIssueReportFormatSettings FromEmbeddedTemplate(HtmlIssueReportTemplate template)
        {
            return new HtmlIssueReportFormatSettings(template);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="HtmlIssueReportFormatSettings"/> class from a template file on disk.
        /// </summary>
        /// <param name="templatePath">Path to the template to use for generating the HTML report.</param>
        /// <returns>Instance of the <see cref="HtmlIssueReportFormatSettings"/> class.</returns>
        public static HtmlIssueReportFormatSettings FromFilePath(FilePath templatePath)
        {
            return new HtmlIssueReportFormatSettings(templatePath);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="HtmlIssueReportFormatSettings"/> class from the content
        /// of a template file.
        /// </summary>
        /// <param name="templateContent">Content of the template to use for generating the HTML report.</param>
        /// <returns>Instance of the <see cref="HtmlIssueReportFormatSettings"/> class.</returns>
        public static HtmlIssueReportFormatSettings FromContent(string templateContent)
        {
            return new HtmlIssueReportFormatSettings(templateContent);
        }
    }
}
