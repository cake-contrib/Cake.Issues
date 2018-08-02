namespace Cake.Issues.MsBuild
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for all MSBuild log file format.
    /// </summary>
    public abstract class MsBuildLogFileFormat : LogFileFormat<MsBuildIssuesProvider, MsBuildIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected MsBuildLogFileFormat(ICakeLog log)
            : base(log)
        {
        }
    }
}
