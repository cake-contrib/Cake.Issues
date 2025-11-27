namespace Cake.Issues;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        var stopwatch = Stopwatch.StartNew();

        var results = new List<IIssue>[this.issueProviders.Count];

        // Process providers in parallel
        _ = Parallel.For(0, this.issueProviders.Count, i =>
        {
            results[i] = this.ReadIssuesFromProvider(this.issueProviders[i]);
        });

        stopwatch.Stop();

        var issuesList = results.SelectMany(r => r).ToList();
        this.log.Verbose(
            "Reading {0} issues from {1} providers took {2} ms",
            issuesList.Count,
            this.issueProviders.Count,
            stopwatch.ElapsedMilliseconds);

        return issuesList;
    }

    private List<IIssue> ReadIssuesFromProvider(IIssueProvider issueProvider)
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

            // Post-process issues - this is thread-safe as each provider gets its own issues
            currentIssues.ForEach(x =>
            {
                x.Run = this.settings.Run;

                if (this.settings.FileLinkSettings != null)
                {
                    x.FileLink = this.settings.FileLinkSettings.GetFileLink(x);
                }
            });
            return currentIssues;
        }
        else
        {
            this.log.Warning("Error initializing issue provider {0}.", providerName);
            return [];
        }
    }
}
