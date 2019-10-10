﻿namespace Cake.Issues.Testing
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of <see cref="BaseLogFileFormat{TIssueProvider, TSettings}"/> for use in test cases.
    /// </summary>
    public class FakeLogFileFormat : BaseLogFileFormat<FakeMultiFormatIssueProvider, FakeMultiFormatIssueProviderSettings>
    {
        private readonly List<IIssue> issues = new List<IIssue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public FakeLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        /// <param name="issues">Issues which should be returned by the log file format.</param>
        public FakeLogFileFormat(ICakeLog log, IEnumerable<IIssue> issues)
            : base(log)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            // ReSharper disable once PossibleMultipleEnumeration
            this.issues.AddRange(issues);
        }

        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            FakeMultiFormatIssueProvider issueProvider,
            RepositorySettings repositorySettings,
            FakeMultiFormatIssueProviderSettings issueProviderSettings)
        {
            return this.issues;
        }
    }
}
