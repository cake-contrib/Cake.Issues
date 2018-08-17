namespace Cake.Issues.MsBuild
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for all MSBuild log file format.
    /// </summary>
    public abstract class BaseMsBuildLogFileFormat : BaseLogFileFormat<MsBuildIssuesProvider, MsBuildIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMsBuildLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected BaseMsBuildLogFileFormat(ICakeLog log)
            : base(log)
        {
        }
    }
}
