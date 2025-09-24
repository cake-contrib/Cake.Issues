namespace Cake.Issues;

using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core.Diagnostics;

/// <summary>
/// Class for reading issues.
/// </summary>
public class IssuesReader
{
    private readonly ICakeLog log;
    private readonly List<IIssueProvider> issueProviders = [];
    private readonly IReadIssuesSettings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="IssuesReader"/> class.
    /// </summary>
    /// <param name="log">Cake log instance.</param>
    /// <param name="issueProviders">List of issue providers to use.</param>
    /// <param name="settings">Settings to use.</param>
    public IssuesReader(
        ICakeLog log,
        IEnumerable<IIssueProvider> issueProviders,
        IReadIssuesSettings settings)
    {
        log.NotNull();
        settings.NotNull();

        issueProviders.NotNullOrEmptyOrEmptyElement();

        this.log = log;
        this.settings = settings;

        this.issueProviders.AddRange(issueProviders);
    }

    /// <summary>
    /// Read issues from issue providers.
    /// </summary>
    /// <returns>List of issues.</returns>
    public IEnumerable<IIssue> ReadIssues()
    {
        // Initialize issue providers and read issues.
        var issues = new List<IIssue>();
        foreach (var issueProvider in this.issueProviders)
        {
            var providerName = issueProvider.GetType().Name;
            this.log.Verbose("Initialize issue provider {0}...", providerName);
            if (issueProvider.Initialize(this.settings))
            {
                this.log.Verbose("Reading issues from {0}...", providerName);
                var currentIssues = issueProvider.ReadIssues().ToList();

                this.log.Verbose(
                    "Found {0} issues using issue provider {1}...",
                    currentIssues.Count,
                    providerName);

                // Create enhanced issues with the Run and FileLink properties set
                // This maintains immutability by creating copies instead of mutating originals
                var enhancedIssues = new List<IIssue>();
                foreach (var issue in currentIssues)
                {
                    var run = this.settings.Run;
                    var fileLink = this.settings.FileLinkSettings?.GetFileLink(issue);

                    // Always create a new issue instance to ensure immutability
                    if (issue is Issue originalIssue)
                    {
                        var enhancedIssue = originalIssue.WithRunAndFileLink(run, fileLink);
                        enhancedIssues.Add(enhancedIssue);
                    }
                    else
                    {
                        // If it's a custom IIssue implementation, we can't enhance it while maintaining immutability
                        // This is a limitation - custom implementations would need to handle this themselves
                        throw new NotSupportedException(
                            $"Issue type {issue.GetType().Name} does not support immutable enhancement. " +
                            "Custom IIssue implementations should handle Run and FileLink setting during construction.");
                    }
                }

                issues.AddRange(enhancedIssues);
            }
            else
            {
                this.log.Warning("Error initializing issue provider {0}.", providerName);
            }
        }

        return issues;
    }
}
