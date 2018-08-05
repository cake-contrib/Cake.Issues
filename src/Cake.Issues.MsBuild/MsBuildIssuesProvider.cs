namespace Cake.Issues.MsBuild
{
    using Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported as MsBuild warnings.
    /// </summary>
    public class MsBuildIssuesProvider : BaseMultiFormatIssueProvider<MsBuildIssuesSettings, MsBuildIssuesProvider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public MsBuildIssuesProvider(ICakeLog log, MsBuildIssuesSettings settings)
            : base(log, settings)
        {
        }

        /// <inheritdoc />
        public override string ProviderName => "MSBuild";
    }
}
