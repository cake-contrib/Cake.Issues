namespace Cake.Issues.BuildServer.Tests;

using Cake.Core.Diagnostics;

/// <summary>
/// Implementation of a <see cref="BaseBuildServer"/> for use in test cases.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public class FakeBuildServer(ICakeLog log) : BaseBuildServer(log)
{
    private readonly List<IIssue> postedIssues = [];

    /// <summary>
    /// Gets the log instance.
    /// </summary>
    public new ICakeLog Log => base.Log;

    /// <summary>
    /// Gets the settings which should be used.
    /// </summary>
    public new IReportIssuesToBuildServerSettings Settings => base.Settings;

    /// <summary>
    /// Gets the issues posted to the build server.
    /// </summary>
    public IEnumerable<IIssue> PostedIssues => this.postedIssues;

    /// <summary>
    /// Gets or sets a value indicating whether the build server should return false during <see cref="Initialize"/>.
    /// </summary>
    public bool ShouldFailOnInitialization { get; set; }

    /// <inheritdoc />
    public override bool Initialize(IReportIssuesToBuildServerSettings settings)
    {
        var result = base.Initialize(settings);

        return result && !this.ShouldFailOnInitialization;
    }

    /// <inheritdoc />
    protected override void InternalPostIssues(IEnumerable<IIssue> issues)
    {
        issues.NotNull();

        this.postedIssues.AddRange(issues);
    }
}
