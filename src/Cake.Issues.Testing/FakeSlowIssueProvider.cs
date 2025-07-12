namespace Cake.Issues.Testing;

using System.Collections.Generic;
using System.Threading;
using Cake.Core.Diagnostics;

/// <summary>
/// Implementation of a <see cref="BaseIssueProvider"/> that simulates slow processing for performance testing.
/// </summary>
public class FakeSlowIssueProvider : BaseIssueProvider
{
    private readonly List<IIssue> issues = [];
    private readonly int delayMs;

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeSlowIssueProvider"/> class.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    /// <param name="issues">Issues which should be returned by the issue provider.</param>
    /// <param name="delayMs">Simulated processing delay in milliseconds.</param>
    public FakeSlowIssueProvider(ICakeLog log, IEnumerable<IIssue> issues, int delayMs)
        : base(log)
    {
        issues.NotNull();

        this.issues.AddRange(issues);
        this.delayMs = delayMs;
    }

    /// <inheritdoc/>
    public override string ProviderName => "Fake Slow Issue Provider";

    /// <inheritdoc/>
    protected override IEnumerable<IIssue> InternalReadIssues()
    {
        // Simulate slow processing
        Thread.Sleep(this.delayMs);
        return this.issues;
    }
}