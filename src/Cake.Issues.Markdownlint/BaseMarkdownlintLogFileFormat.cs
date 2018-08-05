namespace Cake.Issues.Markdownlint
{
    using Core.Diagnostics;

    /// <summary>
    /// Base class for all log file formats supported by the Markdownlint issue provider.
    /// </summary>
    public abstract class BaseMarkdownlintLogFileFormat : BaseLogFileFormat<MarkdownlintIssuesProvider, MarkdownlintIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMarkdownlintLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected BaseMarkdownlintLogFileFormat(ICakeLog log)
            : base(log)
        {
        }
    }
}
