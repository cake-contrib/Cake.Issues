namespace Cake.Issues.MsBuild
{
    using System.Collections.Generic;
    using Core.Diagnostics;
    using IssueProvider;

    /// <summary>
    /// Provider for code analysis issues reported as MsBuild warnings.
    /// </summary>
    internal class MsBuildIssuesProvider : IssueProvider
    {
        private readonly MsBuildIssuesSettings msBuildIssuesSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public MsBuildIssuesProvider(ICakeLog log, MsBuildIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.msBuildIssuesSettings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            return this.msBuildIssuesSettings.Format.ReadIssues(this.Settings, this.msBuildIssuesSettings);
        }
    }
}
