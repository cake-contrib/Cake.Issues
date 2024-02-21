namespace Cake.Issues.EsLint
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for all ESLint log file format implementations.
    /// </summary>
    public abstract class BaseEsLintLogFileFormat : BaseLogFileFormat<EsLintIssuesProvider, EsLintIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEsLintLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected BaseEsLintLogFileFormat(ICakeLog log)
            : base(log)
        {
        }
    }
}
