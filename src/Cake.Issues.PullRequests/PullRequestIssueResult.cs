﻿namespace Cake.Issues.PullRequests
{
    using System.Collections.Generic;

    /// <summary>
    /// Result from reporting issues to a pull request.
    /// </summary>
    public class PullRequestIssueResult
    {
        private readonly List<IIssue> reportedIssues = new List<IIssue>();
        private readonly List<IIssue> postedIssues = new List<IIssue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PullRequestIssueResult"/> class.
        /// </summary>
        /// <param name="reportedIssues">Issues reported by the issue providers.</param>
        /// <param name="postedIssues">Issues posted to the pull request.</param>
        public PullRequestIssueResult(
            IEnumerable<IIssue> reportedIssues,
            IEnumerable<IIssue> postedIssues)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            reportedIssues.NotNull(nameof(reportedIssues));

            // ReSharper disable once PossibleMultipleEnumeration
            postedIssues.NotNull(nameof(postedIssues));

            // ReSharper disable once PossibleMultipleEnumeration
            this.reportedIssues.AddRange(reportedIssues);

            // ReSharper disable once PossibleMultipleEnumeration
            this.postedIssues.AddRange(postedIssues);
        }

        /// <summary>
        /// Gets all issues reported by the issue providers.
        /// </summary>
        public IEnumerable<IIssue> ReportedIssues => this.reportedIssues.AsReadOnly();

        /// <summary>
        /// Gets the issues posted to the pull request.
        /// </summary>
        public IEnumerable<IIssue> PostedIssues => this.postedIssues.AsReadOnly();
    }
}
