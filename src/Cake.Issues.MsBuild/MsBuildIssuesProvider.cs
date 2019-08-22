namespace Cake.Issues.MsBuild
{
    using Cake.Core.Diagnostics;

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

        /// <summary>
        /// Gets the name of the MsBuild issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        public static string ProviderTypeName => typeof(MsBuildIssuesProvider).FullName;

        /// <inheritdoc />
        public override string ProviderName => "MSBuild";
    }
}
