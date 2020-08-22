﻿namespace Cake.Issues.EsLint
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by ESLint.
    /// </summary>
    public class EsLintIssuesProvider : BaseMultiFormatIssueProvider<EsLintIssuesSettings, EsLintIssuesProvider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public EsLintIssuesProvider(ICakeLog log, EsLintIssuesSettings settings)
            : base(log, settings)
        {
        }

        /// <summary>
        /// Gets the name of the ESLint issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        public static string ProviderTypeName => typeof(EsLintIssuesProvider).FullName;
        
        /// <inheritdoc />
        public override string ProviderName => "ESLint";
    }
}
